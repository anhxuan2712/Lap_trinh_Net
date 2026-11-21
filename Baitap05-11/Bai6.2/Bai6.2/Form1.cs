
using System.Data;
using Bai6._2.BUS;
using Bai6._2.DAL;
using Bai6._2.DTO;

namespace Bai6._2;

public partial class Form1 : Form
{
    private readonly HrService service;

    private readonly TextBox nameFilter = new() { Width = 160, PlaceholderText = "Tên nhân viên" };
    private readonly TextBox identityFilter = new() { Width = 140, PlaceholderText = "Số CCCD" };
    private readonly ComboBox departmentFilter = new() { Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };

    private readonly TextBox employeeName = new() { Width = 190 };
    private readonly DateTimePicker birthDate = new() { Width = 145, Format = DateTimePickerFormat.Short, ShowCheckBox = true };
    private readonly TextBox identityNumber = new() { Width = 145 };
    private readonly ComboBox employeeDepartment = new() { Width = 155, DropDownStyle = ComboBoxStyle.DropDownList };

    private readonly NumericUpDown baseSalary = new()
    {
        Width = 130,
        DecimalPlaces = 2,
        Maximum = 1000000000,
        Minimum = 0.01m,
        ThousandsSeparator = true
    };

    private readonly NumericUpDown salaryCoefficient = new()
    {
        Width = 85,
        DecimalPlaces = 2,
        Minimum = 0.01m,
        Maximum = 100,
        Value = 1
    };

    private readonly DataGridView employeesGrid = CreateGrid();
    private readonly DataGridView attendanceGrid = CreateGrid();

    private readonly NumericUpDown month = new()
    {
        Minimum = 1,
        Maximum = 12,
        Value = DateTime.Today.Month,
        Width = 65
    };

    private readonly NumericUpDown year = new()
    {
        Minimum = 2000,
        Maximum = 2100,
        Value = DateTime.Today.Year,
        Width = 80
    };

    private DataTable? attendanceTable;
    private string? attendanceMonth;
    private int? selectedEmployeeId;

    public Form1()
    {
        InitializeComponent();

        var connectionString = Environment.GetEnvironmentVariable("HRMDB_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = "Server=localhost;Port=3306;Database=HRMDB;CharSet=utf8mb4;Uid=root;Pwd=familo23;";
        }

        service = new HrService(new HrRepository(connectionString));

        BuildInterface();

        Load += (_, _) => InitializeData();
    }

    private void BuildInterface()
    {
        Text = "Hệ thống quản lý nhân sự và tính lương";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1050, 680);
        Size = new Size(1250, 800);

        var tabs = new TabControl { Dock = DockStyle.Fill };

        tabs.TabPages.Add(BuildEmployeesTab());
        tabs.TabPages.Add(BuildAttendanceTab());

        Controls.Add(tabs);
    }

    private TabPage BuildEmployeesTab()
    {
        var page = new TabPage("Nhân sự");

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            RowCount = 3,
            ColumnCount = 1
        };

        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        page.Controls.Add(root);

        var filters = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            WrapContents = true,
            Padding = new Padding(0, 0, 0, 8)
        };

        filters.Controls.AddRange([
            LabelFor("Tìm tên:"),
            nameFilter,
            LabelFor("Phòng ban:"),
            departmentFilter,
            LabelFor("CCCD:"),
            identityFilter,
            Button("Tìm kiếm", (_, _) => RefreshEmployees())
        ]);

        root.Controls.Add(filters, 0, 0);

        var editor = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            WrapContents = true,
            Padding = new Padding(0, 0, 0, 8)
        };

        editor.Controls.AddRange([
            LabelFor("Họ tên:"),
            employeeName,
            LabelFor("Ngày sinh:"),
            birthDate,
            LabelFor("CCCD:"),
            identityNumber,
            LabelFor("Phòng ban:"),
            employeeDepartment,
            LabelFor("Lương cơ bản:"),
            baseSalary,
            LabelFor("Hệ số:"),
            salaryCoefficient,
            Button("Thêm", (_, _) => SaveEmployee(false)),
            Button("Cập nhật", (_, _) => SaveEmployee(true)),
            Button("Xóa", (_, _) => DeleteSelectedEmployee()),
            Button("Làm mới", (_, _) => ClearEditor())
        ]);

        root.Controls.Add(editor, 0, 1);

        employeesGrid.ReadOnly = true;
        employeesGrid.SelectionChanged += (_, _) => SelectEmployee();

        root.Controls.Add(employeesGrid, 0, 2);

        return page;
    }

    private TabPage BuildAttendanceTab()
    {
        var page = new TabPage("Chấm công & tính lương");

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            RowCount = 2,
            ColumnCount = 1
        };

        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        page.Controls.Add(root);

        var actions = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            Padding = new Padding(0, 0, 0, 8)
        };

        actions.Controls.AddRange([
            LabelFor("Tháng:"),
            month,
            LabelFor("Năm:"),
            year,
            Button("Tải chấm công", (_, _) => LoadAttendance()),
            Button("Lưu chấm công", (_, _) => SaveAttendance()),
            Button("Chốt lương tháng", (_, _) => ClosePayroll())
        ]);

        root.Controls.Add(actions, 0, 0);

        attendanceGrid.AllowUserToAddRows = false;
        attendanceGrid.CellValidating += AttendanceGridCellValidating;

        root.Controls.Add(attendanceGrid, 0, 1);

        return page;
    }

    private void InitializeData()
    {
        try
        {
            var departments = service.GetDepartments();

            departmentFilter.DataSource = WithAllDepartments(departments.Copy());
            departmentFilter.DisplayMember = "TenPB";
            departmentFilter.ValueMember = "MaPB";

            employeeDepartment.DataSource = departments;
            employeeDepartment.DisplayMember = "TenPB";
            employeeDepartment.ValueMember = "MaPB";

            RefreshEmployees();
            LoadAttendance();
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private static DataTable WithAllDepartments(DataTable departments)
    {
        var row = departments.NewRow();

        row["MaPB"] = DBNull.Value;
        row["TenPB"] = "Tất cả phòng ban";

        departments.Rows.InsertAt(row, 0);

        return departments;
    }

    private void RefreshEmployees()
    {
        try
        {
            int? departmentId = departmentFilter.SelectedValue is int value ? value : null;

            employeesGrid.DataSource = service.SearchEmployees(
                nameFilter.Text,
                departmentId,
                identityFilter.Text
            );

            if (employeesGrid.Columns.Contains("MaPB"))
                employeesGrid.Columns["MaPB"]!.Visible = false;

            ClearEditor();
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void SaveEmployee(bool update)
    {
        try
        {
            if (update && selectedEmployeeId is null)
                throw new InvalidOperationException("Vui lòng chọn nhân viên cần cập nhật.");

            var employee = new Employee(
                update ? selectedEmployeeId : null,
                employeeName.Text.Trim(),
                birthDate.Checked ? birthDate.Value.Date : null,
                string.IsNullOrWhiteSpace(identityNumber.Text) ? null : identityNumber.Text.Trim(),
                employeeDepartment.SelectedValue is int departmentId ? departmentId : null,
                baseSalary.Value,
                salaryCoefficient.Value
            );

            service.SaveEmployee(employee);

            RefreshEmployees();
            LoadAttendance();
            ClearEditor();

            MessageBox.Show(
                "Đã lưu hồ sơ nhân viên.",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void DeleteSelectedEmployee()
    {
        if (selectedEmployeeId is null)
        {
            ShowError(new InvalidOperationException("Vui lòng chọn nhân viên cần xóa."));
            return;
        }

        if (MessageBox.Show(
            "Bạn có chắc muốn xóa nhân viên này?",
            "Xác nhận",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        ) != DialogResult.Yes)
            return;

        try
        {
            service.DeleteEmployee(selectedEmployeeId.Value);

            RefreshEmployees();
            LoadAttendance();
            ClearEditor();
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void SelectEmployee()
    {
        if (employeesGrid.CurrentRow is not { Selected: true, DataBoundItem: DataRowView row })
            return;

        selectedEmployeeId = Convert.ToInt32(row["Mã NV"]);

        employeeName.Text = Convert.ToString(row["Họ tên"]) ?? "";

        birthDate.Checked = row["Ngày sinh"] != DBNull.Value;

        if (birthDate.Checked)
            birthDate.Value = Convert.ToDateTime(row["Ngày sinh"]);

        identityNumber.Text = Convert.ToString(row["CCCD"]) ?? "";

        employeeDepartment.SelectedValue =
            row["MaPB"] == DBNull.Value ? -1 : Convert.ToInt32(row["MaPB"]);

        baseSalary.Value = Math.Clamp(
            Convert.ToDecimal(row["Lương cơ bản"]),
            baseSalary.Minimum,
            baseSalary.Maximum
        );

        salaryCoefficient.Value = Math.Clamp(
            Convert.ToDecimal(row["Hệ số lương"]),
            salaryCoefficient.Minimum,
            salaryCoefficient.Maximum
        );
    }

    private void ClearEditor()
    {
        selectedEmployeeId = null;

        employeeName.Clear();
        identityNumber.Clear();

        birthDate.Checked = false;

        if (employeeDepartment.Items.Count > 0)
            employeeDepartment.SelectedIndex = 0;

        baseSalary.Value = baseSalary.Minimum;
        salaryCoefficient.Value = 1;

        employeesGrid.ClearSelection();
    }

    private void LoadAttendance()
    {
        try
        {
            CommitAttendanceEdits();

            if (attendanceTable?.GetChanges() is not null &&
                MessageBox.Show(
                    "Chấm công đang có thay đổi chưa lưu. Bạn có muốn tải lại và bỏ các thay đổi này?",
                    "Xác nhận tải lại",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                ) != DialogResult.Yes)
                return;

            var selectedMonth = CurrentMonth();

            attendanceTable = service.LoadAttendance(selectedMonth);
            attendanceMonth = selectedMonth;

            attendanceGrid.DataSource = attendanceTable;
            attendanceGrid.ReadOnly = service.IsPayrollClosed(selectedMonth);

            foreach (var column in new[] { "MaCong", "MaNV", "ThangNam" })
            {
                if (attendanceGrid.Columns.Contains(column))
                    attendanceGrid.Columns[column]!.Visible = false;
            }

            attendanceGrid.Columns["HoTen"]!.HeaderText = "Họ tên";
            attendanceGrid.Columns["TenPB"]!.HeaderText = "Phòng ban";
            attendanceGrid.Columns["SoNgayCong"]!.HeaderText = "Số ngày công";
            attendanceGrid.Columns["SoGioOT"]!.HeaderText = "Số giờ OT";

            attendanceGrid.Columns["HoTen"]!.ReadOnly = true;
            attendanceGrid.Columns["TenPB"]!.ReadOnly = true;
            attendanceGrid.Columns["MaCong"]!.ReadOnly = true;
            attendanceGrid.Columns["MaNV"]!.ReadOnly = true;
            attendanceGrid.Columns["ThangNam"]!.ReadOnly = true;
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void SaveAttendance()
    {
        try
        {
            CommitAttendanceEdits();

            if (attendanceTable is null)
                throw new InvalidOperationException("Tải dữ liệu chấm công trước khi lưu.");

            if (attendanceMonth != CurrentMonth())
                throw new InvalidOperationException("Tháng đã thay đổi. Hãy tải chấm công của tháng đang chọn trước khi lưu.");

            service.SaveAttendance(attendanceTable, CurrentMonth());

            MessageBox.Show(
                "Đã lưu chấm công tháng " + CurrentMonth() + ".",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            LoadAttendance();
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void ClosePayroll()
    {
        try
        {
            CommitAttendanceEdits();

            if (attendanceMonth != CurrentMonth())
                throw new InvalidOperationException("Hãy tải chấm công của tháng đang chọn trước khi chốt lương.");

            if (attendanceTable?.GetChanges() is not null)
            {
                if (MessageBox.Show(
                    "Chấm công có thay đổi chưa lưu. Lưu thay đổi trước khi chốt lương?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                ) != DialogResult.Yes)
                    return;

                service.SaveAttendance(attendanceTable, CurrentMonth());
            }
        }
        catch (Exception ex)
        {
            ShowError(ex);
            return;
        }

        if (MessageBox.Show(
            $"Chốt lương tháng {CurrentMonth()}? Sau khi chốt sẽ không thể sửa chấm công.",
            "Xác nhận chốt lương",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        ) != DialogResult.Yes)
            return;

        try
        {
            service.ClosePayroll(CurrentMonth());

            MessageBox.Show(
                "Đã chốt lương và khóa dữ liệu chấm công tháng " + CurrentMonth() + ".",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            attendanceTable = null;
            attendanceMonth = null;

            LoadAttendance();
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void AttendanceGridCellValidating(
        object? sender,
        DataGridViewCellValidatingEventArgs e)
    {
        var column = attendanceGrid.Columns[e.ColumnIndex].Name;

        if (column is not ("SoNgayCong" or "SoGioOT"))
            return;

        if (!double.TryParse(Convert.ToString(e.FormattedValue), out var value) ||
            !double.IsFinite(value) ||
            value < 0 ||
            (column == "SoNgayCong" && value > 31))
        {
            e.Cancel = true;

            MessageBox.Show(
                column == "SoNgayCong"
                    ? "Số ngày công phải từ 0 đến 31."
                    : "Số giờ OT phải không âm.",
                "Dữ liệu không hợp lệ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
    }

    // EndEdit chỉ đẩy giá trị xuống DataRowView; phải EndCurrentEdit thì RowState mới thành Modified.
    private void CommitAttendanceEdits()
    {
        attendanceGrid.EndEdit();

        if (attendanceTable is not null)
            attendanceGrid.BindingContext?[attendanceTable].EndCurrentEdit();
    }

    private string CurrentMonth() => $"{month.Value:00}/{year.Value:0000}";

    private static DataGridView CreateGrid() => new()
    {
        Dock = DockStyle.Fill,
        ReadOnly = false,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false,
        RowHeadersVisible = false
    };

    private static Label LabelFor(string text) =>
        new()
        {
            Text = text,
            AutoSize = true,
            Margin = new Padding(8, 8, 2, 0)
        };

    private static Button Button(string text, EventHandler click)
    {
        var button = new Button
        {
            Text = text,
            AutoSize = true,
            Margin = new Padding(5, 2, 2, 2)
        };

        button.Click += click;

        return button;
    }

    private static void ShowError(Exception exception) =>
        MessageBox.Show(
            exception.Message,
            "Lỗi",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
}
