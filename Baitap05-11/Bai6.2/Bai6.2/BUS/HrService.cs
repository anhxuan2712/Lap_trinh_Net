using System.Data;
using System.Globalization;
using Bai6._2.DAL;
using Bai6._2.DTO;

namespace Bai6._2.BUS;

public sealed class HrService(HrRepository repository)
{
    public DataTable GetDepartments() => repository.GetDepartments();
    public bool IsPayrollClosed(string month) => repository.IsPayrollClosed(ValidateMonth(month));

    public DataTable SearchEmployees(string name, int? departmentId, string identityNumber) =>
        repository.SearchEmployees(name.Trim(), departmentId, identityNumber.Trim());

    public void SaveEmployee(Employee employee)
    {
        if (string.IsNullOrWhiteSpace(employee.Name))
            throw new ArgumentException("Họ tên nhân viên không được để trống.");
        if (employee.BaseSalary <= 0)
            throw new ArgumentException("Lương cơ bản phải lớn hơn 0.");
        if (employee.SalaryCoefficient <= 0)
            throw new ArgumentException("Hệ số lương phải lớn hơn 0.");
        if (employee.IdentityNumber?.Length > 20)
            throw new ArgumentException("CCCD không được vượt quá 20 ký tự.");
        repository.SaveEmployee(employee);
    }

    public void DeleteEmployee(int id)
    {
        if (repository.DeleteEmployee(id) != 1)
            throw new InvalidOperationException("Không tìm thấy nhân viên cần xóa.");
    }

    public DataTable LoadAttendance(string month) => repository.LoadAttendance(ValidateMonth(month));

    public void SaveAttendance(DataTable table, string month)
    {
        month = ValidateMonth(month);
        foreach (DataRow row in table.Rows)
        {
            if (row.RowState == DataRowState.Deleted)
                continue;
            var days = Convert.ToDouble(row["SoNgayCong"]);
            var overtime = Convert.ToDouble(row["SoGioOT"]);
            if (!double.IsFinite(days) || days < 0 || days > 31)
                throw new ArgumentException("Số ngày công phải nằm trong khoảng 0 đến 31.");
            if (!double.IsFinite(overtime) || overtime < 0)
                throw new ArgumentException("Số giờ OT phải lớn hơn hoặc bằng 0.");
        }
        repository.SaveAttendance(table, month);
    }

    public void ClosePayroll(string month) => repository.ClosePayroll(ValidateMonth(month));

    private static string ValidateMonth(string month)
    {
        if (!DateTime.TryParseExact(month, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            throw new ArgumentException("Tháng phải đúng định dạng MM/yyyy.");
        return month;
    }
}