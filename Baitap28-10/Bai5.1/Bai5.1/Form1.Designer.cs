namespace Bai5._1
{
    partial class Form1
    {
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private CheckBox chkShowPassword;
        private Button btnLogin;
        private Button btnExit;
        private ErrorProvider errorProvider;
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblUsername = new Label();
            lblPassword = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            chkShowPassword = new CheckBox();
            btnLogin = new Button();
            btnExit = new Button();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(35, 38);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(110, 20);
            lblUsername.TabIndex = 6;
            lblUsername.Text = "Tên đăng nhập:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(35, 88);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(73, 20);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(165, 35);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(250, 27);
            txtUsername.TabIndex = 0;
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(165, 81);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(250, 27);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.TextChanged += txtPassword_TextChanged;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Location = new Point(165, 125);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(127, 24);
            chkShowPassword.TabIndex = 2;
            chkShowPassword.Text = "Hiện mật khẩu";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(165, 175);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(115, 35);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnExit
            // 
            btnExit.DialogResult = DialogResult.Cancel;
            btnExit.Location = new Point(300, 175);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(115, 35);
            btnExit.TabIndex = 4;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // Form1
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnExit;
            ClientSize = new Size(465, 250);
            Controls.Add(btnExit);
            Controls.Add(btnLogin);
            Controls.Add(chkShowPassword);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
