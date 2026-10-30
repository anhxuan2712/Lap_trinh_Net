
using System.Data;

namespace Bai5._3;

public partial class Form1 : Form
{
    private readonly CafeRepository repository = new();
    private readonly FlowLayoutPanel tablePanel = new() { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(8) };
    private readonly DataGridView orderGrid = new() { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
    private readonly DataGridView menuGrid = new() { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
    private readonly ComboBox menuCombo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 260 };
    private readonly NumericUpDown quantityBox = new() { Minimum = 1, Maximum = 999, Value = 1, Width = 70 };
    private readonly ComboBox categoryCombo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 180 };
    private readonly TextBox menuNameBox = new() { Width = 220 };
    private readonly NumericUpDown priceBox = new() { Minimum = 0, Maximum = 1000000000, DecimalPlaces = 2, Increment = 1000, Width = 140 };
    private readonly Label selectedTableLabel = new() { AutoSize = true, Text = "Chưa chọn bàn", Font = new Font("Segoe UI", 11, FontStyle.Bold) };
    private readonly Label totalLabel = new() { AutoSize = true, Text = "Tổng: 0 đ", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.DarkGreen };
    private readonly Label statusLabel = new() { AutoSize = true, ForeColor = Color.DarkSlateGray };
    private int selectedTableId;
    private int editingMenuId;
    private readonly ComboBox targetTableCombo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 100 };

    public Form1()
    {
        InitializeComponent();
        Text = "Cafe POS - Bài tập 5.3";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1100, 700);
        BuildInterface();
        Shown += (_, _) => RefreshData();
    }

    private void BuildInterface()
    {
        var tabs = new TabControl { Dock = DockStyle.Fill };
        var posPage = new TabPage("Bán hàng");
        var menuPage = new TabPage("Quản lý thực đơn");

        tabs.TabPages.Add(posPage);
        tabs.TabPages.Add(menuPage);
        Controls.Add(tabs);

        var posSplit = new SplitContainer
        {
            Dock = DockStyle.Fill,
            SplitterDistance = 380,
            FixedPanel = FixedPanel.Panel1
        };

        posPage.Controls.Add(posSplit);
        posSplit.Panel1.Controls.Add(tablePanel);

        var orderLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 4,
            ColumnCount = 1,
            Padding = new Padding(8)
        };

        orderLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
        orderLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        orderLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        orderLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));

        posSplit.Panel2.Controls.Add(orderLayout);

        var header = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight
        };

        header.Controls.Add(selectedTableLabel);
        header.Controls.Add(new Label { Width = 35 });
        header.Controls.Add(totalLabel);
        orderLayout.Controls.Add(header, 0, 0);

        var addPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false
        };

        addPanel.Controls.Add(new Label
        {
            Text = "Món:",
            AutoSize = true,
            Margin = new Padding(0, 8, 4, 0)
        });

        addPanel.Controls.Add(menuCombo);

        addPanel.Controls.Add(new Label
        {
            Text = "SL:",
            AutoSize = true,
            Margin = new Padding(12, 8, 4, 0)
        });

        addPanel.Controls.Add(quantityBox);

        var addButton = new Button { Text = "Thêm món", AutoSize = true };
        addButton.Click += AddItemClicked;
        addPanel.Controls.Add(addButton);

        orderLayout.Controls.Add(addPanel, 0, 1);
        orderLayout.Controls.Add(orderGrid, 0, 2);

        var actions = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight
        };

        var payButton = new Button
        {
            Text = "Thanh toán",
            AutoSize = true,
            BackColor = Color.LightGreen
        };

        payButton.Click += PayClicked;
        actions.Controls.Add(payButton);

        actions.Controls.Add(new Label
        {
            Text = "Đổi sang:",
            AutoSize = true,
            Margin = new Padding(12, 8, 4, 0)
        });

        actions.Controls.Add(targetTableCombo);

        var moveButton = new Button { Text = "Đổi bàn", AutoSize = true };
        moveButton.Click += MoveTableClicked;
        actions.Controls.Add(moveButton);
        actions.Controls.Add(statusLabel);

        orderLayout.Controls.Add(actions, 0, 3);

        var menuSplit = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal,
            SplitterDistance = 360
        };

        menuPage.Controls.Add(menuSplit);
        menuSplit.Panel1.Controls.Add(menuGrid);

        var editor = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10),
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true
        };

        menuSplit.Panel2.Controls.Add(editor);

        editor.Controls.Add(new Label
        {
            Text = "Tên món",
            AutoSize = true,
            Margin = new Padding(0, 8, 4, 0)
        });

        editor.Controls.Add(menuNameBox);

        editor.Controls.Add(new Label
        {
            Text = "Danh mục",
            AutoSize = true,
            Margin = new Padding(15, 8, 4, 0)
        });

        editor.Controls.Add(categoryCombo);

        editor.Controls.Add(new Label
        {
            Text = "Đơn giá",
            AutoSize = true,
            Margin = new Padding(15, 8, 4, 0)
        });

        editor.Controls.Add(priceBox);

        var saveButton = new Button
        {
            Text = "Thêm / Cập nhật",
            AutoSize = true
        };

        saveButton.Click += SaveMenuClicked;
        editor.Controls.Add(saveButton);

        var deleteButton = new Button
        {
            Text = "Xóa món",
            AutoSize = true,
            BackColor = Color.MistyRose
        };

        deleteButton.Click += DeleteMenuClicked;
        editor.Controls.Add(deleteButton);

        var clearButton = new Button
        {
            Text = "Làm mới",
            AutoSize = true
        };

        clearButton.Click += (_, _) => ClearMenuEditor();
        editor.Controls.Add(clearButton);

        menuGrid.SelectionChanged += MenuGridSelectionChanged;
    }

    private void RefreshData()
    {
        try
        {
            var categories = repository.LoadCategories();

            categoryCombo.DataSource = categories;
            categoryCombo.DisplayMember = "TenDanhMuc";
            categoryCombo.ValueMember = "MaDanhMuc";

            var menu = repository.LoadMenu();

            menuGrid.DataSource = menu;

            menuCombo.DataSource = menu.Copy();
            menuCombo.DisplayMember = "TenMon";
            menuCombo.ValueMember = "MaMon";

            menuCombo.Format += (_, e) =>
            {
                if (e.ListItem is DataRowView row)
                {
                    e.Value = $"{row["TenMon"]} - {Convert.ToDecimal(row["DonGia"]):N0} đ";
                }
            };

            RefreshTables();
            statusLabel.Text = "Đã kết nối MySQL";
        }
        catch (Exception ex)
        {
            statusLabel.Text = "Chưa kết nối được CSDL";

            MessageBox.Show(
                $"Không thể nạp dữ liệu MySQL. Hãy chạy CafePOSDB.sql và kiểm tra chuỗi kết nối.\n\n{ex.Message}",
                "Lỗi kết nối",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }

    private void RefreshTables()
    {
        tablePanel.Controls.Clear();

        var tables = repository.LoadTables();

        foreach (DataRow row in tables.Rows)
        {
            var id = Convert.ToInt32(row["MaBan"]);
            var status = Convert.ToString(row["TrangThai"]) ?? "Trống";

            var button = new Button
            {
                Text = $"{row["TenBan"]}\n{status}",
                Tag = id,
                Width = 150,
                Height = 85,
                Margin = new Padding(8),
                BackColor = status == "Có khách" ? Color.LightCoral : Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };

            button.Click += TableClicked;
            tablePanel.Controls.Add(button);
        }

        if (selectedTableId != 0 &&
            tables.Select($"MaBan = {selectedTableId}").Length > 0)
        {
            LoadOrder(selectedTableId);
        }

        var targets = tables.Clone();

        foreach (DataRow row in tables.Rows)
        {
            if (Convert.ToInt32(row["MaBan"]) != selectedTableId &&
                Convert.ToString(row["TrangThai"]) == "Trống")
            {
                targets.ImportRow(row);
            }
        }

        targetTableCombo.DataSource = targets;
        targetTableCombo.DisplayMember = "TenBan";
        targetTableCombo.ValueMember = "MaBan";
    }

    private void TableClicked(object? sender, EventArgs e)
    {
        if (sender is not Button button) return;

        selectedTableId = Convert.ToInt32(button.Tag);
        selectedTableLabel.Text = button.Text.Split('\n')[0];

        LoadOrder(selectedTableId);
    }

    private void LoadOrder(int tableId)
    {
        var order = repository.LoadOrder(tableId);

        orderGrid.DataSource = order;

        if (orderGrid.Columns.Contains("MaMon"))
            orderGrid.Columns["MaMon"].Visible = false;

        if (orderGrid.Columns.Contains("DonGia"))
            orderGrid.Columns["DonGia"].DefaultCellStyle.Format = "N0";

        if (orderGrid.Columns.Contains("ThanhTien"))
            orderGrid.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

        var total = order.Rows.Cast<DataRow>()
            .Sum(row => Convert.ToDecimal(row["ThanhTien"]));

        totalLabel.Text = $"Tổng: {total:N0} đ";
    }

    private void AddItemClicked(object? sender, EventArgs e)
    {
        if (selectedTableId == 0)
        {
            MessageBox.Show("Vui lòng chọn bàn.");
            return;
        }

        if (menuCombo.SelectedValue is null)
        {
            MessageBox.Show("Vui lòng chọn món.");
            return;
        }

        try
        {
            repository.AddItem(
                selectedTableId,
                Convert.ToInt32(menuCombo.SelectedValue),
                Convert.ToInt32(quantityBox.Value)
            );

            LoadOrder(selectedTableId);
            RefreshTables();
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void PayClicked(object? sender, EventArgs e)
    {
        if (selectedTableId == 0)
        {
            MessageBox.Show("Vui lòng chọn bàn.");
            return;
        }

        if (MessageBox.Show(
            "Xác nhận thanh toán hóa đơn của bàn này?",
            "Thanh toán",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        try
        {
            repository.Pay(selectedTableId);

            LoadOrder(selectedTableId);
            RefreshTables();

            MessageBox.Show(
                "Thanh toán thành công.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void MoveTableClicked(object? sender, EventArgs e)
    {
        if (selectedTableId == 0)
        {
            MessageBox.Show("Vui lòng chọn bàn hiện tại.");
            return;
        }

        if (targetTableCombo.SelectedValue is null)
        {
            MessageBox.Show("Không có bàn trống để đổi.");
            return;
        }

        try
        {
            repository.MoveTable(
                selectedTableId,
                Convert.ToInt32(targetTableCombo.SelectedValue)
            );

            selectedTableId = 0;
            selectedTableLabel.Text = "Chưa chọn bàn";
            orderGrid.DataSource = null;
            totalLabel.Text = "Tổng: 0 đ";

            RefreshTables();
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void MenuGridSelectionChanged(object? sender, EventArgs e)
    {
        if (menuGrid.CurrentRow?.DataBoundItem is not DataRowView row)
            return;

        editingMenuId = Convert.ToInt32(row["MaMon"]);

        menuNameBox.Text = Convert.ToString(row["TenMon"]) ?? "";

        categoryCombo.SelectedValue = Convert.ToInt32(row["MaDanhMuc"]);

        priceBox.Value = Math.Min(
            priceBox.Maximum,
            Math.Max(priceBox.Minimum, Convert.ToDecimal(row["DonGia"]))
        );
    }

    private void SaveMenuClicked(object? sender, EventArgs e)
    {
        var name = menuNameBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Tên món không được để trống.");
            menuNameBox.Focus();
            return;
        }

        if (priceBox.Value < 0)
        {
            MessageBox.Show("Đơn giá không được âm.");
            return;
        }

        if (categoryCombo.SelectedValue is null)
        {
            MessageBox.Show("Vui lòng chọn danh mục.");
            return;
        }

        try
        {
            repository.SaveMenu(
                editingMenuId,
                name,
                Convert.ToInt32(categoryCombo.SelectedValue),
                priceBox.Value
            );

            ClearMenuEditor();
            RefreshData();
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void DeleteMenuClicked(object? sender, EventArgs e)
    {
        if (editingMenuId == 0)
        {
            MessageBox.Show("Hãy chọn món cần xóa.");
            return;
        }

        if (MessageBox.Show(
            "Xóa món đã chọn?",
            "Xác nhận",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning) != DialogResult.Yes)
        {
            return;
        }

        try
        {
            repository.DeleteMenu(editingMenuId);

            ClearMenuEditor();
            RefreshData();
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void ClearMenuEditor()
    {
        editingMenuId = 0;
        menuNameBox.Clear();
        priceBox.Value = 0;
        menuGrid.ClearSelection();
    }

    private static void ShowError(Exception ex)
    {
        MessageBox.Show(
            ex.Message,
            "Lỗi",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
    }
}
