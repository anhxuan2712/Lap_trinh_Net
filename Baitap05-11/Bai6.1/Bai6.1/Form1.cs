using System.Data;
using Bai6._1.BUS;
using Bai6._1.DAL;
using Bai6._1.DTO;

namespace Bai6._1;

public partial class Form1 : Form
{
    private readonly TextBox connectionStringBox = new() { Width = 590, Text = Environment.GetEnvironmentVariable("RETAIL_MYSQL_CONNECTION") ?? "Server=localhost;Database=SalesDB;User ID=root;Password=familo23;SslMode=None;AllowPublicKeyRetrieval=True;" };
    private readonly Button connectButton = new() { Text = "Kết nối MySQL", AutoSize = true };
    private readonly TabControl tabs = new() { Dock = DockStyle.Fill };
    private readonly DataGridView productsGrid = CreateGrid();
    private readonly DataGridView customersGrid = CreateGrid();
    private readonly DataGridView employeesGrid = CreateGrid();
    private readonly DataGridView cartGrid = CreateGrid();
    private readonly DataGridView topProductsGrid = CreateGrid();
    private readonly TextBox productSearchBox = new() { Width = 220 };
    private readonly TextBox productCodeBox = new() { Width = 150 };
    private readonly TextBox productNameBox = new() { Width = 220 };
    private readonly NumericUpDown productPriceInput = new() { Width = 120, DecimalPlaces = 2, Maximum = 1000000000, Minimum = 0.01M, Value = 1 };
    private readonly NumericUpDown productStockInput = new() { Width = 100, Maximum = 1000000000 };
    private readonly TextBox customerNameBox = new() { Width = 200 };
    private readonly TextBox customerPhoneBox = new() { Width = 140 };
    private readonly TextBox employeeNameBox = new() { Width = 200 };
    private readonly TextBox employeePhoneBox = new() { Width = 140 };
    private readonly TextBox employeeRoleBox = new() { Width = 140 };
    private readonly ComboBox employeeChoice = new() { Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly ComboBox customerChoice = new() { Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly TextBox saleProductCodeBox = new() { Width = 170 };
    private readonly NumericUpDown saleQuantityInput = new() { Width = 90, Minimum = 1, Maximum = 100000 };
    private readonly Label cartTotalLabel = new() { AutoSize = true, Text = "Tổng tiền: 0 đ", Font = new Font("Segoe UI", 12, FontStyle.Bold) };
    private readonly Label reportLabel = new() { AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
    private readonly List<CartItem> cart = [];
    private RetailService? service;
    private string? selectedProductCode;
    private int? selectedCustomerId;
    private int? selectedEmployeeId;

    public Form1()
    {
        InitializeComponent();
        Text = "Hệ thống quản lý bán hàng - MySQL";
        MinimumSize = new Size(1000, 650);
        Size = new Size(1200, 780);
        StartPosition = FormStartPosition.CenterScreen;
        BuildInterface();
    }

    private void BuildInterface()
    {
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(8) };
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var connectionPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, WrapContents = false };
        connectionPanel.Controls.Add(new Label { Text = "MySQL connection string:", AutoSize = true, Margin = new Padding(3, 8, 3, 3) });
        connectionPanel.Controls.Add(connectionStringBox);
        connectionPanel.Controls.Add(connectButton);
        root.Controls.Add(connectionPanel, 0, 0);
        root.Controls.Add(tabs, 0, 1);
        Controls.Add(root);

        tabs.TabPages.Add(BuildProductsTab());
        tabs.TabPages.Add(BuildPeopleTab(false));
        tabs.TabPages.Add(BuildPeopleTab(true));
        tabs.TabPages.Add(BuildSalesTab());
        tabs.TabPages.Add(BuildReportsTab());
        connectButton.Click += (_, _) => ConnectToDatabase();
    }

    private TabPage BuildProductsTab()
    {
        var page = new TabPage("Sản phẩm");
        var layout = CreatePageLayout();
        var searchPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        searchPanel.Controls.Add(new Label { Text = "Tìm theo mã hoặc tên:", AutoSize = true, Margin = new Padding(3, 8, 3, 3) });
        searchPanel.Controls.Add(productSearchBox);
        searchPanel.Controls.Add(MakeButton("Tìm", (_, _) => SearchProducts()));
        searchPanel.Controls.Add(MakeButton("Tất cả", (_, _) => LoadProducts()));
        var editor = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, WrapContents = true };
        AddEditorField(editor, "Mã / mã vạch", productCodeBox);
        AddEditorField(editor, "Tên sản phẩm", productNameBox);
        AddEditorField(editor, "Giá bán", productPriceInput);
        AddEditorField(editor, "Tồn kho", productStockInput);
        editor.Controls.Add(MakeButton("Thêm", (_, _) => SaveProduct(false)));
        editor.Controls.Add(MakeButton("Cập nhật", (_, _) => SaveProduct(true)));
        editor.Controls.Add(MakeButton("Xóa", (_, _) => DeleteProduct()));
        editor.Controls.Add(MakeButton("Làm mới", (_, _) => LoadProducts()));
        layout.Controls.Add(searchPanel, 0, 0);
        layout.Controls.Add(editor, 0, 1);
        layout.Controls.Add(productsGrid, 0, 2);
        productsGrid.SelectionChanged += (_, _) => SelectProductRow();
        page.Controls.Add(layout);
        return page;
    }

    private TabPage BuildPeopleTab(bool employees)
    {
        var page = new TabPage(employees ? "Nhân viên" : "Khách hàng");
        var layout = CreatePageLayout();
        var editor = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, WrapContents = true };
        if (employees)
        {
            AddEditorField(editor, "Tên nhân viên", employeeNameBox);
            AddEditorField(editor, "Điện thoại", employeePhoneBox);
            AddEditorField(editor, "Chức vụ", employeeRoleBox);
        }
        else
        {
            AddEditorField(editor, "Tên khách hàng", customerNameBox);
            AddEditorField(editor, "Điện thoại", customerPhoneBox);
        }
        editor.Controls.Add(MakeButton("Thêm", (_, _) => SavePerson(employees, false)));
        editor.Controls.Add(MakeButton("Cập nhật", (_, _) => SavePerson(employees, true)));
        editor.Controls.Add(MakeButton("Xóa", (_, _) => DeletePerson(employees)));
        editor.Controls.Add(MakeButton("Làm mới", (_, _) => LoadPeople(employees)));
        var grid = employees ? employeesGrid : customersGrid;
        grid.SelectionChanged += (_, _) => SelectPersonRow(employees);
        layout.Controls.Add(editor, 0, 0);
        layout.Controls.Add(grid, 0, 1);
        layout.RowStyles.Clear();
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        page.Controls.Add(layout);
        return page;
    }

    private TabPage BuildSalesTab()
    {
        var page = new TabPage("Bán hàng / Hóa đơn");
        var layout = CreatePageLayout();
        var choices = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, WrapContents = true };
        AddEditorField(choices, "Nhân viên", employeeChoice);
        AddEditorField(choices, "Khách hàng", customerChoice);
        var addRow = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        AddEditorField(addRow, "Mã sản phẩm", saleProductCodeBox);
        AddEditorField(addRow, "Số lượng", saleQuantityInput);
        addRow.Controls.Add(MakeButton("Thêm vào giỏ", (_, _) => AddToCart()));
        addRow.Controls.Add(MakeButton("Xóa dòng chọn", (_, _) => RemoveCartItem()));
        var bottom = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, WrapContents = false };
        bottom.Controls.Add(cartTotalLabel);
        bottom.Controls.Add(MakeButton("Thanh toán", (_, _) => Checkout()));
        layout.Controls.Add(choices, 0, 0);
        layout.Controls.Add(addRow, 0, 1);
        layout.Controls.Add(cartGrid, 0, 2);
        layout.Controls.Add(bottom, 0, 3);
        layout.RowStyles.Clear();
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        RefreshCartGrid();
        page.Controls.Add(layout);
        return page;
    }

    private TabPage BuildReportsTab()
    {
        var page = new TabPage("Báo cáo - Thống kê");
        var layout = CreatePageLayout();
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        actions.Controls.Add(MakeButton("Doanh thu hôm nay", (_, _) => ShowTodayRevenue()));
        actions.Controls.Add(MakeButton("Doanh thu tháng này", (_, _) => ShowMonthRevenue()));
        actions.Controls.Add(MakeButton("Top 5 sản phẩm bán chạy", (_, _) => LoadTopProducts()));
        layout.Controls.Add(actions, 0, 0);
        layout.Controls.Add(reportLabel, 0, 1);
        layout.Controls.Add(topProductsGrid, 0, 2);
        page.Controls.Add(layout);
        return page;
    }

    private void ConnectToDatabase()
    {
        try
        {
            var candidate = new RetailService(new RetailRepository(connectionStringBox.Text.Trim()));
            candidate.TestConnection();
            service = candidate;
            RefreshAll();
            MessageBox.Show("Kết nối MySQL thành công.", "MySQL", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
            service = null;
            ShowError(exception);
        }
    }

    private void RefreshAll()
    {
        LoadProducts();
        LoadPeople(false);
        LoadPeople(true);
        LoadChoices();
        LoadTopProducts();
    }

    private void LoadProducts() => RunWithService(current => productsGrid.DataSource = current.Products());

    private void SearchProducts()
    {
        if (string.IsNullOrWhiteSpace(productSearchBox.Text)) { LoadProducts(); return; }
        RunWithService(current => productsGrid.DataSource = current.SearchProducts(productSearchBox.Text));
    }

    private void LoadPeople(bool employees)
    {
        RunWithService(current => (employees ? employeesGrid : customersGrid).DataSource = employees ? current.Employees() : current.Customers());
    }

    private void LoadChoices()
    {
        RunWithService(current =>
        {
            employeeChoice.DataSource = current.EmployeeChoices();
            employeeChoice.DisplayMember = "TenNV";
            employeeChoice.ValueMember = "MaNV";
            var customers = current.CustomerChoices();
            var guest = customers.NewRow();
            guest["MaKH"] = DBNull.Value;
            guest["TenKH"] = "Khách lẻ";
            customers.Rows.InsertAt(guest, 0);
            customerChoice.DataSource = customers;
            customerChoice.DisplayMember = "TenKH";
            customerChoice.ValueMember = "MaKH";
        });
    }

    private void SaveProduct(bool update)
    {
        RunWithService(current =>
        {
            if (update && selectedProductCode is null) throw new InvalidOperationException("Hãy chọn sản phẩm cần cập nhật.");
            if (update && !string.Equals(selectedProductCode, productCodeBox.Text.Trim(), StringComparison.Ordinal))
                throw new InvalidOperationException("Không thể thay đổi mã sản phẩm. Hãy giữ nguyên mã đã chọn.");
            if (update) current.UpdateProduct(productCodeBox.Text, productNameBox.Text, productPriceInput.Value, (int)productStockInput.Value);
            else current.AddProduct(productCodeBox.Text, productNameBox.Text, productPriceInput.Value, (int)productStockInput.Value);
            LoadProducts();
            ResetProductEditor();
        });
    }

    private void DeleteProduct()
    {
        if (selectedProductCode is null) { ShowError(new InvalidOperationException("Hãy chọn sản phẩm cần xóa.")); return; }
        if (MessageBox.Show("Xóa sản phẩm đang chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        RunWithService(current => { current.DeleteProduct(selectedProductCode); LoadProducts(); ResetProductEditor(); });
    }

    private void SelectProductRow()
    {
        if (productsGrid.CurrentRow is null || productsGrid.CurrentRow.IsNewRow) return;
        var row = productsGrid.CurrentRow;
        if (row.Cells.Count < 4) return;
        selectedProductCode = Convert.ToString(row.Cells[0].Value);
        productCodeBox.Text = selectedProductCode;
        productNameBox.Text = Convert.ToString(row.Cells[1].Value);
        if (decimal.TryParse(Convert.ToString(row.Cells[2].Value), out var price)) productPriceInput.Value = Math.Clamp(price, productPriceInput.Minimum, productPriceInput.Maximum);
        if (int.TryParse(Convert.ToString(row.Cells[3].Value), out var stock)) productStockInput.Value = Math.Clamp(stock, (int)productStockInput.Minimum, (int)productStockInput.Maximum);
    }

    private void ResetProductEditor()
    {
        selectedProductCode = null;
        productCodeBox.Clear();
        productNameBox.Clear();
        productPriceInput.Value = 1;
        productStockInput.Value = 0;
    }

    private void SavePerson(bool employees, bool update)
    {
        RunWithService(current =>
        {
            if (employees)
            {
                if (update && selectedEmployeeId is null) throw new InvalidOperationException("Hãy chọn nhân viên cần cập nhật.");
                if (update) current.UpdateEmployee(selectedEmployeeId!.Value, employeeNameBox.Text, employeePhoneBox.Text, employeeRoleBox.Text);
                else current.AddEmployee(employeeNameBox.Text, employeePhoneBox.Text, employeeRoleBox.Text);
            }
            else
            {
                if (update && selectedCustomerId is null) throw new InvalidOperationException("Hãy chọn khách hàng cần cập nhật.");
                if (update) current.UpdateCustomer(selectedCustomerId!.Value, customerNameBox.Text, customerPhoneBox.Text);
                else current.AddCustomer(customerNameBox.Text, customerPhoneBox.Text);
            }
            LoadPeople(employees);
            LoadChoices();
            if (employees) { selectedEmployeeId = null; employeeNameBox.Clear(); employeePhoneBox.Clear(); employeeRoleBox.Clear(); }
            else { selectedCustomerId = null; customerNameBox.Clear(); customerPhoneBox.Clear(); }
        });
    }

    private void DeletePerson(bool employees)
    {
        var id = employees ? selectedEmployeeId : selectedCustomerId;
        if (id is null) { ShowError(new InvalidOperationException(employees ? "Hãy chọn nhân viên cần xóa." : "Hãy chọn khách hàng cần xóa.")); return; }
        if (MessageBox.Show("Xóa bản ghi đang chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        RunWithService(current =>
        {
            if (employees) current.DeleteEmployee(id.Value); else current.DeleteCustomer(id.Value);
            LoadPeople(employees);
            LoadChoices();
            if (employees) selectedEmployeeId = null; else selectedCustomerId = null;
        });
    }

    private void SelectPersonRow(bool employees)
    {
        var grid = employees ? employeesGrid : customersGrid;
        if (grid.CurrentRow is null || grid.CurrentRow.IsNewRow) return;
        var row = grid.CurrentRow;
        if (employees)
        {
            selectedEmployeeId = Convert.ToInt32(row.Cells[0].Value);
            employeeNameBox.Text = Convert.ToString(row.Cells[1].Value);
            employeePhoneBox.Text = Convert.ToString(row.Cells[2].Value);
            employeeRoleBox.Text = Convert.ToString(row.Cells[3].Value);
        }
        else
        {
            selectedCustomerId = Convert.ToInt32(row.Cells[0].Value);
            customerNameBox.Text = Convert.ToString(row.Cells[1].Value);
            customerPhoneBox.Text = Convert.ToString(row.Cells[2].Value);
        }
    }

    private void AddToCart()
    {
        RunWithService(current =>
        {
            var code = saleProductCodeBox.Text.Trim();
            var product = current.FindProduct(code) ?? throw new InvalidOperationException("Không tìm thấy sản phẩm theo mã đã nhập.");
            var quantity = (int)saleQuantityInput.Value;
            var existing = cart.FindIndex(item => item.ProductCode == product.Code);
            var nextQuantity = quantity + (existing >= 0 ? cart[existing].Quantity : 0);
            if (nextQuantity > product.Stock) throw new InvalidOperationException($"Sản phẩm chỉ còn {product.Stock} trong kho.");
            if (existing >= 0) cart[existing] = new CartItem(product.Code, product.Name, nextQuantity, product.Price);
            else cart.Add(new CartItem(product.Code, product.Name, quantity, product.Price));
            saleProductCodeBox.Clear();
            saleQuantityInput.Value = 1;
            RefreshCartGrid();
        });
    }

    private void RemoveCartItem()
    {
        if (cartGrid.CurrentRow is null || cartGrid.CurrentRow.IsNewRow) return;
        var code = Convert.ToString(cartGrid.CurrentRow.Cells[0].Value);
        cart.RemoveAll(item => item.ProductCode == code);
        RefreshCartGrid();
    }

    private void RefreshCartGrid()
    {
        var table = new DataTable();
        table.Columns.Add("Mã sản phẩm");
        table.Columns.Add("Tên sản phẩm");
        table.Columns.Add("Số lượng", typeof(int));
        table.Columns.Add("Đơn giá", typeof(decimal));
        table.Columns.Add("Thành tiền", typeof(decimal));
        foreach (var item in cart) table.Rows.Add(item.ProductCode, item.ProductName, item.Quantity, item.UnitPrice, item.LineTotal);
        cartGrid.DataSource = table;
        cartTotalLabel.Text = $"Tổng tiền: {cart.Sum(item => item.LineTotal):N0} đ";
    }

    private void Checkout()
    {
        RunWithService(current =>
        {
            if (employeeChoice.SelectedValue is null || employeeChoice.SelectedValue == DBNull.Value) throw new InvalidOperationException("Chưa có nhân viên. Hãy thêm nhân viên trước khi bán hàng.");
            var employeeId = Convert.ToInt32(employeeChoice.SelectedValue);
            int? customerId = customerChoice.SelectedValue is null || customerChoice.SelectedValue == DBNull.Value ? null : Convert.ToInt32(customerChoice.SelectedValue);
            var invoiceId = current.Checkout(employeeId, customerId, cart);
            cart.Clear();
            RefreshCartGrid();
            LoadProducts();
            MessageBox.Show($"Thanh toán thành công. Mã hóa đơn: {invoiceId}", "Hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
        });
    }

    private void ShowTodayRevenue() => RunWithService(current => reportLabel.Text = $"Doanh thu hôm nay: {current.TodayRevenue(DateTime.Today):N0} đ");
    private void ShowMonthRevenue() => RunWithService(current => reportLabel.Text = $"Doanh thu tháng này: {current.MonthRevenue(DateTime.Today):N0} đ");
    private void LoadTopProducts() => RunWithService(current => topProductsGrid.DataSource = current.TopProducts());

    private void RunWithService(Action<RetailService> action)
    {
        if (service is null) { ShowError(new InvalidOperationException("Hãy kết nối MySQL trước. Kiểm tra connection string và chạy script SalesDB.sql.")); return; }
        try { action(service); }
        catch (Exception exception) { ShowError(exception); }
    }

    private static void ShowError(Exception exception) => MessageBox.Show(exception.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

    private static TableLayoutPanel CreatePageLayout()
    {
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(8) };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        return layout;
    }

    private static DataGridView CreateGrid() => new()
    {
        Dock = DockStyle.Fill,
        ReadOnly = true,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false,
        RowHeadersVisible = false
    };

    private static Button MakeButton(string text, EventHandler handler)
    {
        var button = new Button { Text = text, AutoSize = true, Margin = new Padding(5) };
        button.Click += handler;
        return button;
    }

    private static void AddEditorField(FlowLayoutPanel panel, string label, Control input)
    {
        var field = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, AutoSize = true, Margin = new Padding(4) };
        field.Controls.Add(new Label { Text = label, AutoSize = true });
        field.Controls.Add(input);
        panel.Controls.Add(field);
    }
}
