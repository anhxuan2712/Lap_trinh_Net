using Bai6._5.BUS;
using Bai6._5.DTO;

namespace Bai6._5.GUI;

public partial class Form1 : Form
{
    private readonly KaraokeBus _bus = new();
    private readonly FlowLayoutPanel _roomGrid = new();
    private readonly DataGridView _orderGrid = new();
    private readonly ComboBox _serviceSelector = new();
    private readonly NumericUpDown _quantity = new();
    private readonly Label _roomDetails = new();
    private readonly Label _elapsedTime = new();
    private readonly Button _openRoomButton = new();
    private readonly Button _addServiceButton = new();
    private readonly Button _payButton = new();

    private readonly System.Windows.Forms.Timer _elapsedTimer =
        new() { Interval = 1000 };

    private IReadOnlyList<RoomDto> _rooms = [];
    private IReadOnlyList<ServiceDto> _services = [];
    private int? _selectedRoomId;

    public Form1()
    {
        InitializeComponent();

        Text = "Karaoke Management System";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(950, 620);
        Size = new Size(1200, 760);

        BuildInterface();

        Shown += (_, _) => RunSafely(RefreshAll);

        _elapsedTimer.Tick += (_, _) => UpdateElapsedTime();
        _elapsedTimer.Start();
    }

    private void BuildInterface()
    {
        var header = new Label
        {
            Text = "QUẢN LÝ KARAOKE",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            BackColor = Color.FromArgb(32, 45, 64),
            ForeColor = Color.White
        };

        var main = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            Padding = new Padding(12)
        };

        main.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 36));

        main.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 64));

        main.RowStyles.Add(
            new RowStyle(SizeType.Percent, 100));

        var leftPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(4)
        };

        leftPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 40));

        leftPanel.RowStyles.Add(
            new RowStyle(SizeType.Percent, 100));

        var roomTitle = new Label
        {
            Text = "SƠ ĐỒ PHÒNG HÁT",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Segoe UI", 12, FontStyle.Bold)
        };

        _roomGrid.Dock = DockStyle.Fill;
        _roomGrid.AutoScroll = true;
        _roomGrid.Padding = new Padding(4);
        _roomGrid.WrapContents = true;

        leftPanel.Controls.Add(roomTitle, 0, 0);
        leftPanel.Controls.Add(_roomGrid, 0, 1);

        var rightPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4,
            Padding = new Padding(8, 4, 4, 4)
        };

        rightPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 40));

        rightPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 65));

        rightPanel.RowStyles.Add(
            new RowStyle(SizeType.AutoSize));

        rightPanel.RowStyles.Add(
            new RowStyle(SizeType.Percent, 100));

        var orderTitle = new Label
        {
            Text = "DỊCH VỤ ĐÃ GỌI",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Segoe UI", 12, FontStyle.Bold)
        };

        var details = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            AutoSize = true
        };

        _roomDetails.AutoSize = true;
        _roomDetails.Font = new Font("Segoe UI", 11, FontStyle.Bold);

        _elapsedTime.AutoSize = true;
        _elapsedTime.Font = new Font("Segoe UI", 10);

        details.Controls.Add(_roomDetails, 0, 0);
        details.Controls.Add(_elapsedTime, 0, 1);

        var actions = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            WrapContents = true,
            Padding = new Padding(0, 6, 0, 6)
        };

        _openRoomButton.Text = "Mở phòng";
        _openRoomButton.Width = 100;
        _openRoomButton.Height = 32;
        _openRoomButton.Click += (_, _) => RunSafely(OpenSelectedRoom);

        _serviceSelector.Width = 170;
        _serviceSelector.DropDownStyle = ComboBoxStyle.DropDownList;

        _quantity.Minimum = 1;
        _quantity.Maximum = 1000;
        _quantity.Width = 65;

        _addServiceButton.Text = "Gọi dịch vụ";
        _addServiceButton.Width = 105;
        _addServiceButton.Height = 32;
        _addServiceButton.Click += (_, _) => RunSafely(AddSelectedService);

        _payButton.Text = "Thanh toán";
        _payButton.Width = 105;
        _payButton.Height = 32;
        _payButton.BackColor = Color.FromArgb(39, 145, 84);
        _payButton.ForeColor = Color.White;
        _payButton.Click += (_, _) => RunSafely(PaySelectedRoom);

        actions.Controls.Add(_openRoomButton);
        actions.Controls.Add(_serviceSelector);
        actions.Controls.Add(_quantity);
        actions.Controls.Add(_addServiceButton);
        actions.Controls.Add(_payButton);

        _orderGrid.Dock = DockStyle.Fill;
        _orderGrid.ReadOnly = true;
        _orderGrid.AllowUserToAddRows = false;
        _orderGrid.AllowUserToDeleteRows = false;
        _orderGrid.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;
        _orderGrid.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;
        _orderGrid.MultiSelect = false;
        _orderGrid.RowHeadersVisible = false;

        rightPanel.Controls.Add(orderTitle, 0, 0);
        rightPanel.Controls.Add(details, 0, 1);
        rightPanel.Controls.Add(actions, 0, 2);
        rightPanel.Controls.Add(_orderGrid, 0, 3);

        main.Controls.Add(leftPanel, 0, 0);
        main.Controls.Add(rightPanel, 1, 0);

        Controls.Add(main);
        Controls.Add(header);

        header.Dock = DockStyle.Top;
        header.Height = 52;
        header.BringToFront();
    }

    private void RefreshAll()
    {
        _rooms = _bus.GetRooms();
        _services = _bus.GetServices();

        _serviceSelector.DataSource = null;
        _serviceSelector.DataSource = _services.ToList();
        _serviceSelector.DisplayMember = nameof(ServiceDto.Name);
        _serviceSelector.ValueMember = nameof(ServiceDto.Id);

        if (_selectedRoomId is not null &&
            _rooms.All(room => room.Id != _selectedRoomId))
        {
            _selectedRoomId = null;
        }

        if (_selectedRoomId is null && _rooms.Count > 0)
        {
            _selectedRoomId = _rooms[0].Id;
        }

        _roomGrid.Controls.Clear();

        foreach (var room in _rooms)
        {
            bool isSelected = room.Id == _selectedRoomId;

            var roomButton = new Button
            {
                Text = $"{room.Name}\n" +
                       $"{(room.ActiveInvoiceId is null ? "TRỐNG" : "ĐANG HÁT")}\n" +
                       $"{room.HourlyRate:N0} đ/giờ",

                Width = 175,
                Height = 105,
                Margin = new Padding(8),

                BackColor = room.ActiveInvoiceId is null
                    ? Color.FromArgb(76, 175, 80)
                    : Color.FromArgb(220, 65, 65),

                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            roomButton.FlatAppearance.BorderSize =
                isSelected ? 4 : 0;

            roomButton.FlatAppearance.BorderColor =
                Color.FromArgb(32, 45, 64);

            roomButton.Click += (_, _) =>
            {
                _selectedRoomId = room.Id;
                RunSafely(ShowSelectedRoom);
            };

            _roomGrid.Controls.Add(roomButton);
        }

        ShowSelectedRoom();
    }

    private RoomDto? SelectedRoom =>
        _rooms.FirstOrDefault(room => room.Id == _selectedRoomId);

    private void ShowSelectedRoom()
    {
        var room = SelectedRoom;

        if (room is null)
        {
            _roomDetails.Text =
                "Chưa có phòng. Hãy tạo phòng trong cơ sở dữ liệu.";

            _elapsedTime.Text = string.Empty;
            _orderGrid.DataSource = null;

            SetActionAvailability(null);
            return;
        }

        _roomDetails.Text =
            $"{room.Name} - {room.Type} - {room.HourlyRate:N0} đ/giờ";

        _elapsedTime.Text = room.StartedAt is null
            ? "Trạng thái: Trống"
            : $"Bắt đầu: {room.StartedAt:dd/MM/yyyy HH:mm:ss}";

        _orderGrid.DataSource = room.ActiveInvoiceId is null
            ? null
            : _bus.GetOrderItems(room.ActiveInvoiceId.Value);

        SetActionAvailability(room);
        UpdateElapsedTime();
        RefreshRoomButtons();
    }

    private void RefreshRoomButtons()
    {
        for (int i = 0; i < _roomGrid.Controls.Count && i < _rooms.Count; i++)
        {
            if (_roomGrid.Controls[i] is Button button)
            {
                button.FlatAppearance.BorderSize =
                    _rooms[i].Id == _selectedRoomId ? 4 : 0;
            }
        }
    }

    private void SetActionAvailability(RoomDto? room)
    {
        bool isEmpty =
            room is not null && room.ActiveInvoiceId is null;

        bool isOccupied =
            room?.ActiveInvoiceId is not null;

        _openRoomButton.Enabled = isEmpty;
        _addServiceButton.Enabled = isOccupied && _services.Count > 0;
        _payButton.Enabled = isOccupied;
    }

    private void UpdateElapsedTime()
    {
        var startedAt = SelectedRoom?.StartedAt;

        if (startedAt is not null)
        {
            var elapsed = DateTime.Now - startedAt.Value;

            if (elapsed < TimeSpan.Zero)
                elapsed = TimeSpan.Zero;

            _elapsedTime.Text =
                $"Đang hát: {elapsed:hh\\:mm\\:ss} ({elapsed.Days} ngày)";
        }
    }

    private void OpenSelectedRoom()
    {
        _bus.OpenRoom(SelectedRoom);

        RefreshAll();
    }

    private void AddSelectedService()
    {
        var room = SelectedRoom;

        _bus.AddService(
            room,
            _serviceSelector.SelectedItem as ServiceDto,
            (int)_quantity.Value);

        _orderGrid.DataSource =
            _bus.GetOrderItems(room!.ActiveInvoiceId!.Value);
    }

    private void PaySelectedRoom()
    {
        var room = SelectedRoom;
        _bus.EnsureRoomOccupied(room);

        var confirmation = MessageBox.Show(
            $"Thanh toán và đóng {room!.Name}?",
            "Xác nhận thanh toán",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirmation != DialogResult.Yes)
            return;

        var result = _bus.PayRoom(room);

        MessageBox.Show(
            $"Đã thanh toán hóa đơn #{result.InvoiceId}.\n" +
            $"Tiền phòng: {result.RoomTotal:N0} đ\n" +
            $"Tiền dịch vụ: {result.ServiceTotal:N0} đ\n" +
            $"Tổng cộng: {result.Total:N0} đ",
            "Thanh toán thành công",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        RefreshAll();
    }

    private void RunSafely(Action action)
    {
        try
        {
            action();
        }
        catch (Exception exception)
        {
            MessageBox.Show(
                this,
                exception.Message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _elapsedTimer.Stop();
        _elapsedTimer.Dispose();

        base.OnFormClosed(e);
    }
}
