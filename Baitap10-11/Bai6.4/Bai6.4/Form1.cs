using System.Globalization;
using Bai6._4.DAL;

namespace Bai6._4;

public partial class Form1 : Form
{
    private readonly GarageRepository _repository = new();
    private readonly TabControl _tabs = new();
    private readonly DataGridView _customerGrid = NewGrid();
    private readonly DataGridView _vehicleGrid = NewGrid();
    private readonly DataGridView _partGrid = NewGrid();
    private readonly DataGridView _repairPartsGrid = NewGrid();
    private readonly DataGridView _invoiceGrid = NewGrid();

    private readonly TextBox _customerId = new() { ReadOnly = true, Width = 70 };
    private readonly TextBox _customerName = new() { Width = 180, MaxLength = 100 };
    private readonly TextBox _customerPhone = new() { Width = 130, MaxLength = 15 };
    private readonly TextBox _customerAddress = new() { Width = 220, MaxLength = 200 };
    private readonly TextBox _plate = new() { Width = 130, MaxLength = 20 };
    private readonly TextBox _make = new() { Width = 130, MaxLength = 50 };
    private readonly TextBox _year = new() { Width = 80, MaxLength = 4 };
    private readonly ComboBox _vehicleCustomer = new() { Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly TextBox _partId = new() { ReadOnly = true, Width = 70 };
    private readonly TextBox _partName = new() { Width = 180, MaxLength = 150 };
    private readonly TextBox _partPrice = new() { Width = 110 };
    private readonly TextBox _partStock = new() { Width = 80 };
    private readonly ComboBox _repairVehicle = new() { Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly TextBox _repairDescription = new() { Width = 240, MaxLength = 1000 };
    private readonly TextBox _labor = new() { Width = 100, Text = "0" };

    public Form1()
    {
        InitializeComponent();
        Text = "Garage Management System - Quản lý sửa chữa ô tô";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1000, 650);
        Size = new Size(1250, 800);
        BuildInterface();
        Shown += (_, _) => RunSafely(RefreshAll);
    }

    private void BuildInterface()
    {
        _tabs.Dock = DockStyle.Fill;
        Controls.Add(_tabs);

        var customerPage = CreatePage("Chủ xe", _customerGrid, out var customerToolbar);
        AddField(customerToolbar, "Mã KH", _customerId);
        AddField(customerToolbar, "Tên chủ xe", _customerName);
        AddField(customerToolbar, "Điện thoại", _customerPhone);
        AddField(customerToolbar, "Địa chỉ", _customerAddress);
        customerToolbar.Controls.Add(CreateButton("Lưu chủ xe", SaveCustomer));
        customerToolbar.Controls.Add(CreateButton("Mới", ClearCustomer));
        customerToolbar.Controls.Add(CreateButton("Xóa chủ xe", DeleteCustomer));
        customerPage.Controls.Add(customerToolbar);
        _customerGrid.SelectionChanged += (_, _) => ShowSelectedCustomer();
        _tabs.TabPages.Add(customerPage);

        var vehiclePage = CreatePage("Tiếp nhận xe", _vehicleGrid, out var vehicleToolbar);
        AddField(vehicleToolbar, "Biển số", _plate);
        AddField(vehicleToolbar, "Chủ xe", _vehicleCustomer);
        AddField(vehicleToolbar, "Hiệu xe", _make);
        AddField(vehicleToolbar, "Năm SX", _year);
        vehicleToolbar.Controls.Add(CreateButton("Tra cứu biển số", SearchVehicle));
        vehicleToolbar.Controls.Add(CreateButton("Lưu xe", SaveVehicle));
        vehicleToolbar.Controls.Add(CreateButton("Xóa xe", DeleteVehicle));
        _vehicleGrid.SelectionChanged += (_, _) => ShowSelectedVehicle();
        _tabs.TabPages.Add(vehiclePage);

        var partPage = CreatePage("Kho phụ tùng", _partGrid, out var partToolbar);
        AddField(partToolbar, "Mã phụ tùng", _partId);
        AddField(partToolbar, "Tên phụ tùng", _partName);
        AddField(partToolbar, "Đơn giá", _partPrice);
        AddField(partToolbar, "Tồn kho", _partStock);
        partToolbar.Controls.Add(CreateButton("Lưu phụ tùng", SavePart));
        partToolbar.Controls.Add(CreateButton("Mới", ClearPart));
        partToolbar.Controls.Add(CreateButton("Xóa phụ tùng", DeletePart));
        _partGrid.SelectionChanged += (_, _) => ShowSelectedPart();
        _tabs.TabPages.Add(partPage);

        var repairPage = new TabPage("Phiếu sửa chữa / Thanh toán");
        var repairToolbar = NewToolbar();
        AddField(repairToolbar, "Xe", _repairVehicle);
        AddField(repairToolbar, "Nội dung sửa", _repairDescription);
        AddField(repairToolbar, "Tiền công", _labor);
        repairToolbar.Controls.Add(CreateButton("Lập phiếu & trừ kho", CreateRepair));
        repairToolbar.Controls.Add(CreateButton("Thanh toán phiếu chọn", PaySelectedInvoice));
        var repairSplit = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal,
            SplitterDistance = 300,
            Panel1MinSize = 140,
            Panel2MinSize = 140
        };
        ConfigureRepairPartsGrid();
        repairSplit.Panel1.Controls.Add(_repairPartsGrid);
        repairSplit.Panel2.Controls.Add(_invoiceGrid);
        repairPage.Controls.Add(repairSplit);
        repairPage.Controls.Add(repairToolbar);
        repairPage.Padding = new Padding(6);
        _tabs.TabPages.Add(repairPage);
    }

    private TabPage CreatePage(string title, DataGridView grid, out FlowLayoutPanel toolbar)
    {
        var page = new TabPage(title) { Padding = new Padding(6) };
        toolbar = NewToolbar();
        page.Controls.Add(grid);
        page.Controls.Add(toolbar);
        return page;
    }

    private static FlowLayoutPanel NewToolbar() => new()
    {
        Dock = DockStyle.Top,
        AutoSize = true,
        AutoSizeMode = AutoSizeMode.GrowAndShrink,
        FlowDirection = FlowDirection.LeftToRight,
        WrapContents = true,
        Padding = new Padding(4),
        Margin = new Padding(0)
    };

    private static DataGridView NewGrid() => new()
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

    private static void AddField(FlowLayoutPanel toolbar, string label, Control control)
    {
        var field = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Margin = new Padding(4)
        };
        field.Controls.Add(new Label { Text = label, AutoSize = true, Margin = new Padding(0, 0, 0, 3) });
        field.Controls.Add(control);
        toolbar.Controls.Add(field);
    }

    private static Button CreateButton(string text, Action action)
    {
        var button = new Button { Text = text, AutoSize = true, Height = 32, Margin = new Padding(6, 22, 4, 4) };
        button.Click += (_, _) => RunSafely(action);
        return button;
    }

    private static void RunSafely(Action action)
    {
        try
        {
            action();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Không thể hoàn tất thao tác", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void RefreshAll()
    {
        _repository.TestConnection();
        RefreshCustomers();
        RefreshVehicles();
        RefreshParts();
        RefreshInvoices();
    }

    private void RefreshCustomers()
    {
        var customers = _repository.GetCustomers();
        _customerGrid.DataSource = customers;
        _customerGrid.ClearSelection();
        _vehicleCustomer.DataSource = customers;
        _vehicleCustomer.DisplayMember = nameof(CustomerDto.DisplayName);
        _vehicleCustomer.ValueMember = nameof(CustomerDto.Id);
        _vehicleCustomer.SelectedIndex = customers.Count == 0 ? -1 : 0;
    }

    private void RefreshVehicles()
    {
        var vehicles = _repository.GetVehicles();
        _vehicleGrid.DataSource = vehicles;
        _vehicleGrid.ClearSelection();
        _repairVehicle.DataSource = vehicles;
        _repairVehicle.DisplayMember = nameof(VehicleDto.Plate);
        _repairVehicle.ValueMember = nameof(VehicleDto.Plate);
        _repairVehicle.SelectedIndex = vehicles.Count == 0 ? -1 : 0;
    }

    private void RefreshParts()
    {
        var parts = _repository.GetParts();
        _partGrid.DataSource = parts;
        _partGrid.ClearSelection();
        _repairPartsGrid.Rows.Clear();
        foreach (var part in parts)
        {
            _repairPartsGrid.Rows.Add(false, part.Id, part.Name, part.Price, part.Stock, 1);
        }
    }

    private void RefreshInvoices()
    {
        _invoiceGrid.DataSource = _repository.GetInvoices();
        _invoiceGrid.ClearSelection();
    }

    private void ConfigureRepairPartsGrid()
    {
        _repairPartsGrid.ReadOnly = false;
        _repairPartsGrid.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Use", HeaderText = "Dùng", FillWeight = 45 });
        _repairPartsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "PartId", HeaderText = "Mã", ReadOnly = true, FillWeight = 45 });
        _repairPartsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "Phụ tùng", ReadOnly = true, FillWeight = 180 });
        _repairPartsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Price", HeaderText = "Đơn giá", ReadOnly = true, FillWeight = 90 });
        _repairPartsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Stock", HeaderText = "Tồn kho", ReadOnly = true, FillWeight = 60 });
        _repairPartsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Số lượng dùng", FillWeight = 75 });
    }

    private void SaveCustomer()
    {
        var name = _customerName.Text.Trim();
        if (name.Length == 0)
        {
            throw new InvalidOperationException("Vui lòng nhập tên chủ xe.");
        }
        int? id = int.TryParse(_customerId.Text, out var customerId) ? customerId : null;
        _repository.SaveCustomer(id, name, _customerPhone.Text, _customerAddress.Text);
        RefreshCustomers();
        ClearCustomer();
        MessageBox.Show("Đã lưu thông tin chủ xe.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void DeleteCustomer()
    {
        if (!int.TryParse(_customerId.Text, out var customerId))
        {
            throw new InvalidOperationException("Hãy chọn chủ xe cần xóa.");
        }
        _repository.DeleteCustomer(customerId);
        RefreshCustomers();
        ClearCustomer();
        MessageBox.Show("Đã xóa chủ xe.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ShowSelectedCustomer()
    {
        if (_customerGrid.CurrentRow?.DataBoundItem is not CustomerDto customer)
        {
            return;
        }
        _customerId.Text = customer.Id.ToString(CultureInfo.InvariantCulture);
        _customerName.Text = customer.Name;
        _customerPhone.Text = customer.Phone;
        _customerAddress.Text = customer.Address;
    }

    private void ClearCustomer()
    {
        _customerGrid.ClearSelection();
        _customerId.Clear();
        _customerName.Clear();
        _customerPhone.Clear();
        _customerAddress.Clear();
    }

    private void SearchVehicle()
    {
        if (string.IsNullOrWhiteSpace(_plate.Text))
        {
            throw new InvalidOperationException("Nhập biển số cần tra cứu.");
        }
        var vehicle = _repository.FindVehicle(_plate.Text);
        if (vehicle is null)
        {
            MessageBox.Show("Không tìm thấy xe. Hãy chọn chủ xe và nhập thông tin để tiếp nhận xe mới.", "Tra cứu xe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        ShowVehicle(vehicle);
    }

    private void SaveVehicle()
    {
        if (string.IsNullOrWhiteSpace(_plate.Text))
        {
            throw new InvalidOperationException("Vui lòng nhập biển số xe.");
        }
        if (_vehicleCustomer.SelectedValue is not int customerId)
        {
            throw new InvalidOperationException("Hãy tạo và chọn chủ xe trước khi tiếp nhận xe.");
        }
        int? year = null;
        if (!string.IsNullOrWhiteSpace(_year.Text))
        {
            if (!int.TryParse(_year.Text, out var parsedYear) || parsedYear < 1886 || parsedYear > DateTime.Now.Year + 1)
            {
                throw new InvalidOperationException("Năm sản xuất không hợp lệ.");
            }
            year = parsedYear;
        }
        _repository.SaveVehicle(_plate.Text, customerId, _make.Text, year);
        RefreshVehicles();
        MessageBox.Show("Đã lưu thông tin xe.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void DeleteVehicle()
    {
        if (string.IsNullOrWhiteSpace(_plate.Text))
        {
            throw new InvalidOperationException("Nhập hoặc chọn biển số xe cần xóa.");
        }
        _repository.DeleteVehicle(_plate.Text);
        RefreshVehicles();
        _plate.Clear();
        _make.Clear();
        _year.Clear();
        MessageBox.Show("Đã xóa xe.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ShowSelectedVehicle()
    {
        if (_vehicleGrid.CurrentRow?.DataBoundItem is VehicleDto vehicle)
        {
            ShowVehicle(vehicle);
        }
    }

    private void ShowVehicle(VehicleDto vehicle)
    {
        _plate.Text = vehicle.Plate;
        _make.Text = vehicle.Make;
        _year.Text = vehicle.Year?.ToString(CultureInfo.InvariantCulture) ?? "";
        _vehicleCustomer.SelectedValue = vehicle.CustomerId;
    }

    private void SavePart()
    {
        var name = _partName.Text.Trim();
        if (name.Length == 0 || !decimal.TryParse(_partPrice.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var price) || price < 0 ||
            !int.TryParse(_partStock.Text, out var stock) || stock < 0)
        {
            throw new InvalidOperationException("Nhập tên phụ tùng, đơn giá không âm và số lượng tồn kho không âm.");
        }
        int? id = int.TryParse(_partId.Text, out var partId) ? partId : null;
        _repository.SavePart(id, name, price, stock);
        RefreshParts();
        ClearPart();
        MessageBox.Show("Đã lưu phụ tùng và tồn kho.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void DeletePart()
    {
        if (!int.TryParse(_partId.Text, out var partId))
        {
            throw new InvalidOperationException("Hãy chọn phụ tùng cần xóa.");
        }
        _repository.DeletePart(partId);
        RefreshParts();
        ClearPart();
        MessageBox.Show("Đã xóa phụ tùng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ShowSelectedPart()
    {
        if (_partGrid.CurrentRow?.DataBoundItem is not PartDto part)
        {
            return;
        }
        _partId.Text = part.Id.ToString(CultureInfo.InvariantCulture);
        _partName.Text = part.Name;
        _partPrice.Text = part.Price.ToString(CultureInfo.CurrentCulture);
        _partStock.Text = part.Stock.ToString(CultureInfo.InvariantCulture);
    }

    private void ClearPart()
    {
        _partGrid.ClearSelection();
        _partId.Clear();
        _partName.Clear();
        _partPrice.Clear();
        _partStock.Clear();
    }

    private void CreateRepair()
    {
        if (_repairVehicle.SelectedValue is not string plate)
        {
            throw new InvalidOperationException("Hãy tiếp nhận xe trước khi lập phiếu sửa chữa.");
        }
        if (!decimal.TryParse(_labor.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var labor) || labor < 0)
        {
            throw new InvalidOperationException("Tiền công phải là số không âm.");
        }

        _repairPartsGrid.EndEdit();
        var lines = new List<RepairLineInput>();
        foreach (DataGridViewRow row in _repairPartsGrid.Rows)
        {
            if (row.Cells["Use"].Value is not true)
            {
                continue;
            }
            if (!int.TryParse(Convert.ToString(row.Cells["Quantity"].Value, CultureInfo.CurrentCulture), out var quantity) || quantity <= 0)
            {
                throw new InvalidOperationException($"Số lượng dùng của '{row.Cells["Name"].Value}' phải là số nguyên lớn hơn 0.");
            }
            lines.Add(new RepairLineInput(Convert.ToInt32(row.Cells["PartId"].Value, CultureInfo.InvariantCulture), quantity));
        }

        var invoiceId = _repository.CreateRepair(plate, _repairDescription.Text, labor, lines);
        RefreshParts();
        RefreshInvoices();
        MessageBox.Show($"Đã lập phiếu #{invoiceId}, cập nhật kho trong cùng transaction. Trạng thái: Đã hoàn thành.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void PaySelectedInvoice()
    {
        if (_invoiceGrid.CurrentRow?.DataBoundItem is not RepairInvoiceDto invoice)
        {
            throw new InvalidOperationException("Hãy chọn phiếu sửa chữa cần thanh toán.");
        }
        _repository.PayInvoice(invoice.Id);
        RefreshInvoices();
        MessageBox.Show($"Phiếu #{invoice.Id} đã được thanh toán. Tổng tiền: {invoice.Total:N2}", "Hóa đơn dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
