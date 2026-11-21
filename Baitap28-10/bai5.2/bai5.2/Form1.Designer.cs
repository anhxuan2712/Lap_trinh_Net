namespace bai5._2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblFullName;
        private TextBox txtFullName;
        private GroupBox grpGender;
        private RadioButton rbMale;
        private RadioButton rbFemale;
        private Label lblCourse;
        private ComboBox cboCourse;
        private Label lblBirthDate;
        private DateTimePicker dtpBirthDate;
        private Label lblPhone;
        private MaskedTextBox mtxtPhone;
        private CheckBox chkAgree;
        private Button btnRegister;

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
            lblTitle = new Label();
            lblFullName = new Label();
            txtFullName = new TextBox();
            grpGender = new GroupBox();
            rbMale = new RadioButton();
            rbFemale = new RadioButton();
            lblCourse = new Label();
            cboCourse = new ComboBox();
            lblBirthDate = new Label();
            dtpBirthDate = new DateTimePicker();
            lblPhone = new Label();
            mtxtPhone = new MaskedTextBox();
            chkAgree = new CheckBox();
            btnRegister = new Button();
            grpGender.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(200, 27);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(287, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ĐĂNG KÝ KHÓA HỌC";
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(51, 107);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(76, 20);
            lblFullName.TabIndex = 1;
            lblFullName.Text = "Họ và tên:";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(183, 101);
            txtFullName.Margin = new Padding(3, 4, 3, 4);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(434, 27);
            txtFullName.TabIndex = 2;
            // 
            // grpGender
            // 
            grpGender.Controls.Add(rbMale);
            grpGender.Controls.Add(rbFemale);
            grpGender.Location = new Point(183, 149);
            grpGender.Margin = new Padding(3, 4, 3, 4);
            grpGender.Name = "grpGender";
            grpGender.Padding = new Padding(3, 4, 3, 4);
            grpGender.Size = new Size(434, 73);
            grpGender.TabIndex = 3;
            grpGender.TabStop = false;
            grpGender.Text = "Giới tính";
            // 
            // rbMale
            // 
            rbMale.AutoSize = true;
            rbMale.Location = new Point(17, 31);
            rbMale.Margin = new Padding(3, 4, 3, 4);
            rbMale.Name = "rbMale";
            rbMale.Size = new Size(62, 24);
            rbMale.TabIndex = 0;
            rbMale.TabStop = true;
            rbMale.Text = "Nam";
            rbMale.UseVisualStyleBackColor = true;
            // 
            // rbFemale
            // 
            rbFemale.AutoSize = true;
            rbFemale.Location = new Point(126, 31);
            rbFemale.Margin = new Padding(3, 4, 3, 4);
            rbFemale.Name = "rbFemale";
            rbFemale.Size = new Size(50, 24);
            rbFemale.TabIndex = 1;
            rbFemale.TabStop = true;
            rbFemale.Text = "Nữ";
            rbFemale.UseVisualStyleBackColor = true;
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.Location = new Point(51, 253);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(74, 20);
            lblCourse.TabIndex = 4;
            lblCourse.Text = "Khóa học:";
            // 
            // cboCourse
            // 
            cboCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCourse.FormattingEnabled = true;
            cboCourse.Location = new Point(183, 248);
            cboCourse.Margin = new Padding(3, 4, 3, 4);
            cboCourse.Name = "cboCourse";
            cboCourse.Size = new Size(434, 28);
            cboCourse.TabIndex = 5;
            // 
            // lblBirthDate
            // 
            lblBirthDate.AutoSize = true;
            lblBirthDate.Location = new Point(51, 307);
            lblBirthDate.Name = "lblBirthDate";
            lblBirthDate.Size = new Size(77, 20);
            lblBirthDate.TabIndex = 6;
            lblBirthDate.Text = "Ngày sinh:";
            // 
            // dtpBirthDate
            // 
            dtpBirthDate.Format = DateTimePickerFormat.Short;
            dtpBirthDate.Location = new Point(183, 301);
            dtpBirthDate.Margin = new Padding(3, 4, 3, 4);
            dtpBirthDate.Name = "dtpBirthDate";
            dtpBirthDate.Size = new Size(205, 27);
            dtpBirthDate.TabIndex = 7;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(51, 360);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(81, 20);
            lblPhone.TabIndex = 8;
            lblPhone.Text = "Điện thoại:";
            // 
            // mtxtPhone
            // 
            mtxtPhone.Location = new Point(183, 355);
            mtxtPhone.Margin = new Padding(3, 4, 3, 4);
            mtxtPhone.Mask = "(000) 000-0000";
            mtxtPhone.Name = "mtxtPhone";
            mtxtPhone.Size = new Size(205, 27);
            mtxtPhone.TabIndex = 9;
            // 
            // chkAgree
            // 
            chkAgree.AutoSize = true;
            chkAgree.Location = new Point(183, 413);
            chkAgree.Margin = new Padding(3, 4, 3, 4);
            chkAgree.Name = "chkAgree";
            chkAgree.Size = new Size(284, 24);
            chkAgree.TabIndex = 10;
            chkAgree.Text = "Tôi đồng ý với các điều khoản đăng ký";
            chkAgree.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(276, 474);
            btnRegister.Margin = new Padding(3, 4, 3, 4);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(149, 47);
            btnRegister.TabIndex = 11;
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 560);
            Controls.Add(lblTitle);
            Controls.Add(lblFullName);
            Controls.Add(txtFullName);
            Controls.Add(grpGender);
            Controls.Add(lblCourse);
            Controls.Add(cboCourse);
            Controls.Add(lblBirthDate);
            Controls.Add(dtpBirthDate);
            Controls.Add(lblPhone);
            Controls.Add(mtxtPhone);
            Controls.Add(chkAgree);
            Controls.Add(btnRegister);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng ký khóa học";
            grpGender.ResumeLayout(false);
            grpGender.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
