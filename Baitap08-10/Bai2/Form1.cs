using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Bai2
{
    public partial class Form1 : Form
    {
        private string duongDanAnh = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Thiết lập giá trị mặc định cho ComboBox nếu chưa có trên Designer
            if (cboLoaiSuCo.Items.Count == 0)
            {
                cboLoaiSuCo.Items.AddRange(new object[] { "Phần cứng", "Phần mềm", "Mạng", "Tài khoản" });
            }
            cboLoaiSuCo.SelectedIndex = 0;

            // Thiết lập mặc định
            radThap.Checked = true;
            dtpNgayGhiNhan.Value = DateTime.Now;
            picAnhLoi.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        // Sự kiện Tải ảnh lỗi
        private void btnTaiAnhLoi_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh chụp lỗi";
                ofd.Filter = "Tệp hình ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Tất cả tệp (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    duongDanAnh = ofd.FileName;
                    picAnhLoi.Image = Image.FromFile(duongDanAnh);
                }
            }
        }

        // Sự kiện Gửi yêu cầu
        private void btnGuiYeuCau_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra thông tin bắt buộc
            if (string.IsNullOrWhiteSpace(txtMaPhieu.Text))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhieu.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNguoiYeuCau.Text))
            {
                MessageBox.Show("Vui lòng nhập người yêu cầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNguoiYeuCau.Focus();
                return;
            }

            if (cboLoaiSuCo.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại sự cố!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiSuCo.Focus();
                return;
            }

            // 2. Lấy mức độ ưu tiên
            string mucDoUuTien = "Thấp";
            if (radTrungBinh.Checked)
                mucDoUuTien = "Trung bình";
            else if (radKhanCap.Checked)
                mucDoUuTien = "Khẩn cấp";

            // 3. Gom danh sách thiết bị ảnh hưởng
            List<string> thietBiList = new List<string>();
            if (chkMayTinhBan.Checked) thietBiList.Add("Máy tính bàn");
            if (chkLaptop.Checked) thietBiList.Add("Laptop");
            if (chkMayIn.Checked) thietBiList.Add("Máy in");
            if (chkDienThoai.Checked) thietBiList.Add("Điện thoại");

            string danhSachThietBi = thietBiList.Count > 0 ? string.Join(", ", thietBiList) : "Không có";

            // 4. Trạng thái ảnh lỗi
            string trangThaiAnh = picAnhLoi.Image != null ? (string.IsNullOrEmpty(duongDanAnh) ? "Đã tải ảnh" : duongDanAnh) : "Chưa tải ảnh";

            // 5. Tạo nội dung tóm tắt hiển thị
            string thongBao = "THÔNG TIN PHIẾU HỖ TRỢ\n\n" +
                             $"Mã phiếu: {txtMaPhieu.Text.Trim()}\n" +
                             $"Người yêu cầu: {txtNguoiYeuCau.Text.Trim()}\n" +
                             $"Ngày ghi nhận: {dtpNgayGhiNhan.Value:dd/MM/yyyy}\n" +
                             $"Mức độ ưu tiên: {mucDoUuTien}\n" +
                             $"Loại sự cố: {cboLoaiSuCo.SelectedItem}\n" +
                             $"Thiết bị ảnh hưởng: {danhSachThietBi}\n" +
                             $"Ảnh chụp lỗi: {trangThaiAnh}";

            MessageBox.Show(thongBao, "Tóm tắt yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Sự kiện Nhập lại
        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            txtMaPhieu.Clear();
            txtNguoiYeuCau.Clear();
            dtpNgayGhiNhan.Value = DateTime.Now;
            radThap.Checked = true;

            if (cboLoaiSuCo.Items.Count > 0)
                cboLoaiSuCo.SelectedIndex = 0;

            chkMayTinhBan.Checked = false;
            chkLaptop.Checked = false;
            chkMayIn.Checked = false;
            chkDienThoai.Checked = false;

            if (picAnhLoi.Image != null)
            {
                picAnhLoi.Image.Dispose();
                picAnhLoi.Image = null;
            }
            duongDanAnh = "";

            txtMaPhieu.Focus();
        }
    }
}
