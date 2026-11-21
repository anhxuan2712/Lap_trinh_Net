using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ClinicManagement.BUS;
using ClinicManagement.DTO;

namespace ClinicManagement.GUI
{
    public partial class FormKeDon : Form
    {
        private readonly HoSoKhamBUS hoSoBus = new HoSoKhamBUS();
        private readonly ThuocBUS thuocBus = new ThuocBUS();
        private readonly DonThuocBUS donBus = new DonThuocBUS();

        private readonly BindingList<ChiTietDonThuocDTO> donTam = new BindingList<ChiTietDonThuocDTO>();

        public FormKeDon()
        {
            InitializeComponent();
        }

        private void FormKeDon_Load(object sender, EventArgs e)
        {
            dgvDonThuoc.DataSource = donTam;
            FormatGridDon();
            nudTienKham.ValueChanged += (s, ev) => CapNhatTongTien();
            LoadHoSo();
            LoadThuoc();
            CapNhatTongTien();
        }

        private void FormatGridDon()
        {
            if (dgvDonThuoc.Columns["DonGia"] != null) dgvDonThuoc.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            if (dgvDonThuoc.Columns["ThanhTien"] != null) dgvDonThuoc.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            if (dgvDonThuoc.Columns["LieuDung"] != null) dgvDonThuoc.Columns["LieuDung"].FillWeight = 200;
        }

        private void LoadHoSo()
        {
            try
            {
                dgvHoSoCho.DataSource = hoSoBus.GetChoKeDon();
                if (dgvHoSoCho.Columns["NgayKham"] != null)
                    dgvHoSoCho.Columns["NgayKham"].DefaultCellStyle.Format = "dd/MM HH:mm";
                if (dgvHoSoCho.Columns["TrieuChung"] != null) dgvHoSoCho.Columns["TrieuChung"].Visible = false;
                if (dgvHoSoCho.Columns["ChanDoan"] != null) dgvHoSoCho.Columns["ChanDoan"].Visible = false;
                txtTrieuChungXem.Clear();
                txtChanDoan.Clear();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void LoadThuoc()
        {
            try
            {
                cboThuoc.DataSource = thuocBus.GetAll();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private HoSoKhamDTO HoSoDangChon()
        {
            return dgvHoSoCho.CurrentRow == null ? null : dgvHoSoCho.CurrentRow.DataBoundItem as HoSoKhamDTO;
        }

        private void dgvHoSoCho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HoSoKhamDTO hs = HoSoDangChon();
            if (hs == null) return;
            txtTrieuChungXem.Text = hs.TrieuChung;
            txtChanDoan.Text = hs.ChanDoan;
            grpKeDon.Text = "Kê đơn thuốc – Hồ sơ #" + hs.MaHoSo + " – " + hs.HoTenBN;
        }

        private void cboThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThuocDTO t = cboThuoc.SelectedItem as ThuocDTO;
            lblTonKho.Text = t == null ? "" : "Tồn: " + t.SoLuongTon + " " + t.DonViTinh;
        }

        private void btnThemThuoc_Click(object sender, EventArgs e)
        {
            ThuocDTO t = cboThuoc.SelectedItem as ThuocDTO;
            if (t == null) { ShowError(new Exception("Vui lòng chọn thuốc!")); return; }

            int soLuong = (int)nudSoLuong.Value;
            ChiTietDonThuocDTO daCo = donTam.FirstOrDefault(x => x.MaThuoc == t.MaThuoc);
            if (daCo != null)
            {
                daCo.SoLuong += soLuong;
                if (txtLieuDung.Text.Trim() != "") daCo.LieuDung = txtLieuDung.Text.Trim();
                donTam.ResetBindings();
            }
            else
            {
                donTam.Add(new ChiTietDonThuocDTO
                {
                    MaThuoc = t.MaThuoc,
                    TenThuoc = t.TenThuoc,
                    DonViTinh = t.DonViTinh,
                    DonGia = t.DonGia,
                    SoLuong = soLuong,
                    LieuDung = txtLieuDung.Text.Trim()
                });
                FormatGridDon();
            }

            int tong = donTam.First(x => x.MaThuoc == t.MaThuoc).SoLuong;
            if (tong > t.SoLuongTon)
                MessageBox.Show("Lưu ý: \"" + t.TenThuoc + "\" chỉ còn " + t.SoLuongTon + " " + t.DonViTinh +
                                " trong kho. Đơn sẽ bị hủy (Rollback) nếu lưu với số lượng " + tong + ".",
                    "Cảnh báo tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            txtLieuDung.Clear();
            nudSoLuong.Value = 1;
            CapNhatTongTien();
        }

        private void btnXoaThuoc_Click(object sender, EventArgs e)
        {
            if (dgvDonThuoc.CurrentRow == null) return;
            ChiTietDonThuocDTO ct = dgvDonThuoc.CurrentRow.DataBoundItem as ChiTietDonThuocDTO;
            if (ct != null) donTam.Remove(ct);
            CapNhatTongTien();
        }

        private void CapNhatTongTien()
        {
            decimal tong = DonThuocBUS.TinhTongTien(nudTienKham.Value, donTam);
            lblTongTien.Text = "Tổng chi phí dự kiến: " + tong.ToString("N0") + " đ";
        }

        private void btnLuuXuatDon_Click(object sender, EventArgs e)
        {
            HoSoKhamDTO hs = HoSoDangChon();
            try
            {
                if (hs == null) throw new Exception("Vui lòng chọn hồ sơ bệnh nhân cần kê đơn!");
                int maDon = donBus.LuuVaXuatDon(hs.MaHoSo, txtChanDoan.Text, nudTienKham.Value, donTam.ToList());
                MessageBox.Show("Lưu & xuất đơn thành công!\nMã đơn: " + maDon +
                                "\nTồn kho thuốc đã được trừ tương ứng.\nĐơn chuyển sang \"" + TrangThaiDon.ChoLayThuoc + "\".",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                donTam.Clear();
                grpKeDon.Text = "Kê đơn thuốc";
                LoadHoSo();
                LoadThuoc();
                CapNhatTongTien();
            }
            catch (ThieuThuocException ex)
            {
                MessageBox.Show(ex.Message, "Không đủ thuốc – Đã Rollback", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadThuoc();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
