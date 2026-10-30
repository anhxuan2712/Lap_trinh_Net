using System.Data;
using Bai5._4.DTO;
using MySql.Data.MySqlClient;

namespace Bai5._4.DAL;

public sealed class HotelDAL
{
    public const string ConnectionString =
        "Server=localhost;Port=3306;Database=HotelDB;Uid=root;Pwd=familo23;CharSet=utf8mb4;";

    public DataTable LoadRooms()
    {
        const string sql = "SELECT SoPhong, LoaiPhong, GiaTheoGio, TrangThai FROM Phong ORDER BY SoPhong";
        return GetData(sql);
    }

    public DataTable LoadServices()
    {
        const string sql = "SELECT MaDV, TenDV, DonGia FROM DichVu ORDER BY TenDV";
        return GetData(sql);
    }

    public DataTable LoadCurrentStays()
    {
        const string sql = """
            SELECT p.MaPhieu, p.SoPhong, k.HoTen, k.CCCD, p.NgayNhanPhong,
                   TIMESTAMPDIFF(MINUTE, p.NgayNhanPhong, NOW()) AS SoPhutDaO
            FROM PhieuDatPhong p
            INNER JOIN KhachHang k ON k.MaKH = p.MaKH
            WHERE p.NgayTraPhong IS NULL
            ORDER BY p.NgayNhanPhong
            """;
        return GetData(sql);
    }

    public KhachHangDTO? FindCustomerByCccd(string cccd)
    {
        const string sql = "SELECT MaKH, HoTen, CCCD, COALESCE(SoDienThoai, '') AS SoDienThoai FROM KhachHang WHERE CCCD = @CCCD";
        using var connection = new MySqlConnection(ConnectionString);
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.Add("@CCCD", MySqlDbType.VarChar, 20).Value = cccd;
        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read()
            ? new KhachHangDTO
            {
                MaKH = reader.GetInt32("MaKH"), HoTen = reader.GetString("HoTen"),
                CCCD = reader.GetString("CCCD"), SoDienThoai = reader.GetString("SoDienThoai")
            }
            : null;
    }

    public int AddCustomer(KhachHangDTO customer)
    {
        const string sql = "INSERT INTO KhachHang (HoTen, CCCD, SoDienThoai) VALUES (@HoTen, @CCCD, @SoDienThoai)";
        using var connection = new MySqlConnection(ConnectionString);
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@HoTen", customer.HoTen);
        command.Parameters.AddWithValue("@CCCD", customer.CCCD);
        command.Parameters.AddWithValue("@SoDienThoai", string.IsNullOrWhiteSpace(customer.SoDienThoai) ? DBNull.Value : customer.SoDienThoai);
        connection.Open();
        command.ExecuteNonQuery();
        return checked((int)command.LastInsertedId);
    }

    public int CheckIn(string roomNumber, int customerId)
    {
        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            const string roomSql = "SELECT TrangThai FROM Phong WHERE SoPhong = @SoPhong FOR UPDATE";
            using var roomCommand = new MySqlCommand(roomSql, connection, transaction);
            roomCommand.Parameters.AddWithValue("@SoPhong", roomNumber);
            var status = roomCommand.ExecuteScalar()?.ToString();
            if (status is null) throw new InvalidOperationException("Phòng không tồn tại.");
            if (!string.Equals(status, "Trống", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Phòng đã được sử dụng hoặc đang dọn dẹp.");

            const string insertSql = "INSERT INTO PhieuDatPhong (SoPhong, MaKH, NgayNhanPhong) VALUES (@SoPhong, @MaKH, NOW())";
            using var insert = new MySqlCommand(insertSql, connection, transaction);
            insert.Parameters.AddWithValue("@SoPhong", roomNumber);
            insert.Parameters.AddWithValue("@MaKH", customerId);
            insert.ExecuteNonQuery();
            int receiptId = checked((int)insert.LastInsertedId);

            const string updateSql = "UPDATE Phong SET TrangThai = 'Đang ở' WHERE SoPhong = @SoPhong";
            using var update = new MySqlCommand(updateSql, connection, transaction);
            update.Parameters.AddWithValue("@SoPhong", roomNumber);
            update.ExecuteNonQuery();
            transaction.Commit();
            return receiptId;
        }
        catch { transaction.Rollback(); throw; }
    }

    public void AddService(int receiptId, int serviceId, int quantity)
    {
        const string sql = "INSERT INTO SuDungDichVu (MaPhieu, MaDV, SoLuong, ThoiGian) VALUES (@MaPhieu, @MaDV, @SoLuong, NOW())";
        Execute(sql, new MySqlParameter("@MaPhieu", receiptId), new MySqlParameter("@MaDV", serviceId), new MySqlParameter("@SoLuong", quantity));
    }

    public DataTable LoadUsedServices(int receiptId)
    {
        const string sql = """
            SELECT d.TenDV, s.SoLuong, d.DonGia, s.SoLuong * d.DonGia AS ThanhTien, s.ThoiGian
            FROM SuDungDichVu s INNER JOIN DichVu d ON d.MaDV = s.MaDV
            WHERE s.MaPhieu = @MaPhieu ORDER BY s.ThoiGian
            """;
        return GetData(sql, new MySqlParameter("@MaPhieu", receiptId));
    }

    public decimal Checkout(int receiptId)
    {
        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            const string detailSql = """
                SELECT p.SoPhong, p.NgayNhanPhong, r.GiaTheoGio,
                       COALESCE((SELECT SUM(s.SoLuong * d.DonGia) FROM SuDungDichVu s INNER JOIN DichVu d ON d.MaDV = s.MaDV WHERE s.MaPhieu = p.MaPhieu), 0) AS TienDichVu
                FROM PhieuDatPhong p INNER JOIN Phong r ON r.SoPhong = p.SoPhong
                WHERE p.MaPhieu = @MaPhieu AND p.NgayTraPhong IS NULL FOR UPDATE
                """;
            using var detail = new MySqlCommand(detailSql, connection, transaction);
            detail.Parameters.AddWithValue("@MaPhieu", receiptId);
            using var reader = detail.ExecuteReader();
            if (!reader.Read()) throw new InvalidOperationException("Phiếu không tồn tại hoặc đã checkout.");
            string roomNumber = reader.GetString("SoPhong");
            decimal roomPrice = reader.GetDecimal("GiaTheoGio");
            decimal serviceTotal = reader.GetDecimal("TienDichVu");
            reader.Close();

            const string totalSql = "SELECT CEILING(GREATEST(1, TIMESTAMPDIFF(MINUTE, NgayNhanPhong, NOW())) / 60.0) FROM PhieuDatPhong WHERE MaPhieu = @MaPhieu";
            using var totalCommand = new MySqlCommand(totalSql, connection, transaction);
            totalCommand.Parameters.AddWithValue("@MaPhieu", receiptId);
            int hours = Convert.ToInt32(totalCommand.ExecuteScalar());
            decimal total = hours * roomPrice + serviceTotal;

            const string updateReceiptSql = "UPDATE PhieuDatPhong SET NgayTraPhong = NOW(), TongTienThanhToan = @TongTien WHERE MaPhieu = @MaPhieu";
            using var updateReceipt = new MySqlCommand(updateReceiptSql, connection, transaction);
            updateReceipt.Parameters.AddWithValue("@TongTien", total);
            updateReceipt.Parameters.AddWithValue("@MaPhieu", receiptId);
            updateReceipt.ExecuteNonQuery();

            const string updateRoomSql = "UPDATE Phong SET TrangThai = 'Đang dọn dẹp' WHERE SoPhong = @SoPhong";
            using var updateRoom = new MySqlCommand(updateRoomSql, connection, transaction);
            updateRoom.Parameters.AddWithValue("@SoPhong", roomNumber);
            updateRoom.ExecuteNonQuery();
            transaction.Commit();
            return total;
        }
        catch { transaction.Rollback(); throw; }
    }

    private static DataTable GetData(string sql, params MySqlParameter[] parameters)
    {
        using var connection = new MySqlConnection(ConnectionString);
        using var adapter = new MySqlDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddRange(parameters);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    private static void Execute(string sql, params MySqlParameter[] parameters)
    {
        using var connection = new MySqlConnection(ConnectionString);
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddRange(parameters);
        connection.Open();
        command.ExecuteNonQuery();
    }
}
