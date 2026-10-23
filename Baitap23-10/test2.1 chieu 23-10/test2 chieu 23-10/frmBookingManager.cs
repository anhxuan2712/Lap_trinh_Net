using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace test2_chieu_23_10
{
    public partial class frmBookingManager : Form
    {
        private BindingList<BookingModel> bookingList;
        private BindingSource bookingBindingSource;

        public frmBookingManager()
        {
            InitializeComponent();
        }

        private void frmBookingManager_Load(object sender, EventArgs e)
        {
            InitializeGrid();
            InitializeDataBinding();
        }

        private void InitializeGrid()
        {
            dgvBookings.AutoGenerateColumns = false;
            dgvBookings.ReadOnly = true;
            dgvBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBookings.MultiSelect = false;
            dgvBookings.AllowUserToAddRows = false;
            dgvBookings.AllowUserToDeleteRows = false;
            dgvBookings.RowHeadersVisible = false;
            dgvBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvBookings.Columns.Clear();

            DataGridViewTextBoxColumn colBookingId = new DataGridViewTextBoxColumn
            {
                Name = "colBookingId",
                HeaderText = "Mã Đặt",
                DataPropertyName = "BookingId",
                FillWeight = 85
            };

            DataGridViewTextBoxColumn colCustomerName = new DataGridViewTextBoxColumn
            {
                Name = "colCustomerName",
                HeaderText = "Tên Khách",
                DataPropertyName = "CustomerName",
                FillWeight = 140
            };

            DataGridViewTextBoxColumn colIdentityCard = new DataGridViewTextBoxColumn
            {
                Name = "colIdentityCard",
                HeaderText = "CCCD",
                DataPropertyName = "IdentityCard",
                FillWeight = 110
            };

            DataGridViewTextBoxColumn colRoomType = new DataGridViewTextBoxColumn
            {
                Name = "colRoomType",
                HeaderText = "Loại Phòng",
                DataPropertyName = "RoomType",
                FillWeight = 95
            };

            DataGridViewTextBoxColumn colCheckIn = new DataGridViewTextBoxColumn
            {
                Name = "colCheckInDate",
                HeaderText = "Ngày Nhận",
                DataPropertyName = "CheckInDate",
                FillWeight = 100
            };
            colCheckIn.DefaultCellStyle.Format = "dd/MM/yyyy";

            DataGridViewTextBoxColumn colDays = new DataGridViewTextBoxColumn
            {
                Name = "colDays",
                HeaderText = "Số Đêm",
                DataPropertyName = "Days",
                FillWeight = 75
            };

            DataGridViewTextBoxColumn colPrice = new DataGridViewTextBoxColumn
            {
                Name = "colPricePerNight",
                HeaderText = "Giá/Đêm",
                DataPropertyName = "PricePerNight",
                FillWeight = 115
            };
            colPrice.DefaultCellStyle.Format = "N0";

            dgvBookings.Columns.AddRange(new DataGridViewColumn[]
            {
                colBookingId,
                colCustomerName,
                colIdentityCard,
                colRoomType,
                colCheckIn,
                colDays,
                colPrice
            });
        }

        private void InitializeDataBinding()
        {
            bookingList = new BindingList<BookingModel>
            {
                new BookingModel
                {
                    BookingId = "BK001",
                    CustomerName = "Nguyễn Văn An",
                    IdentityCard = "012345678901",
                    RoomType = "Deluxe",
                    CheckInDate = DateTime.Today,
                    Days = 3,
                    PricePerNight = 1200000
                },
                new BookingModel
                {
                    BookingId = "BK002",
                    CustomerName = "Trần Thị Bích",
                    IdentityCard = "098765432",
                    RoomType = "VIP",
                    CheckInDate = DateTime.Today.AddDays(1),
                    Days = 2,
                    PricePerNight = 2500000
                }
            };

            bookingBindingSource = new BindingSource();
            bookingBindingSource.DataSource = bookingList;
            bookingBindingSource.CurrentChanged += bookingBindingSource_CurrentChanged;
            dgvBookings.DataSource = bookingBindingSource;

            txtBookingId.DataBindings.Add("Text", bookingBindingSource, "BookingId", true, DataSourceUpdateMode.Never);
            txtCustomerName.DataBindings.Add("Text", bookingBindingSource, "CustomerName", true, DataSourceUpdateMode.Never);
            txtIdentityCard.DataBindings.Add("Text", bookingBindingSource, "IdentityCard", true, DataSourceUpdateMode.Never);
            cboRoomType.DataBindings.Add("Text", bookingBindingSource, "RoomType", true, DataSourceUpdateMode.Never);
            dtpCheckIn.DataBindings.Add("Value", bookingBindingSource, "CheckInDate", true, DataSourceUpdateMode.Never);
            numDays.DataBindings.Add("Value", bookingBindingSource, "Days", true, DataSourceUpdateMode.Never);
            txtPricePerNight.DataBindings.Add("Text", bookingBindingSource, "PricePerNight", true, DataSourceUpdateMode.Never, 0, "N0");

            DisplayCurrentBooking();
        }

        private void bookingBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            DisplayCurrentBooking();
        }

        private void DisplayCurrentBooking()
        {
            if (bookingBindingSource != null && bookingBindingSource.Current is BookingModel selected)
            {
                txtBookingId.Text = selected.BookingId;
                txtCustomerName.Text = selected.CustomerName;
                txtIdentityCard.Text = selected.IdentityCard;
                cboRoomType.SelectedItem = selected.RoomType;
                dtpCheckIn.Value = selected.CheckInDate;
                numDays.Value = selected.Days;
                txtPricePerNight.Text = selected.PricePerNight.ToString("N0");
            }
        }

        private void dgvBookings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < bookingList.Count)
            {
                errorProvider.Clear();
                bookingBindingSource.Position = e.RowIndex;
                DisplayCurrentBooking();
            }
        }

        private bool ValidateInput(out decimal price)
        {
            bool isValid = true;
            errorProvider.Clear();
            price = 0;

            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                errorProvider.SetError(txtCustomerName, "Tên khách hàng không được để trống.");
                isValid = false;
            }

            string idCard = txtIdentityCard.Text.Trim();
            if (!Regex.IsMatch(idCard, @"^(\d{9}|\d{12})$"))
            {
                errorProvider.SetError(txtIdentityCard, "CCCD phải đủ 9 hoặc 12 chữ số.");
                isValid = false;
            }

            string rawPrice = txtPricePerNight.Text.Replace(",", "").Replace(".", "").Trim();
            if (!decimal.TryParse(rawPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out price) || price <= 0)
            {
                errorProvider.SetError(txtPricePerNight, "Giá phòng phải là số lớn hơn 0.");
                isValid = false;
            }

            return isValid;
        }

        private string GenerateBookingId()
        {
            int maxId = 0;
            foreach (var item in bookingList)
            {
                if (!string.IsNullOrEmpty(item.BookingId) && item.BookingId.StartsWith("BK"))
                {
                    if (int.TryParse(item.BookingId.Substring(2), out int num))
                    {
                        if (num > maxId)
                        {
                            maxId = num;
                        }
                    }
                }
            }
            return string.Format("BK{0:D3}", maxId + 1);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out decimal price))
            {
                return;
            }

            string newId = GenerateBookingId();
            BookingModel newBooking = new BookingModel
            {
                BookingId = newId,
                CustomerName = txtCustomerName.Text.Trim(),
                IdentityCard = txtIdentityCard.Text.Trim(),
                RoomType = cboRoomType.SelectedItem != null ? cboRoomType.SelectedItem.ToString() : cboRoomType.Text,
                CheckInDate = dtpCheckIn.Value,
                Days = (int)numDays.Value,
                PricePerNight = price
            };

            bookingList.Add(newBooking);
            bookingBindingSource.MoveLast();
            DisplayCurrentBooking();
            MessageBox.Show("Thêm đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (bookingBindingSource.Current == null)
            {
                MessageBox.Show("Vui lòng chọn thông tin đặt phòng cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput(out decimal price))
            {
                return;
            }

            BookingModel current = (BookingModel)bookingBindingSource.Current;
            current.CustomerName = txtCustomerName.Text.Trim();
            current.IdentityCard = txtIdentityCard.Text.Trim();
            current.RoomType = cboRoomType.SelectedItem != null ? cboRoomType.SelectedItem.ToString() : cboRoomType.Text;
            current.CheckInDate = dtpCheckIn.Value;
            current.Days = (int)numDays.Value;
            current.PricePerNight = price;

            bookingBindingSource.ResetCurrentItem();
            MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (bookingBindingSource.Current == null)
            {
                MessageBox.Show("Vui lòng chọn đặt phòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BookingModel current = (BookingModel)bookingBindingSource.Current;
            DialogResult dialogResult = MessageBox.Show(
                string.Format("Bạn có chắc chắn muốn xóa đặt phòng {0} của khách hàng {1} không?", current.BookingId, current.CustomerName),
                "Cảnh báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                bookingBindingSource.RemoveCurrent();
                if (bookingList.Count == 0)
                {
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    DisplayCurrentBooking();
                }
                MessageBox.Show("Xóa đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            dgvBookings.ClearSelection();
            txtBookingId.Text = GenerateBookingId();
            txtCustomerName.Clear();
            txtIdentityCard.Clear();
            if (cboRoomType.Items.Count > 0)
            {
                cboRoomType.SelectedIndex = 0;
            }
            dtpCheckIn.Value = DateTime.Today;
            numDays.Value = 1;
            txtPricePerNight.Clear();
            txtCustomerName.Focus();
        }
    }
}
