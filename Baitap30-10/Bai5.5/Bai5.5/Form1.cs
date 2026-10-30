using System.ComponentModel;
using System.Data;
using Bai5._5.BUS;
using Bai5._5.DTO;

namespace Bai5._5;

public partial class Form1 : Form
{
    private readonly InventoryBUS bus = new();
    private readonly BindingList<ChiTietPhieuKhoDTO> chiTietPhieu = [];

    private readonly DataGridView gridVatTu = CreateGrid();
    private readonly DataGridView gridNhaCungCap = CreateGrid();
    private readonly DataGridView gridChiTiet = CreateGrid();
    private readonly DataGridView gridTonThap = CreateGrid();

    private readonly TextBox txtMaVatTu = new() { Width = 120 };
    private readonly TextBox txtTenVatTu = new() { Width = 180 };
    private readonly TextBox txtDVT = new() { Width = 90 };
    private readonly NumericUpDown numTon = CreateAmountInput();
    private readonly TextBox txtTimVatTu = new() { Width = 180 };

    private readonly TextBox txtMaNCC = new() { Width = 70, ReadOnly = true };
    private readonly TextBox txtTenNCC = new() { Width = 180 };
    private readonly TextBox txtDienThoai = new() { Width = 130 };
    private readonly TextBox txtDiaChi = new() { Width = 220 };
    private readonly TextBox txtTimNCC = new() { Width = 180 };

    private readonly ComboBox cboLoaiPhieu = new() { Width = 110, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly ComboBox cboNhaCungCap = new() { Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly ComboBox cboVatTu = new() { Width = 340, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly NumericUpDown numSoLuong = CreateAmountInput();
    private readonly NumericUpDown numDonGia = CreateAmountInput();
    private readonly NumericUpDown numNguong = CreateAmountInput();
    private readonly Label lblTongGiaTri = new() { AutoSize = true, Padding = new Padding(12, 8, 0, 0) };

    private bool dangSuaVatTu;

    public Form1()
    {
        InitializeComponent();
        Text = "Hệ thống quản lý nhập - xuất kho";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1000, 650);
        Size = new Size(1280, 800);

        var tabs = new TabControl { Dock = DockStyle.Fill };
        tabs.TabPages.Add(BuildVatTuTab());
        tabs.TabPages.Add(BuildNhaCungCapTab());
        tabs.TabPages.Add(BuildPhieuKhoTab());
        tabs.TabPages.Add(BuildBaoCaoTab());
        Controls.Add(tabs);

        Load += (_, _) => TryRefreshAll();
    }

    private TabPage BuildVatTuTab()
    {
        var page = new TabPage("Vật tư");
        var panel = NewInputPanel();
        AddField(panel, "Mã vật tư", txtMaVatTu);
        AddField(panel, "Tên vật tư", txtTenVatTu);
        AddField(panel, "Đơn vị tính", txtDVT);
        AddField(panel, "Số lượng tồn", numTon);
        panel.Controls.Add(CreateButton("Thêm", (_, _) => RunAction(() =>
        {
            bus.AddVatTu(ReadVatTu());
            ClearVatTu();
        }, "Đã thêm vật tư.")));
        panel.Controls.Add(CreateButton("Cập nhật", (_, _) => RunAction(() =>
        {
            bus.UpdateVatTu(ReadVatTu());
            ClearVatTu();
        }, "Đã cập nhật vật tư.")));
        panel.Controls.Add(CreateButton("Xóa", (_, _) => DeleteVatTu()));
        panel.Controls.Add(CreateButton("Làm mới", (_, _) => ClearVatTu()));
        AddField(panel, "Tra cứu mã/tên", txtTimVatTu);
        panel.Controls.Add(CreateButton("Tìm", (_, _) => RunAction(() => gridVatTu.DataSource = bus.GetVatTu(txtTimVatTu.Text), "Đã cập nhật kết quả tìm kiếm.", false)));
        page.Controls.Add(gridVatTu);
        page.Controls.Add(panel);
        gridVatTu.SelectionChanged += (_, _) => SelectVatTu();
        return page;
    }

    private TabPage BuildNhaCungCapTab()
    {
        var page = new TabPage("Nhà cung cấp");
        var panel = NewInputPanel();
        AddField(panel, "Mã NCC", txtMaNCC);
        AddField(panel, "Tên nhà cung cấp", txtTenNCC);
        AddField(panel, "Điện thoại", txtDienThoai);
        AddField(panel, "Địa chỉ", txtDiaChi);
        panel.Controls.Add(CreateButton("Thêm", (_, _) => RunAction(() =>
        {
            bus.AddNhaCungCap(ReadNhaCungCap());
            ClearNhaCungCap();
        }, "Đã thêm nhà cung cấp.")));
        panel.Controls.Add(CreateButton("Cập nhật", (_, _) => RunAction(() =>
        {
            bus.UpdateNhaCungCap(ReadNhaCungCap());
            ClearNhaCungCap();
        }, "Đã cập nhật nhà cung cấp.")));
        panel.Controls.Add(CreateButton("Xóa", (_, _) => DeleteNhaCungCap()));
        panel.Controls.Add(CreateButton("Làm mới", (_, _) => ClearNhaCungCap()));
        AddField(panel, "Tra cứu tên/điện thoại", txtTimNCC);
        panel.Controls.Add(CreateButton("Tìm", (_, _) => RunAction(() => gridNhaCungCap.DataSource = bus.GetNhaCungCap(txtTimNCC.Text), "Đã cập nhật kết quả tìm kiếm.", false)));
        page.Controls.Add(gridNhaCungCap);
        page.Controls.Add(panel);
        gridNhaCungCap.SelectionChanged += (_, _) => SelectNhaCungCap();
        return page;
    }

    private TabPage BuildPhieuKhoTab()
    {
        var page = new TabPage("Lập phiếu nhập / xuất");
        var panel = NewInputPanel();
        cboLoaiPhieu.Items.AddRange(["NHAP", "XUAT"]);
        cboLoaiPhieu.SelectedIndex = 0;
        cboLoaiPhieu.SelectedIndexChanged += (_, _) => cboNhaCungCap.Enabled = cboLoaiPhieu.SelectedItem?.ToString() == "NHAP";
        numSoLuong.Minimum = 0.01m;
        numSoLuong.Value = 1;
        numDonGia.Minimum = 0;
        AddField(panel, "Loại phiếu", cboLoaiPhieu);
        AddField(panel, "Nhà cung cấp (phiếu nhập)", cboNhaCungCap);
        AddField(panel, "Vật tư", cboVatTu);
        AddField(panel, "Số lượng", numSoLuong);
        AddField(panel, "Đơn giá", numDonGia);
        panel.Controls.Add(CreateButton("Thêm vật tư vào phiếu", (_, _) => AddChiTiet()));
        panel.Controls.Add(CreateButton("Xóa dòng chọn", (_, _) => RemoveChiTiet()));
        panel.Controls.Add(CreateButton("Lưu phiếu kho", (_, _) => SavePhieuKho()));
        ConfigureDetailGrid();
        page.Controls.Add(gridChiTiet);
        page.Controls.Add(panel);
        return page;
    }

    private TabPage BuildBaoCaoTab()
    {
        var page = new TabPage("Cảnh báo & báo cáo");
        var panel = NewInputPanel();
        numNguong.Value = 10;
        AddField(panel, "Ngưỡng cảnh báo", numNguong);
        panel.Controls.Add(CreateButton("Lọc tồn kho thấp", (_, _) => RunAction(RefreshTonThap, "Đã cập nhật danh sách tồn kho thấp.", false)));
        panel.Controls.Add(CreateButton("Tính giá trị tồn kho", (_, _) => RunAction(RefreshTongGiaTri, "Đã tính giá trị tồn kho.", false)));
        lblTongGiaTri.Text = "Giá trị tồn (theo giá nhập bình quân): chưa tính";
        panel.Controls.Add(lblTongGiaTri);
        page.Controls.Add(gridTonThap);
        page.Controls.Add(panel);
        return page;
    }

    private void ConfigureDetailGrid()
    {
        gridChiTiet.AutoGenerateColumns = false;
        gridChiTiet.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ChiTietPhieuKhoDTO.MaVatTu), HeaderText = "Mã vật tư", ReadOnly = true, Width = 140 });
        gridChiTiet.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ChiTietPhieuKhoDTO.TenVatTu), HeaderText = "Tên vật tư", ReadOnly = true, Width = 260 });
        gridChiTiet.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ChiTietPhieuKhoDTO.SoLuong), HeaderText = "Số lượng", ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
        gridChiTiet.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ChiTietPhieuKhoDTO.DonGia), HeaderText = "Đơn giá", ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
        gridChiTiet.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ChiTietPhieuKhoDTO.ThanhTien), HeaderText = "Thành tiền", ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
        gridChiTiet.DataSource = chiTietPhieu;
    }

    private void AddChiTiet()
    {
        if (cboVatTu.SelectedItem is not DataRowView selected)
        {
            ShowError("Vui lòng chọn vật tư.");
            return;
        }

        string maVatTu = Convert.ToString(selected["MaVatTu"]) ?? string.Empty;
        if (chiTietPhieu.Any(item => item.MaVatTu == maVatTu))
        {
            ShowError("Vật tư này đã có trong phiếu.");
            return;
        }

        chiTietPhieu.Add(new ChiTietPhieuKhoDTO
        {
            MaVatTu = maVatTu,
            TenVatTu = Convert.ToString(selected["TenVatTu"]) ?? string.Empty,
            SoLuong = numSoLuong.Value,
            DonGia = numDonGia.Value
        });
    }

    private void RemoveChiTiet()
    {
        int index = gridChiTiet.CurrentRow?.Index ?? -1;
        if (index >= 0 && index < chiTietPhieu.Count)
            chiTietPhieu.RemoveAt(index);
    }

    private void SavePhieuKho()
    {
        try
        {
            int supplierId = Convert.ToInt32(cboNhaCungCap.SelectedValue ?? 0);
            int receiptId = bus.SavePhieuKho(new PhieuKhoDTO
            {
                LoaiPhieu = cboLoaiPhieu.SelectedItem?.ToString() ?? string.Empty,
                MaNCC = supplierId == 0 ? null : supplierId,
                ChiTiet = chiTietPhieu.ToList()
            });
            chiTietPhieu.Clear();
            MessageBox.Show($"Đã lưu phiếu kho số {receiptId}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TryRefreshAll();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private VatTuDTO ReadVatTu() => new()
    {
        MaVatTu = txtMaVatTu.Text,
        TenVatTu = txtTenVatTu.Text,
        DVT = txtDVT.Text,
        SoLuongTon = numTon.Value
    };

    private NhaCungCapDTO ReadNhaCungCap() => new()
    {
        MaNCC = int.TryParse(txtMaNCC.Text, out int maNCC) ? maNCC : 0,
        TenNCC = txtTenNCC.Text,
        DienThoai = txtDienThoai.Text,
        DiaChi = txtDiaChi.Text
    };

    private void SelectVatTu()
    {
        if (gridVatTu.CurrentRow?.DataBoundItem is not DataRowView row)
            return;
        txtMaVatTu.Text = Convert.ToString(row["MaVatTu"]);
        txtTenVatTu.Text = Convert.ToString(row["TenVatTu"]);
        txtDVT.Text = row["DVT"] is DBNull ? string.Empty : Convert.ToString(row["DVT"]);
        numTon.Value = Math.Clamp(Convert.ToDecimal(row["SoLuongTon"]), numTon.Minimum, numTon.Maximum);
        dangSuaVatTu = true;
        txtMaVatTu.ReadOnly = true;
    }

    private void SelectNhaCungCap()
    {
        if (gridNhaCungCap.CurrentRow?.DataBoundItem is not DataRowView row)
            return;
        txtMaNCC.Text = Convert.ToString(row["MaNCC"]);
        txtTenNCC.Text = Convert.ToString(row["TenNCC"]);
        txtDienThoai.Text = row["DienThoai"] is DBNull ? string.Empty : Convert.ToString(row["DienThoai"]);
        txtDiaChi.Text = row["DiaChi"] is DBNull ? string.Empty : Convert.ToString(row["DiaChi"]);
    }

    private void DeleteVatTu()
    {
        if (!dangSuaVatTu)
        {
            ShowError("Vui lòng chọn vật tư cần xóa.");
            return;
        }
        if (MessageBox.Show("Xóa vật tư đang chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;
        RunAction(() =>
        {
            bus.DeleteVatTu(txtMaVatTu.Text);
            ClearVatTu();
        }, "Đã xóa vật tư.");
    }

    private void DeleteNhaCungCap()
    {
        if (!int.TryParse(txtMaNCC.Text, out int maNCC))
        {
            ShowError("Vui lòng chọn nhà cung cấp cần xóa.");
            return;
        }
        if (MessageBox.Show("Xóa nhà cung cấp đang chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;
        RunAction(() =>
        {
            bus.DeleteNhaCungCap(maNCC);
            ClearNhaCungCap();
        }, "Đã xóa nhà cung cấp.");
    }

    private void ClearVatTu()
    {
        txtMaVatTu.Clear();
        txtMaVatTu.ReadOnly = false;
        txtTenVatTu.Clear();
        txtDVT.Clear();
        numTon.Value = 0;
        dangSuaVatTu = false;
        gridVatTu.ClearSelection();
    }

    private void ClearNhaCungCap()
    {
        txtMaNCC.Clear();
        txtTenNCC.Clear();
        txtDienThoai.Clear();
        txtDiaChi.Clear();
        gridNhaCungCap.ClearSelection();
    }

    private void TryRefreshAll()
    {
        try
        {
            gridVatTu.DataSource = bus.GetVatTu();
            gridNhaCungCap.DataSource = bus.GetNhaCungCap();
            RefreshPhieuCombos();
            RefreshTonThap();
            RefreshTongGiaTri();
        }
        catch (Exception ex)
        {
            ShowError($"Không thể tải dữ liệu. Kiểm tra MySQL, CSDL InventoryDB và cấu hình kết nối.\n\n{ex.Message}");
        }
    }

    private void RefreshPhieuCombos()
    {
        DataTable suppliers = bus.GetNhaCungCap();
        DataRow defaultSupplier = suppliers.NewRow();
        defaultSupplier["MaNCC"] = 0;
        defaultSupplier["TenNCC"] = "-- Chỉ dùng cho phiếu xuất --";
        suppliers.Rows.InsertAt(defaultSupplier, 0);
        cboNhaCungCap.DataSource = suppliers;
        cboNhaCungCap.DisplayMember = "TenNCC";
        cboNhaCungCap.ValueMember = "MaNCC";
        cboNhaCungCap.SelectedIndex = 0;

        DataTable materials = bus.GetVatTu();
        materials.Columns.Add("HienThi", typeof(string));
        foreach (DataRow row in materials.Rows)
            row["HienThi"] = $"{row["MaVatTu"]} - {row["TenVatTu"]} (tồn: {row["SoLuongTon"]})";
        cboVatTu.DataSource = materials;
        cboVatTu.DisplayMember = "HienThi";
        cboVatTu.ValueMember = "MaVatTu";
    }

    private void RefreshTonThap() => gridTonThap.DataSource = bus.GetVatTuTonDuoi(numNguong.Value);

    private void RefreshTongGiaTri() =>
        lblTongGiaTri.Text = $"Giá trị tồn (theo giá nhập bình quân): {bus.GetTongGiaTriTonKho():N2}";

    private void RunAction(Action action, string successMessage, bool refresh = true)
    {
        try
        {
            action();
            MessageBox.Show(successMessage, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (refresh)
                TryRefreshAll();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private static void ShowError(string message) =>
        MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

    private static NumericUpDown CreateAmountInput() => new()
    {
        Width = 120,
        DecimalPlaces = 2,
        Minimum = 0,
        Maximum = 9999999999999999m,
        ThousandsSeparator = true
    };

    private static FlowLayoutPanel NewInputPanel() => new()
    {
        Dock = DockStyle.Top,
        AutoSize = true,
        WrapContents = true,
        Padding = new Padding(8),
        FlowDirection = FlowDirection.LeftToRight
    };

    private static void AddField(FlowLayoutPanel panel, string label, Control control)
    {
        var field = new FlowLayoutPanel
        {
            AutoSize = true,
            FlowDirection = FlowDirection.TopDown,
            Margin = new Padding(4, 2, 8, 4),
            WrapContents = false
        };
        field.Controls.Add(new Label { Text = label, AutoSize = true });
        field.Controls.Add(control);
        panel.Controls.Add(field);
    }

    private static Button CreateButton(string text, EventHandler onClick)
    {
        var button = new Button { Text = text, AutoSize = true, Height = 34, Margin = new Padding(5, 20, 5, 2) };
        button.Click += onClick;
        return button;
    }
}
