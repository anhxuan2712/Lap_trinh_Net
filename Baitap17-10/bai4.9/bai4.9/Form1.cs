using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace bai4._9
{
    public partial class Form1 : Form
    {
        private readonly BindingList<Appointment> appointments = new BindingList<Appointment>();
        private Appointment selectedAppointment;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            appointmentsBindingSource.DataSource = appointments;
            doctorComboBox.SelectedIndex = 0;
            appointmentStatusComboBox.SelectedIndex = 0;
            appointmentDatePicker.Value = DateTime.Now.AddMinutes(30);
            RefreshAppointments();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!ValidateAppointment(null))
                return;

            if (MessageBox.Show("Bạn có chắc muốn thêm lịch hẹn này?", "Xác nhận thêm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            appointments.Add(ReadAppointment());
            RefreshAppointments();
            ClearAppointmentFields();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (selectedAppointment == null)
            {
                MessageBox.Show("Vui lòng chọn lịch hẹn cần cập nhật.", "Chưa chọn lịch hẹn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidateAppointment(selectedAppointment))
                return;

            if (MessageBox.Show("Bạn có chắc muốn cập nhật lịch hẹn này?", "Xác nhận cập nhật", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            Appointment updated = ReadAppointment();
            selectedAppointment.AppointmentId = updated.AppointmentId;
            selectedAppointment.PatientName = updated.PatientName;
            selectedAppointment.Phone = updated.Phone;
            selectedAppointment.AppointmentDate = updated.AppointmentDate;
            selectedAppointment.Doctor = updated.Doctor;
            selectedAppointment.Reason = updated.Reason;
            selectedAppointment.Status = updated.Status;
            Appointment updatedAppointment = selectedAppointment;
            appointmentsBindingSource.ResetBindings(false);
            RefreshAppointments(updatedAppointment.AppointmentId);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (selectedAppointment == null)
            {
                MessageBox.Show("Vui lòng chọn lịch hẹn cần xóa.", "Chưa chọn lịch hẹn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa lịch hẹn của " + selectedAppointment.PatientName + "?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            appointments.Remove(selectedAppointment);
            selectedAppointment = null;
            RefreshAppointments();
            ClearAppointmentFields();
        }

        private void AppointmentsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (appointmentsDataGridView.SelectedRows.Count == 0)
                return;

            Appointment appointment = appointmentsDataGridView.SelectedRows[0].DataBoundItem as Appointment;
            if (appointment == null)
                return;

            selectedAppointment = appointment;
            appointmentIdTextBox.Text = appointment.AppointmentId;
            patientNameTextBox.Text = appointment.PatientName;
            phoneTextBox.Text = appointment.Phone;
            appointmentDatePicker.Value = appointment.AppointmentDate;
            doctorComboBox.SelectedItem = appointment.Doctor;
            appointmentStatusComboBox.SelectedItem = appointment.Status;
            reasonTextBox.Text = appointment.Reason;
            ClearValidationErrors();
        }

        private void FilterCriteriaChanged(object sender, EventArgs e)
        {
            filterDatePicker.Enabled = filterDateCheckBox.Checked;
            RefreshAppointments();
        }

        private void ClearFiltersButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();
            filterDateCheckBox.Checked = false;
            filterStatusComboBox.SelectedIndex = 0;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            selectedAppointment = null;
            appointmentsDataGridView.ClearSelection();
            ClearAppointmentFields();
        }

        private void ExportMenuItem_Click(object sender, EventArgs e)
        {
            if (exportSaveFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            var rows = appointmentsDataGridView.Rows
                .Cast<DataGridViewRow>()
                .Where(row => !row.IsNewRow)
                .Select(row => row.DataBoundItem as Appointment)
                .Where(appointment => appointment != null)
                .ToList();

            using (var writer = new StreamWriter(exportSaveFileDialog.FileName, false, new UTF8Encoding(true)))
            {
                writer.WriteLine("Mã lịch hẹn,Họ tên bệnh nhân,Số điện thoại,Ngày giờ hẹn,Bác sĩ,Lý do khám,Trạng thái");
                foreach (Appointment appointment in rows)
                {
                    writer.WriteLine(string.Join(",", new[]
                    {
                        appointment.AppointmentId,
                        appointment.PatientName,
                        appointment.Phone,
                        appointment.AppointmentDate.ToString("dd/MM/yyyy HH:mm"),
                        appointment.Doctor,
                        appointment.Reason,
                        appointment.Status
                    }.Select(EscapeCsv)));
                }
            }

            MessageBox.Show("Đã xuất " + rows.Count + " lịch hẹn.", "Xuất CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RefreshAppointments(string appointmentIdToSelect = null)
        {
            string search = searchTextBox.Text.Trim();
            string status = filterStatusComboBox.SelectedItem as string;
            DateTime selectedDate = filterDatePicker.Value.Date;

            var visibleAppointments = appointments.Where(appointment =>
                (string.IsNullOrEmpty(search) || appointment.PatientName.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) >= 0 || appointment.Phone.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) &&
                (!filterDateCheckBox.Checked || appointment.AppointmentDate.Date == selectedDate) &&
                (string.IsNullOrEmpty(status) || status == "Tất cả" || appointment.Status == status)).ToList();

            appointmentsBindingSource.DataSource = new BindingList<Appointment>(visibleAppointments);
            appointmentsBindingSource.ResetBindings(false);
            countStatusLabel.Text = "Số lịch hẹn sau khi lọc: " + visibleAppointments.Count;

            if (!string.IsNullOrEmpty(appointmentIdToSelect))
            {
                foreach (DataGridViewRow row in appointmentsDataGridView.Rows)
                {
                    Appointment appointment = row.DataBoundItem as Appointment;
                    if (appointment != null && appointment.AppointmentId == appointmentIdToSelect)
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
        }

        private bool ValidateAppointment(Appointment appointmentBeingEdited)
        {
            ClearValidationErrors();
            bool isValid = true;
            string appointmentId = appointmentIdTextBox.Text.Trim();
            string phone = Regex.Replace(phoneTextBox.Text.Trim(), @"[\s().-]", string.Empty);

            if (string.IsNullOrWhiteSpace(appointmentId))
            {
                validationErrorProvider.SetError(appointmentIdTextBox, "Vui lòng nhập mã lịch hẹn.");
                isValid = false;
            }
            else if (appointments.Any(item => item != appointmentBeingEdited && string.Equals(item.AppointmentId, appointmentId, StringComparison.OrdinalIgnoreCase)))
            {
                validationErrorProvider.SetError(appointmentIdTextBox, "Mã lịch hẹn đã tồn tại.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(patientNameTextBox.Text))
            {
                validationErrorProvider.SetError(patientNameTextBox, "Vui lòng nhập họ tên bệnh nhân.");
                isValid = false;
            }

            if (!Regex.IsMatch(phone, @"^(0[35789]\d{8}|\+84[35789]\d{8}|02\d{8,9})$"))
            {
                validationErrorProvider.SetError(phoneTextBox, "Số điện thoại không hợp lệ.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(reasonTextBox.Text))
            {
                validationErrorProvider.SetError(reasonTextBox, "Vui lòng nhập lý do khám.");
                isValid = false;
            }

            if (appointmentDatePicker.Value < DateTime.Now)
            {
                validationErrorProvider.SetError(appointmentDatePicker, "Ngày giờ hẹn không được nằm trong quá khứ.");
                isValid = false;
            }

            if (!isValid)
                MessageBox.Show("Vui lòng kiểm tra các trường được báo lỗi.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return isValid;
        }

        private Appointment ReadAppointment()
        {
            return new Appointment
            {
                AppointmentId = appointmentIdTextBox.Text.Trim(),
                PatientName = patientNameTextBox.Text.Trim(),
                Phone = Regex.Replace(phoneTextBox.Text.Trim(), @"[\s().-]", string.Empty),
                AppointmentDate = appointmentDatePicker.Value,
                Doctor = doctorComboBox.SelectedItem == null ? string.Empty : doctorComboBox.SelectedItem.ToString(),
                Reason = reasonTextBox.Text.Trim(),
                Status = appointmentStatusComboBox.SelectedItem == null ? "Chờ khám" : appointmentStatusComboBox.SelectedItem.ToString()
            };
        }

        private void ClearValidationErrors()
        {
            validationErrorProvider.SetError(appointmentIdTextBox, string.Empty);
            validationErrorProvider.SetError(patientNameTextBox, string.Empty);
            validationErrorProvider.SetError(phoneTextBox, string.Empty);
            validationErrorProvider.SetError(appointmentDatePicker, string.Empty);
            validationErrorProvider.SetError(reasonTextBox, string.Empty);
        }

        private void ClearAppointmentFields()
        {
            selectedAppointment = null;
            appointmentsDataGridView.ClearSelection();
            appointmentsDataGridView.CurrentCell = null;
            appointmentIdTextBox.Clear();
            patientNameTextBox.Clear();
            phoneTextBox.Clear();
            reasonTextBox.Clear();
            appointmentDatePicker.Value = DateTime.Now.AddMinutes(30);
            doctorComboBox.SelectedIndex = 0;
            appointmentStatusComboBox.SelectedIndex = 0;
            ClearValidationErrors();
        }

        private static string EscapeCsv(string value)
        {
            string escaped = (value ?? string.Empty).Replace("\"", "\"\"");
            return "\"" + escaped + "\"";
        }
    }

    internal sealed class Appointment
    {
        public string AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string Phone { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Doctor { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}
