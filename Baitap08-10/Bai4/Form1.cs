using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Bai4
{
    public partial class Form1 : Form
    {
        // Màu sắc đại diện cho các trạng thái
        private readonly Color COLOR_TRONG = Color.White;
        private readonly Color COLOR_DANG_CHON = Color.LightGreen;
        private readonly Color COLOR_DA_DAT = Color.FromArgb(217, 83, 79); // Đỏ nhạt / IndianRed

        // Danh sách quản lý 20 nút vị trí
        private List<Button> danhSachGhe = new List<Button>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Cấu hình ComboBox khung giờ nếu chưa có
            if (cboKhungGio.Items.Count == 0)
            {
                cboKhungGio.Items.AddRange(new object[] { "Sáng - 100.000đ", "Tối - 150.000đ" });
            }
            cboKhungGio.SelectedIndex = 0;

            // 2. Khởi tạo / Liên kết 20 nút vị trí
            KhoiTaoSoDoViTri();

            // 3. Cập nhật thống kê ban đầu
            CapNhatThongKe();
        }

        private void KhoiTaoSoDoViTri()
        {
            danhSachGhe.Clear();

            // Trường hợp 1: Nếu đã có các Button trên Form/TableLayoutPanel/FlowLayoutPanel
            // (ví dụ tìm theo tiền tố tên hoặc duyệt Controls trong panelSoDo)
            Control container = pnlSoDo != null ? (Control)pnlSoDo : (Control)this;
            
            // Tìm tất cả các Button vị trí có sẵn
            for (int i = 1; i <= 20; i++)
            {
                Control[] found = container.Controls.Find("btnViTri" + i, true);
                if (found.Length == 0)
                    found = container.Controls.Find("btnSlot_" + i, true);
                if (found.Length == 0)
                    found = container.Controls.Find("button" + i, true);

                if (found.Length > 0 && found[0] is Button btn)
                {
                    btn.Click += BtnViTri_Click;
                    danhSachGhe.Add(btn);
                }
            }

            // Trường hợp 2: Nếu chưa tạo trên Designer, tự động sinh 20 Button vào Panel / TableLayoutPanel
            if (danhSachGhe.Count == 0 && pnlSoDo != null)
            {
                pnlSoDo.Controls.Clear();
                for (int i = 1; i <= 20; i++)
                {
                    Button btn = new Button
                    {
                        Name = "btnViTri" + i,
                        Text = "Vị trí " + i,
                        Size = new Size(110, 50),
                        Margin = new Padding(5),
                        BackColor = COLOR_TRONG,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                        Cursor = Cursors.Hand
                    };

                    btn.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
                    btn.FlatAppearance.BorderSize = 1;
                    btn.Click += BtnViTri_Click;

                    // Giả lập một số vị trí đã đặt trước (vị trí 3, 8, 13, 14, 18, 19 như trong ảnh mẫu)
                    if (i == 3 || i == 8 || i == 13 || i == 14 || i == 18 || i == 19)
                    {
                        btn.BackColor = COLOR_DA_DAT;
                        btn.ForeColor = Color.White;
                    }

                    pnlSoDo.Controls.Add(btn);
                    danhSachGhe.Add(btn);
                }
            }
            else
            {
                // Nếu đã có sẵn trên Form, kiểm tra và gán màu mặc định nếu chưa đặt
                foreach (Button btn in danhSachGhe)
                {
                    if (btn.BackColor != COLOR_DA_DAT && btn.BackColor != COLOR_DANG_CHON)
                    {
                        btn.BackColor = COLOR_TRONG;
                    }
                }
            }
        }

        // Sự kiện Click dùng chung cho toàn bộ 20 Button vị trí
        private void BtnViTri_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                // Kiểm tra nếu vị trí đã bị khóa / đã đặt
                if (btn.BackColor == COLOR_DA_DAT)
                {
                    MessageBox.Show("Vị trí này đã có người đặt hoặc đã bị khóa.", "Vị trí không khả dụng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chuyển đổi trạng thái giữa Đang chọn <-> Trống
                if (btn.BackColor == COLOR_DANG_CHON)
                {
                    btn.BackColor = COLOR_TRONG;
                    btn.ForeColor = Color.Black;
                }
                else
                {
                    btn.BackColor = COLOR_DANG_CHON;
                    btn.ForeColor = Color.Black;
                }

                // Cập nhật lại số lượng và tiền
                CapNhatThongKe();
            }
        }

        // Lấy đơn giá hiện tại dựa trên khung giờ
        private double LayDonGiaKhungGio()
        {
            if (cboKhungGio.SelectedIndex == 1) // Tối - 150.000đ
                return 150000;
            return 100000; // Sáng - 100.000đ (mặc định)
        }

        // Cập nhật số vị trí và tạm tính tiền
        private void CapNhatThongKe()
        {
            int soLuongChon = 0;
            foreach (Button btn in danhSachGhe)
            {
                if (btn.BackColor == COLOR_DANG_CHON)
                {
                    soLuongChon++;
                }
            }

            double donGia = LayDonGiaKhungGio();
            double tamTinh = soLuongChon * donGia;

            lblSoViTri.Text = $"Số vị trí đang chọn: {soLuongChon}";
            lblTamTinh.Text = $"Tạm tính tiền: {tamTinh:N0}đ";
        }

        // Khi thay đổi ComboBox Khung giờ
        private void cboKhungGio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatThongKe();
        }

        // Nút "Hủy chọn tất cả"
        private void btnHuyChonTatCa_Click(object sender, EventArgs e)
        {
            foreach (Button btn in danhSachGhe)
            {
                if (btn.BackColor == COLOR_DANG_CHON)
                {
                    btn.BackColor = COLOR_TRONG;
                    btn.ForeColor = Color.Black;
                }
            }

            CapNhatThongKe();
        }

        // Nút "Xác nhận đặt"
        private void btnXacNhanDat_Click(object sender, EventArgs e)
        {
            int soLuongChon = 0;
            foreach (Button btn in danhSachGhe)
            {
                if (btn.BackColor == COLOR_DANG_CHON)
                    soLuongChon++;
            }

            if (soLuongChon == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một vị trí để đặt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double donGia = LayDonGiaKhungGio();
            double tongTien = soLuongChon * donGia;

            // Hộp thoại xác nhận đặt bàn
            DialogResult dr = MessageBox.Show(
                $"Xác nhận đặt {soLuongChon} vị trí với số tiền {tongTien:#,##0}đ?",
                "Xác nhận đặt bàn",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (dr == DialogResult.Yes)
            {
                // Chuyển toàn bộ các vị trí đã chọn sang trạng thái Đã đặt (Đỏ)
                foreach (Button btn in danhSachGhe)
                {
                    if (btn.BackColor == COLOR_DANG_CHON)
                    {
                        btn.BackColor = COLOR_DA_DAT;
                        btn.ForeColor = Color.White;
                    }
                }

                // Reset số lượng đang chọn và tạm tính tiền về 0
                CapNhatThongKe();

                // Thông báo hoàn tất
                MessageBox.Show("Đặt vị trí thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
