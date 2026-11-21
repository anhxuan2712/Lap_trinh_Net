
using System;
using System.Windows.Forms;

namespace bai5._2
{
    public partial class Form1 : Form
    {
        private RegistrationRepository? _repository;

        public Form1()
        {
            InitializeComponent();

            // Cấu hình các điều khiển
            mtxtPhone.Mask = "(000) 000-0000";

            dtpBirthDate.Format = DateTimePickerFormat.Custom;
            dtpBirthDate.CustomFormat = "dd/MM/yyyy";

            cboCourse.DropDownStyle = ComboBoxStyle.DropDownList;

            // Gắn sự kiện
            Load += Form1_Load;
            btnRegister.Click += BtnRegister_Click;
        }

        // Khởi tạo repository và nạp khóa học
        private void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                _repository = new RegistrationRepository();

                cboCourse.DisplayMember = nameof(Course.Name);
                cboCourse.ValueMember = nameof(Course.Id);
                cboCourse.DataSource = _repository.GetCourses();

                cboCourse.SelectedIndex = -1;
            }
            catch (Exception exception)
            {
                btnRegister.Enabled = false;

                MessageBox.Show(
                    $"Không thể kết nối hoặc khởi tạo MySQL.\n{exception.Message}",
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Xử lý nút Đăng ký
        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            // 1. Kiểm tra họ tên
            string fullName = txtFullName.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName))
            {
                ShowValidationMessage("Vui lòng nhập họ và tên.");
                txtFullName.Focus();
                return;
            }

            // 2. Kiểm tra giới tính
            if (!rbMale.Checked && !rbFemale.Checked)
            {
                ShowValidationMessage("Vui lòng chọn giới tính.");
                return;
            }

            // 3. Kiểm tra khóa học
            if (cboCourse.SelectedItem is not Course selectedCourse)
            {
                ShowValidationMessage("Vui lòng chọn khóa học.");
                cboCourse.Focus();
                return;
            }

            // 4. Kiểm tra số điện thoại
            if (!mtxtPhone.MaskCompleted)
            {
                ShowValidationMessage(
                    "Vui lòng nhập đầy đủ số điện thoại theo định dạng (000) 000-0000.");

                mtxtPhone.Focus();
                return;
            }

            // 5. Kiểm tra đồng ý điều khoản
            if (!chkAgree.Checked)
            {
                ShowValidationMessage(
                    "Vui lòng đồng ý với các điều khoản đăng ký.");

                chkAgree.Focus();
                return;
            }

            // 6. Lấy dữ liệu từ Form
            string gender = rbMale.Checked ? "Nam" : "Nữ";
            string phone = mtxtPhone.Text;
            DateTime birthDate = dtpBirthDate.Value.Date;

            // 7. Lưu dữ liệu vào MySQL
            try
            {
                if (_repository == null)
                {
                    ShowValidationMessage(
                        "Chưa kết nối được cơ sở dữ liệu MySQL.");
                    return;
                }

                _repository.SaveRegistration(
                    fullName,
                    gender,
                    selectedCourse.Id,
                    birthDate,
                    phone);

                // 8. Hiển thị thông tin đăng ký
                string summary =
                    $"Họ và tên: {fullName}\n" +
                    $"Giới tính: {gender}\n" +
                    $"Khóa học: {selectedCourse.Name}\n" +
                    $"Ngày sinh: {birthDate:dd/MM/yyyy}\n" +
                    $"Điện thoại: {phone}\n" +
                    $"Đồng ý điều khoản: Có";

                MessageBox.Show(
                    summary,
                    "Đăng ký thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    $"Không thể lưu thông tin đăng ký.\n{exception.Message}",
                    "Lỗi lưu dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Hiển thị thông báo dữ liệu chưa hợp lệ
        private static void ShowValidationMessage(string message)
        {
            MessageBox.Show(
                message,
                "Thông tin chưa hợp lệ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
