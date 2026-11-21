using System;
using System.Drawing;
using System.Windows.Forms;
using LibraryManagement.BUS;
using LibraryManagement.DTO;

namespace LibraryManagement.GUI
{
    public partial class FormDocGia : Form
    {
        private readonly DocGiaBUS bus = new DocGiaBUS();

        public FormDocGia()
        {
            InitializeComponent();
        }

        private void FormDocGia_Load(object sender, EventArgs e)
        {
            dtpNgayHetHan.Value = DateTime.Today.AddYears(1);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgvDocGia.DataSource = bus.GetAll();
                if (dgvDocGia.Columns["NgayHetHanThe"] != null)
                    dgvDocGia.Columns["NgayHetHanThe"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private DocGiaDTO GetInput()
        {
            int ma;
            int.TryParse(txtMaDocGia.Text, out ma);
            return new DocGiaDTO(ma, txtHoTen.Text, txtEmail.Text, dtpNgayHetHan.Value.Date.AddDays(1).AddSeconds(-1));
        }

        private void ClearInput()
        {
            txtMaDocGia.Clear();
            txtHoTen.Clear();
            txtEmail.Clear();
            dtpNgayHetHan.Value = DateTime.Today.AddYears(1);
            lblTinhTrangThe.Text = "";
            txtHoTen.Focus();
        }

        private void HienThiTinhTrangThe(DocGiaDTO dg)
        {
            if (dg.TheConHan)
            {
                lblTinhTrangThe.Text = "Thẻ còn hạn";
                lblTinhTrangThe.ForeColor = Color.SeaGreen;
            }
            else
            {
                lblTinhTrangThe.Text = "Thẻ hết hạn";
                lblTinhTrangThe.ForeColor = Color.Firebrick;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DocGiaDTO dg = GetInput();
                dg.MaDocGia = 0;
                if (bus.Them(dg))
                {
                    MessageBox.Show("Thêm độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (bus.Sua(GetInput()))
                {
                    MessageBox.Show("Cập nhật độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DocGiaDTO dg = GetInput();
                if (dg.MaDocGia <= 0) throw new Exception("Vui lòng chọn độc giả cần xóa!");
                if (MessageBox.Show("Xóa độc giả \"" + dg.HoTen + "\"?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                if (bus.Xoa(dg.MaDocGia))
                {
                    LoadData();
                    ClearInput();
                }
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInput();
            LoadData();
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            try
            {
                DocGiaDTO dg = GetInput();
                if (dg.MaDocGia <= 0) throw new Exception("Vui lòng chọn độc giả cần gia hạn!");
                DateTime moc = dtpNgayHetHan.Value.Date < DateTime.Today ? DateTime.Today : dtpNgayHetHan.Value.Date;
                dtpNgayHetHan.Value = moc.AddYears(1);
                if (bus.Sua(GetInput()))
                {
                    MessageBox.Show("Đã gia hạn thẻ đến " + dtpNgayHetHan.Value.ToString("dd/MM/yyyy"), "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiTinhTrangThe(GetInput());
                    LoadData();
                }
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DocGiaDTO dg = dgvDocGia.Rows[e.RowIndex].DataBoundItem as DocGiaDTO;
            if (dg == null) return;
            txtMaDocGia.Text = dg.MaDocGia.ToString();
            txtHoTen.Text = dg.HoTen;
            txtEmail.Text = dg.Email;
            dtpNgayHetHan.Value = dg.NgayHetHanThe.HasValue ? dg.NgayHetHanThe.Value : DateTime.Today;
            HienThiTinhTrangThe(dg);
        }

        private void dgvDocGia_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DocGiaDTO dg = dgvDocGia.Rows[e.RowIndex].DataBoundItem as DocGiaDTO;
            if (dg != null && !dg.TheConHan)
                e.CellStyle.ForeColor = Color.Firebrick;
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
