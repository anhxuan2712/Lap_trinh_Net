using MySql.Data.MySqlClient;

namespace Bai6._4.DAL;

public sealed class GarageRepository
{
    private readonly string _connectionString;

    public GarageRepository()
    {
        _connectionString = DbConfig.ConnectionString;
    }

    public void TestConnection()
    {
        using var connection = Open();
    }

    public List<CustomerDto> GetCustomers()
    {
        const string sql = "SELECT MaKH, TenKH, DienThoai, DiaChi FROM KhachHang ORDER BY TenKH";
        using var connection = Open();
        using var command = new MySqlCommand(sql, connection);
        using var reader = command.ExecuteReader();
        var customers = new List<CustomerDto>();
        while (reader.Read())
        {
            customers.Add(new CustomerDto(
                reader.GetInt32("MaKH"),
                reader.GetString("TenKH"),
                reader.IsDBNull(reader.GetOrdinal("DienThoai")) ? "" : reader.GetString("DienThoai"),
                reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? "" : reader.GetString("DiaChi")));
        }
        return customers;
    }

    public void SaveCustomer(int? customerId, string name, string? phone, string? address)
    {
        var sql = customerId.HasValue
            ? "UPDATE KhachHang SET TenKH = @name, DienThoai = @phone, DiaChi = @address WHERE MaKH = @id"
            : "INSERT INTO KhachHang (TenKH, DienThoai, DiaChi) VALUES (@name, @phone, @address)";
        using var connection = Open();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(phone) ? DBNull.Value : phone.Trim());
        command.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(address) ? DBNull.Value : address.Trim());
        if (customerId.HasValue)
        {
            command.Parameters.AddWithValue("@id", customerId.Value);
        }
        command.ExecuteNonQuery();
    }

    public void DeleteCustomer(int customerId)
    {
        using var connection = Open();
        using var command = new MySqlCommand("DELETE FROM KhachHang WHERE MaKH = @id", connection);
        command.Parameters.AddWithValue("@id", customerId);
        command.ExecuteNonQuery();
    }

    public List<VehicleDto> GetVehicles()
    {
        const string sql = "SELECT x.BienSo, x.MaKH, k.TenKH, x.HieuXe, x.NamSanXuat FROM Xe x INNER JOIN KhachHang k ON k.MaKH = x.MaKH ORDER BY x.BienSo";
        using var connection = Open();
        using var command = new MySqlCommand(sql, connection);
        using var reader = command.ExecuteReader();
        var vehicles = new List<VehicleDto>();
        while (reader.Read())
        {
            vehicles.Add(ReadVehicle(reader));
        }
        return vehicles;
    }

    public VehicleDto? FindVehicle(string plate)
    {
        const string sql = "SELECT x.BienSo, x.MaKH, k.TenKH, x.HieuXe, x.NamSanXuat FROM Xe x INNER JOIN KhachHang k ON k.MaKH = x.MaKH WHERE x.BienSo = @plate";
        using var connection = Open();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@plate", plate.Trim());
        using var reader = command.ExecuteReader();
        return reader.Read() ? ReadVehicle(reader) : null;
    }

    public void SaveVehicle(string plate, int customerId, string? make, int? year)
    {
        const string sql = "INSERT INTO Xe (BienSo, MaKH, HieuXe, NamSanXuat) VALUES (@plate, @customerId, @make, @year) ON DUPLICATE KEY UPDATE MaKH = @customerId, HieuXe = @make, NamSanXuat = @year";
        using var connection = Open();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@plate", plate.Trim());
        command.Parameters.AddWithValue("@customerId", customerId);
        command.Parameters.AddWithValue("@make", string.IsNullOrWhiteSpace(make) ? DBNull.Value : make.Trim());
        command.Parameters.AddWithValue("@year", year.HasValue ? year.Value : DBNull.Value);
        command.ExecuteNonQuery();
    }

    public void DeleteVehicle(string plate)
    {
        using var connection = Open();
        using var command = new MySqlCommand("DELETE FROM Xe WHERE BienSo = @plate", connection);
        command.Parameters.AddWithValue("@plate", plate.Trim());
        command.ExecuteNonQuery();
    }

    public List<PartDto> GetParts()
    {
        const string sql = "SELECT MaPhuTung, TenPhuTung, DonGia, SoLuongTon FROM PhuTung ORDER BY TenPhuTung";
        using var connection = Open();
        using var command = new MySqlCommand(sql, connection);
        using var reader = command.ExecuteReader();
        var parts = new List<PartDto>();
        while (reader.Read())
        {
            parts.Add(new PartDto(reader.GetInt32("MaPhuTung"), reader.GetString("TenPhuTung"), reader.GetDecimal("DonGia"), reader.GetInt32("SoLuongTon")));
        }
        return parts;
    }

    public void SavePart(int? partId, string name, decimal price, int stock)
    {
        var sql = partId.HasValue
            ? "UPDATE PhuTung SET TenPhuTung = @name, DonGia = @price, SoLuongTon = @stock WHERE MaPhuTung = @id"
            : "INSERT INTO PhuTung (TenPhuTung, DonGia, SoLuongTon) VALUES (@name, @price, @stock)";
        using var connection = Open();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@name", name.Trim());
        command.Parameters.AddWithValue("@price", price);
        command.Parameters.AddWithValue("@stock", stock);
        if (partId.HasValue)
        {
            command.Parameters.AddWithValue("@id", partId.Value);
        }
        command.ExecuteNonQuery();
    }

    public void DeletePart(int partId)
    {
        using var connection = Open();
        using var command = new MySqlCommand("DELETE FROM PhuTung WHERE MaPhuTung = @id", connection);
        command.Parameters.AddWithValue("@id", partId);
        command.ExecuteNonQuery();
    }

    public List<RepairInvoiceDto> GetInvoices()
    {
        const string sql = "SELECT MaPhieu, BienSo, NgayNhan, NoiDungSua, TongTien, TrangThai FROM PhieuSuaChua ORDER BY MaPhieu DESC";
        using var connection = Open();
        using var command = new MySqlCommand(sql, connection);
        using var reader = command.ExecuteReader();
        var invoices = new List<RepairInvoiceDto>();
        while (reader.Read())
        {
            invoices.Add(new RepairInvoiceDto(
                reader.GetInt32("MaPhieu"),
                reader.GetString("BienSo"),
                reader.GetDateTime("NgayNhan"),
                reader.IsDBNull(reader.GetOrdinal("NoiDungSua")) ? "" : reader.GetString("NoiDungSua"),
                reader.GetDecimal("TongTien"),
                reader.GetString("TrangThai")));
        }
        return invoices;
    }

    public int CreateRepair(string plate, string description, decimal labor, IReadOnlyCollection<RepairLineInput> lines)
    {
        if (lines.Count == 0)
        {
            throw new InvalidOperationException("Hãy chọn ít nhất một phụ tùng cho phiếu sửa chữa.");
        }

        if (lines.Any(line => line.Quantity <= 0) || lines.Select(line => line.PartId).Distinct().Count() != lines.Count)
        {
            throw new InvalidOperationException("Số lượng phụ tùng phải lớn hơn 0 và không được chọn trùng.");
        }

        using var connection = Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            var lockedParts = new List<(RepairLineInput Line, decimal Price)>();
            foreach (var line in lines)
            {
                const string lockSql = "SELECT TenPhuTung, DonGia, SoLuongTon FROM PhuTung WHERE MaPhuTung = @partId FOR UPDATE";
                using var lockCommand = new MySqlCommand(lockSql, connection, transaction);
                lockCommand.Parameters.AddWithValue("@partId", line.PartId);
                using var reader = lockCommand.ExecuteReader();
                if (!reader.Read())
                {
                    throw new InvalidOperationException($"Phụ tùng mã {line.PartId} không còn tồn tại.");
                }

                var partName = reader.GetString("TenPhuTung");
                var price = reader.GetDecimal("DonGia");
                var stock = reader.GetInt32("SoLuongTon");
                reader.Close();
                if (stock < line.Quantity)
                {
                    throw new InvalidOperationException($"Phụ tùng '{partName}' không đủ tồn kho. Còn {stock}, cần {line.Quantity}.");
                }
                lockedParts.Add((line, price));
            }

            var total = labor + lockedParts.Sum(item => item.Price * item.Line.Quantity);
            const string invoiceSql = "INSERT INTO PhieuSuaChua (BienSo, NoiDungSua, TongTien, TrangThai) VALUES (@plate, @description, @total, 'Đã hoàn thành'); SELECT LAST_INSERT_ID();";
            using var invoiceCommand = new MySqlCommand(invoiceSql, connection, transaction);
            invoiceCommand.Parameters.AddWithValue("@plate", plate);
            invoiceCommand.Parameters.AddWithValue("@description", string.IsNullOrWhiteSpace(description) ? DBNull.Value : description.Trim());
            invoiceCommand.Parameters.AddWithValue("@total", total);
            var invoiceId = Convert.ToInt32(invoiceCommand.ExecuteScalar());

            for (var index = 0; index < lockedParts.Count; index++)
            {
                var item = lockedParts[index];
                const string stockSql = "UPDATE PhuTung SET SoLuongTon = SoLuongTon - @quantity WHERE MaPhuTung = @partId AND SoLuongTon >= @quantity";
                using var stockCommand = new MySqlCommand(stockSql, connection, transaction);
                stockCommand.Parameters.AddWithValue("@quantity", item.Line.Quantity);
                stockCommand.Parameters.AddWithValue("@partId", item.Line.PartId);
                if (stockCommand.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException("Tồn kho đã thay đổi. Phiếu sửa chữa đã được hủy, vui lòng tải lại danh sách phụ tùng.");
                }

                const string detailSql = "INSERT INTO ChiTietSuaChua (MaPhieu, MaPhuTung, SoLuong, TienCong) VALUES (@invoiceId, @partId, @quantity, @labor)";
                using var detailCommand = new MySqlCommand(detailSql, connection, transaction);
                detailCommand.Parameters.AddWithValue("@invoiceId", invoiceId);
                detailCommand.Parameters.AddWithValue("@partId", item.Line.PartId);
                detailCommand.Parameters.AddWithValue("@quantity", item.Line.Quantity);
                detailCommand.Parameters.AddWithValue("@labor", index == 0 ? labor : 0m);
                detailCommand.ExecuteNonQuery();
            }

            transaction.Commit();
            return invoiceId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void PayInvoice(int invoiceId)
    {
        const string sql = "UPDATE PhieuSuaChua SET TrangThai = 'Đã thanh toán' WHERE MaPhieu = @id AND TrangThai = 'Đã hoàn thành'";
        using var connection = Open();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", invoiceId);
        if (command.ExecuteNonQuery() != 1)
        {
            throw new InvalidOperationException("Không tìm thấy phiếu chưa thanh toán hoặc phiếu chưa ở trạng thái 'Đã hoàn thành'.");
        }
    }

    private static VehicleDto ReadVehicle(MySqlDataReader reader) => new(
        reader.GetString("BienSo"),
        reader.GetInt32("MaKH"),
        reader.GetString("TenKH"),
        reader.IsDBNull(reader.GetOrdinal("HieuXe")) ? "" : reader.GetString("HieuXe"),
        reader.IsDBNull(reader.GetOrdinal("NamSanXuat")) ? null : reader.GetInt32("NamSanXuat"));

    private MySqlConnection Open()
    {
        var connection = new MySqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}

public sealed record CustomerDto(int Id, string Name, string Phone, string Address)
{
    public string DisplayName => string.IsNullOrWhiteSpace(Phone) ? Name : $"{Name} ({Phone})";
}

public sealed record VehicleDto(string Plate, int CustomerId, string CustomerName, string Make, int? Year);

public sealed record PartDto(int Id, string Name, decimal Price, int Stock);

public sealed record RepairLineInput(int PartId, int Quantity);

public sealed record RepairInvoiceDto(int Id, string Plate, DateTime ReceivedAt, string Description, decimal Total, string Status);
