using System.Data;
using Bai6._2.DTO;
using MySql.Data.MySqlClient;

namespace Bai6._2.DAL;

public sealed class HrRepository(string connectionString)
{
    private MySqlConnection CreateConnection() => new(connectionString);

    public DataTable GetDepartments()
    {
        using var connection = CreateConnection();
        using var adapter = new MySqlDataAdapter("SELECT MaPB, TenPB FROM PhongBan ORDER BY TenPB", connection);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public bool IsPayrollClosed(string month)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var command = new MySqlCommand("SELECT EXISTS(SELECT 1 FROM BangLuong WHERE ThangNam=@month)", connection);
        command.Parameters.Add("@month", MySqlDbType.VarChar, 7).Value = month;
        return Convert.ToBoolean(command.ExecuteScalar());
    }

    public DataTable SearchEmployees(string name, int? departmentId, string identityNumber)
    {
        using var connection = CreateConnection();
        using var command = new MySqlCommand(
            "SELECT nv.MaNV AS `Mã NV`, nv.HoTen AS `Họ tên`, nv.NgaySinh AS `Ngày sinh`, nv.CCCD AS `CCCD`, " +
            "nv.MaPB, pb.TenPB AS `Phòng ban`, nv.LuongCoBan AS `Lương cơ bản`, nv.HeSoLuong AS `Hệ số lương` " +
            "FROM NhanVien nv LEFT JOIN PhongBan pb ON pb.MaPB=nv.MaPB " +
            "WHERE nv.HoTen LIKE @name AND (@department IS NULL OR nv.MaPB=@department) " +
            "AND (@identity='' OR nv.CCCD LIKE @identity) ORDER BY nv.MaNV DESC", connection);
        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = $"%{name}%";
        command.Parameters.Add("@department", MySqlDbType.Int32).Value = departmentId is null ? DBNull.Value : departmentId.Value;
        command.Parameters.Add("@identity", MySqlDbType.VarChar).Value = identityNumber.Length == 0 ? "" : $"%{identityNumber}%";
        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public int SaveEmployee(Employee employee)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var command = new MySqlCommand(employee.Id is null
            ? "INSERT INTO NhanVien (HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong) VALUES (@name,@birth,@identity,@department,@salary,@coefficient)"
            : "UPDATE NhanVien SET HoTen=@name, NgaySinh=@birth, CCCD=@identity, MaPB=@department, LuongCoBan=@salary, HeSoLuong=@coefficient WHERE MaNV=@id", connection);
        AddEmployeeParameters(command, employee);
        if (employee.Id is not null)
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = employee.Id.Value;
        try
        {
            return command.ExecuteNonQuery();
        }
        catch (MySqlException ex) when (ex.Number == (int)MySqlErrorCode.DuplicateKeyEntry)
        {
            throw new InvalidOperationException("Số CCCD này đã tồn tại.", ex);
        }
    }

    public int DeleteEmployee(int id)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var command = new MySqlCommand("DELETE FROM NhanVien WHERE MaNV=@id", connection);
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        try
        {
            return command.ExecuteNonQuery();
        }
        catch (MySqlException ex) when (ex.Number == (int)MySqlErrorCode.RowIsReferenced2)
        {
            throw new InvalidOperationException("Không thể xóa nhân viên đã có dữ liệu chấm công hoặc bảng lương.", ex);
        }
    }

    public DataTable LoadAttendance(string month)
    {
        using var connection = CreateConnection();
        using var command = new MySqlCommand(
            "SELECT bc.MaCong, nv.MaNV, @month AS ThangNam, nv.HoTen, pb.TenPB, " +
            "COALESCE(bc.SoNgayCong, CAST(0 AS FLOAT)) AS SoNgayCong, " +
            "COALESCE(bc.SoGioOT, CAST(0 AS FLOAT)) AS SoGioOT " +
            "FROM NhanVien nv LEFT JOIN PhongBan pb ON pb.MaPB=nv.MaPB " +
            "LEFT JOIN BangChamCong bc ON bc.MaNV=nv.MaNV AND bc.ThangNam=@month " +
            "ORDER BY nv.MaNV", connection);
        command.Parameters.Add("@month", MySqlDbType.VarChar, 7).Value = month;
        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public void SaveAttendance(DataTable table, string month)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            EnsurePayrollOpen(connection, transaction, month, "Tháng này đã chốt lương, không thể sửa chấm công.");

            foreach (DataRow row in table.Rows)
            {
                // Dòng chưa có trong BangChamCong (MaCong rỗng) luôn được ghi; dòng đã có chỉ ghi khi bị sửa.
                if (row.RowState == DataRowState.Unchanged && !row.IsNull("MaCong"))
                    continue;

                using var upsert = new MySqlCommand(
                    "INSERT INTO BangChamCong (MaNV, ThangNam, SoNgayCong, SoGioOT) VALUES (@employee,@month,@days,@overtime) " +
                    "ON DUPLICATE KEY UPDATE SoNgayCong=@days, SoGioOT=@overtime", connection, transaction);
                upsert.Parameters.Add("@employee", MySqlDbType.Int32).Value = row["MaNV"];
                upsert.Parameters.Add("@month", MySqlDbType.VarChar, 7).Value = month;
                upsert.Parameters.Add("@days", MySqlDbType.Float).Value = row["SoNgayCong"];
                upsert.Parameters.Add("@overtime", MySqlDbType.Float).Value = row["SoGioOT"];
                upsert.ExecuteNonQuery();
            }

            transaction.Commit();
            table.AcceptChanges();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void ClosePayroll(string month)
    {
        using var connection = CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            EnsurePayrollOpen(connection, transaction, month, "Tháng này đã được chốt lương trước đó.");

            using var lockCommand = new MySqlCommand(
                "SELECT bc.MaNV, bc.SoNgayCong, bc.SoGioOT, nv.LuongCoBan, nv.HeSoLuong " +
                "FROM BangChamCong bc JOIN NhanVien nv ON nv.MaNV=bc.MaNV WHERE bc.ThangNam=@month FOR UPDATE", connection, transaction);
            lockCommand.Parameters.Add("@month", MySqlDbType.VarChar, 7).Value = month;
            var payrollRows = new List<(int EmployeeId, decimal NetPay)>();
            using (var reader = lockCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var days = Convert.ToDecimal(reader["SoNgayCong"]);
                    var overtime = Convert.ToDecimal(reader["SoGioOT"]);
                    var baseSalary = reader.GetDecimal("LuongCoBan");
                    var coefficient = Convert.ToDecimal(reader["HeSoLuong"]);
                    var pay = decimal.Round(baseSalary * coefficient / 22m * days + overtime * 50000m, 2, MidpointRounding.AwayFromZero);
                    payrollRows.Add((reader.GetInt32("MaNV"), pay));
                }
            }

            if (payrollRows.Count == 0)
                throw new InvalidOperationException("Không có dữ liệu chấm công để chốt lương tháng này.");

            foreach (var (employeeId, netPay) in payrollRows)
            {
                using var insert = new MySqlCommand(
                    "INSERT INTO BangLuong (MaNV, ThangNam, ThucLinh) VALUES (@employee,@month,@pay)", connection, transaction);
                insert.Parameters.Add("@employee", MySqlDbType.Int32).Value = employeeId;
                insert.Parameters.Add("@month", MySqlDbType.VarChar, 7).Value = month;
                insert.Parameters.Add("@pay", MySqlDbType.Decimal).Value = netPay;
                insert.ExecuteNonQuery();
            }
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private static void EnsurePayrollOpen(MySqlConnection connection, MySqlTransaction transaction, string month, string message)
    {
        using var check = new MySqlCommand("SELECT COUNT(*) FROM BangLuong WHERE ThangNam=@month", connection, transaction);
        check.Parameters.Add("@month", MySqlDbType.VarChar, 7).Value = month;
        if (Convert.ToInt32(check.ExecuteScalar()) > 0)
            throw new InvalidOperationException(message);
    }

    private static void AddEmployeeParameters(MySqlCommand command, Employee employee)
    {
        command.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = employee.Name;
        command.Parameters.Add("@birth", MySqlDbType.DateTime).Value = employee.BirthDate is null ? DBNull.Value : employee.BirthDate.Value;
        command.Parameters.Add("@identity", MySqlDbType.VarChar, 20).Value = (object?)employee.IdentityNumber ?? DBNull.Value;
        command.Parameters.Add("@department", MySqlDbType.Int32).Value = employee.DepartmentId is null ? DBNull.Value : employee.DepartmentId.Value;
        command.Parameters.Add("@salary", MySqlDbType.Decimal).Value = employee.BaseSalary;
        command.Parameters.Add("@coefficient", MySqlDbType.Float).Value = employee.SalaryCoefficient;
    }
}