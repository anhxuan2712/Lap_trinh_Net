using System.Data;
using Bai6._1.DTO;
using MySqlConnector;

namespace Bai6._1.DAL;

public sealed class RetailRepository(string connectionString)
{
    private MySqlConnection CreateConnection() => new(connectionString);

    public void TestConnection()
    {
        using var connection = CreateConnection();
        connection.Open();
    }

    public DataTable GetTable(string sql, params MySqlParameter[] parameters)
    {
        using var connection = CreateConnection();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddRange(parameters);
        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public Product? GetProduct(string code)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var command = new MySqlCommand("SELECT MaSP, TenSP, GiaBan, SoLuongTon FROM SanPham WHERE MaSP = @code", connection);
        command.Parameters.Add("@code", MySqlDbType.VarChar, 20).Value = code;
        using var reader = command.ExecuteReader();
        return reader.Read()
            ? new Product(reader.GetString("MaSP"), reader.GetString("TenSP"), reader.GetDecimal("GiaBan"), reader.GetInt32("SoLuongTon"))
            : null;
    }

    public DataTable SearchProducts(string query) => GetTable(
        "SELECT MaSP AS `Mã sản phẩm`, TenSP AS `Tên sản phẩm`, GiaBan AS `Giá bán`, SoLuongTon AS `Tồn kho` FROM SanPham WHERE MaSP LIKE @query OR TenSP LIKE @query ORDER BY TenSP",
        new MySqlParameter("@query", MySqlDbType.VarChar) { Value = $"%{query}%" });

    public DataTable GetProducts() => GetTable("SELECT MaSP AS `Mã sản phẩm`, TenSP AS `Tên sản phẩm`, GiaBan AS `Giá bán`, SoLuongTon AS `Tồn kho` FROM SanPham ORDER BY TenSP");
    public DataTable GetCustomers() => GetTable("SELECT MaKH AS `Mã khách hàng`, TenKH AS `Tên khách hàng`, DienThoai AS `Điện thoại`, DiemTichLuy AS `Điểm tích lũy` FROM KhachHang ORDER BY TenKH");
    public DataTable GetEmployees() => GetTable("SELECT MaNV AS `Mã nhân viên`, TenNV AS `Tên nhân viên`, SoDienThoai AS `Điện thoại`, ChucVu AS `Chức vụ` FROM NhanVien ORDER BY TenNV");

    public DataTable GetCustomerChoices() => GetTable("SELECT MaKH, TenKH FROM KhachHang ORDER BY TenKH");
    public DataTable GetEmployeeChoices() => GetTable("SELECT MaNV, TenNV FROM NhanVien ORDER BY TenNV");

    public int AddProduct(Product product) => Execute(
        "INSERT INTO SanPham (MaSP, TenSP, GiaBan, SoLuongTon) VALUES (@code, @name, @price, @stock)",
        ProductParameters(product));

    public int UpdateProduct(Product product) => Execute(
        "UPDATE SanPham SET TenSP=@name, GiaBan=@price, SoLuongTon=@stock WHERE MaSP=@code",
        ProductParameters(product));

    public int DeleteProduct(string code) => Execute("DELETE FROM SanPham WHERE MaSP=@code", new MySqlParameter("@code", MySqlDbType.VarChar, 20) { Value = code });

    public int AddCustomer(string name, string? phone) => Execute(
        "INSERT INTO KhachHang (TenKH, DienThoai) VALUES (@name, @phone)", NamePhoneParameters(name, phone));

    public int UpdateCustomer(int id, string name, string? phone) => Execute(
        "UPDATE KhachHang SET TenKH=@name, DienThoai=@phone WHERE MaKH=@id",
        [.. NamePhoneParameters(name, phone), new MySqlParameter("@id", MySqlDbType.Int32) { Value = id }]);

    public int DeleteCustomer(int id) => Execute("DELETE FROM KhachHang WHERE MaKH=@id", new MySqlParameter("@id", MySqlDbType.Int32) { Value = id });

    public int AddEmployee(string name, string? phone, string? role) => Execute(
        "INSERT INTO NhanVien (TenNV, SoDienThoai, ChucVu) VALUES (@name, @phone, @role)", EmployeeParameters(name, phone, role));

    public int UpdateEmployee(int id, string name, string? phone, string? role) => Execute(
        "UPDATE NhanVien SET TenNV=@name, SoDienThoai=@phone, ChucVu=@role WHERE MaNV=@id",
        [.. EmployeeParameters(name, phone, role), new MySqlParameter("@id", MySqlDbType.Int32) { Value = id }]);

    public int DeleteEmployee(int id) => Execute("DELETE FROM NhanVien WHERE MaNV=@id", new MySqlParameter("@id", MySqlDbType.Int32) { Value = id });

    public decimal GetRevenue(DateTime fromInclusive, DateTime toExclusive)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var command = new MySqlCommand("SELECT COALESCE(SUM(TongTien), 0) FROM HoaDon WHERE NgayLap >= @from AND NgayLap < @to", connection);
        command.Parameters.Add("@from", MySqlDbType.DateTime).Value = fromInclusive;
        command.Parameters.Add("@to", MySqlDbType.DateTime).Value = toExclusive;
        return Convert.ToDecimal(command.ExecuteScalar());
    }

    public DataTable GetTopProducts()
    {
        using var connection = CreateConnection();
        connection.Open();
        using var command = new MySqlCommand(
            "SELECT sp.MaSP AS `Mã sản phẩm`, sp.TenSP AS `Tên sản phẩm`, SUM(ct.SoLuong) AS `Đã bán` FROM ChiTietHoaDon ct JOIN SanPham sp ON sp.MaSP=ct.MaSP GROUP BY sp.MaSP, sp.TenSP ORDER BY SUM(ct.SoLuong) DESC LIMIT 5",
            connection);
        using var reader = command.ExecuteReader();
        var result = new DataTable();
        result.Load(reader);
        return result;
    }

    public int CreateSale(int employeeId, int? customerId, IReadOnlyCollection<CartItem> items)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            var currentItems = new List<(CartItem Item, decimal Price)>();
            foreach (var item in items)
            {
                using var lockCommand = new MySqlCommand(
                    "SELECT GiaBan, SoLuongTon FROM SanPham WHERE MaSP=@code FOR UPDATE", connection, transaction);
                lockCommand.Parameters.Add("@code", MySqlDbType.VarChar, 20).Value = item.ProductCode;
                using var reader = lockCommand.ExecuteReader();
                if (!reader.Read())
                    throw new InvalidOperationException($"Không tìm thấy sản phẩm {item.ProductCode}.");
                var price = reader.GetDecimal("GiaBan");
                var stock = reader.GetInt32("SoLuongTon");
                reader.Close();
                if (stock < item.Quantity)
                    throw new InvalidOperationException($"Sản phẩm {item.ProductCode} chỉ còn {stock} trong kho.");
                currentItems.Add((item, price));
            }

            var total = currentItems.Sum(line => line.Price * line.Item.Quantity);
            using var headerCommand = new MySqlCommand(
                "INSERT INTO HoaDon (MaNV, MaKH, TongTien) VALUES (@employee, @customer, @total)",
                connection, transaction);
            headerCommand.Parameters.Add("@employee", MySqlDbType.Int32).Value = employeeId;
            headerCommand.Parameters.Add("@customer", MySqlDbType.Int32).Value = customerId is null ? DBNull.Value : customerId.Value;
            headerCommand.Parameters.Add("@total", MySqlDbType.Decimal).Value = total;
            headerCommand.ExecuteNonQuery();
            var invoiceId = checked((int)headerCommand.LastInsertedId);

            foreach (var (item, price) in currentItems)
            {
                using var detailCommand = new MySqlCommand(
                    "INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia) VALUES (@invoice, @product, @quantity, @price)",
                    connection, transaction);
                detailCommand.Parameters.Add("@invoice", MySqlDbType.Int32).Value = invoiceId;
                detailCommand.Parameters.Add("@product", MySqlDbType.VarChar, 20).Value = item.ProductCode;
                detailCommand.Parameters.Add("@quantity", MySqlDbType.Int32).Value = item.Quantity;
                detailCommand.Parameters.Add("@price", MySqlDbType.Decimal).Value = price;
                detailCommand.ExecuteNonQuery();

                using var stockCommand = new MySqlCommand(
                    "UPDATE SanPham SET SoLuongTon=SoLuongTon-@quantity WHERE MaSP=@product AND SoLuongTon>=@quantity",
                    connection, transaction);
                stockCommand.Parameters.Add("@quantity", MySqlDbType.Int32).Value = item.Quantity;
                stockCommand.Parameters.Add("@product", MySqlDbType.VarChar, 20).Value = item.ProductCode;
                if (stockCommand.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException($"Không đủ tồn kho cho sản phẩm {item.ProductCode}.");
            }

            if (customerId is not null)
            {
                using var pointsCommand = new MySqlCommand(
                    "UPDATE KhachHang SET DiemTichLuy=DiemTichLuy+FLOOR(@total/100000) WHERE MaKH=@customer",
                    connection, transaction);
                pointsCommand.Parameters.Add("@total", MySqlDbType.Decimal).Value = total;
                pointsCommand.Parameters.Add("@customer", MySqlDbType.Int32).Value = customerId.Value;
                pointsCommand.ExecuteNonQuery();
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

    private int Execute(string sql, params MySqlParameter[] parameters)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddRange(parameters);
        return command.ExecuteNonQuery();
    }

    private static MySqlParameter[] ProductParameters(Product product) =>
    [
        new("@code", MySqlDbType.VarChar, 20) { Value = product.Code },
        new("@name", MySqlDbType.VarChar, 150) { Value = product.Name },
        new("@price", MySqlDbType.Decimal) { Value = product.Price },
        new("@stock", MySqlDbType.Int32) { Value = product.Stock }
    ];

    private static MySqlParameter[] NamePhoneParameters(string name, string? phone) =>
    [
        new("@name", MySqlDbType.VarChar, 100) { Value = name },
        new("@phone", MySqlDbType.VarChar, 15) { Value = (object?)phone ?? DBNull.Value }
    ];

    private static MySqlParameter[] EmployeeParameters(string name, string? phone, string? role) =>
    [
        new("@name", MySqlDbType.VarChar, 100) { Value = name },
        new("@phone", MySqlDbType.VarChar, 15) { Value = (object?)phone ?? DBNull.Value },
        new("@role", MySqlDbType.VarChar, 50) { Value = (object?)role ?? DBNull.Value }
    ];
}