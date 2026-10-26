using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bai4._10
{
    public partial class Form1 : Form
    {
        private readonly BindingList<LoanRecord> loans = new BindingList<LoanRecord>();
        private readonly BindingSource loanSource = new BindingSource();
        private LoanRecord selectedRecord;

        public Form1()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            equipmentComboBox.Items.AddRange(new object[]
            {
                "Laptop",
                "Máy chiếu",
                "Bộ Arduino",
                "Máy đo đa năng",
                "Bộ thí nghiệm điện"
            });
            statusFilterComboBox.Items.AddRange(new object[]
            {
                "Tất cả",
                "Đang mượn",
                "Quá hạn",
                "Đã trả"
            });
            statusFilterComboBox.SelectedIndex = 0;
            borrowDatePicker.Value = DateTime.Today;
            dueDatePicker.Value = DateTime.Today.AddDays(7);
            loanSource.DataSource = loans;
            loansDataGridView.DataSource = loanSource;

            addButton.Click += AddButton_Click;
            updateButton.Click += UpdateButton_Click;
            deleteButton.Click += DeleteButton_Click;
            markReturnedButton.Click += MarkReturnedButton_Click;
            clearButton.Click += ClearButton_Click;
            exportButton.Click += ExportButton_Click;
            loansDataGridView.SelectionChanged += LoansDataGridView_SelectionChanged;
            searchTextBox.TextChanged += SearchCriteria_Changed;
            statusFilterComboBox.SelectedIndexChanged += SearchCriteria_Changed;

            ApplyFilters();
            SetSelectedRecord(null);
            UpdateStatusTotals();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            LoanRecord record;
            if (!TryBuildRecord(null, out record))
                return;

            loans.Add(record);
            RefreshGrid();
            ClearInput();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (selectedRecord == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            LoanRecord updatedRecord;
            if (!TryBuildRecord(selectedRecord, out updatedRecord))
                return;

            LoanRecord recordToUpdate = selectedRecord;
            recordToUpdate.LoanId = updatedRecord.LoanId;
            recordToUpdate.StudentId = updatedRecord.StudentId;
            recordToUpdate.StudentName = updatedRecord.StudentName;
            recordToUpdate.Equipment = updatedRecord.Equipment;
            recordToUpdate.Quantity = updatedRecord.Quantity;
            recordToUpdate.BorrowDate = updatedRecord.BorrowDate;
            recordToUpdate.DueDate = updatedRecord.DueDate;
            recordToUpdate.ActualReturnDate = updatedRecord.ActualReturnDate;
            RefreshGrid();
            SelectRecord(recordToUpdate);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (selectedRecord == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            loans.Remove(selectedRecord);
            RefreshGrid();
            ClearInput();
        }

        private void MarkReturnedButton_Click(object sender, EventArgs e)
        {
            if (selectedRecord == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu cần đánh dấu đã trả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DateTime.Today < selectedRecord.BorrowDate.Date)
            {
                errorProvider1.SetError(actualReturnDatePicker, "Ngày trả thực tế không được trước ngày mượn.");
                return;
            }

            LoanRecord returnedRecord = selectedRecord;
            returnedRecord.ActualReturnDate = DateTime.Today;
            RefreshGrid();
            SelectRecord(returnedRecord);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void SearchCriteria_Changed(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Xuất danh sách phiếu mượn";
                dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                dialog.DefaultExt = "csv";
                dialog.AddExtension = true;
                dialog.FileName = "phieu-muon-thiet-bi.csv";

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                StringBuilder csv = new StringBuilder();
                csv.AppendLine("Mã phiếu,Mã sinh viên,Họ tên sinh viên,Thiết bị,Số lượng,Ngày mượn,Hạn trả,Ngày trả thực tế,Tình trạng");
                foreach (LoanRecord record in GetFilteredRecords())
                {
                    csv.Append(CsvField(record.LoanId)).Append(',')
                        .Append(CsvField(record.StudentId)).Append(',')
                        .Append(CsvField(record.StudentName)).Append(',')
                        .Append(CsvField(record.Equipment)).Append(',')
                        .Append(record.Quantity.ToString(CultureInfo.InvariantCulture)).Append(',')
                        .Append(record.BorrowDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)).Append(',')
                        .Append(record.DueDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)).Append(',')
                        .Append(record.ActualReturnDate.HasValue
                            ? record.ActualReturnDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                            : string.Empty).Append(',')
                        .AppendLine(CsvField(record.Status));
                }

                File.WriteAllText(dialog.FileName, csv.ToString(), new UTF8Encoding(true));
                MessageBox.Show("Đã xuất danh sách phiếu ra CSV.", "Xuất dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoansDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            LoanRecord record = loansDataGridView.CurrentRow == null
                ? null
                : loansDataGridView.CurrentRow.DataBoundItem as LoanRecord;
            SetSelectedRecord(record);
        }

        private void SetSelectedRecord(LoanRecord record)
        {
            selectedRecord = record;
            bool hasSelection = record != null;
            updateButton.Enabled = hasSelection;
            deleteButton.Enabled = hasSelection;
            markReturnedButton.Enabled = hasSelection && !record.ActualReturnDate.HasValue;

            if (!hasSelection)
            {
                currentStatusLabel.Text = "Chưa chọn phiếu";
                return;
            }

            loanIdTextBox.Text = record.LoanId;
            studentIdTextBox.Text = record.StudentId;
            studentNameTextBox.Text = record.StudentName;
            equipmentComboBox.SelectedItem = record.Equipment;
            quantityNumericUpDown.Value = record.Quantity;
            borrowDatePicker.Value = record.BorrowDate;
            dueDatePicker.Value = record.DueDate;
            actualReturnDatePicker.Checked = record.ActualReturnDate.HasValue;
            if (record.ActualReturnDate.HasValue)
                actualReturnDatePicker.Value = record.ActualReturnDate.Value;
            currentStatusLabel.Text = record.Status;
        }

        private void ClearInput()
        {
            loansDataGridView.ClearSelection();
            loansDataGridView.CurrentCell = null;
            SetSelectedRecord(null);
            loanIdTextBox.Clear();
            studentIdTextBox.Clear();
            studentNameTextBox.Clear();
            equipmentComboBox.SelectedIndex = -1;
            quantityNumericUpDown.Value = 1;
            borrowDatePicker.Value = DateTime.Today;
            dueDatePicker.Value = DateTime.Today.AddDays(7);
            actualReturnDatePicker.Checked = false;
            errorProvider1.Clear();
        }

        private void SelectRecord(LoanRecord record)
        {
            foreach (DataGridViewRow row in loansDataGridView.Rows)
            {
                if (ReferenceEquals(row.DataBoundItem, record))
                {
                    row.Selected = true;
                    loansDataGridView.CurrentCell = row.Cells[0];
                    SetSelectedRecord(record);
                    return;
                }
            }
        }

        private void RefreshGrid()
        {
            ApplyFilters();
            UpdateStatusTotals();
        }

        private bool TryBuildRecord(LoanRecord currentRecord, out LoanRecord record)
        {
            errorProvider1.Clear();
            bool isValid = true;
            string loanId = loanIdTextBox.Text.Trim();
            string studentId = studentIdTextBox.Text.Trim();
            string equipment = equipmentComboBox.SelectedItem as string;
            DateTime? actualReturnDate = actualReturnDatePicker.Checked
                ? actualReturnDatePicker.Value.Date
                : (DateTime?)null;

            if (string.IsNullOrWhiteSpace(loanId))
            {
                errorProvider1.SetError(loanIdTextBox, "Vui lòng nhập mã phiếu.");
                isValid = false;
            }
            else if (loans.Any(existing => !ReferenceEquals(existing, currentRecord)
                && string.Equals(existing.LoanId, loanId, StringComparison.OrdinalIgnoreCase)))
            {
                errorProvider1.SetError(loanIdTextBox, "Mã phiếu đã tồn tại.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(studentId))
            {
                errorProvider1.SetError(studentIdTextBox, "Vui lòng nhập mã sinh viên.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(equipment))
            {
                errorProvider1.SetError(equipmentComboBox, "Vui lòng chọn thiết bị.");
                isValid = false;
            }

            if (quantityNumericUpDown.Value <= 0)
            {
                errorProvider1.SetError(quantityNumericUpDown, "Số lượng phải lớn hơn 0.");
                isValid = false;
            }

            DateTime borrowDate = borrowDatePicker.Value.Date;
            DateTime dueDate = dueDatePicker.Value.Date;
            if (dueDate < borrowDate)
            {
                errorProvider1.SetError(dueDatePicker, "Hạn trả không được trước ngày mượn.");
                isValid = false;
            }

            if (actualReturnDate.HasValue && actualReturnDate.Value < borrowDate)
            {
                errorProvider1.SetError(actualReturnDatePicker, "Ngày trả thực tế không được trước ngày mượn.");
                isValid = false;
            }

            if (!isValid)
            {
                record = null;
                return false;
            }

            record = new LoanRecord
            {
                LoanId = loanId,
                StudentId = studentId,
                StudentName = studentNameTextBox.Text.Trim(),
                Equipment = equipment,
                Quantity = decimal.ToInt32(quantityNumericUpDown.Value),
                BorrowDate = borrowDate,
                DueDate = dueDate,
                ActualReturnDate = actualReturnDate
            };
            return true;
        }

        private void ApplyFilters()
        {
            loanSource.DataSource = new BindingList<LoanRecord>(GetFilteredRecords());
            UpdateStatusTotals();
        }

        private System.Collections.Generic.List<LoanRecord> GetFilteredRecords()
        {
            string searchText = searchTextBox.Text.Trim();
            string selectedStatus = statusFilterComboBox.SelectedItem as string;
            return loans.Where(record =>
                (string.IsNullOrEmpty(searchText)
                    || record.StudentId.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase) >= 0
                    || record.Equipment.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase) >= 0)
                && (string.IsNullOrEmpty(selectedStatus)
                    || selectedStatus == "Tất cả"
                    || record.Status == selectedStatus)).ToList();
        }

        private static string CsvField(string value)
        {
            return "\"" + (value ?? string.Empty).Replace("\"", "\"\"") + "\"";
        }

        private void UpdateStatusTotals()
        {
            totalCountStatusLabel.Text = "Tổng số phiếu: " + loans.Count;
            overdueCountStatusLabel.Text = "Số phiếu quá hạn: " + loans.Count(record => record.IsOverdue);
        }
    }

    internal sealed class LoanRecord
    {
        public string LoanId { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string Equipment { get; set; }
        public int Quantity { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public bool IsOverdue
        {
            get { return !ActualReturnDate.HasValue && DueDate.Date < DateTime.Today; }
        }
        public string Status
        {
            get
            {
                if (ActualReturnDate.HasValue)
                    return "Đã trả";
                return IsOverdue ? "Quá hạn" : "Đang mượn";
            }
        }
    }
}
