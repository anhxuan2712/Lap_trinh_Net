using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BilliardsManagement.BUS;
using BilliardsManagement.DTO;

namespace BilliardsManagement.GUI
{
    public partial class FormMain : Form
    {
        private static readonly Color MauTrong = Color.SeaGreen;
        private static readonly Color MauDangChoi = Color.OrangeRed;

        private readonly BanBidaBUS banBus = new BanBidaBUS();
        private readonly HoaDonBUS hoaDonBus = new HoaDonBUS();
        private readonly DichVuBUS dichVuBus = new DichVuBUS();
        private readonly Timer timer = new Timer { Interval = 1000 };

        private readonly Dictionary<int, Button> nutBan = new Dictionary<int, Button>();
        private BanBidaDTO banDangChon;
        private HoaDonDTO hoaDonDangMo;
        private DataTable dtDichVu;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            timer.Tick += Timer_Tick;
            timer.Start();
            LoadMenu();
            LoadSoDo(null);
        }

        private void LoadMenu()
        {
            try
            {
                cboDichVu.DisplayMember = "HienThi";
                cboDichVu.ValueMember = "MaDV";
                cboDichVu.DataSource = dichVuBus.GetMenu();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void LoadSoDo(int? chonLaiMaBan)
        {
            List<BanBidaDTO> ds;
            try
            {
                ds = banBus.GetAll();
            }
            catch (Exception ex)
            {
                ShowError(ex);
                return;
            }

            flpBan.SuspendLayout();
            flpBan.Controls.Clear();
            nutBan.Clear();
            BanBidaDTO banChon = null;
            foreach (BanBidaDTO ban in ds)
            {
                Button btn = new Button
                {
                    Width = 160,
                    Height = 105,
                    Margin = new Padding(6),
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.White,
                    BackColor = ban.DangChoi ? MauDangChoi : MauTrong,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    Tag = ban,
                    Text = TextNut(ban)
                };
                btn.FlatAppearance.BorderColor = Color.White;
                btn.FlatAppearance.BorderSize = 1;
                btn.Click += NutBan_Click;
                flpBan.Controls.Add(btn);
                nutBan[ban.MaBan] = btn;
                if (chonLaiMaBan.HasValue && ban.MaBan == chonLaiMaBan.Value) banChon = ban;
            }
            flpBan.ResumeLayout();
            ChonBan(banChon);
        }

        private static string TextNut(BanBidaDTO ban)
        {
            string dong3 = ban.DangChoi && ban.GioBatDau.HasValue
                ? HoaDonBUS.DinhDangThoiGian(DateTime.Now - ban.GioBatDau.Value)
                : ban.TrangThai;
            return ban.TenBan + Environment.NewLine + ban.LoaiBan + Environment.NewLine + dong3;
        }

        private void NutBan_Click(object sender, EventArgs e)
        {
            BanBidaDTO ban = (BanBidaDTO)((Button)sender).Tag;
            if (banDangChon != null && ban.MaBan == banDangChon.MaBan) return;
            if (!XuLyThayDoiChuaLuu()) return;
            ChonBan(ban);
        }

        private void ChonBan(BanBidaDTO ban)
        {
            banDangChon = ban;
            hoaDonDangMo = null;
            dtDichVu = null;

            foreach (Button b in nutBan.Values)
            {
                bool chon = ban != null && ((BanBidaDTO)b.Tag).MaBan == ban.MaBan;
                b.FlatAppearance.BorderColor = chon ? Color.Navy : Color.White;
                b.FlatAppearance.BorderSize = chon ? 5 : 1;
            }

            if (ban == null)
            {
                lblTenBan.Text = "(Chưa chọn bàn)";
                lblLoaiBan.Text = "Loại bàn: ";
                lblGia.Text = "Giá/giờ: ";
                lblTrangThai.Text = "Trạng thái: ";
                lblTrangThai.ForeColor = SystemColors.ControlText;
            }
            else
            {
                lblTenBan.Text = ban.TenBan;
                lblLoaiBan.Text = "Loại bàn: " + ban.LoaiBan;
                lblGia.Text = "Giá/giờ: " + ban.GiaTheoGio.ToString("N0") + " đ";
                lblTrangThai.Text = "Trạng thái: " + ban.TrangThai;
                lblTrangThai.ForeColor = ban.DangChoi ? MauDangChoi : MauTrong;

                if (ban.DangChoi)
                {
                    try
                    {
                        hoaDonDangMo = hoaDonBus.GetHoaDonDangMo(ban.MaBan);
                        if (hoaDonDangMo != null)
                            dtDichVu = dichVuBus.GetDichVuDaGoi(hoaDonDangMo.MaHD);
                    }
                    catch (Exception ex) { ShowError(ex); }
                }
            }

            dgvDichVu.DataSource = dtDichVu;
            CauHinhLuoiDichVu();

            bool coHoaDon = hoaDonDangMo != null;
            btnMoBan.Enabled = ban != null && !ban.DangChoi;
            cboDichVu.Enabled = nudSL.Enabled = btnThemMon.Enabled = coHoaDon;
            btnXoaMon.Enabled = btnLuuDichVu.Enabled = btnThanhToan.Enabled = coHoaDon;
            grpDichVu.Text = coHoaDon
                ? "Dịch vụ đã gọi – Hóa đơn #" + hoaDonDangMo.MaHD + " (SqlDataAdapter + DataTable)"
                : "Dịch vụ đã gọi (SqlDataAdapter + DataTable)";
            CapNhatTien();
        }

        private void CauHinhLuoiDichVu()
        {
            if (dtDichVu == null) return;
            foreach (DataGridViewColumn c in dgvDichVu.Columns) c.ReadOnly = true;
            dgvDichVu.Columns["MaHD"].Visible = false;
            dgvDichVu.Columns["MaDV"].Visible = false;
            dgvDichVu.Columns["TenDV"].HeaderText = "Dịch vụ";
            dgvDichVu.Columns["TenDV"].FillWeight = 200;
            dgvDichVu.Columns["SoLuong"].HeaderText = "Số lượng (sửa được)";
            dgvDichVu.Columns["SoLuong"].ReadOnly = false;
            dgvDichVu.Columns["SoLuong"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgvDichVu.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvDichVu.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvDichVu.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgvDichVu.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (Button b in nutBan.Values)
            {
                BanBidaDTO ban = (BanBidaDTO)b.Tag;
                if (ban.DangChoi) b.Text = TextNut(ban);
            }
            CapNhatTien();
        }

        private bool LamTronBlock15
        {
            get { return rdoBlock15.Checked; }
        }

        private void CapNhatTien()
        {
            decimal tienDV = DichVuBUS.TinhTienDichVu(dtDichVu);
            lblTienDV.Text = "Tiền dịch vụ: " + tienDV.ToString("N0") + " đ" +
                             (DichVuBUS.CoThayDoiChuaLuu(dtDichVu) ? "  (chưa lưu)" : "");

            if (hoaDonDangMo == null || banDangChon == null)
            {
                lblGioBatDau.Text = "Giờ bắt đầu: ";
                lblThoiGian.Text = "Thời gian chơi: ";
                lblTienGioTamTinh.Text = "Tiền giờ tạm tính: ";
                lblChiTietTien.Text = banDangChon == null ? "Chọn một bàn trên sơ đồ." :
                    (banDangChon.DangChoi ? "" : "Bàn đang trống – bấm \"MỞ BÀN\" để bắt đầu tính giờ.");
                return;
            }

            DateTime now = DateTime.Now;
            HoaDonDTO tam = hoaDonBus.TinhTien(new HoaDonDTO { GioBatDau = hoaDonDangMo.GioBatDau },
                banDangChon.GiaTheoGio, tienDV, now, LamTronBlock15);

            lblGioBatDau.Text = "Giờ bắt đầu: " + hoaDonDangMo.GioBatDau.ToString("HH:mm dd/MM");
            lblThoiGian.Text = "Thời gian chơi: " + HoaDonBUS.DinhDangThoiGian(now - hoaDonDangMo.GioBatDau);
            lblTienGioTamTinh.Text = "Tiền giờ tạm tính: " + tam.TienGioChoi.ToString("N0") + " đ";
            lblChiTietTien.Text =
                "Tính tiền: " + tam.SoPhutTinhTien + " phút (" + (LamTronBlock15 ? "block 15'" : "theo phút") + ")" +
                "  ×  " + banDangChon.GiaTheoGio.ToString("N0") + " đ/giờ" + Environment.NewLine +
                "Tiền giờ: " + tam.TienGioChoi.ToString("N0") + " đ    +    Dịch vụ: " + tienDV.ToString("N0") + " đ" + Environment.NewLine +
                "TỔNG TẠM TÍNH: " + tam.TongTien.ToString("N0") + " đ";
        }

        private void rdoCachTinh_CheckedChanged(object sender, EventArgs e)
        {
            CapNhatTien();
        }

        private void btnMoBan_Click(object sender, EventArgs e)
        {
            try
            {
                int maHD = banBus.MoBan(banDangChon);
                int maBan = banDangChon.MaBan;
                LoadSoDo(maBan);
                MessageBox.Show("Đã mở " + banDangChon.TenBan + " – Hóa đơn #" + maHD + "\nBắt đầu tính giờ từ " +
                                DateTime.Now.ToString("HH:mm:ss"), "Mở bàn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowError(ex);
                LoadSoDo(banDangChon == null ? (int?)null : banDangChon.MaBan);
            }
        }

        private void btnTaiLaiSoDo_Click(object sender, EventArgs e)
        {
            if (!XuLyThayDoiChuaLuu()) return;
            LoadMenu();
            LoadSoDo(banDangChon == null ? (int?)null : banDangChon.MaBan);
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            try
            {
                dichVuBus.ThemMon(dtDichVu, hoaDonDangMo == null ? 0 : hoaDonDangMo.MaHD,
                    cboDichVu.SelectedItem as MenuDichVuDTO, (int)nudSL.Value);
                nudSL.Value = 1;
            }
            catch (CanhBaoTonKhoException ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) { ShowError(ex); }
            CapNhatTien();
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            DataRowView drv = dgvDichVu.CurrentRow == null ? null : dgvDichVu.CurrentRow.DataBoundItem as DataRowView;
            if (drv == null) return;
            if (MessageBox.Show("Xóa món \"" + drv["TenDV"] + "\" khỏi hóa đơn?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            drv.Row.Delete();
            CapNhatTien();
        }

        private void btnLuuDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDichVu.EndEdit();
                int n = dichVuBus.LuuThayDoi(dtDichVu, hoaDonDangMo.MaHD);
                MessageBox.Show("Đã lưu " + n + " thay đổi dịch vụ vào CSDL (adapter.Update).", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
            CapNhatTien();
        }

        private void dgvDichVu_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dtDichVu == null || dgvDichVu.Columns[e.ColumnIndex].Name != "SoLuong") return;
            DataRowView drv = dgvDichVu.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (drv == null) return;
            if (drv["SoLuong"] == DBNull.Value || Convert.ToInt32(drv["SoLuong"]) <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0. Dùng nút \"Xóa món\" nếu muốn bỏ món.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                drv["SoLuong"] = 1;
            }
            CapNhatTien();
        }

        private void dgvDichVu_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Số lượng phải là số nguyên dương!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.ThrowException = false;
        }

        private bool XuLyThayDoiChuaLuu()
        {
            dgvDichVu.EndEdit();
            if (!DichVuBUS.CoThayDoiChuaLuu(dtDichVu) || hoaDonDangMo == null) return true;
            DialogResult kq = MessageBox.Show("Dịch vụ của " + banDangChon.TenBan + " có thay đổi chưa lưu. Lưu lại?",
                "Chưa lưu", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (kq == DialogResult.Cancel) return false;
            if (kq == DialogResult.Yes)
            {
                try { dichVuBus.LuuThayDoi(dtDichVu, hoaDonDangMo.MaHD); }
                catch (Exception ex) { ShowError(ex); return false; }
            }
            return true;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                if (hoaDonDangMo == null || banDangChon == null) throw new Exception("Vui lòng chọn bàn đang chơi!");

                dgvDichVu.EndEdit();
                if (DichVuBUS.CoThayDoiChuaLuu(dtDichVu))
                    dichVuBus.LuuThayDoi(dtDichVu, hoaDonDangMo.MaHD);

                decimal tienDV = DichVuBUS.TinhTienDichVu(dtDichVu);
                HoaDonDTO tam = hoaDonBus.TinhTien(new HoaDonDTO { GioBatDau = hoaDonDangMo.GioBatDau },
                    banDangChon.GiaTheoGio, tienDV, DateTime.Now, LamTronBlock15);

                string xacNhan =
                    banDangChon.TenBan + " – Hóa đơn #" + hoaDonDangMo.MaHD + Environment.NewLine +
                    "Bắt đầu: " + hoaDonDangMo.GioBatDau.ToString("HH:mm:ss") + "   Kết thúc: " + tam.GioKetThuc.Value.ToString("HH:mm:ss") + Environment.NewLine +
                    "Tính tiền: " + tam.SoPhutTinhTien + " phút × " + banDangChon.GiaTheoGio.ToString("N0") + " đ/giờ" + Environment.NewLine +
                    "Tiền giờ chơi: " + tam.TienGioChoi.ToString("N0") + " đ" + Environment.NewLine +
                    "Tiền dịch vụ: " + tienDV.ToString("N0") + " đ" + Environment.NewLine +
                    "--------------------------------" + Environment.NewLine +
                    "TỔNG CỘNG: " + tam.TongTien.ToString("N0") + " đ" + Environment.NewLine + Environment.NewLine +
                    "Xác nhận thanh toán?";
                if (MessageBox.Show(xacNhan, "Thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                int maBan = banDangChon.MaBan;
                HoaDonDTO kq = hoaDonBus.ThanhToan(hoaDonDangMo, banDangChon.GiaTheoGio, LamTronBlock15);
                MessageBox.Show("Thanh toán thành công!" + Environment.NewLine +
                                "Tiền giờ: " + kq.TienGioChoi.ToString("N0") + " đ" + Environment.NewLine +
                                "Tiền dịch vụ: " + kq.TienDichVu.ToString("N0") + " đ" + Environment.NewLine +
                                "TỔNG: " + kq.TongTien.ToString("N0") + " đ" + Environment.NewLine +
                                "Bàn đã được trả về trạng thái \"Trống\".",
                    "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMenu();
                LoadSoDo(maBan);
            }
            catch (Exception ex)
            {
                ShowError(ex);
                if (banDangChon != null) LoadSoDo(banDangChon.MaBan);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!XuLyThayDoiChuaLuu()) e.Cancel = true;
            else timer.Stop();
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
