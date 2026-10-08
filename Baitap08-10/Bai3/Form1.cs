using System;
using System.Windows.Forms;

namespace Bai3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Thiết lập ComboBox Đơn vị tính nếu chưa có
            if (cboDVT.Items.Count == 0)
            {
                cboDVT.Items.AddRange(new object[] { "Cái", "Bộ", "Kg", "Mét" });
            }
            cboDVT.SelectedIndex = 0;

            // Thiết lập cấu hình cho ListView nếu chưa cấu hình trên Designer
            lsvVatTu.View = View.Details;
            lsvVatTu.FullRowSelect = true;
            lsvVatTu.GridLines = true;
            lsvVatTu.MultiSelect = false;

            if (lsvVatTu.Columns.Count == 0)
            {
                lsvVatTu.Columns.Add("Mã VT", 100);
                lsvVatTu.Columns.Add("Tên VT", 150);
                lsvVatTu.Columns.Add("Đơn vị tính", 100);
                lsvVatTu.Columns.Add("Đơn giá", 100);
            }
        }

        // Kiểm tra xem Mã VT đã tồn tại trong ListView hay chưa
        private bool KiemTraTrungMa(string maVT, ListViewItem boQuaItem = null)
        {
            foreach (ListViewItem item in lsvVatTu.Items)
            {
                if (boQuaItem != null && item == boQuaItem)
                    continue;

                if (string.Equals(item.Text.Trim(), maVT.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        // Validate form nhập liệu
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtMaVT.Text))
            {
                MessageBox.Show("Vui lòng nhập mã vật tư.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaVT.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenVT.Text))
            {
                MessageBox.Show("Vui lòng nhập tên vật tư.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenVT.Focus();
                return false;
            }

            if (cboDVT.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đơn vị tính.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDVT.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDonGia.Text) || !double.TryParse(txtDonGia.Text.Trim(), out double gia) || gia < 0)
            {
                MessageBox.Show("Đơn giá phải là số hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                txtDonGia.SelectAll();
                return false;
            }

            return true;
        }

        // Nút Thêm mới
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            string maVT = txtMaVT.Text.Trim();

            // Kiểm tra trùng mã
            if (KiemTraTrungMa(maVT))
            {
                MessageBox.Show("Mã vật tư đã tồn tại trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaVT.Focus();
                txtMaVT.SelectAll();
                return;
            }

            // Tạo item mới
            ListViewItem lvi = new ListViewItem(maVT);
            lvi.SubItems.Add(txtTenVT.Text.Trim());
            lvi.SubItems.Add(cboDVT.SelectedItem.ToString());
            lvi.SubItems.Add(txtDonGia.Text.Trim());

            lsvVatTu.Items.Add(lvi);

            // Làm mới các ô nhập liệu
            XoaTrangInput();
            txtMaVT.Focus();
        }

        // Nút Cập nhật
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (lsvVatTu.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateForm()) return;

            ListViewItem selectedItem = lsvVatTu.SelectedItems[0];
            string maVT = txtMaVT.Text.Trim();

            // Kiểm tra trùng mã với các dòng khác
            if (KiemTraTrungMa(maVT, selectedItem))
            {
                MessageBox.Show("Mã vật tư đã tồn tại trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaVT.Focus();
                txtMaVT.SelectAll();
                return;
            }

            // Cập nhật thông tin
            selectedItem.Text = maVT;
            selectedItem.SubItems[1].Text = txtTenVT.Text.Trim();
            selectedItem.SubItems[2].Text = cboDVT.SelectedItem.ToString();
            selectedItem.SubItems[3].Text = txtDonGia.Text.Trim();

            MessageBox.Show("Cập nhật vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Nút Xóa dòng
        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (lsvVatTu.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                lsvVatTu.Items.Remove(lsvVatTu.SelectedItems[0]);
                XoaTrangInput();
            }
        }

        // Nút Xóa toàn bộ
        private void btnXoaToanBo_Click(object sender, EventArgs e)
        {
            if (lsvVatTu.Items.Count == 0)
            {
                MessageBox.Show("Danh sách đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa toàn bộ danh sách?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                lsvVatTu.Items.Clear();
                XoaTrangInput();
            }
        }

        // Sự kiện khi click / chọn dòng trong ListView
        private void lsvVatTu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvVatTu.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvVatTu.SelectedItems[0];
                txtMaVT.Text = item.Text;
                txtTenVT.Text = item.SubItems[1].Text;
                cboDVT.SelectedItem = item.SubItems[2].Text;
                txtDonGia.Text = item.SubItems[3].Text;
            }
        }

        private void XoaTrangInput()
        {
            txtMaVT.Clear();
            txtTenVT.Clear();
            txtDonGia.Clear();
            if (cboDVT.Items.Count > 0)
                cboDVT.SelectedIndex = 0;
        }
    }
}
