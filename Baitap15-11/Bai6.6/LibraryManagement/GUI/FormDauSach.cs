using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LibraryManagement.BUS;
using LibraryManagement.DTO;

namespace LibraryManagement.GUI
{
    public partial class FormDauSach : Form
    {
        private readonly DauSachBUS bus = new DauSachBUS();
        private static readonly string[] TheLoaiMacDinh = { "Công nghệ thông tin", "Kinh tế", "Văn học" };

        public FormDauSach()
        {
            InitializeComponent();
        }

        private void FormDauSach_Load(object sender, EventArgs e)
        {
            LoadTheLoai();
            LoadData();
        }

        private void LoadTheLoai()
        {
            try
            {
                List<string> ds = new List<string>(TheLoaiMacDinh);
                foreach (string tl in bus.GetDanhSachTheLoai())
                    if (!ds.Contains(tl)) ds.Add(tl);
                cboTheLoai.Items.Clear();
                cboTheLoai.Items.AddRange(ds.ToArray());
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void LoadData()
        {
            try
            {
                dgvDauSach.DataSource = bus.TimKiem(txtTimKiem.Text);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private DauSachDTO GetInput()
        {
            int ma;
            int.TryParse(txtMaSach.Text, out ma);
            return new DauSachDTO(ma, txtTenSach.Text, cboTheLoai.Text, (int)nudSoLuong.Value);
        }

        private void ClearInput()
        {
            txtMaSach.Clear();
            txtTenSach.Clear();
            cboTheLoai.Text = "";
            nudSoLuong.Value = 0;
            txtTenSach.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DauSachDTO s = GetInput();
                s.MaSach = 0;
                if (bus.Them(s))
                {
                    MessageBox.Show("Thêm đầu sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTheLoai();
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
                    MessageBox.Show("Cập nhật đầu sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTheLoai();
                    LoadData();
                }
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DauSachDTO s = GetInput();
                if (s.MaSach <= 0) throw new Exception("Vui lòng chọn đầu sách cần xóa!");
                if (MessageBox.Show("Xóa đầu sách \"" + s.TenSach + "\"?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                if (bus.Xoa(s.MaSach))
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
            txtTimKiem.Clear();
            LoadData();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvDauSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DauSachDTO s = dgvDauSach.Rows[e.RowIndex].DataBoundItem as DauSachDTO;
            if (s == null) return;
            txtMaSach.Text = s.MaSach.ToString();
            txtTenSach.Text = s.TenSach;
            cboTheLoai.Text = s.TheLoai;
            nudSoLuong.Value = Math.Min(nudSoLuong.Maximum, s.SoLuongCon);
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
