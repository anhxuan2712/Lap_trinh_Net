using System;
using System.Drawing;
using System.Windows.Forms;
using ClinicManagement.BUS;
using ClinicManagement.DTO;

namespace ClinicManagement.GUI
{
    public partial class FormThuoc : Form
    {
        private readonly ThuocBUS bus = new ThuocBUS();

        public FormThuoc()
        {
            InitializeComponent();
        }

        private void FormThuoc_Load(object sender, EventArgs e)
        {
            cboDonViTinh.Items.AddRange(new object[] { "Viên", "Vỉ", "Chai", "Hộp", "Gói", "Tuýp", "Ống" });
            LoadData();
            ClearInput();
        }

        private void LoadData()
        {
            try
            {
                dgvThuoc.DataSource = bus.TimKiem(txtTimKiem.Text);
                if (dgvThuoc.Columns["DonGia"] != null) dgvThuoc.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private ThuocDTO GetInput()
        {
            int ma;
            int.TryParse(txtMaThuoc.Text, out ma);
            return new ThuocDTO(ma, txtTenThuoc.Text, cboDonViTinh.Text, nudDonGia.Value, (int)nudSoLuongTon.Value);
        }

        private void ClearInput()
        {
            txtMaThuoc.Clear();
            txtTenThuoc.Clear();
            cboDonViTinh.Text = "Viên";
            nudDonGia.Value = 0;
            nudSoLuongTon.Value = 0;
            nudSoLuongTon.Enabled = true;
            nudNhapThem.Value = 0;
            txtTenThuoc.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                ThuocDTO t = GetInput();
                t.MaThuoc = 0;
                if (bus.Them(t))
                {
                    MessageBox.Show("Thêm thuốc mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInput();
                }
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (bus.CapNhat(GetInput()))
                {
                    MessageBox.Show("Cập nhật thông tin / đơn giá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                ThuocDTO t = GetInput();
                if (t.MaThuoc <= 0) throw new Exception("Vui lòng chọn thuốc cần xóa!");
                if (MessageBox.Show("Xóa thuốc \"" + t.TenThuoc + "\"?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                if (bus.Xoa(t.MaThuoc))
                {
                    LoadData();
                    ClearInput();
                }
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            try
            {
                ThuocDTO t = GetInput();
                int sl = (int)nudNhapThem.Value;
                if (bus.NhapKho(t.MaThuoc, sl))
                {
                    MessageBox.Show("Đã nhập thêm " + sl + " " + t.DonViTinh + " \"" + t.TenThuoc + "\" vào kho.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    nudSoLuongTon.Value += sl;
                    nudNhapThem.Value = 0;
                    LoadData();
                }
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            ClearInput();
            LoadData();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ThuocDTO t = dgvThuoc.Rows[e.RowIndex].DataBoundItem as ThuocDTO;
            if (t == null) return;
            txtMaThuoc.Text = t.MaThuoc.ToString();
            txtTenThuoc.Text = t.TenThuoc;
            cboDonViTinh.Text = t.DonViTinh;
            nudDonGia.Value = Math.Min(nudDonGia.Maximum, t.DonGia);
            nudSoLuongTon.Value = Math.Min(nudSoLuongTon.Maximum, t.SoLuongTon);
            nudSoLuongTon.Enabled = false;
            dgvThuoc.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = t.SoLuongTon < 10 ? Color.IndianRed : SystemColors.Highlight;
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
