using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bai5
{
    public partial class Form1 : Form
    {
        private Timer timerHeThong;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Kích hoạt nhận phím tắt trên toàn Form
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            // 2. Cấu hình ComboBox Loại vận chuyển nếu chưa có
            if (cboLoaiVanChuyen.Items.Count == 0)
            {
                cboLoaiVanChuyen.Items.AddRange(new object[] {
                    "Tiêu chuẩn (3-5 ngày)",
                    "Nhanh (1-2 ngày)",
                    "Hỏa tốc (trong ngày)"
                });
            }
            cboLoaiVanChuyen.SelectedIndex = 0;

            // 3. Cấu hình DataGridView
            CauHinhDataGridView();

            // 4. Khởi tạo Timer đồng hồ thời gian thực
            KhoiTaoTimer();

            // 5. Cập nhật StatusStrip ban đầu
            TinhTongVaCapNhatStatusStrip();
        }

        private void CauHinhDataGridView()
        {
            // Nếu chưa thiết lập các cột trên Designer thì thêm tự động
            if (dgvHangHoa.Columns.Count == 0)
            {
                dgvHangHoa.Columns.Add("colTenHang", "Tên hàng");
                dgvHangHoa.Columns.Add("colSoLuong", "Số lượng");
                dgvHangHoa.Columns.Add("colTrongLuong", "Trọng lượng (kg)");
                dgvHangHoa.Columns.Add("colDonGia", "Đơn giá (VNĐ)");
                dgvHangHoa.Columns.Add("colThanhTien", "Thành tiền (VNĐ)");
            }

            // Đặt cột Thành tiền ở chế độ chỉ đọc (tự động tính)
            if (dgvHangHoa.Columns.Contains("colThanhTien"))
            {
                dgvHangHoa.Columns["colThanhTien"].ReadOnly = true;
                dgvHangHoa.Columns["colThanhTien"].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            }

            dgvHangHoa.AllowUserToAddRows = true;
            dgvHangHoa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHangHoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Gắn sự kiện xử lý
            dgvHangHoa.CellValueChanged += DgvHangHoa_CellValueChanged;
            dgvHangHoa.CellValidating += DgvHangHoa_CellValidating;
            dgvHangHoa.RowsRemoved += DgvHangHoa_RowsRemoved;
        }

        private void KhoiTaoTimer()
        {
            timerHeThong = new Timer
            {
                Interval = 1000 // 1 giây
            };
            timerHeThong.Tick += (s, ev) =>
            {
                lblThoiGian.Text = $"Thời gian: {DateTime.Now:HH:mm:ss}";
            };
            timerHeThong.Start();
            lblThoiGian.Text = $"Thời gian: {DateTime.Now:HH:mm:ss}";
        }

        // Validate ô dữ liệu khi người dùng rời ô
        private void DgvHangHoa_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Bỏ qua dòng mới đang thêm
            if (dgvHangHoa.Rows[e.RowIndex].IsNewRow) return;

            string colName = dgvHangHoa.Columns[e.ColumnIndex].Name;
            string valueStr = e.FormattedValue?.ToString()?.Trim();

            // Kiểm tra cột Số lượng (phải là số nguyên > 0)
            if (colName == "colSoLuong" || colName.ToLower().Contains("soluong"))
            {
                if (!string.IsNullOrEmpty(valueStr))
                {
                    if (!int.TryParse(valueStr, out int sl) || sl <= 0)
                    {
                        dgvHangHoa.Rows[e.RowIndex].ErrorText = "Số lượng phải là số nguyên > 0!";
                        errorProvider1.SetError(dgvHangHoa, "Số lượng phải là số nguyên > 0!");
                    }
                    else
                    {
                        dgvHangHoa.Rows[e.RowIndex].ErrorText = string.Empty;
                        errorProvider1.SetError(dgvHangHoa, string.Empty);
                    }
                }
            }
            // Kiểm tra cột Trọng lượng (phải là số > 0)
            else if (colName == "colTrongLuong" || colName.ToLower().Contains("trongluong"))
            {
                if (!string.IsNullOrEmpty(valueStr))
                {
                    if (!double.TryParse(valueStr, out double tl) || tl <= 0)
                    {
                        dgvHangHoa.Rows[e.RowIndex].ErrorText = "Trọng lượng phải lớn hơn 0 kg!";
                        errorProvider1.SetError(dgvHangHoa, "Trọng lượng phải lớn hơn 0 kg!");
                    }
                    else
                    {
                        dgvHangHoa.Rows[e.RowIndex].ErrorText = string.Empty;
                        errorProvider1.SetError(dgvHangHoa, string.Empty);
                    }
                }
            }
        }

        // Tự động tính Thành tiền của dòng và tính tổng StatusStrip
        private void DgvHangHoa_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHangHoa.Rows.Count) return;

            DataGridViewRow row = dgvHangHoa.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            // Lấy giá trị Số lượng và Đơn giá
            int soLuong = 0;
            double donGia = 0;

            var valSoLuong = row.Cells["colSoLuong"]?.Value ?? row.Cells[1]?.Value;
            var valDonGia = row.Cells["colDonGia"]?.Value ?? row.Cells[3]?.Value;

            if (valSoLuong != null) int.TryParse(valSoLuong.ToString().Trim(), out soLuong);
            if (valDonGia != null) double.TryParse(valDonGia.ToString().Trim(), out donGia);

            // Thành tiền = Số lượng * Đơn giá
            double thanhTien = soLuong * donGia;

            // Gán vào ô Thành tiền (cột 4)
            if (dgvHangHoa.Columns.Contains("colThanhTien"))
                row.Cells["colThanhTien"].Value = thanhTien.ToString("N0");
            else if (row.Cells.Count > 4)
                row.Cells[4].Value = thanhTien.ToString("N0");

            // Cập nhật lại toàn bộ tổng trên StatusStrip
            TinhTongVaCapNhatStatusStrip();
        }

        private void DgvHangHoa_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            TinhTongVaCapNhatStatusStrip();
        }

        // Hàm tính toán và hiển thị các số liệu trên StatusStrip
        private void TinhTongVaCapNhatStatusStrip()
        {
            int tongSoLuong = 0;
            double tongTrongLuong = 0;
            double tongTien = 0;

            foreach (DataGridViewRow row in dgvHangHoa.Rows)
            {
                if (row.IsNewRow) continue;

                // Lấy số lượng
                var valSL = row.Cells["colSoLuong"]?.Value ?? (row.Cells.Count > 1 ? row.Cells[1].Value : null);
                if (valSL != null && int.TryParse(valSL.ToString().Trim(), out int sl))
                {
                    tongSoLuong += sl;
                }

                // Lấy trọng lượng
                var valTL = row.Cells["colTrongLuong"]?.Value ?? (row.Cells.Count > 2 ? row.Cells[2].Value : null);
                if (valTL != null && double.TryParse(valTL.ToString().Trim(), out double tl))
                {
                    tongTrongLuong += tl;
                }

                // Lấy thành tiền hoặc tính trực tiếp: SL * Đơn giá
                var valDG = row.Cells["colDonGia"]?.Value ?? (row.Cells.Count > 3 ? row.Cells[3].Value : null);
                if (valSL != null && valDG != null &&
                    int.TryParse(valSL.ToString().Trim(), out int slRow) &&
                    double.TryParse(valDG.ToString().Trim(), out double dgRow))
                {
                    tongTien += (slRow * dgRow);
                }
            }

            // Hiển thị lên StatusStrip
            lblTongSoLuong.Text = $"Tổng số lượng: {tongSoLuong}";
            lblTongTrongLuong.Text = $"Tổng trọng lượng: {tongTrongLuong:N1} kg";
            lblTongTien.Text = $"Tổng tiền: {tongTien:N0} VNĐ";
        }

        // Xử lý phím tắt: F2 thêm dòng nhanh, Delete xóa dòng
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Nhấn F2: Thêm dòng mới nhanh và focus vào ô Tên hàng
            if (e.KeyCode == Keys.F2)
            {
                int newRowIndex = dgvHangHoa.Rows.Add();
                dgvHangHoa.CurrentCell = dgvHangHoa.Rows[newRowIndex].Cells[0];
                dgvHangHoa.BeginEdit(true);
                e.Handled = true;
            }
            // Nhấn phím Delete: Xóa dòng đang chọn
            else if (e.KeyCode == Keys.Delete && !dgvHangHoa.IsCurrentCellInEditMode)
            {
                if (dgvHangHoa.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvHangHoa.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            dgvHangHoa.Rows.Remove(row);
                        }
                    }
                    e.Handled = true;
                }
            }
        }
    }
}
