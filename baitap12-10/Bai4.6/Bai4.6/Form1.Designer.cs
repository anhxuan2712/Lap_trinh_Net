namespace Bai4._6
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Label lblStudentId;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblAvatar;
        private System.Windows.Forms.TextBox txtStudentId;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.ComboBox cboGender;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Button btnChooseAvatar;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnExportCsv;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.lblStudentId = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblAvatar = new System.Windows.Forms.Label();
            this.txtStudentId = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.cboGender = new System.Windows.Forms.ComboBox();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.btnChooseAvatar = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStudents
            // 
            this.dgvStudents.AllowUserToAddRows = false;
            this.dgvStudents.AllowUserToDeleteRows = false;
            this.dgvStudents.AutoGenerateColumns = false;
            this.dgvStudents.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudents.Location = new System.Drawing.Point(12, 12);
            this.dgvStudents.MultiSelect = false;
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.ReadOnly = true;
            this.dgvStudents.RowHeadersVisible = false;
            this.dgvStudents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudents.Size = new System.Drawing.Size(760, 210);
            this.dgvStudents.TabIndex = 0;
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.lblStudentId);
            this.grpDetails.Controls.Add(this.txtStudentId);
            this.grpDetails.Controls.Add(this.lblFullName);
            this.grpDetails.Controls.Add(this.txtFullName);
            this.grpDetails.Controls.Add(this.lblAge);
            this.grpDetails.Controls.Add(this.txtAge);
            this.grpDetails.Controls.Add(this.lblGender);
            this.grpDetails.Controls.Add(this.cboGender);
            this.grpDetails.Controls.Add(this.lblAvatar);
            this.grpDetails.Controls.Add(this.picAvatar);
            this.grpDetails.Controls.Add(this.btnChooseAvatar);
            this.grpDetails.Location = new System.Drawing.Point(12, 234);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(760, 170);
            this.grpDetails.TabIndex = 1;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Thông tin sinh viên";
            // 
            // lblStudentId
            // 
            this.lblStudentId.AutoSize = true;
            this.lblStudentId.Location = new System.Drawing.Point(18, 30);
            this.lblStudentId.Name = "lblStudentId";
            this.lblStudentId.Size = new System.Drawing.Size(50, 15);
            this.lblStudentId.TabIndex = 0;
            this.lblStudentId.Text = "Mã SV:";
            // 
            // txtStudentId
            // 
            this.txtStudentId.Location = new System.Drawing.Point(90, 27);
            this.txtStudentId.Name = "txtStudentId";
            this.txtStudentId.Size = new System.Drawing.Size(180, 23);
            this.txtStudentId.TabIndex = 1;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(300, 30);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(44, 15);
            this.lblFullName.TabIndex = 2;
            this.lblFullName.Text = "Họ tên:";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(360, 27);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(180, 23);
            this.txtFullName.TabIndex = 3;
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(18, 70);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(34, 15);
            this.lblAge.TabIndex = 4;
            this.lblAge.Text = "Tuổi:";
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(90, 67);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(180, 23);
            this.txtAge.TabIndex = 5;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(300, 70);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(55, 15);
            this.lblGender.TabIndex = 6;
            this.lblGender.Text = "Giới tính:";
            // 
            // cboGender
            // 
            this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGender.FormattingEnabled = true;
            this.cboGender.Location = new System.Drawing.Point(360, 67);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(180, 23);
            this.cboGender.TabIndex = 7;
            // 
            // lblAvatar
            // 
            this.lblAvatar.AutoSize = true;
            this.lblAvatar.Location = new System.Drawing.Point(580, 30);
            this.lblAvatar.Name = "lblAvatar";
            this.lblAvatar.Size = new System.Drawing.Size(43, 15);
            this.lblAvatar.TabIndex = 8;
            this.lblAvatar.Text = "Avatar:";
            // 
            // picAvatar
            // 
            this.picAvatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAvatar.Location = new System.Drawing.Point(580, 50);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(80, 80);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAvatar.TabIndex = 9;
            this.picAvatar.TabStop = false;
            // 
            // btnChooseAvatar
            // 
            this.btnChooseAvatar.Location = new System.Drawing.Point(580, 135);
            this.btnChooseAvatar.Name = "btnChooseAvatar";
            this.btnChooseAvatar.Size = new System.Drawing.Size(120, 25);
            this.btnChooseAvatar.TabIndex = 10;
            this.btnChooseAvatar.Text = "Chọn ảnh...";
            this.btnChooseAvatar.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 415);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(120, 415);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(228, 415);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 30);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Location = new System.Drawing.Point(632, 415);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(140, 30);
            this.btnExportCsv.TabIndex = 5;
            this.btnExportCsv.Text = "Xuất file CSV";
            this.btnExportCsv.UseVisualStyleBackColor = true;
            // 
            // Form1
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.dgvStudents);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnExportCsv);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý sinh viên";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
