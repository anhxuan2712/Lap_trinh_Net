using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace bai4._11
{
    public partial class Form1 : Form
    {
        private readonly BindingList<RepairOrder> originalOrders = new BindingList<RepairOrder>();
        private readonly BindingSource ordersBindingSource = new BindingSource();
        private RepairOrder selectedOrder = null;
        private string currentImagePath = string.Empty;
        private bool isPopulatingForm = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            SetupDataGridView();
            SetupControls();
            AddSampleData();
            ApplyFilterAndRefresh();
        }

        private void SetupDataGridView()
        {
            dataGridViewOrders.AutoGenerateColumns = false;
            dataGridViewOrders.Columns.Clear();

            dataGridViewOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderID",
                HeaderText = "Mã đơn",
                Name = "colOrderID",
                Width = 90
            });

            dataGridViewOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerName",
                HeaderText = "Tên khách hàng",
                Name = "colCustomerName",
                Width = 140
            });

            dataGridViewOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Phone",
                HeaderText = "Số điện thoại",
                Name = "colPhone",
                Width = 110
            });

            dataGridViewOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DeviceType",
                HeaderText = "Loại thiết bị",
                Name = "colDeviceType",
                Width = 110
            });

            dataGridViewOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReceivedDate",
                HeaderText = "Ngày tiếp nhận",
                Name = "colReceivedDate",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dataGridViewOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EstimatedCost",
                HeaderText = "Chi phí dự kiến",
                Name = "colEstimatedCost",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dataGridViewOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Trạng thái",
                Name = "colStatus",
                Width = 120
            });

            dataGridViewOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ErrorDescription",
                HeaderText = "Mô tả lỗi",
                Name = "colErrorDescription",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridViewOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrders.MultiSelect = false;
            dataGridViewOrders.ReadOnly = true;
            dataGridViewOrders.AllowUserToAddRows = false;
            dataGridViewOrders.AllowUserToDeleteRows = false;
            dataGridViewOrders.RowHeadersVisible = false;

            ordersBindingSource.DataSource = new BindingList<RepairOrder>();
            dataGridViewOrders.DataSource = ordersBindingSource;
        }

        private void SetupControls()
        {
            if (comboBoxDeviceType.Items.Count > 0)
                comboBoxDeviceType.SelectedIndex = 0;

            if (comboBoxStatus.Items.Count > 0)
                comboBoxStatus.SelectedIndex = 0;

            if (comboBoxFilterStatus.Items.Count > 0)
                comboBoxFilterStatus.SelectedIndex = 0;

            dateTimePickerReceivedDate.Value = DateTime.Today;
            pictureBoxDevice.SizeMode = PictureBoxSizeMode.Zoom;

            Button buttonClear = new Button
            {
                Name = "buttonClear",
                Text = "Làm mới",
                Location = new Point(265, 10),
                Size = new Size(75, 40),
                UseVisualStyleBackColor = true
            };
            buttonClear.Click += (s, ev) => ClearInputFields();
            panelButtons.Controls.Add(buttonClear);

            toolStripStatusLabel1.Text = "Số đơn (theo bộ lọc): ";
            toolStripStatusLabel2.Text = " | Tổng chi phí chưa hoàn tất: ";
        }

        private void AddSampleData()
        {
            originalOrders.Add(new RepairOrder
            {
                OrderID = "ORD001",
                CustomerName = "Nguyễn Văn An",
                Phone = "0912345678",
                DeviceType = "Laptop",
                ErrorDescription = "Không lên nguồn, quạt kêu to bất thường",
                ReceivedDate = DateTime.Today.AddDays(-3),
                EstimatedCost = 750000m,
                Status = "Đang sửa",
                ImagePath = string.Empty
            });

            originalOrders.Add(new RepairOrder
            {
                OrderID = "ORD002",
                CustomerName = "Trần Thị Bình",
                Phone = "0987654321",
                DeviceType = "Điện thoại",
                ErrorDescription = "Vỡ màn hình OLED, cảm ứng bị loạn",
                ReceivedDate = DateTime.Today.AddDays(-2),
                EstimatedCost = 1200000m,
                Status = "Chờ linh kiện",
                ImagePath = string.Empty
            });

            originalOrders.Add(new RepairOrder
            {
                OrderID = "ORD003",
                CustomerName = "Lê Văn Cường",
                Phone = "0905112233",
                DeviceType = "Máy in",
                ErrorDescription = "Kẹt giấy liên tục, bản in bị mờ mực",
                ReceivedDate = DateTime.Today.AddDays(-5),
                EstimatedCost = 350000m,
                Status = "Hoàn tất",
                ImagePath = string.Empty
            });

            originalOrders.Add(new RepairOrder
            {
                OrderID = "ORD004",
                CustomerName = "Phạm Thị Dung",
                Phone = "0934567890",
                DeviceType = "Máy tính bảng",
                ErrorDescription = "Pin bị phồng, tụt pin nhanh khi mở ứng dụng",
                ReceivedDate = DateTime.Today.AddDays(-1),
                EstimatedCost = 500000m,
                Status = "Mới tiếp nhận",
                ImagePath = string.Empty
            });

            originalOrders.Add(new RepairOrder
            {
                OrderID = "ORD005",
                CustomerName = "Vũ Đình Giang",
                Phone = "0961239876",
                DeviceType = "Router",
                ErrorDescription = "Mất sóng băng tần 5GHz, chập chờn khi nhiều máy kết nối",
                ReceivedDate = DateTime.Today.AddDays(-4),
                EstimatedCost = 250000m,
                Status = "Đã hủy",
                ImagePath = string.Empty
            });
        }

        private void LoadDeviceImage(string path)
        {
            try
            {
                if (pictureBoxDevice.Image != null)
                {
                    var oldImg = pictureBoxDevice.Image;
                    pictureBoxDevice.Image = null;
                    oldImg.Dispose();
                }

                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        using (var temp = Image.FromStream(stream))
                        {
                            pictureBoxDevice.Image = new Bitmap(temp);
                        }
                    }
                    pictureBoxDevice.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch
            {
                if (pictureBoxDevice.Image != null)
                {
                    var oldImg = pictureBoxDevice.Image;
                    pictureBoxDevice.Image = null;
                    oldImg.Dispose();
                }
            }
        }

        private void ButtonSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh thiết bị sửa chữa";
                ofd.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Tất cả tệp (*.*)|*.*";
                ofd.FilterIndex = 1;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    currentImagePath = ofd.FileName;
                    LoadDeviceImage(currentImagePath);
                }
            }
        }

        private void DataGridViewOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (isPopulatingForm)
                return;

            if (dataGridViewOrders.SelectedRows.Count == 0)
            {
                selectedOrder = null;
                return;
            }

            RepairOrder order = dataGridViewOrders.SelectedRows[0].DataBoundItem as RepairOrder;
            if (order == null)
            {
                selectedOrder = null;
                return;
            }

            selectedOrder = order;
            PopulateFormFromOrder(order);
        }

        private void PopulateFormFromOrder(RepairOrder order)
        {
            if (order == null)
                return;

            isPopulatingForm = true;
            try
            {
                errorProvider1.Clear();
                textBoxOrderID.Text = order.OrderID;
                textBoxCustomerName.Text = order.CustomerName;
                textBoxPhone.Text = order.Phone;
                comboBoxDeviceType.SelectedItem = order.DeviceType;
                textBoxErrorDescription.Text = order.ErrorDescription;
                dateTimePickerReceivedDate.Value = order.ReceivedDate;
                textBoxEstimatedCost.Text = order.EstimatedCost.ToString("N0", CultureInfo.InvariantCulture);
                comboBoxStatus.SelectedItem = order.Status;

                currentImagePath = order.ImagePath;
                LoadDeviceImage(currentImagePath);
            }
            finally
            {
                isPopulatingForm = false;
            }
        }

        private void ClearInputFields()
        {
            isPopulatingForm = true;
            try
            {
                selectedOrder = null;
                dataGridViewOrders.ClearSelection();
                errorProvider1.Clear();

                textBoxOrderID.Clear();
                textBoxCustomerName.Clear();
                textBoxPhone.Clear();
                if (comboBoxDeviceType.Items.Count > 0)
                    comboBoxDeviceType.SelectedIndex = 0;
                textBoxErrorDescription.Clear();
                dateTimePickerReceivedDate.Value = DateTime.Today;
                textBoxEstimatedCost.Text = "0";
                if (comboBoxStatus.Items.Count > 0)
                    comboBoxStatus.SelectedIndex = 0;

                currentImagePath = string.Empty;
                if (pictureBoxDevice.Image != null)
                {
                    var oldImg = pictureBoxDevice.Image;
                    pictureBoxDevice.Image = null;
                    oldImg.Dispose();
                }
            }
            finally
            {
                isPopulatingForm = false;
            }
        }

        private bool ValidateInput(RepairOrder orderBeingEdited)
        {
            errorProvider1.Clear();
            bool isValid = true;

            string orderId = textBoxOrderID.Text.Trim();
            string customerName = textBoxCustomerName.Text.Trim();
            string phone = textBoxPhone.Text.Trim();
            string deviceType = comboBoxDeviceType.SelectedItem as string;
            string errorDesc = textBoxErrorDescription.Text.Trim();
            string status = comboBoxStatus.SelectedItem as string;
            string costText = textBoxEstimatedCost.Text.Trim();

            // 1. Kiểm tra mã đơn
            if (string.IsNullOrWhiteSpace(orderId))
            {
                errorProvider1.SetError(textBoxOrderID, "Mã đơn không được để trống.");
                isValid = false;
            }
            else if (originalOrders.Any(o => !ReferenceEquals(o, orderBeingEdited)
                     && string.Equals(o.OrderID, orderId, StringComparison.OrdinalIgnoreCase)))
            {
                errorProvider1.SetError(textBoxOrderID, "Mã đơn đã tồn tại. Vui lòng nhập mã khác.");
                isValid = false;
            }

            // 2. Tên khách hàng không để trống
            if (string.IsNullOrWhiteSpace(customerName))
            {
                errorProvider1.SetError(textBoxCustomerName, "Tên khách hàng không được để trống.");
                isValid = false;
            }

            // 3. Số điện thoại không để trống
            if (string.IsNullOrWhiteSpace(phone))
            {
                errorProvider1.SetError(textBoxPhone, "Số điện thoại không được để trống.");
                isValid = false;
            }
            else if (!Regex.IsMatch(phone, @"^[0-9+() -]{7,15}$"))
            {
                errorProvider1.SetError(textBoxPhone, "Số điện thoại không hợp lệ (từ 7 đến 15 chữ số).");
                isValid = false;
            }

            // 4. Loại thiết bị
            if (string.IsNullOrWhiteSpace(deviceType))
            {
                errorProvider1.SetError(comboBoxDeviceType, "Vui lòng chọn loại thiết bị.");
                isValid = false;
            }

            // 5. Mô tả lỗi bắt buộc
            if (string.IsNullOrWhiteSpace(errorDesc))
            {
                errorProvider1.SetError(textBoxErrorDescription, "Mô tả lỗi bắt buộc phải nhập.");
                isValid = false;
            }

            // 6. Chi phí dự kiến phải là số không âm
            decimal cost;
            if (string.IsNullOrWhiteSpace(costText))
            {
                errorProvider1.SetError(textBoxEstimatedCost, "Vui lòng nhập chi phí dự kiến.");
                isValid = false;
            }
            else if (!TryParseCost(costText, out cost))
            {
                errorProvider1.SetError(textBoxEstimatedCost, "Chi phí dự kiến phải là số hợp lệ.");
                isValid = false;
            }
            else if (cost < 0)
            {
                errorProvider1.SetError(textBoxEstimatedCost, "Chi phí dự kiến phải là số không âm (>= 0).");
                isValid = false;
            }

            // 7. Trạng thái
            if (string.IsNullOrWhiteSpace(status))
            {
                errorProvider1.SetError(comboBoxStatus, "Vui lòng chọn trạng thái đơn.");
                isValid = false;
            }

            if (!isValid)
            {
                MessageBox.Show("Dữ liệu không hợp lệ. Vui lòng kiểm tra lại các trường được báo đỏ!",
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }

        private bool TryParseCost(string input, out decimal cost)
        {
            cost = 0;
            if (string.IsNullOrWhiteSpace(input))
                return false;

            string cleaned = input.Trim().Replace(" ", "");

            if (decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.InvariantCulture, out cost))
                return true;

            if (decimal.TryParse(cleaned, NumberStyles.Number, new CultureInfo("vi-VN"), out cost))
                return true;

            cleaned = cleaned.Replace(",", "").Replace(".", "");
            return decimal.TryParse(cleaned, NumberStyles.Integer, CultureInfo.InvariantCulture, out cost);
        }

        private List<RepairOrder> GetFilteredOrders()
        {
            string search = textBoxSearch.Text.Trim();
            string filterStatus = comboBoxFilterStatus.SelectedItem as string;

            return originalOrders.Where(order =>
            {
                bool matchSearch = string.IsNullOrEmpty(search) ||
                    (!string.IsNullOrEmpty(order.OrderID) && order.OrderID.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) >= 0) ||
                    (!string.IsNullOrEmpty(order.CustomerName) && order.CustomerName.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) >= 0) ||
                    (!string.IsNullOrEmpty(order.Phone) && order.Phone.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) >= 0);

                bool matchStatus = string.IsNullOrEmpty(filterStatus) ||
                    filterStatus == "All" ||
                    filterStatus == "Tất cả" ||
                    string.Equals(order.Status, filterStatus, StringComparison.OrdinalIgnoreCase);

                return matchSearch && matchStatus;
            }).ToList();
        }

        private void UpdateStatusStrip(List<RepairOrder> filtered)
        {
            toolStripStatusLabel1.Text = "Số đơn (theo bộ lọc): ";
            toolStripStatusLabelCount.Text = filtered.Count.ToString();

            decimal totalIncompleteFiltered = filtered
                .Where(o => !string.Equals(o.Status, "Hoàn tất", StringComparison.OrdinalIgnoreCase)
                         && !string.Equals(o.Status, "Đã hủy", StringComparison.OrdinalIgnoreCase))
                .Sum(o => o.EstimatedCost);

            decimal totalIncompleteAll = originalOrders
                .Where(o => !string.Equals(o.Status, "Hoàn tất", StringComparison.OrdinalIgnoreCase)
                         && !string.Equals(o.Status, "Đã hủy", StringComparison.OrdinalIgnoreCase))
                .Sum(o => o.EstimatedCost);

            toolStripStatusLabel2.Text = " | Tổng chi phí chưa hoàn tất: ";
            toolStripStatusLabelTotal.Text = totalIncompleteFiltered.ToString("N0") + " VNĐ";
            toolStripStatusLabelTotal.ToolTipText = string.Format("Chưa hoàn tất (theo bộ lọc): {0:N0} VNĐ | (Toàn bộ cửa hàng: {1:N0} VNĐ)",
                totalIncompleteFiltered, totalIncompleteAll);
        }

        private void ApplyFilterAndRefresh(string selectOrderId = null)
        {
            var filtered = GetFilteredOrders();
            ordersBindingSource.DataSource = new BindingList<RepairOrder>(filtered);
            ordersBindingSource.ResetBindings(false);

            UpdateStatusStrip(filtered);

            if (!string.IsNullOrEmpty(selectOrderId))
            {
                foreach (DataGridViewRow row in dataGridViewOrders.Rows)
                {
                    RepairOrder item = row.DataBoundItem as RepairOrder;
                    if (item != null && string.Equals(item.OrderID, selectOrderId, StringComparison.OrdinalIgnoreCase))
                    {
                        row.Selected = true;
                        dataGridViewOrders.CurrentCell = row.Cells[0];
                        selectedOrder = item;
                        PopulateFormFromOrder(item);
                        return;
                    }
                }
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(null))
                return;

            if (MessageBox.Show("Bạn có chắc chắn muốn thêm đơn đăng ký dịch vụ này?",
                "Xác nhận thêm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            decimal cost;
            TryParseCost(textBoxEstimatedCost.Text.Trim(), out cost);

            RepairOrder newOrder = new RepairOrder
            {
                OrderID = textBoxOrderID.Text.Trim(),
                CustomerName = textBoxCustomerName.Text.Trim(),
                Phone = textBoxPhone.Text.Trim(),
                DeviceType = comboBoxDeviceType.SelectedItem != null ? comboBoxDeviceType.SelectedItem.ToString() : "Khác",
                ErrorDescription = textBoxErrorDescription.Text.Trim(),
                ReceivedDate = dateTimePickerReceivedDate.Value,
                EstimatedCost = cost,
                Status = comboBoxStatus.SelectedItem != null ? comboBoxStatus.SelectedItem.ToString() : "Mới tiếp nhận",
                ImagePath = currentImagePath
            };

            originalOrders.Add(newOrder);
            ApplyFilterAndRefresh(newOrder.OrderID);
            MessageBox.Show("Đã thêm đơn đăng ký [" + newOrder.OrderID + "] thành công!",
                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (selectedOrder == null)
            {
                MessageBox.Show("Vui lòng chọn đơn cần cập nhật từ danh sách.",
                    "Chưa chọn đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidateInput(selectedOrder))
                return;

            if (MessageBox.Show("Bạn có chắc chắn muốn cập nhật đơn [" + selectedOrder.OrderID + "]?",
                "Xác nhận cập nhật", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            decimal cost;
            TryParseCost(textBoxEstimatedCost.Text.Trim(), out cost);

            selectedOrder.OrderID = textBoxOrderID.Text.Trim();
            selectedOrder.CustomerName = textBoxCustomerName.Text.Trim();
            selectedOrder.Phone = textBoxPhone.Text.Trim();
            selectedOrder.DeviceType = comboBoxDeviceType.SelectedItem != null ? comboBoxDeviceType.SelectedItem.ToString() : "Khác";
            selectedOrder.ErrorDescription = textBoxErrorDescription.Text.Trim();
            selectedOrder.ReceivedDate = dateTimePickerReceivedDate.Value;
            selectedOrder.EstimatedCost = cost;
            selectedOrder.Status = comboBoxStatus.SelectedItem != null ? comboBoxStatus.SelectedItem.ToString() : "Mới tiếp nhận";
            selectedOrder.ImagePath = currentImagePath;

            ApplyFilterAndRefresh(selectedOrder.OrderID);
            MessageBox.Show("Đã cập nhật đơn [" + selectedOrder.OrderID + "] thành công!",
                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (selectedOrder == null)
            {
                MessageBox.Show("Vui lòng chọn đơn cần xóa từ danh sách.",
                    "Chưa chọn đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa đơn [" + selectedOrder.OrderID + "] của khách hàng " + selectedOrder.CustomerName + "?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            string deletedId = selectedOrder.OrderID;
            originalOrders.Remove(selectedOrder);
            selectedOrder = null;
            ClearInputFields();
            ApplyFilterAndRefresh();
            MessageBox.Show("Đã xóa đơn [" + deletedId + "] thành công!",
                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilterAndRefresh();
        }

        private void ComboBoxFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilterAndRefresh();
        }

        private void ExportCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BindingList<RepairOrder> boundList = ordersBindingSource.DataSource as BindingList<RepairOrder>;
            List<RepairOrder> currentList = boundList != null ? boundList.ToList() : GetFilteredOrders();

            if (currentList.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu đơn nào đang hiển thị để xuất CSV.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Xuất danh sách đơn sửa chữa ra CSV";
                sfd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                sfd.DefaultExt = "csv";
                sfd.FileName = "DanhSachDonSuaChua_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        using (var writer = new StreamWriter(sfd.FileName, false, new UTF8Encoding(true)))
                        {
                            writer.WriteLine("Mã đơn,Tên khách hàng,Số điện thoại,Loại thiết bị,Mô tả lỗi,Ngày tiếp nhận,Chi phí dự kiến,Trạng thái,Đường dẫn ảnh");
                            foreach (RepairOrder order in currentList)
                            {
                                writer.WriteLine(string.Join(",", new string[]
                                {
                                    EscapeCsv(order.OrderID),
                                    EscapeCsv(order.CustomerName),
                                    EscapeCsv(order.Phone),
                                    EscapeCsv(order.DeviceType),
                                    EscapeCsv(order.ErrorDescription),
                                    order.ReceivedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                                    order.EstimatedCost.ToString("N0", CultureInfo.InvariantCulture),
                                    EscapeCsv(order.Status),
                                    EscapeCsv(order.ImagePath)
                                }));
                            }
                        }

                        MessageBox.Show("Đã xuất " + currentList.Count + " đơn sửa chữa ra tệp CSV thành công!\nĐường dẫn: " + sfd.FileName,
                            "Xuất CSV thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi khi ghi tệp CSV:\n" + ex.Message,
                            "Lỗi xuất file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "\"\"";
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng Repair Service Tracker?",
                "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }

    public class RepairOrder
    {
        public string OrderID { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string DeviceType { get; set; }
        public string ErrorDescription { get; set; }
        public DateTime ReceivedDate { get; set; }
        public decimal EstimatedCost { get; set; }
        public string Status { get; set; }
        public string ImagePath { get; set; }
    }
}
