using System;
using System.Windows.Forms;

namespace bt_5._1_10_10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, System.EventArgs e)
        {
            epCheck.Clear();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                epCheck.SetError(txtUsername, "Tên đăng nhập không được để trống.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                epCheck.SetError(txtPassword, "Mật khẩu không được để trống.");
                isValid = false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                epCheck.SetError(txtConfirmPassword, "Mật khẩu nhập lại không khớp.");
                isValid = false;
            }

            if (CalculateAge(dtpBirthDate.Value.Date) < 18)
            {
                epCheck.SetError(dtpBirthDate, "Người đăng ký phải đủ 18 tuổi.");
                isValid = false;
            }

            if (!rdoMale.Checked && !rdoFemale.Checked)
            {
                epCheck.SetError(grpAdditional, "Vui lòng chọn giới tính.");
                isValid = false;
            }

            if (!chkTerms.Checked)
            {
                epCheck.SetError(chkTerms, "Bạn phải đồng ý với điều khoản dịch vụ.");
                isValid = false;
            }

            if (isValid)
            {
                MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            dtpBirthDate.Value = DateTime.Today.AddYears(-18);
            rdoMale.Checked = false;
            rdoFemale.Checked = false;
            chkTerms.Checked = false;
            epCheck.Clear();
            txtUsername.Focus();
        }

    }
}
