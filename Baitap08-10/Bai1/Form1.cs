using System;
using System.Windows.Forms;

namespace Bai1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Thiết lập trạng thái ban đầu
            txtTongTien.ReadOnly = true;
            txtDonGia.Focus();
        }

        // Sự kiện click nút "Tính tiền"
        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            // 1. Validate Đơn giá dịch vụ
            if (string.IsNullOrWhiteSpace(txtDonGia.Text) || !double.TryParse(txtDonGia.Text.Trim(), out double donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá dịch vụ phải là số hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDonGia.Focus();
                txtDonGia.SelectAll();
                return;
            }

            // 2. Validate Số lượng khách
            if (string.IsNullOrWhiteSpace(txtSoLuong.Text) || !int.TryParse(txtSoLuong.Text.Trim(), out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng khách phải là số!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoLuong.Focus();
                txtSoLuong.SelectAll();
                return;
            }

            // 3. Validate % Giảm giá
            double phanTramGiam = 0;
            if (!string.IsNullOrWhiteSpace(txtGiamGia.Text))
            {
                if (!double.TryParse(txtGiamGia.Text.Trim(), out phanTramGiam) || phanTramGiam < 0 || phanTramGiam > 100)
                {
                    MessageBox.Show("Phần trăm giảm giá phải là số từ 0 đến 100!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGiamGia.Focus();
                    txtGiamGia.SelectAll();
                    return;
                }
            }

            // 4. Tính toán tổng tiền theo công thức: (Đơn giá * Số lượng) * (100 - % Giảm) / 100
            double tongTien = (donGia * soLuong) * ((100.0 - phanTramGiam) / 100.0);

            // 5. Hiển thị kết quả (định dạng số có dấu phân cách hàng nghìn)
            txtTongTien.Text = tongTien.ToString("N2"); // hoặc tongTien.ToString("#,##0")
        }

        // Sự kiện click nút "Làm mới"
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtGiamGia.Clear();
            txtTongTien.Clear();
            txtDonGia.Focus();
        }
    }
}
