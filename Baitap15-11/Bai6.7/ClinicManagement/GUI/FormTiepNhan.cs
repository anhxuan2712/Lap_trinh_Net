using System;
using System.Drawing;
using System.Windows.Forms;
using ClinicManagement.BUS;
using ClinicManagement.DTO;

namespace ClinicManagement.GUI
{
    public partial class FormTiepNhan : Form
    {
        private readonly BenhNhanBUS benhNhanBus = new BenhNhanBUS();
        private readonly HoSoKhamBUS hoSoBus = new HoSoKhamBUS();

        public FormTiepNhan()
        {
            InitializeComponent();
        }

        private void FormTiepNhan_Load(object sender, EventArgs e)
        {
            AcceptButton = btnTraCuu;
            XoaThongTin();
        }

        private void XoaThongTin()
        {
            txtMaBN.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            nudNamSinh.Value = 1990;
            txtTrieuChung.Clear();
            dgvHoSo.DataSource = null;
            btnCapNhatBN.Enabled = false;
            btnLuuBN.Enabled = true;
        }

        private BenhNhanDTO GetInput()
        {
            int ma;
            int.TryParse(txtMaBN.Text, out ma);
            return new BenhNhanDTO(ma, txtHoTen.Text, txtSDT.Text, (int)nudNamSinh.Value);
        }

        private void HienThiBenhNhan(BenhNhanDTO bn)
        {
            txtMaBN.Text = bn.MaBN.ToString();
            txtHoTen.Text = bn.HoTen;
            txtSDT.Text = bn.SoDienThoai;
            nudNamSinh.Value = bn.NamSinh.HasValue ? Math.Max(nudNamSinh.Minimum, Math.Min(nudNamSinh.Maximum, bn.NamSinh.Value)) : 1990;
            btnCapNhatBN.Enabled = true;
            btnLuuBN.Enabled = false;
            LoadLichSu(bn.MaBN);
        }

        private void LoadLichSu(int maBN)
        {
            try
            {
                dgvHoSo.DataSource = hoSoBus.GetByBenhNhan(maBN);
                if (dgvHoSo.Columns["NgayKham"] != null)
                    dgvHoSo.Columns["NgayKham"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            try
            {
                BenhNhanDTO bn = benhNhanBus.TraCuuTheoSDT(txtSDTTra.Text);
                if (bn != null)
                {
                    lblKetQuaTra.Text = "Đã tìm thấy bệnh nhân: " + bn.HoTen;
                    lblKetQuaTra.ForeColor = Color.SeaGreen;
                    HienThiBenhNhan(bn);
                }
                else
                {
                    lblKetQuaTra.Text = "Bệnh nhân mới – vui lòng nhập thông tin.";
                    lblKetQuaTra.ForeColor = Color.DarkOrange;
                    XoaThongTin();
                    txtSDT.Text = BenhNhanBUS.ChuanHoaSDT(txtSDTTra.Text);
                    txtHoTen.Focus();
                }
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnNhapMoi_Click(object sender, EventArgs e)
        {
            XoaThongTin();
            lblKetQuaTra.Text = "";
            txtHoTen.Focus();
        }

        private void btnLuuBN_Click(object sender, EventArgs e)
        {
            try
            {
                BenhNhanDTO bn = GetInput();
                int ma = benhNhanBus.Them(bn);
                bn.MaBN = ma;
                MessageBox.Show("Đã thêm bệnh nhân mới. Mã BN: " + ma, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HienThiBenhNhan(bn);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnCapNhatBN_Click(object sender, EventArgs e)
        {
            try
            {
                if (benhNhanBus.Sua(GetInput()))
                    MessageBox.Show("Cập nhật thông tin bệnh nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnTaoHoSo_Click(object sender, EventArgs e)
        {
            try
            {
                BenhNhanDTO bn = GetInput();
                bool laBenhNhanMoi = bn.MaBN <= 0;
                int maHoSo = hoSoBus.TiepNhan(bn, txtTrieuChung.Text);
                MessageBox.Show(
                    (laBenhNhanMoi ? "Đã thêm bệnh nhân mới (Mã BN: " + bn.MaBN + ").\n" : "") +
                    "Tạo hồ sơ khám thành công! Mã hồ sơ: " + maHoSo + "\nBệnh nhân chuyển sang chờ khám & kê đơn.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTrieuChung.Clear();
                HienThiBenhNhan(bn);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
