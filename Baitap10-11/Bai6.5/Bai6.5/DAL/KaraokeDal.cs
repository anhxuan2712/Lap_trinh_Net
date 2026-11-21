using System.Data;
using Bai6._5.DTO;
using MySql.Data.MySqlClient;

namespace Bai6._5.DAL;

/// <summary>
/// Lớp truy cập dữ liệu: chỉ chứa câu lệnh SQL, không chứa quy tắc nghiệp vụ.
/// Các hàm nhận IDbTransaction được BUS gọi bên trong một giao dịch.
/// </summary>
public sealed class KaraokeDal
{
    public IDbConnection OpenConnection()
    {
        var connection = new MySqlConnection(DbConfig.ConnectionString);
        connection.Open();
        return connection;
    }

    // ===== Truy vấn hiển thị =====

    public List<RoomDto> GetRooms()
    {
        const string sql =
            "SELECT p.MaPhong, p.TenPhong, p.LoaiPhong, p.GiaTheoGio, hd.MaHD, hd.GioVao " +
            "FROM PhongHat p " +
            "LEFT JOIN HoaDon hd ON hd.MaPhong = p.MaPhong AND hd.TrangThai = 0 " +
            "ORDER BY p.MaPhong;";

        using var connection = new MySqlConnection(DbConfig.ConnectionString);
        connection.Open();
        using var command = new MySqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        var rooms = new List<RoomDto>();
        while (reader.Read())
        {
            rooms.Add(new RoomDto
            {
                Id = reader.GetInt32("MaPhong"),
                Name = reader.GetString("TenPhong"),
                Type = reader.IsDBNull(reader.GetOrdinal("LoaiPhong")) ? "Thường" : reader.GetString("LoaiPhong"),
                HourlyRate = reader.GetDecimal("GiaTheoGio"),
                ActiveInvoiceId = reader.IsDBNull(reader.GetOrdinal("MaHD")) ? null : reader.GetInt32("MaHD"),
                StartedAt = reader.IsDBNull(reader.GetOrdinal("GioVao")) ? null : reader.GetDateTime("GioVao")
            });
        }
        return rooms;
    }

    public List<ServiceDto> GetServices()
    {
        const string sql = "SELECT MaDV, TenDV, DonGia, TonKho FROM DichVu ORDER BY TenDV;";

        using var connection = new MySqlConnection(DbConfig.ConnectionString);
        connection.Open();
        using var command = new MySqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        var services = new List<ServiceDto>();
        while (reader.Read())
        {
            services.Add(new ServiceDto
            {
                Id = reader.GetInt32("MaDV"),
                Name = reader.GetString("TenDV"),
                UnitPrice = reader.GetDecimal("DonGia"),
                Stock = reader.GetInt32("TonKho")
            });
        }
        return services;
    }

    public DataTable GetOrderItems(int invoiceId)
    {
        const string sql =
            "SELECT dv.TenDV AS DichVu, ct.SoLuong, ct.DonGia, " +
            "ct.SoLuong * ct.DonGia AS ThanhTien " +
            "FROM ChiTietDichVu ct " +
            "INNER JOIN DichVu dv ON dv.MaDV = ct.MaDV " +
            "WHERE ct.MaHD = @invoiceId ORDER BY dv.TenDV;";

        using var connection = new MySqlConnection(DbConfig.ConnectionString);
        using var adapter = new MySqlDataAdapter(sql, connection);
        adapter.SelectCommand!.Parameters.AddWithValue("@invoiceId", invoiceId);

        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    // ===== Phòng =====

    /// <summary>Đọc và khóa trạng thái phòng. Trả về null nếu không có phòng.</summary>
    public string? GetRoomStatusForUpdate(IDbTransaction transaction, int roomId)
    {
        using var command = Command(transaction,
            "SELECT TrangThai FROM PhongHat WHERE MaPhong = @roomId FOR UPDATE;");
        command.Parameters.AddWithValue("@roomId", roomId);
        return command.ExecuteScalar() as string;
    }

    public int UpdateRoomStatus(IDbTransaction transaction, int roomId, string status)
    {
        using var command = Command(transaction,
            "UPDATE PhongHat SET TrangThai = @status WHERE MaPhong = @roomId;");
        command.Parameters.AddWithValue("@status", status);
        command.Parameters.AddWithValue("@roomId", roomId);
        return command.ExecuteNonQuery();
    }

    // ===== Hóa đơn =====

    public int CountOpenInvoices(IDbTransaction transaction, int roomId)
    {
        using var command = Command(transaction,
            "SELECT COUNT(*) FROM HoaDon WHERE MaPhong = @roomId AND TrangThai = 0;");
        command.Parameters.AddWithValue("@roomId", roomId);
        return Convert.ToInt32(command.ExecuteScalar());
    }

    public void InsertInvoice(IDbTransaction transaction, int roomId)
    {
        using var command = Command(transaction,
            "INSERT INTO HoaDon (MaPhong, GioVao, TrangThai) VALUES (@roomId, NOW(), 0);");
        command.Parameters.AddWithValue("@roomId", roomId);
        command.ExecuteNonQuery();
    }

    /// <summary>Đọc và khóa trạng thái hóa đơn. Trả về null nếu không có hóa đơn.</summary>
    public int? GetInvoiceStatusForUpdate(IDbTransaction transaction, int invoiceId)
    {
        using var command = Command(transaction,
            "SELECT TrangThai FROM HoaDon WHERE MaHD = @invoiceId FOR UPDATE;");
        command.Parameters.AddWithValue("@invoiceId", invoiceId);
        var status = command.ExecuteScalar();
        return status is null || status == DBNull.Value ? null : Convert.ToInt32(status);
    }

    /// <summary>Đọc và khóa hóa đơn đang mở của phòng. Trả về null nếu không có.</summary>
    public InvoiceDto? GetOpenInvoiceForUpdate(IDbTransaction transaction, int roomId, int invoiceId)
    {
        using var command = Command(transaction,
            "SELECT hd.MaHD, hd.MaPhong, hd.GioVao, p.GiaTheoGio FROM HoaDon hd " +
            "INNER JOIN PhongHat p ON p.MaPhong = hd.MaPhong " +
            "WHERE hd.MaHD = @invoiceId AND hd.MaPhong = @roomId AND hd.TrangThai = 0 FOR UPDATE;");
        command.Parameters.AddWithValue("@invoiceId", invoiceId);
        command.Parameters.AddWithValue("@roomId", roomId);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
            return null;

        return new InvoiceDto
        {
            Id = reader.GetInt32("MaHD"),
            RoomId = reader.GetInt32("MaPhong"),
            StartedAt = reader.GetDateTime("GioVao"),
            HourlyRate = reader.GetDecimal("GiaTheoGio")
        };
    }

    public int CloseInvoice(IDbTransaction transaction, int invoiceId,
        DateTime endedAt, decimal roomTotal, decimal serviceTotal)
    {
        using var command = Command(transaction,
            "UPDATE HoaDon SET GioRa = @endedAt, TongTienPhong = @roomTotal, " +
            "TongTienDichVu = @serviceTotal, TrangThai = 1 " +
            "WHERE MaHD = @invoiceId AND TrangThai = 0;");
        command.Parameters.AddWithValue("@endedAt", endedAt);
        command.Parameters.AddWithValue("@roomTotal", roomTotal);
        command.Parameters.AddWithValue("@serviceTotal", serviceTotal);
        command.Parameters.AddWithValue("@invoiceId", invoiceId);
        return command.ExecuteNonQuery();
    }

    // ===== Dịch vụ =====

    /// <summary>Đọc và khóa dịch vụ (tồn kho, đơn giá). Trả về null nếu không có.</summary>
    public ServiceDto? GetServiceForUpdate(IDbTransaction transaction, int serviceId)
    {
        using var command = Command(transaction,
            "SELECT MaDV, TenDV, DonGia, TonKho FROM DichVu WHERE MaDV = @serviceId FOR UPDATE;");
        command.Parameters.AddWithValue("@serviceId", serviceId);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
            return null;

        return new ServiceDto
        {
            Id = reader.GetInt32("MaDV"),
            Name = reader.GetString("TenDV"),
            UnitPrice = reader.GetDecimal("DonGia"),
            Stock = reader.GetInt32("TonKho")
        };
    }

    /// <summary>Tổng số lượng dịch vụ đã gọi trong các hóa đơn chưa thanh toán.</summary>
    public int GetPendingQuantity(IDbTransaction transaction, int serviceId)
    {
        using var command = Command(transaction,
            "SELECT COALESCE(SUM(ct.SoLuong), 0) " +
            "FROM ChiTietDichVu ct INNER JOIN HoaDon hd ON hd.MaHD = ct.MaHD " +
            "WHERE ct.MaDV = @serviceId AND hd.TrangThai = 0;");
        command.Parameters.AddWithValue("@serviceId", serviceId);
        return Convert.ToInt32(command.ExecuteScalar());
    }

    public void UpsertOrderItem(IDbTransaction transaction, int invoiceId,
        int serviceId, int quantity, decimal unitPrice)
    {
        using var command = Command(transaction,
            "INSERT INTO ChiTietDichVu (MaHD, MaDV, SoLuong, DonGia) " +
            "VALUES (@invoiceId, @serviceId, @quantity, @unitPrice) " +
            "ON DUPLICATE KEY UPDATE SoLuong = SoLuong + @quantity;");
        command.Parameters.AddWithValue("@invoiceId", invoiceId);
        command.Parameters.AddWithValue("@serviceId", serviceId);
        command.Parameters.AddWithValue("@quantity", quantity);
        command.Parameters.AddWithValue("@unitPrice", unitPrice);
        command.ExecuteNonQuery();
    }

    public List<OrderLineDto> GetOrderLines(IDbTransaction transaction, int invoiceId)
    {
        using var command = Command(transaction,
            "SELECT MaDV, SoLuong, DonGia FROM ChiTietDichVu " +
            "WHERE MaHD = @invoiceId ORDER BY MaDV;");
        command.Parameters.AddWithValue("@invoiceId", invoiceId);

        using var reader = command.ExecuteReader();
        var lines = new List<OrderLineDto>();
        while (reader.Read())
        {
            lines.Add(new OrderLineDto
            {
                ServiceId = reader.GetInt32("MaDV"),
                Quantity = reader.GetInt32("SoLuong"),
                UnitPrice = reader.GetDecimal("DonGia")
            });
        }
        return lines;
    }

    /// <summary>Trừ tồn kho. Trả về số dòng bị ảnh hưởng (0 nếu không đủ hàng).</summary>
    public int DecreaseStock(IDbTransaction transaction, int serviceId, int quantity)
    {
        using var command = Command(transaction,
            "UPDATE DichVu SET TonKho = TonKho - @quantity " +
            "WHERE MaDV = @serviceId AND TonKho >= @quantity;");
        command.Parameters.AddWithValue("@quantity", quantity);
        command.Parameters.AddWithValue("@serviceId", serviceId);
        return command.ExecuteNonQuery();
    }

    private static MySqlCommand Command(IDbTransaction transaction, string sql)
    {
        var mySqlTransaction = (MySqlTransaction)transaction;
        return new MySqlCommand(sql, mySqlTransaction.Connection, mySqlTransaction);
    }
}
