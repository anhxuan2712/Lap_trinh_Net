
using Bai6._3.BUS;
using Bai6._3.DTO;
using System.Data;

namespace Bai6._3;

public partial class Form1 : Form
{
    private readonly ProjectTaskBus bus = new();
    private readonly ComboBox projectFilter = new();
    private readonly ComboBox statusFilter = new();
    private readonly DataGridView taskGrid = new();
    private readonly DataGridView overdueGrid = new();
    private readonly DataGridView reportGrid = new();
    private readonly TextBox projectName = new();
    private readonly TextBox taskName = new();
    private readonly TextBox taskDescription = new();
    private readonly DateTimePicker dueDate = new();
    private readonly ComboBox priority = new();
    private readonly ComboBox taskStatus = new();
    private readonly CheckedListBox members = new();
    private readonly Label connectionState = new();

    public Form1()
    {
        InitializeComponent();
        BuildInterface();
        LoadData();
    }

    private void BuildInterface()
    {
        Text = "Project & Task Management - Bai 6.3";
        Width = 1280;
        Height = 820;
        MinimumSize = new Size(1000, 650);
        StartPosition = FormStartPosition.CenterScreen;

        var tabs = new TabControl { Dock = DockStyle.Fill };

        var dashboard = new TabPage("Task board");
        var projects = new TabPage("Dự án & Task");
        var reports = new TabPage("Cảnh báo & Báo cáo");

        tabs.TabPages.AddRange([dashboard, projects, reports]);
        Controls.Add(tabs);

        projectFilter.Items.Add("Tất cả dự án");
        statusFilter.Items.AddRange(["Tất cả trạng thái", "To Do", "In Progress", "Review", "Done"]);

        projectFilter.DropDownStyle = statusFilter.DropDownStyle = ComboBoxStyle.DropDownList;

        projectFilter.SelectedIndexChanged += (_, _) => LoadTasks();
        statusFilter.SelectedIndexChanged += (_, _) => LoadTasks();

        var filters = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 48,
            Padding = new Padding(8)
        };

        filters.Controls.AddRange([
            new Label { Text = "Dự án:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) },
            projectFilter,
            new Label { Text = "Trạng thái:", AutoSize = true, Padding = new Padding(12, 7, 0, 0) },
            statusFilter
        ]);

        dashboard.Controls.Add(filters);

        ConfigureGrid(taskGrid);
        dashboard.Controls.Add(taskGrid);
        taskGrid.Dock = DockStyle.Fill;
        taskGrid.CellDoubleClick += TaskGrid_CellDoubleClick;

        var projectPanel = new GroupBox
        {
            Text = "Quản lý dự án",
            Dock = DockStyle.Top,
            Height = 105,
            Padding = new Padding(12, 24, 12, 12)
        };

        projectName.SetBounds(15, 42, 320, 28);

        var addProject = new Button { Text = "Thêm dự án" };
        addProject.SetBounds(350, 41, 110, 29);
        addProject.Click += AddProject_Click;

        var editProject = new Button { Text = "Sửa tên" };
        editProject.SetBounds(470, 41, 100, 29);
        editProject.Click += EditProject_Click;

        var deleteProject = new Button { Text = "Xóa dự án" };
        deleteProject.SetBounds(580, 41, 105, 29);
        deleteProject.Click += DeleteProject_Click;

        projectPanel.Controls.AddRange([
            new Label
            {
                Text = "Tên dự án mới / tên dự án cần thêm",
                AutoSize = true,
                Location = new Point(15, 22)
            },
            projectName,
            addProject,
            editProject,
            deleteProject
        ]);

        projects.Controls.Add(projectPanel);

        var taskPanel = new GroupBox
        {
            Text = "Tạo công việc và phân công",
            Dock = DockStyle.Top,
            Height = 245,
            Padding = new Padding(12, 24, 12, 12)
        };

        taskName.SetBounds(15, 48, 280, 28);
        taskDescription.SetBounds(310, 48, 300, 28);
        dueDate.SetBounds(625, 48, 175, 28);
        priority.SetBounds(815, 48, 130, 28);

        priority.Items.AddRange(["Cao", "Trung bình", "Thấp"]);
        priority.SelectedIndex = 1;

        taskStatus.SetBounds(960, 48, 145, 28);
        taskStatus.Items.AddRange(["To Do", "In Progress", "Review", "Done"]);
        taskStatus.SelectedIndex = 0;

        members.SetBounds(15, 105, 595, 92);
        members.CheckOnClick = true;

        var addTask = new Button
        {
            Text = "Tạo task",
            Width = 110,
            Height = 30
        };

        addTask.SetBounds(625, 110, 110, 30);
        addTask.Click += AddTask_Click;

        var refresh = new Button
        {
            Text = "Làm mới",
            Width = 110,
            Height = 30
        };

        refresh.SetBounds(745, 110, 110, 30);
        refresh.Click += (_, _) => LoadData();

        taskPanel.Controls.AddRange([
            new Label { Text = "Tên task", AutoSize = true, Location = new Point(15, 25) },
            new Label { Text = "Mô tả", AutoSize = true, Location = new Point(310, 25) },
            new Label { Text = "Hạn nộp", AutoSize = true, Location = new Point(625, 25) },
            new Label { Text = "Độ ưu tiên", AutoSize = true, Location = new Point(815, 25) },
            new Label { Text = "Trạng thái", AutoSize = true, Location = new Point(960, 25) },
            new Label { Text = "Chọn thành viên được phân công", AutoSize = true, Location = new Point(15, 85) },
            taskName,
            taskDescription,
            dueDate,
            priority,
            taskStatus,
            members,
            addTask,
            refresh
        ]);

        projects.Controls.Add(taskPanel);

        var hint = new Label
        {
            Text = "HƯỚNG DẪN: Nhấp đúp vào một dòng trên Task board để đổi trạng thái công việc.",
            Dock = DockStyle.Top,
            Height = 45,
            Padding = new Padding(12),
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.DarkBlue,
            BackColor = Color.AliceBlue
        };

        projects.Controls.Add(hint);
        hint.BringToFront();

        ConfigureGrid(overdueGrid);
        ConfigureGrid(reportGrid);

        var overdueButton = new Button
        {
            Text = "Tải task trễ hạn",
            Dock = DockStyle.Top,
            Height = 35
        };

        overdueButton.Click += (_, _) => LoadOverdue();

        var reportButton = new Button
        {
            Text = "Tải tỷ lệ hoàn thành theo thành viên",
            Dock = DockStyle.Top,
            Height = 35
        };

        reportButton.Click += (_, _) => LoadReport();

        reports.Controls.Add(reportGrid);
        reports.Controls.Add(reportButton);
        reports.Controls.Add(overdueGrid);
        reports.Controls.Add(overdueButton);

        reportGrid.Dock = overdueGrid.Dock = DockStyle.Fill;

        connectionState.Text = "MySQL: chưa kiểm tra";
        connectionState.Dock = DockStyle.Bottom;
        connectionState.Height = 25;
        connectionState.Padding = new Padding(8, 3, 0, 0);

        Controls.Add(connectionState);
    }

    private void EditProject_Click(object? sender, EventArgs e)
    {
        if (projectFilter.SelectedItem is not ProjectDto project ||
            string.IsNullOrWhiteSpace(projectName.Text))
        {
            MessageBox.Show("Chọn dự án và nhập tên mới.");
            return;
        }

        bus.UpdateProject(project.Id, projectName.Text.Trim());
        projectName.Clear();
        LoadProjects();
    }

    private void DeleteProject_Click(object? sender, EventArgs e)
    {
        if (projectFilter.SelectedItem is not ProjectDto project)
        {
            MessageBox.Show("Hãy chọn dự án cần xóa.");
            return;
        }

        if (MessageBox.Show(
            $"Xóa dự án '{project.Name}' và toàn bộ task?",
            "Xác nhận",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        bus.DeleteProject(project.Id);
        LoadProjects();
    }

    private static void ConfigureGrid(DataGridView grid)
    {
        grid.AutoGenerateColumns = true;
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }

    private void LoadData()
    {
        try
        {
            bus.EnsureConnection();
            bus.EnsureSampleMembers();
            connectionState.Text = "MySQL: đã kết nối";
            LoadProjects();
            LoadMembers();
            LoadTasks();
        }
        catch (Exception ex)
        {
            connectionState.Text = "MySQL: lỗi kết nối";

            MessageBox.Show(
                ex.Message,
                "Không thể kết nối MySQL",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void LoadProjects()
    {
        var projects = bus.GetProjects();

        projectFilter.Items.Clear();
        projectFilter.Items.Add("Tất cả dự án");

        foreach (var project in projects)
            projectFilter.Items.Add(project);

        projectFilter.SelectedIndex = 0;
    }

    private void LoadMembers()
    {
        members.Items.Clear();

        foreach (var member in bus.GetMembers())
            members.Items.Add(member, false);
    }

    private void LoadTasks()
    {
        if (statusFilter.Items.Count == 0)
            return;

        int? projectId =
            projectFilter.SelectedIndex > 0 &&
            projectFilter.SelectedItem is ProjectDto p
                ? p.Id
                : null;

        var status = statusFilter.SelectedIndex > 0
            ? statusFilter.SelectedItem?.ToString()
            : null;

        taskGrid.DataSource = bus.GetTasks(projectId, status).ToTable();
    }

    private void AddProject_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(projectName.Text))
        {
            MessageBox.Show("Tên dự án không được trống.");
            return;
        }

        bus.AddProject(new ProjectDto
        {
            Name = projectName.Text.Trim(),
            Status = "Đang chạy"
        });

        projectName.Clear();
        LoadProjects();
    }

    private void AddTask_Click(object? sender, EventArgs e)
    {
        if (projectFilter.SelectedIndex <= 0 ||
            projectFilter.SelectedItem is not ProjectDto project)
        {
            MessageBox.Show("Hãy chọn dự án ở bộ lọc phía trên.");
            return;
        }

        if (string.IsNullOrWhiteSpace(taskName.Text))
        {
            MessageBox.Show("Tên task không được trống.");
            return;
        }

        var memberIds = members.CheckedItems
            .OfType<MemberDto>()
            .Select(x => x.Id)
            .ToList();

        bus.AddTask(new TaskDto
        {
            ProjectId = project.Id,
            Name = taskName.Text.Trim(),
            Description = taskDescription.Text.Trim(),
            DueDate = dueDate.Value,
            Priority = priority.Text,
            Status = taskStatus.Text
        }, memberIds);

        taskName.Clear();
        taskDescription.Clear();

        LoadTasks();
    }

    private void TaskGrid_CellDoubleClick(
        object? sender,
        DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 ||
            taskGrid.Rows[e.RowIndex].Cells["Id"].Value is not int id)
            return;

        var statuses = new[]
        {
            "To Do",
            "In Progress",
            "Review",
            "Done"
        };

        var current = taskGrid.Rows[e.RowIndex]
            .Cells["Status"].Value?.ToString() ?? statuses[0];

        var next = statuses[
            (Array.IndexOf(statuses, current) + 1) % statuses.Length
        ];

        bus.UpdateTaskStatus(id, next);
        LoadTasks();
    }

    private void LoadOverdue()
    {
        overdueGrid.DataSource = bus.GetOverdueTasks().ToTable();
    }

    private void LoadReport()
    {
        reportGrid.DataSource = bus.GetCompletionReport().ToTable();
    }
}

internal static class DataTableExtensions
{
    public static DataTable ToTable<T>(this IEnumerable<T> items)
    {
        var table = new DataTable();
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
            table.Columns.Add(property.Name);

        foreach (var item in items)
        {
            table.Rows.Add(
                properties.Select(p => p.GetValue(item) ?? DBNull.Value).ToArray()
            );
        }

        return table;
    }
}
