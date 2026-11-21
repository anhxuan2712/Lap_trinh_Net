namespace ClinicManagement.GUI
{
    partial class FormKeDon
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
            this.grpHoSo = new System.Windows.Forms.GroupBox();
            this.dgvHoSoCho = new System.Windows.Forms.DataGridView();
            this.lblTrieuChungXem = new System.Windows.Forms.Label();
            this.txtTrieuChungXem = new System.Windows.Forms.TextBox();
            this.lblChanDoan = new System.Windows.Forms.Label();
            this.txtChanDoan = new System.Windows.Forms.TextBox();
            this.grpKeDon = new System.Windows.Forms.GroupBox();
            this.lblThuoc = new System.Windows.Forms.Label();
            this.cboThuoc = new System.Windows.Forms.ComboBox();
            this.lblTonKho = new System.Windows.Forms.Label();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblLieuDung = new System.Windows.Forms.Label();
            this.txtLieuDung = new System.Windows.Forms.TextBox();
            this.btnThemThuoc = new System.Windows.Forms.Button();
            this.btnXoaThuoc = new System.Windows.Forms.Button();
            this.dgvDonThuoc = new System.Windows.Forms.DataGridView();
            this.lblTienKham = new System.Windows.Forms.Label();
            this.nudTienKham = new System.Windows.Forms.NumericUpDown();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.btnLuuXuatDon = new System.Windows.Forms.Button();
            this.grpHoSo.SuspendLayout();
            this.grpKeDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoSoCho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonThuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTienKham)).BeginInit();
            this.SuspendLayout();
            // 
            // grpHoSo
            // 
            this.grpHoSo.Controls.Add(this.txtChanDoan);
            this.grpHoSo.Controls.Add(this.lblChanDoan);
            this.grpHoSo.Controls.Add(this.txtTrieuChungXem);
            this.grpHoSo.Controls.Add(this.lblTrieuChungXem);
            this.grpHoSo.Controls.Add(this.dgvHoSoCho);
            this.grpHoSo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.grpHoSo.Location = new System.Drawing.Point(12, 8);
            this.grpHoSo.Name = "grpHoSo";
            this.grpHoSo.Size = new System.Drawing.Size(420, 660);
            this.grpHoSo.TabIndex = 0;
            this.grpHoSo.Text = "Hồ sơ chờ kê đơn";
            // 
            // dgvHoSoCho
            // 
            this.dgvHoSoCho.Location = new System.Drawing.Point(12, 28);
            this.dgvHoSoCho.Name = "dgvHoSoCho";
            this.dgvHoSoCho.Size = new System.Drawing.Size(396, 380);
            this.dgvHoSoCho.TabIndex = 1;
            this.dgvHoSoCho.AllowUserToAddRows = false;
            this.dgvHoSoCho.AllowUserToDeleteRows = false;
            this.dgvHoSoCho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoSoCho.BackgroundColor = System.Drawing.Color.White;
            this.dgvHoSoCho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoSoCho.MultiSelect = false;
            this.dgvHoSoCho.ReadOnly = true;
            this.dgvHoSoCho.RowHeadersWidth = 30;
            this.dgvHoSoCho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoSoCho.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHoSoCho_CellClick);
            // 
            // lblTrieuChungXem
            // 
            this.lblTrieuChungXem.AutoSize = true;
            this.lblTrieuChungXem.Location = new System.Drawing.Point(12, 418);
            this.lblTrieuChungXem.Name = "lblTrieuChungXem";
            this.lblTrieuChungXem.TabIndex = 2;
            this.lblTrieuChungXem.Text = "Triệu chứng:";
            // 
            // txtTrieuChungXem
            // 
            this.txtTrieuChungXem.Location = new System.Drawing.Point(12, 442);
            this.txtTrieuChungXem.Name = "txtTrieuChungXem";
            this.txtTrieuChungXem.Size = new System.Drawing.Size(396, 90);
            this.txtTrieuChungXem.TabIndex = 3;
            this.txtTrieuChungXem.Multiline = true;
            this.txtTrieuChungXem.ReadOnly = true;
            this.txtTrieuChungXem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // 
            // lblChanDoan
            // 
            this.lblChanDoan.AutoSize = true;
            this.lblChanDoan.Location = new System.Drawing.Point(12, 542);
            this.lblChanDoan.Name = "lblChanDoan";
            this.lblChanDoan.TabIndex = 4;
            this.lblChanDoan.Text = "Chẩn đoán của bác sĩ:";
            // 
            // txtChanDoan
            // 
            this.txtChanDoan.Location = new System.Drawing.Point(12, 566);
            this.txtChanDoan.Name = "txtChanDoan";
            this.txtChanDoan.Size = new System.Drawing.Size(396, 78);
            this.txtChanDoan.TabIndex = 5;
            this.txtChanDoan.Multiline = true;
            this.txtChanDoan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // 
            // grpKeDon
            // 
            this.grpKeDon.Controls.Add(this.btnLuuXuatDon);
            this.grpKeDon.Controls.Add(this.lblTongTien);
            this.grpKeDon.Controls.Add(this.nudTienKham);
            this.grpKeDon.Controls.Add(this.lblTienKham);
            this.grpKeDon.Controls.Add(this.dgvDonThuoc);
            this.grpKeDon.Controls.Add(this.btnXoaThuoc);
            this.grpKeDon.Controls.Add(this.btnThemThuoc);
            this.grpKeDon.Controls.Add(this.txtLieuDung);
            this.grpKeDon.Controls.Add(this.lblLieuDung);
            this.grpKeDon.Controls.Add(this.nudSoLuong);
            this.grpKeDon.Controls.Add(this.lblSoLuong);
            this.grpKeDon.Controls.Add(this.lblTonKho);
            this.grpKeDon.Controls.Add(this.cboThuoc);
            this.grpKeDon.Controls.Add(this.lblThuoc);
            this.grpKeDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.grpKeDon.Location = new System.Drawing.Point(442, 8);
            this.grpKeDon.Name = "grpKeDon";
            this.grpKeDon.Size = new System.Drawing.Size(646, 660);
            this.grpKeDon.TabIndex = 6;
            this.grpKeDon.Text = "Kê đơn thuốc";
            // 
            // lblThuoc
            // 
            this.lblThuoc.AutoSize = true;
            this.lblThuoc.Location = new System.Drawing.Point(15, 32);
            this.lblThuoc.Name = "lblThuoc";
            this.lblThuoc.TabIndex = 7;
            this.lblThuoc.Text = "Thuốc:";
            // 
            // cboThuoc
            // 
            this.cboThuoc.Location = new System.Drawing.Point(100, 29);
            this.cboThuoc.Name = "cboThuoc";
            this.cboThuoc.Size = new System.Drawing.Size(300, 25);
            this.cboThuoc.TabIndex = 8;
            this.cboThuoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThuoc.SelectedIndexChanged += new System.EventHandler(this.cboThuoc_SelectedIndexChanged);
            // 
            // lblTonKho
            // 
            this.lblTonKho.AutoSize = true;
            this.lblTonKho.Location = new System.Drawing.Point(410, 32);
            this.lblTonKho.Name = "lblTonKho";
            this.lblTonKho.TabIndex = 9;
            this.lblTonKho.Text = "";
            this.lblTonKho.ForeColor = System.Drawing.Color.DimGray;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(15, 68);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.TabIndex = 10;
            this.lblSoLuong.Text = "Số lượng:";
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.Location = new System.Drawing.Point(100, 65);
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(80, 25);
            this.nudSoLuong.TabIndex = 11;
            this.nudSoLuong.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblLieuDung
            // 
            this.lblLieuDung.AutoSize = true;
            this.lblLieuDung.Location = new System.Drawing.Point(195, 68);
            this.lblLieuDung.Name = "lblLieuDung";
            this.lblLieuDung.TabIndex = 12;
            this.lblLieuDung.Text = "Liều dùng:";
            // 
            // txtLieuDung
            // 
            this.txtLieuDung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.txtLieuDung.Location = new System.Drawing.Point(280, 65);
            this.txtLieuDung.Name = "txtLieuDung";
            this.txtLieuDung.Size = new System.Drawing.Size(350, 25);
            this.txtLieuDung.TabIndex = 13;
            // 
            // btnThemThuoc
            // 
            this.btnThemThuoc.Location = new System.Drawing.Point(100, 102);
            this.btnThemThuoc.Name = "btnThemThuoc";
            this.btnThemThuoc.Size = new System.Drawing.Size(130, 32);
            this.btnThemThuoc.TabIndex = 14;
            this.btnThemThuoc.Text = "Thêm thuốc";
            this.btnThemThuoc.UseVisualStyleBackColor = true;
            this.btnThemThuoc.Click += new System.EventHandler(this.btnThemThuoc_Click);
            // 
            // btnXoaThuoc
            // 
            this.btnXoaThuoc.Location = new System.Drawing.Point(240, 102);
            this.btnXoaThuoc.Name = "btnXoaThuoc";
            this.btnXoaThuoc.Size = new System.Drawing.Size(130, 32);
            this.btnXoaThuoc.TabIndex = 15;
            this.btnXoaThuoc.Text = "Xóa thuốc";
            this.btnXoaThuoc.UseVisualStyleBackColor = true;
            this.btnXoaThuoc.Click += new System.EventHandler(this.btnXoaThuoc_Click);
            // 
            // dgvDonThuoc
            // 
            this.dgvDonThuoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDonThuoc.Location = new System.Drawing.Point(15, 145);
            this.dgvDonThuoc.Name = "dgvDonThuoc";
            this.dgvDonThuoc.Size = new System.Drawing.Size(615, 380);
            this.dgvDonThuoc.TabIndex = 16;
            this.dgvDonThuoc.AllowUserToAddRows = false;
            this.dgvDonThuoc.AllowUserToDeleteRows = false;
            this.dgvDonThuoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDonThuoc.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonThuoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonThuoc.MultiSelect = false;
            this.dgvDonThuoc.ReadOnly = true;
            this.dgvDonThuoc.RowHeadersWidth = 30;
            this.dgvDonThuoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // lblTienKham
            // 
            this.lblTienKham.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.lblTienKham.AutoSize = true;
            this.lblTienKham.Location = new System.Drawing.Point(15, 545);
            this.lblTienKham.Name = "lblTienKham";
            this.lblTienKham.TabIndex = 17;
            this.lblTienKham.Text = "Tiền khám:";
            // 
            // nudTienKham
            // 
            this.nudTienKham.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.nudTienKham.Location = new System.Drawing.Point(100, 542);
            this.nudTienKham.Name = "nudTienKham";
            this.nudTienKham.Size = new System.Drawing.Size(140, 25);
            this.nudTienKham.TabIndex = 18;
            this.nudTienKham.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            this.nudTienKham.Increment = new decimal(new int[] { 10000, 0, 0, 0 });
            this.nudTienKham.Value = new decimal(new int[] { 100000, 0, 0, 0 });
            this.nudTienKham.ThousandsSeparator = true;
            // 
            // lblTongTien
            // 
            this.lblTongTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Location = new System.Drawing.Point(255, 545);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.TabIndex = 19;
            this.lblTongTien.Text = "";
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.Firebrick;
            // 
            // btnLuuXuatDon
            // 
            this.btnLuuXuatDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuuXuatDon.Location = new System.Drawing.Point(430, 585);
            this.btnLuuXuatDon.Name = "btnLuuXuatDon";
            this.btnLuuXuatDon.Size = new System.Drawing.Size(200, 58);
            this.btnLuuXuatDon.TabIndex = 20;
            this.btnLuuXuatDon.Text = "LƯU && XUẤT ĐƠN";
            this.btnLuuXuatDon.UseVisualStyleBackColor = true;
            this.btnLuuXuatDon.Click += new System.EventHandler(this.btnLuuXuatDon_Click);
            // 
            // FormKeDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.grpKeDon);
            this.Controls.Add(this.grpHoSo);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1100, 680);
            this.Name = "FormKeDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Khám bệnh & Kê đơn thuốc";
            this.Load += new System.EventHandler(this.FormKeDon_Load);
            this.grpHoSo.ResumeLayout(false);
            this.grpHoSo.PerformLayout();
            this.grpKeDon.ResumeLayout(false);
            this.grpKeDon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoSoCho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonThuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTienKham)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHoSo;
        private System.Windows.Forms.DataGridView dgvHoSoCho;
        private System.Windows.Forms.Label lblTrieuChungXem;
        private System.Windows.Forms.TextBox txtTrieuChungXem;
        private System.Windows.Forms.Label lblChanDoan;
        private System.Windows.Forms.TextBox txtChanDoan;
        private System.Windows.Forms.GroupBox grpKeDon;
        private System.Windows.Forms.Label lblThuoc;
        private System.Windows.Forms.ComboBox cboThuoc;
        private System.Windows.Forms.Label lblTonKho;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Label lblLieuDung;
        private System.Windows.Forms.TextBox txtLieuDung;
        private System.Windows.Forms.Button btnThemThuoc;
        private System.Windows.Forms.Button btnXoaThuoc;
        private System.Windows.Forms.DataGridView dgvDonThuoc;
        private System.Windows.Forms.Label lblTienKham;
        private System.Windows.Forms.NumericUpDown nudTienKham;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Button btnLuuXuatDon;
    }
}
