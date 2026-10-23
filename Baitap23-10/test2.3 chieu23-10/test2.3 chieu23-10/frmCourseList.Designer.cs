using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace test2._3_chieu23_10
{
    partial class frmCourseList
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.txtCourseId = new System.Windows.Forms.TextBox();
            this.lblCourseId = new System.Windows.Forms.Label();
            this.txtCourseName = new System.Windows.Forms.TextBox();
            this.lblCourseName = new System.Windows.Forms.Label();
            this.numCredits = new System.Windows.Forms.NumericUpDown();
            this.lblCredits = new System.Windows.Forms.Label();
            this.cboDepartment = new System.Windows.Forms.ComboBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.txtInstructor = new System.Windows.Forms.TextBox();
            this.lblInstructor = new System.Windows.Forms.Label();
            this.numMaxStudents = new System.Windows.Forms.NumericUpDown();
            this.lblMaxStudents = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.dgvCourses = new System.Windows.Forms.DataGridView();
            this.grpInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCredits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxStudents)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCourses)).BeginInit();
            this.SuspendLayout();

            // grpInput
            this.grpInput.Controls.Add(this.txtCourseId);
            this.grpInput.Controls.Add(this.lblCourseId);
            this.grpInput.Controls.Add(this.txtCourseName);
            this.grpInput.Controls.Add(this.lblCourseName);
            this.grpInput.Controls.Add(this.numCredits);
            this.grpInput.Controls.Add(this.lblCredits);
            this.grpInput.Controls.Add(this.cboDepartment);
            this.grpInput.Controls.Add(this.lblDepartment);
            this.grpInput.Controls.Add(this.txtInstructor);
            this.grpInput.Controls.Add(this.lblInstructor);
            this.grpInput.Controls.Add(this.numMaxStudents);
            this.grpInput.Controls.Add(this.lblMaxStudents);
            this.grpInput.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.grpInput.Location = new System.Drawing.Point(10, 10);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(360, 240);
            this.grpInput.TabIndex = 0;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Thông tin môn học";

            // lblCourseId
            this.lblCourseId.AutoSize = true;
            this.lblCourseId.Location = new System.Drawing.Point(10, 30);
            this.lblCourseId.Name = "lblCourseId";
            this.lblCourseId.Size = new System.Drawing.Size(86, 21);
            this.lblCourseId.TabIndex = 0;
            this.lblCourseId.Text = "Mã môn:";

            // txtCourseId
            this.txtCourseId.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCourseId.Location = new System.Drawing.Point(120, 27);
            this.txtCourseId.Name = "txtCourseId";
            this.txtCourseId.ReadOnly = true;
            this.txtCourseId.Size = new System.Drawing.Size(220, 29);
            this.txtCourseId.TabIndex = 1;

            // lblCourseName
            this.lblCourseName.AutoSize = true;
            this.lblCourseName.Location = new System.Drawing.Point(10, 65);
            this.lblCourseName.Name = "lblCourseName";
            this.lblCourseName.Size = new System.Drawing.Size(90, 21);
            this.lblCourseName.TabIndex = 2;
            this.lblCourseName.Text = "Tên môn:";

            // txtCourseName
            this.txtCourseName.Location = new System.Drawing.Point(120, 62);
            this.txtCourseName.Name = "txtCourseName";
            this.txtCourseName.Size = new System.Drawing.Size(220, 29);
            this.txtCourseName.TabIndex = 3;

            // lblCredits
            this.lblCredits.AutoSize = true;
            this.lblCredits.Location = new System.Drawing.Point(10, 100);
            this.lblCredits.Name = "lblCredits";
            this.lblCredits.Size = new System.Drawing.Size(77, 21);
            this.lblCredits.TabIndex = 4;
            this.lblCredits.Text = "Số tín chỉ:";

            // numCredits
            this.numCredits.Location = new System.Drawing.Point(120, 97);
            this.numCredits.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numCredits.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numCredits.Value = new decimal(new int[] { 3, 0, 0, 0 });
            this.numCredits.Name = "numCredits";
            this.numCredits.Size = new System.Drawing.Size(120, 29);
            this.numCredits.TabIndex = 5;

            // lblDepartment
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Location = new System.Drawing.Point(10, 135);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(48, 21);
            this.lblDepartment.TabIndex = 6;
            this.lblDepartment.Text = "Khoa:";

            // cboDepartment
            this.cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepartment.Items.AddRange(new object[] { "Công nghệ thông tin", "Kinh tế & Quản trị", "Điện - Điện tử", "Ngoại ngữ" });
            this.cboDepartment.Location = new System.Drawing.Point(120, 132);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(220, 29);
            this.cboDepartment.TabIndex = 7;

            // lblInstructor
            this.lblInstructor.AutoSize = true;
            this.lblInstructor.Location = new System.Drawing.Point(10, 170);
            this.lblInstructor.Name = "lblInstructor";
            this.lblInstructor.Size = new System.Drawing.Size(89, 21);
            this.lblInstructor.TabIndex = 8;
            this.lblInstructor.Text = "Giảng viên:";

            // txtInstructor
            this.txtInstructor.Location = new System.Drawing.Point(120, 167);
            this.txtInstructor.Name = "txtInstructor";
            this.txtInstructor.Size = new System.Drawing.Size(220, 29);
            this.txtInstructor.TabIndex = 9;

            // lblMaxStudents
            this.lblMaxStudents.AutoSize = true;
            this.lblMaxStudents.Location = new System.Drawing.Point(10, 205);
            this.lblMaxStudents.Name = "lblMaxStudents";
            this.lblMaxStudents.Size = new System.Drawing.Size(78, 21);
            this.lblMaxStudents.TabIndex = 10;
            this.lblMaxStudents.Text = "Sĩ số tối đa:";

            // numMaxStudents
            this.numMaxStudents.Location = new System.Drawing.Point(120, 202);
            this.numMaxStudents.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numMaxStudents.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            this.numMaxStudents.Value = new decimal(new int[] { 60, 0, 0, 0 });
            this.numMaxStudents.Name = "numMaxStudents";
            this.numMaxStudents.Size = new System.Drawing.Size(120, 29);
            this.numMaxStudents.TabIndex = 11;

            // pnlButtons
            this.pnlButtons.Controls.Add(this.btnAdd);
            this.pnlButtons.Controls.Add(this.btnUpdate);
            this.pnlButtons.Controls.Add(this.btnDelete);
            this.pnlButtons.Controls.Add(this.btnClear);
            this.pnlButtons.Location = new System.Drawing.Point(10, 255);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(360, 45);
            this.pnlButtons.TabIndex = 1;

            // btnAdd
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(0, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(82, 34);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnUpdate
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(90, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(82, 34);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // btnDelete
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(180, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(82, 34);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnClear
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(270, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(82, 34);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Làm mới";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // grpList
            this.grpList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpList.Controls.Add(this.dgvCourses);
            this.grpList.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.grpList.Location = new System.Drawing.Point(380, 10);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(590, 500);
            this.grpList.TabIndex = 2;
            this.grpList.TabStop = false;
            this.grpList.Text = "Danh mục môn học";

            // dgvCourses
            this.dgvCourses.AllowUserToAddRows = false;
            this.dgvCourses.AllowUserToDeleteRows = false;
            this.dgvCourses.BackgroundColor = System.Drawing.Color.White;
            this.dgvCourses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCourses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCourses.MultiSelect = false;
            this.dgvCourses.Name = "dgvCourses";
            this.dgvCourses.ReadOnly = true;
            this.dgvCourses.RowHeadersVisible = false;
            this.dgvCourses.RowTemplate.Height = 28;
            this.dgvCourses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCourses.TabIndex = 0;
            this.dgvCourses.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCourses_CellClick);

            // frmCourseList
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.ClientSize = new System.Drawing.Size(984, 520);
            this.Controls.Add(this.grpList);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.grpInput);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "frmCourseList";
            this.Text = "Danh mục Môn học";
            this.Load += new System.EventHandler(this.frmCourseList_Load);
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCredits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxStudents)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCourses)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.Label lblCourseId;
        private System.Windows.Forms.TextBox txtCourseId;
        private System.Windows.Forms.Label lblCourseName;
        private System.Windows.Forms.TextBox txtCourseName;
        private System.Windows.Forms.Label lblCredits;
        private System.Windows.Forms.NumericUpDown numCredits;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.ComboBox cboDepartment;
        private System.Windows.Forms.Label lblInstructor;
        private System.Windows.Forms.TextBox txtInstructor;
        private System.Windows.Forms.Label lblMaxStudents;
        private System.Windows.Forms.NumericUpDown numMaxStudents;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.DataGridView dgvCourses;
    }
}
