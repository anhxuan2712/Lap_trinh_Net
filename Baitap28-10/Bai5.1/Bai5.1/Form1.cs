
using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Bai5._1
{
    public partial class Form1 : Form
    {
        private readonly string connectionString =
            "Server=localhost;" +
            "Port=3306;" +
            "Database=login_db;" +
            "Uid=root;" +
            "Pwd=familo23;";

        public Form1()
        {
            InitializeComponent();

            AcceptButton = btnLogin;
            CancelButton = btnExit;

            txtPassword.UseSystemPasswordChar = true;
            chkShowPassword.Checked = false;
        }

        // Hiện hoặc ẩn mật khẩu
        private void chkShowPassword_CheckedChanged(
            object? sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar =
                !chkShowPassword.Checked;
        }

        // Đăng nhập và lưu tài khoản mới vào MySQL
        private void btnLogin_Click(
            object? sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using MySqlConnection conn =
                    new MySqlConnection(connectionString);

                conn.Open();

                // Kiểm tra tên đăng nhập đã tồn tại chưa
                string checkSql =
                    "SELECT Password FROM Users " +
                    "WHERE Username = @username LIMIT 1";

                using MySqlCommand checkCmd =
                    new MySqlCommand(checkSql, conn);

                checkCmd.Parameters.AddWithValue(
                    "@username", txtUsername.Text.Trim());

                object? result = checkCmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                {
                    // Tài khoản mới: lưu vào database
                    string insertSql =
                        "INSERT INTO Users (Username, Password) " +
                        "VALUES (@username, @password)";

                    using MySqlCommand insertCmd =
                        new MySqlCommand(insertSql, conn);

                    insertCmd.Parameters.AddWithValue(
                        "@username", txtUsername.Text.Trim());

                    insertCmd.Parameters.AddWithValue(
                        "@password", txtPassword.Text);

                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show(
                        "Tài khoản mới đã được lưu vào MySQL!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    // Tài khoản đã tồn tại: kiểm tra mật khẩu
                    string savedPassword = result.ToString()!;

                    if (savedPassword == txtPassword.Text)
                    {
                        MessageBox.Show(
                            "Đăng nhập thành công!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Mật khẩu không đúng!",
                            "Đăng nhập thất bại",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(
                    "Lỗi MySQL: " + ex.Message,
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Nút thoát
        private void btnExit_Click(
            object? sender, EventArgs e)
        {
            Close();
        }

        // Xóa lỗi tên đăng nhập
        private void txtUsername_TextChanged(
            object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUsername.Text))
                errorProvider.SetError(txtUsername, "");
        }

        // Xóa lỗi mật khẩu
        private void txtPassword_TextChanged(
            object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                errorProvider.SetError(txtPassword, "");
        }

        // Kiểm tra dữ liệu đầu vào
        private bool ValidateInput()
        {
            errorProvider.Clear();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider.SetError(
                    txtUsername,
                    "Tên đăng nhập không được để trống.");

                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider.SetError(
                    txtPassword,
                    "Mật khẩu không được để trống.");

                isValid = false;
            }

            if (!isValid)
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                    txtUsername.Focus();
                else
                    txtPassword.Focus();
            }

            return isValid;
        }
    }
}
