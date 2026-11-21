using System.Data;
using Bai6._1.DAL;
using Bai6._1.DTO;

namespace Bai6._1.BUS;

public sealed class RetailService(RetailRepository repository)
{
    public void TestConnection() => repository.TestConnection();
    public DataTable Products() => repository.GetProducts();
    public DataTable SearchProducts(string query) => repository.SearchProducts(query.Trim());
    public DataTable Customers() => repository.GetCustomers();
    public DataTable Employees() => repository.GetEmployees();
    public DataTable CustomerChoices() => repository.GetCustomerChoices();
    public DataTable EmployeeChoices() => repository.GetEmployeeChoices();
    public Product? FindProduct(string code) => repository.GetProduct(code.Trim());
    public DataTable TopProducts() => repository.GetTopProducts();
    public decimal TodayRevenue(DateTime date) => repository.GetRevenue(date.Date, date.Date.AddDays(1));
    public decimal MonthRevenue(DateTime date) => repository.GetRevenue(new DateTime(date.Year, date.Month, 1), new DateTime(date.Year, date.Month, 1).AddMonths(1));

    public void AddProduct(string code, string name, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Trim().Length > 20) throw new ArgumentException("Mã sản phẩm không được trống và tối đa 20 ký tự.");
        if (string.IsNullOrWhiteSpace(name) || name.Trim().Length > 150) throw new ArgumentException("Tên sản phẩm không được trống và tối đa 150 ký tự.");
        if (price <= 0) throw new ArgumentException("Giá bán phải lớn hơn 0.");
        if (stock < 0) throw new ArgumentException("Tồn kho không được âm.");
        repository.AddProduct(new Product(code.Trim(), name.Trim(), price, stock));
    }

    public void UpdateProduct(string code, string name, decimal price, int stock)
    {
        AddProductInputValidation(code, name, price, stock);
        if (repository.UpdateProduct(new Product(code.Trim(), name.Trim(), price, stock)) == 0) throw new InvalidOperationException("Không tìm thấy sản phẩm cần cập nhật.");
    }

    public void DeleteProduct(string code)
    {
        if (repository.DeleteProduct(code) == 0) throw new InvalidOperationException("Không tìm thấy sản phẩm cần xóa.");
    }

    public void AddCustomer(string name, string? phone) { ValidatePerson(name, phone); repository.AddCustomer(name.Trim(), Normalize(phone)); }
    public void UpdateCustomer(int id, string name, string? phone) { ValidatePerson(name, phone); if (repository.UpdateCustomer(id, name.Trim(), Normalize(phone)) == 0) throw new InvalidOperationException("Không tìm thấy khách hàng cần cập nhật."); }
    public void DeleteCustomer(int id) { if (repository.DeleteCustomer(id) == 0) throw new InvalidOperationException("Không tìm thấy khách hàng cần xóa."); }
    public void AddEmployee(string name, string? phone, string? role) { ValidatePerson(name, phone); repository.AddEmployee(name.Trim(), Normalize(phone), Normalize(role)); }
    public void UpdateEmployee(int id, string name, string? phone, string? role) { ValidatePerson(name, phone); if (repository.UpdateEmployee(id, name.Trim(), Normalize(phone), Normalize(role)) == 0) throw new InvalidOperationException("Không tìm thấy nhân viên cần cập nhật."); }
    public void DeleteEmployee(int id) { if (repository.DeleteEmployee(id) == 0) throw new InvalidOperationException("Không tìm thấy nhân viên cần xóa."); }

    public int Checkout(int employeeId, int? customerId, IReadOnlyCollection<CartItem> items)
    {
        if (employeeId <= 0) throw new ArgumentException("Vui lòng chọn nhân viên lập hóa đơn.");
        if (items.Count == 0) throw new ArgumentException("Giỏ hàng đang trống.");
        if (items.Any(item => item.Quantity <= 0)) throw new ArgumentException("Số lượng bán phải lớn hơn 0.");
        return repository.CreateSale(employeeId, customerId, items);
    }

    private static void AddProductInputValidation(string code, string name, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Trim().Length > 20) throw new ArgumentException("Mã sản phẩm không được trống và tối đa 20 ký tự.");
        if (string.IsNullOrWhiteSpace(name) || name.Trim().Length > 150) throw new ArgumentException("Tên sản phẩm không được trống và tối đa 150 ký tự.");
        if (price <= 0) throw new ArgumentException("Giá bán phải lớn hơn 0.");
        if (stock < 0) throw new ArgumentException("Tồn kho không được âm.");
    }

    private static void ValidatePerson(string name, string? phone)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Trim().Length > 100) throw new ArgumentException("Tên không được trống và tối đa 100 ký tự.");
        if (phone?.Trim().Length > 15) throw new ArgumentException("Số điện thoại tối đa 15 ký tự.");
    }

    private static string? Normalize(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}