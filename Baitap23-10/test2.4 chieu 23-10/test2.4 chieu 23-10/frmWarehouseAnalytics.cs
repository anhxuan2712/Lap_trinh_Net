using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test2._4_chieu_23_10
{
    public partial class frmWarehouseAnalytics : Form
    {
        private CancellationTokenSource cts;
        private BindingList<InventoryReportItem> reportDataList;

        public frmWarehouseAnalytics()
        {
            InitializeComponent();
        }

        private void frmWarehouseAnalytics_Load(object sender, EventArgs e)
        {
            InitializeGrid();
            cboWarehouse.SelectedIndex = 0;
            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today;
            lblStatusMessage.Text = "Trạng thái: Sẵn sàng thực hiện.";
        }

        private void InitializeGrid()
        {
            dgvReportData.AutoGenerateColumns = false;
            dgvReportData.ReadOnly = true;
            dgvReportData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReportData.MultiSelect = false;
            dgvReportData.AllowUserToAddRows = false;
            dgvReportData.AllowUserToDeleteRows = false;
            dgvReportData.RowHeadersVisible = false;
            dgvReportData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvReportData.Columns.Clear();

            var colCode = new DataGridViewTextBoxColumn
            {
                HeaderText = "Mã hàng",
                DataPropertyName = "ItemCode",
                FillWeight = 80
            };

            var colName = new DataGridViewTextBoxColumn
            {
                HeaderText = "Tên mặt hàng",
                DataPropertyName = "ItemName",
                FillWeight = 180
            };

            var colCat = new DataGridViewTextBoxColumn
            {
                HeaderText = "Ngành hàng",
                DataPropertyName = "Category",
                FillWeight = 110
            };

            var colUnit = new DataGridViewTextBoxColumn
            {
                HeaderText = "ĐVT",
                DataPropertyName = "Unit",
                FillWeight = 60
            };

            var colQty = new DataGridViewTextBoxColumn
            {
                HeaderText = "Tồn kho",
                DataPropertyName = "StockQuantity",
                FillWeight = 85
            };
            colQty.DefaultCellStyle.Format = "N0";
            colQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            var colPrice = new DataGridViewTextBoxColumn
            {
                HeaderText = "Đơn giá nhập",
                DataPropertyName = "UnitPrice",
                FillWeight = 100
            };
            colPrice.DefaultCellStyle.Format = "N0";
            colPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            var colTotal = new DataGridViewTextBoxColumn
            {
                HeaderText = "Tổng giá trị (VNĐ)",
                DataPropertyName = "TotalValue",
                FillWeight = 130
            };
            colTotal.DefaultCellStyle.Format = "N0";
            colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            var colStatus = new DataGridViewTextBoxColumn
            {
                HeaderText = "Trạng thái tồn",
                DataPropertyName = "Status",
                FillWeight = 100
            };

            dgvReportData.Columns.AddRange(new DataGridViewColumn[]
            {
                colCode, colName, colCat, colUnit, colQty, colPrice, colTotal, colStatus
            });
        }

        private async void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
            {
                MessageBox.Show("Khoảng thời gian không hợp lệ: 'Từ ngày' phải nhỏ hơn hoặc bằng 'Đến ngày'!",
                                "Lỗi chọn ngày", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string warehouseName = cboWarehouse.SelectedItem?.ToString() ?? "Kho Tổng";
            DateTime fromDate = dtpFromDate.Value.Date;
            DateTime toDate = dtpToDate.Value.Date;

            // Khởi tạo CancellationTokenSource
            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            // Điều chỉnh giao diện lúc bắt đầu
            btnAnalyze.Enabled = false;
            btnCancel.Enabled = true;
            cboWarehouse.Enabled = false;
            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;

            pgbStatus.Value = 0;
            lblPercent.Text = "0%";
            lblStatusMessage.Text = "Trạng thái: Bắt đầu kết nối dữ liệu kho...";

            // Tiến trình thread-safe
            var progress = new Progress<ProgressUpdate>(update =>
            {
                pgbStatus.Value = Math.Min(100, Math.Max(0, update.Percentage));
                lblPercent.Text = update.Percentage + "%";
                lblStatusMessage.Text = "Trạng thái: " + update.Message;
            });

            try
            {
                // Gọi xử lý ngầm qua Task.Run, không khóa UI Thread
                List<InventoryReportItem> results = await Task.Run(() =>
                    SimulateHeavyInventoryAnalytics(warehouseName, fromDate, toDate, progress, token), token);

                // Khi hoàn thành thành công
                reportDataList = new BindingList<InventoryReportItem>(results);
                dgvReportData.DataSource = reportDataList;

                lblStatusMessage.Text = "Hoàn tất";
                MessageBox.Show($"Tổng hợp báo cáo tồn kho thành công cho {warehouseName}!\nTổng số mặt hàng: {results.Count}",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                // Người dùng đã hủy tác vụ
                pgbStatus.Value = 0;
                lblPercent.Text = "0%";
                lblStatusMessage.Text = "Trạng thái: Tác vụ đã bị hủy bởi người dùng.";
                MessageBox.Show("Bạn đã hủy tiến trình tổng hợp báo cáo kho hàng!",
                                "Đã hủy tác vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra trong quá trình tổng hợp: " + ex.Message,
                                "Lỗi xử lý", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatusMessage.Text = "Trạng thái: Xảy ra lỗi.";
            }
            finally
            {
                // Khôi phục trạng thái nút bấm
                btnAnalyze.Enabled = true;
                btnCancel.Enabled = false;
                cboWarehouse.Enabled = true;
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;

                cts.Dispose();
                cts = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (cts != null && !cts.IsCancellationRequested)
            {
                btnCancel.Enabled = false;
                lblStatusMessage.Text = "Trạng thái: Đang gửi tín hiệu hủy tác vụ...";
                cts.Cancel();
            }
        }

        private List<InventoryReportItem> SimulateHeavyInventoryAnalytics(
            string warehouse,
            DateTime fromDate,
            DateTime toDate,
            IProgress<ProgressUpdate> progress,
            CancellationToken token)
        {
            // Các giai đoạn xử lý tổng thời gian khoảng 4 - 5 giây
            var stages = new[]
            {
                new { Pct = 10, Msg = "Đang kết nối cơ sở dữ liệu kho từ xa..." },
                new { Pct = 25, Msg = "Đang quét lịch sử phiếu nhập kho theo mốc thời gian..." },
                new { Pct = 40, Msg = "Đang quét lịch sử phiếu xuất và điều chuyển nội bộ..." },
                new { Pct = 55, Msg = "Đang tính toán giá trị tồn theo phương pháp bình quân gia quyền..." },
                new { Pct = 70, Msg = "Đang đối chiếu số dư sổ cái và thẻ kho..." },
                new { Pct = 85, Msg = "Đang kiểm tra ngưỡng an toàn tồn kho tối thiểu..." },
                new { Pct = 95, Msg = "Đang đóng gói dữ liệu và xuất bảng phân tích..." },
                new { Pct = 100, Msg = "Hoàn tất tổng hợp!" }
            };

            foreach (var stage in stages)
            {
                // Kiểm tra yêu cầu hủy
                token.ThrowIfCancellationRequested();

                // Giả lập tính toán tốn thời gian (khoảng 500-600ms mỗi giai đoạn => tổng ~ 4.5s)
                Thread.Sleep(550);

                // Báo cáo tiến độ về UI Thread
                progress?.Report(new ProgressUpdate
                {
                    Percentage = stage.Pct,
                    Message = stage.Msg
                });
            }

            token.ThrowIfCancellationRequested();

            // Sinh dữ liệu kết quả phân tích theo kho
            return GenerateMockInventoryData(warehouse);
        }

        private List<InventoryReportItem> GenerateMockInventoryData(string warehouse)
        {
            var list = new List<InventoryReportItem>
            {
                new InventoryReportItem { ItemCode = "VT001", ItemName = "Thép cuộn mạ kẽm Ø6", Category = "Vật liệu xây dựng", Unit = "Tấn", StockQuantity = 85, UnitPrice = 16500000, Status = "Tồn ổn định" },
                new InventoryReportItem { ItemCode = "VT002", ItemName = "Xi măng PCB40 Hoàng Thạch", Category = "Vật liệu xây dựng", Unit = "Bao", StockQuantity = 1250, UnitPrice = 88000, Status = "Tồn ổn định" },
                new InventoryReportItem { ItemCode = "TB010", ItemName = "Máy biến áp 3 pha 250kVA", Category = "Thiết bị điện", Unit = "Bộ", StockQuantity = 6, UnitPrice = 145000000, Status = "Cận ngưỡng min" },
                new InventoryReportItem { ItemCode = "TB011", ItemName = "Cáp đồng trần tiếp địa 50mm2", Category = "Thiết bị điện", Unit = "Cuộn", StockQuantity = 420, UnitPrice = 3200000, Status = "Tồn ổn định" },
                new InventoryReportItem { ItemCode = "LK005", ItemName = "Sơn phủ epoxy bóng công nghiệp", Category = "Hóa chất", Unit = "Thùng", StockQuantity = 180, UnitPrice = 1250000, Status = "Tồn dồi dào" },
                new InventoryReportItem { ItemCode = "LK008", ItemName = "Bu lông cường độ cao M20x80", Category = "Cơ khí", Unit = "Hộp", StockQuantity = 650, UnitPrice = 450000, Status = "Tồn dồi dào" },
                new InventoryReportItem { ItemCode = "TB022", ItemName = "Động cơ điện không đồng bộ 15kW", Category = "Thiết bị điện", Unit = "Cái", StockQuantity = 12, UnitPrice = 18500000, Status = "Tồn ổn định" },
                new InventoryReportItem { ItemCode = "VT015", ItemName = "Ống thép đúc phi 114x4.5mm", Category = "Vật liệu xây dựng", Unit = "Cây", StockQuantity = 340, UnitPrice = 890000, Status = "Tồn ổn định" },
                new InventoryReportItem { ItemCode = "LK019", ItemName = "Que hàn chịu lực Kiswel E7018", Category = "Cơ khí", Unit = "Thùng", StockQuantity = 40, UnitPrice = 620000, Status = "Cảnh báo thiếu hàng" },
                new InventoryReportItem { ItemCode = "VT030", ItemName = "Tấm inox 304 dày 2mm", Category = "Vật liệu xây dựng", Unit = "Tấm", StockQuantity = 95, UnitPrice = 3400000, Status = "Tồn ổn định" }
            };

            // Tùy biến nhẹ theo kho
            if (warehouse.Contains("Miền Trung"))
            {
                list.RemoveAt(0);
                list.Add(new InventoryReportItem { ItemCode = "MT001", ItemName = "Ngói màu chống thấm cao cấp", Category = "Vật liệu xây dựng", Unit = "Viên", StockQuantity = 4500, UnitPrice = 18000, Status = "Tồn dồi dào" });
            }
            else if (warehouse.Contains("Miền Nam"))
            {
                list.Add(new InventoryReportItem { ItemCode = "MN005", ItemName = "Máy nén khí trục vít 37kW", Category = "Thiết bị điện", Unit = "Bộ", StockQuantity = 4, UnitPrice = 195000000, Status = "Tồn ổn định" });
            }

            return list;
        }
    }
}
