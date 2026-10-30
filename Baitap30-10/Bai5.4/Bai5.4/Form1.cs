using System.Data;
using Bai5._4.BUS;
using Bai5._4.DTO;

namespace Bai5._4;

public partial class Form1 : Form
{
    private readonly HotelBUS bus = new();
    private readonly DataGridView roomGrid = CreateGrid();
    private readonly DataGridView stayGrid = CreateGrid();
    private readonly DataGridView serviceGrid = CreateGrid();
    private readonly DataGridView usedServiceGrid = CreateGrid();
    private readonly ComboBox roomTypeFilter = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 150 };
    private readonly ComboBox roomStatusFilter = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 150 };
    private readonly TextBox cccdBox = new() { Width = 150 };
    private readonly TextBox nameBox = new() { Width = 180 };
    private readonly TextBox phoneBox = new() { Width = 130 };
    private readonly ComboBox roomCombo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 130 };
    private readonly TextBox serviceCccdBox = new() { Width = 130 };
    private readonly ComboBox serviceCombo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220 };
    private readonly NumericUpDown quantityBox = new() { Minimum = 1, Maximum = 999, Value = 1, Width = 70 };
    private readonly ComboBox receiptCombo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 300 };
    private readonly Label statusLabel = new() { AutoSize = true, ForeColor = Color.DarkSlateGray };
    private DataTable? rooms;
    private DataTable? services;
    private DataTable? stays;

    public Form1()
    {
        InitializeComponent();
        Text = "Hotel Management System - Bài tập 5.4 (MySQL)";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1100, 700);
        BuildInterface();
        Shown += (_, _) => RefreshData();
    }

    private void BuildInterface()
    {
        var tabs = new TabControl { Dock = DockStyle.Fill };
        tabs.TabPages.Add(BuildRoomPage());
        tabs.TabPages.Add(BuildServicePage());
        tabs.TabPages.Add(BuildCheckoutPage());
        Controls.Add(tabs);
        Controls.Add(new StatusStrip { Dock = DockStyle.Bottom, Items = { new ToolStripControlHost(statusLabel) } });
    }

    private TabPage BuildRoomPage()
    {
        var page = new TabPage("Phòng và Check-in");
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(8) };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48)); layout.RowStyles.Add(new RowStyle(SizeType.Percent, 55));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 115)); layout.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
        page.Controls.Add(layout);
        var filters = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
        filters.Controls.Add(Label("Loại phòng:")); filters.Controls.Add(roomTypeFilter); filters.Controls.Add(Label("Trạng thái:")); filters.Controls.Add(roomStatusFilter);
        var clear = new Button { Text = "Xóa bộ lọc", AutoSize = true }; clear.Click += (_, _) => { roomTypeFilter.SelectedIndex = 0; roomStatusFilter.SelectedIndex = 0; ApplyRoomFilter(); }; filters.Controls.Add(clear);
        layout.Controls.Add(filters, 0, 0); layout.Controls.Add(roomGrid, 0, 1);
        var checkin = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = true, Padding = new Padding(4) };
        checkin.Controls.Add(Label("CCCD:")); checkin.Controls.Add(cccdBox);
        var find = new Button { Text = "Tra cứu", AutoSize = true }; find.Click += FindCustomerClicked; checkin.Controls.Add(find);
        checkin.Controls.Add(Label("Họ tên:")); checkin.Controls.Add(nameBox); checkin.Controls.Add(Label("Điện thoại:")); checkin.Controls.Add(phoneBox); checkin.Controls.Add(Label("Phòng trống:")); checkin.Controls.Add(roomCombo);
        var checkinButton = new Button { Text = "Nhận phòng", AutoSize = true, BackColor = Color.LightGreen }; checkinButton.Click += CheckInClicked; checkin.Controls.Add(checkinButton);
        layout.Controls.Add(checkin, 0, 2); layout.Controls.Add(stayGrid, 0, 3);
        return page;
    }

    private TabPage BuildServicePage()
    {
        var page = new TabPage("Gọi dịch vụ");
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(8) };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55)); layout.RowStyles.Add(new RowStyle(SizeType.Percent, 48)); layout.RowStyles.Add(new RowStyle(SizeType.Percent, 52)); page.Controls.Add(layout);
        var add = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
        add.Controls.Add(Label("CCCD:")); add.Controls.Add(serviceCccdBox); var lookup = new Button { Text = "Chọn phiếu", AutoSize = true }; lookup.Click += LookupReceiptClicked; add.Controls.Add(lookup);
        add.Controls.Add(Label("Dịch vụ:")); add.Controls.Add(serviceCombo); add.Controls.Add(Label("SL:")); add.Controls.Add(quantityBox); var addButton = new Button { Text = "Gọi dịch vụ", AutoSize = true }; addButton.Click += AddServiceClicked; add.Controls.Add(addButton);
        layout.Controls.Add(add, 0, 0); layout.Controls.Add(serviceGrid, 0, 1); layout.Controls.Add(usedServiceGrid, 0, 2); return page;
    }

    private TabPage BuildCheckoutPage()
    {
        var page = new TabPage("Check-out và hóa đơn");
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(12) };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55)); layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); page.Controls.Add(layout);
        var top = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false }; top.Controls.Add(Label("Phiếu đang ở:")); top.Controls.Add(receiptCombo);
        var refresh = new Button { Text = "Nạp lại", AutoSize = true }; refresh.Click += (_, _) => RefreshData(); top.Controls.Add(refresh);
        var checkout = new Button { Text = "Thanh toán / Check-out", AutoSize = true, BackColor = Color.LightGreen }; checkout.Click += CheckoutClicked; top.Controls.Add(checkout);
        layout.Controls.Add(top, 0, 0); layout.Controls.Add(usedServiceGrid, 0, 1); return page;
    }

    private void RefreshData()
    {
        try
        {
            rooms = bus.LoadRooms(); services = bus.LoadServices(); stays = bus.LoadCurrentStays(); roomGrid.DataSource = rooms; serviceGrid.DataSource = services; stayGrid.DataSource = stays;
            roomTypeFilter.DataSource = ValuesWithAll(rooms, "LoaiPhong"); roomStatusFilter.DataSource = ValuesWithAll(rooms, "TrangThai");
            var freeRooms = rooms.Select("TrangThai = 'Trống'"); roomCombo.DataSource = CopyRows(freeRooms, rooms); roomCombo.DisplayMember = "SoPhong"; roomCombo.ValueMember = "SoPhong";
            serviceCombo.DataSource = services.Copy(); serviceCombo.DisplayMember = "TenDV"; serviceCombo.ValueMember = "MaDV";
            receiptCombo.DataSource = stays.Copy(); receiptCombo.DisplayMember = "MaPhieu"; receiptCombo.ValueMember = "MaPhieu"; ApplyRoomFilter(); BindSelectedReceipt(); statusLabel.Text = "Đã kết nối HotelDB bằng MySQL.";
        }
        catch (Exception ex) { statusLabel.Text = "Chưa kết nối được HotelDB."; MessageBox.Show("Không thể nạp dữ liệu. Hãy chạy HotelDB.sql và kiểm tra MySQL/chuỗi kết nối.\n\n" + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    private void ApplyRoomFilter()
    {
        if (rooms is null) return; var type = roomTypeFilter.SelectedItem?.ToString() ?? "Tất cả"; var status = roomStatusFilter.SelectedItem?.ToString() ?? "Tất cả"; var filters = new List<string>();
        if (type != "Tất cả") filters.Add($"LoaiPhong = '{type.Replace("'", "''")}'"); if (status != "Tất cả") filters.Add($"TrangThai = '{status.Replace("'", "''")}'"); roomGrid.DataSource = new DataView(rooms) { RowFilter = string.Join(" AND ", filters) };
    }

    private void FindCustomerClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(cccdBox.Text)) { MessageBox.Show("Vui lòng nhập CCCD."); return; }
        try { var customer = bus.FindCustomerByCccd(cccdBox.Text.Trim()); nameBox.Text = customer?.HoTen ?? ""; phoneBox.Text = customer?.SoDienThoai ?? ""; statusLabel.Text = customer is null ? "Khách mới, nhập họ tên để tiếp tục." : "Đã tìm thấy khách hàng."; } catch (Exception ex) { ShowError(ex); }
    }

    private void CheckInClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(cccdBox.Text) || string.IsNullOrWhiteSpace(nameBox.Text) || roomCombo.SelectedValue is null) { MessageBox.Show("CCCD, họ tên và phòng là bắt buộc."); return; }
        try { var customer = bus.FindCustomerByCccd(cccdBox.Text.Trim()); int customerId = customer?.MaKH ?? bus.AddCustomer(new KhachHangDTO { HoTen = nameBox.Text.Trim(), CCCD = cccdBox.Text.Trim(), SoDienThoai = phoneBox.Text.Trim() }); int receiptId = bus.CheckIn(roomCombo.SelectedValue.ToString()!, customerId); MessageBox.Show($"Nhận phòng thành công. Mã phiếu: {receiptId}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); RefreshData(); } catch (Exception ex) { ShowError(ex); }
    }

    private void LookupReceiptClicked(object? sender, EventArgs e)
    {
        if (stays is null) return; var cccd = serviceCccdBox.Text.Trim(); var rows = stays.Select($"CCCD = '{cccd.Replace("'", "''")}'"); if (rows.Length == 0) { MessageBox.Show("Không tìm thấy khách đang ở theo CCCD."); return; } receiptCombo.SelectedValue = rows[0]["MaPhieu"]; BindSelectedReceipt();
    }

    private void AddServiceClicked(object? sender, EventArgs e)
    {
        if (receiptCombo.SelectedValue is null || serviceCombo.SelectedValue is null) { MessageBox.Show("Hãy chọn phiếu và dịch vụ."); return; }
        try { bus.AddService(Convert.ToInt32(receiptCombo.SelectedValue), Convert.ToInt32(serviceCombo.SelectedValue), (int)quantityBox.Value); BindSelectedReceipt(); MessageBox.Show("Đã ghi nhận dịch vụ."); } catch (Exception ex) { ShowError(ex); }
    }

    private void CheckoutClicked(object? sender, EventArgs e)
    {
        if (receiptCombo.SelectedValue is null) { MessageBox.Show("Không có phiếu đang ở."); return; }
        try { decimal total = bus.Checkout(Convert.ToInt32(receiptCombo.SelectedValue)); MessageBox.Show($"Thanh toán thành công. Tổng tiền: {total:N0} đ", "Hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information); RefreshData(); } catch (Exception ex) { ShowError(ex); }
    }

    private void BindSelectedReceipt()
    {
        if (receiptCombo.SelectedValue is not null && int.TryParse(receiptCombo.SelectedValue.ToString(), out int receiptId)) usedServiceGrid.DataSource = bus.LoadUsedServices(receiptId);
    }

    private static DataGridView CreateGrid() => new() { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
    private static Label Label(string text) => new() { Text = text, AutoSize = true, Margin = new Padding(6, 8, 4, 0) };
    private static List<string> ValuesWithAll(DataTable table, string column) => new[] { "Tất cả" }.Concat(table.AsEnumerable().Select(r => r.Field<string>(column) ?? "").Distinct()).ToList();
    private static DataTable CopyRows(IEnumerable<DataRow> rows, DataTable source) { var table = source.Clone(); foreach (var row in rows) table.ImportRow(row); return table; }
    private void ShowError(Exception ex) => MessageBox.Show(ex.Message, "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
