namespace LibraryManagement.GUI
{
    partial class FormThongKe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpQuaHan = new System.Windows.Forms.GroupBox();
            this.btnTaiQuaHan = new System.Windows.Forms.Button();
            this.lblTongQuaHan = new System.Windows.Forms.Label();
            this.lvQuaHan = new System.Windows.Forms.ListView();
            this.grpThongKe = new System.Windows.Forms.GroupBox();
            this.dgvThongKe = new System.Windows.Forms.DataGridView();
            this.lblChonTheLoai = new System.Windows.Forms.Label();
            this.cboTheLoaiTK = new System.Windows.Forms.ComboBox();
            this.btnDem = new System.Windows.Forms.Button();
            this.lblKetQuaScalar = new System.Windows.Forms.Label();
            this.lblTongPhieu = new System.Windows.Forms.Label();
            this.grpQuaHan.SuspendLayout();
            this.grpThongKe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).BeginInit();
            this.SuspendLayout();
            // 
            // grpQuaHan
            // 
            this.grpQuaHan.Controls.Add(this.lvQuaHan);
            this.grpQuaHan.Controls.Add(this.lblTongQuaHan);
            this.grpQuaHan.Controls.Add(this.btnTaiQuaHan);
            this.grpQuaHan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.grpQuaHan.Location = new System.Drawing.Point(12, 8);
            this.grpQuaHan.Name = "grpQuaHan";
            this.grpQuaHan.Size = new System.Drawing.Size(976, 300);
            this.grpQuaHan.TabIndex = 0;
            this.grpQuaHan.Text = "Cảnh báo sách quá hạn (MySqlDataReader)";
            // 
            // btnTaiQuaHan
            // 
            this.btnTaiQuaHan.Location = new System.Drawing.Point(15, 26);
            this.btnTaiQuaHan.Name = "btnTaiQuaHan";
            this.btnTaiQuaHan.Size = new System.Drawing.Size(150, 32);
            this.btnTaiQuaHan.TabIndex = 1;
            this.btnTaiQuaHan.Text = "Tải danh sách";
            this.btnTaiQuaHan.UseVisualStyleBackColor = true;
            this.btnTaiQuaHan.Click += new System.EventHandler(this.btnTaiQuaHan_Click);
            // 
            // lblTongQuaHan
            // 
            this.lblTongQuaHan.AutoSize = true;
            this.lblTongQuaHan.Location = new System.Drawing.Point(180, 33);
            this.lblTongQuaHan.Name = "lblTongQuaHan";
            this.lblTongQuaHan.TabIndex = 2;
            this.lblTongQuaHan.Text = "";
            this.lblTongQuaHan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongQuaHan.ForeColor = System.Drawing.Color.Firebrick;
            // 
            // lvQuaHan
            // 
            this.lvQuaHan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.lvQuaHan.Location = new System.Drawing.Point(15, 65);
            this.lvQuaHan.Name = "lvQuaHan";
            this.lvQuaHan.Size = new System.Drawing.Size(945, 220);
            this.lvQuaHan.TabIndex = 3;
            this.lvQuaHan.View = System.Windows.Forms.View.Details;
            this.lvQuaHan.FullRowSelect = true;
            this.lvQuaHan.GridLines = true;
            this.lvQuaHan.HideSelection = false;
            this.lvQuaHan.UseCompatibleStateImageBehavior = false;
            // 
            // grpThongKe
            // 
            this.grpThongKe.Controls.Add(this.lblTongPhieu);
            this.grpThongKe.Controls.Add(this.lblKetQuaScalar);
            this.grpThongKe.Controls.Add(this.btnDem);
            this.grpThongKe.Controls.Add(this.cboTheLoaiTK);
            this.grpThongKe.Controls.Add(this.lblChonTheLoai);
            this.grpThongKe.Controls.Add(this.dgvThongKe);
            this.grpThongKe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongKe.Location = new System.Drawing.Point(12, 316);
            this.grpThongKe.Name = "grpThongKe";
            this.grpThongKe.Size = new System.Drawing.Size(976, 292);
            this.grpThongKe.TabIndex = 4;
            this.grpThongKe.Text = "Thống kê lượt mượn theo thể loại (GROUP BY / ExecuteScalar)";
            // 
            // dgvThongKe
            // 
            this.dgvThongKe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.dgvThongKe.Location = new System.Drawing.Point(15, 30);
            this.dgvThongKe.Name = "dgvThongKe";
            this.dgvThongKe.Size = new System.Drawing.Size(560, 245);
            this.dgvThongKe.TabIndex = 5;
            this.dgvThongKe.AllowUserToAddRows = false;
            this.dgvThongKe.AllowUserToDeleteRows = false;
            this.dgvThongKe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThongKe.BackgroundColor = System.Drawing.Color.White;
            this.dgvThongKe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongKe.MultiSelect = false;
            this.dgvThongKe.ReadOnly = true;
            this.dgvThongKe.RowHeadersWidth = 30;
            this.dgvThongKe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // lblChonTheLoai
            // 
            this.lblChonTheLoai.AutoSize = true;
            this.lblChonTheLoai.Location = new System.Drawing.Point(595, 35);
            this.lblChonTheLoai.Name = "lblChonTheLoai";
            this.lblChonTheLoai.TabIndex = 6;
            this.lblChonTheLoai.Text = "Thể loại:";
            // 
            // cboTheLoaiTK
            // 
            this.cboTheLoaiTK.Location = new System.Drawing.Point(670, 32);
            this.cboTheLoaiTK.Name = "cboTheLoaiTK";
            this.cboTheLoaiTK.Size = new System.Drawing.Size(210, 25);
            this.cboTheLoaiTK.TabIndex = 7;
            this.cboTheLoaiTK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // btnDem
            // 
            this.btnDem.Location = new System.Drawing.Point(890, 29);
            this.btnDem.Name = "btnDem";
            this.btnDem.Size = new System.Drawing.Size(70, 32);
            this.btnDem.TabIndex = 8;
            this.btnDem.Text = "Đếm";
            this.btnDem.UseVisualStyleBackColor = true;
            this.btnDem.Click += new System.EventHandler(this.btnDem_Click);
            // 
            // lblKetQuaScalar
            // 
            this.lblKetQuaScalar.Location = new System.Drawing.Point(595, 80);
            this.lblKetQuaScalar.Name = "lblKetQuaScalar";
            this.lblKetQuaScalar.Size = new System.Drawing.Size(365, 60);
            this.lblKetQuaScalar.TabIndex = 9;
            this.lblKetQuaScalar.Text = "";
            this.lblKetQuaScalar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            // 
            // lblTongPhieu
            // 
            this.lblTongPhieu.Location = new System.Drawing.Point(595, 150);
            this.lblTongPhieu.Name = "lblTongPhieu";
            this.lblTongPhieu.Size = new System.Drawing.Size(365, 110);
            this.lblTongPhieu.TabIndex = 10;
            this.lblTongPhieu.Text = "";
            // 
            // FormThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Controls.Add(this.grpThongKe);
            this.Controls.Add(this.grpQuaHan);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1000, 620);
            this.Name = "FormThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cảnh báo quá hạn & Thống kê";
            this.Load += new System.EventHandler(this.FormThongKe_Load);
            this.grpQuaHan.ResumeLayout(false);
            this.grpQuaHan.PerformLayout();
            this.grpThongKe.ResumeLayout(false);
            this.grpThongKe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpQuaHan;
        private System.Windows.Forms.Button btnTaiQuaHan;
        private System.Windows.Forms.Label lblTongQuaHan;
        private System.Windows.Forms.ListView lvQuaHan;
        private System.Windows.Forms.GroupBox grpThongKe;
        private System.Windows.Forms.DataGridView dgvThongKe;
        private System.Windows.Forms.Label lblChonTheLoai;
        private System.Windows.Forms.ComboBox cboTheLoaiTK;
        private System.Windows.Forms.Button btnDem;
        private System.Windows.Forms.Label lblKetQuaScalar;
        private System.Windows.Forms.Label lblTongPhieu;
    }
}
