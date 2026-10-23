using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test2._5_chieu_23_10
{
    public partial class frmIoTMonitor : Form
    {
        private CancellationTokenSource cts;
        private BindingList<SensorLogItem> logList;
        private int totalCycles = 0;
        private readonly Random random = new Random();

        public frmIoTMonitor()
        {
            InitializeComponent();
        }

        private void frmIoTMonitor_Load(object sender, EventArgs e)
        {
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            dgvLogs.AutoGenerateColumns = false;
            dgvLogs.ReadOnly = true;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.MultiSelect = false;
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.RowHeadersVisible = false;
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvLogs.Columns.Clear();

            var colTime = new DataGridViewTextBoxColumn
            {
                HeaderText = "Thời gian",
                DataPropertyName = "Timestamp",
                FillWeight = 100
            };

            var colSensor = new DataGridViewTextBoxColumn
            {
                HeaderText = "Cảm biến",
                DataPropertyName = "SensorId",
                FillWeight = 100
            };

            var colTemp = new DataGridViewTextBoxColumn
            {
                HeaderText = "Nhiệt độ (°C)",
                DataPropertyName = "Temperature",
                FillWeight = 100
            };
            colTemp.DefaultCellStyle.Format = "N1";
            colTemp.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            var colStatus = new DataGridViewTextBoxColumn
            {
                HeaderText = "Trạng thái",
                DataPropertyName = "Status",
                FillWeight = 120
            };

            var colNote = new DataGridViewTextBoxColumn
            {
                HeaderText = "Ghi chú",
                DataPropertyName = "WarningNote",
                FillWeight = 180
            };

            dgvLogs.Columns.AddRange(new DataGridViewColumn[]
            {
                colTime, colSensor, colTemp, colStatus, colNote
            });

            logList = new BindingList<SensorLogItem>();
            dgvLogs.DataSource = logList;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string sensorId = "IOT-TMP-01";

            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            btnStart.Enabled = false;
            btnStop.Enabled = true;

            lblStatusConnection.Text = "Trạng thái: Đang kết nối giám sát...";

            // Khởi chạy tiến trình chạy ngầm lặp vô hạn định kỳ 2 giây/lần
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        // Sinh dữ liệu nhiệt độ ngẫu nhiên từ 30°C đến 42°C
                        double currentTemp = Math.Round(30.0 + (random.NextDouble() * 12.0), 1);

                        // Cập nhật giao diện tuân thủ Thread Safety
                        UpdateSensorDataThreadSafe(sensorId, currentTemp);

                        // Định kỳ 2 giây/lần
                        await Task.Delay(2000, token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }, token);
        }

        /// <summary>
        /// Áp dụng quy tắc Thread Safety: dùng this.InvokeRequired hoặc this.Invoke(new Action(...))
        /// CẤM gán trực tiếp lên lblTemperature.Text hay thêm dòng vào dgvLogs từ Thread phụ.
        /// </summary>
        private void UpdateSensorDataThreadSafe(string sensorId, double temp)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateSensorDataThreadSafe(sensorId, temp)));
                return;
            }

            // Xử lý an toàn trên UI Thread
            totalCycles++;

            // Hiển thị nhiệt độ: Đổi màu đỏ nếu > 37°C, xanh nếu bình thường
            lblTemperature.Text = string.Format("{0:F1} °C", temp);
            bool isWarning = temp > 37.0;

            if (isWarning)
            {
                lblTemperature.ForeColor = Color.Red;
            }
            else
            {
                lblTemperature.ForeColor = Color.Green;
            }

            // Ghi nhật ký vào DataGridView
            var item = new SensorLogItem
            {
                Timestamp = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
                SensorId = sensorId,
                Temperature = temp,
                Status = isWarning ? "CẢNH BÁO QUÁ NHIỆT" : "Bình thường",
                WarningNote = isWarning ? "Nhiệt độ vượt quá ngưỡng an toàn 37°C" : "Nhiệt độ ổn định"
            };

            logList.Insert(0, item);

            // Cập nhật StatusStrip
            lblStatusConnection.Text = "Trạng thái: Đang giám sát (Real-time)";
            lblStatusCycles.Text = string.Format("Số lần nhận chu kỳ tín hiệu: {0}", totalCycles);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopMonitoring();
        }

        private void StopMonitoring()
        {
            if (cts != null && !cts.IsCancellationRequested)
            {
                cts.Cancel();
                cts.Dispose();
                cts = null;
            }

            btnStart.Enabled = true;
            btnStop.Enabled = false;

            lblStatusConnection.Text = "Trạng thái: Đã dừng giám sát";
        }

        private void frmIoTMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopMonitoring();
        }
    }
}
