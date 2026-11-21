namespace LibraryManagement.GUI
{
    partial class FormMuonTra
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
            this.grpLapPhieu = new System.Windows.Forms.GroupBox();
            this.lblDocGia = new System.Windows.Forms.Label();
            this.cboDocGia = new System.Windows.Forms.ComboBox();
            this.lblTinhTrangThe = new System.Windows.Forms.Label();
            this.lblHanTra = new System.Windows.Forms.Label();
            this.dtpHanTra = new System.Windows.Forms.DateTimePicker();
            this.lblSach = new System.Windows.Forms.Label();
            this.cboSach = new System.Windows.Forms.ComboBox();
            this.lblSL = new System.Windows.Forms.Label();
            this.nudSLMuon = new System.Windows.Forms.NumericUpDown();
            this.btnThemSach = new System.Windows.Forms.Button();
            this.btnBoSach = new System.Windows.Forms.Button();
            this.dgvSachMuon = new System.Windows.Forms.DataGridView();
            this.btnLapPhieu = new System.Windows.Forms.Button();
            this.grpDanhSach = new System.Windows.Forms.GroupBox();
            this.lblLocDocGia = new System.Windows.Forms.Label();
            this.cboLocDocGia = new System.Windows.Forms.ComboBox();
            this.lblLocTrangThai = new System.Windows.Forms.Label();
            this.cboLocTrangThai = new System.Windows.Forms.ComboBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnCapNhatQuaHan = new System.Windows.Forms.Button();
            this.btnTraSach = new System.Windows.Forms.Button();
            this.dgvPhieuMuon = new System.Windows.Forms.DataGridView();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.grpLapPhieu.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSLMuon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSachMuon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuMuon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // grpLapPhieu
            // 
            this.grpLapPhieu.Controls.Add(this.btnLapPhieu);
            this.grpLapPhieu.Controls.Add(this.dgvSachMuon);
            this.grpLapPhieu.Controls.Add(this.btnBoSach);
            this.grpLapPhieu.Controls.Add(this.btnThemSach);
            this.grpLapPhieu.Controls.Add(this.nudSLMuon);
            this.grpLapPhieu.Controls.Add(this.lblSL);
            this.grpLapPhieu.Controls.Add(this.cboSach);
            this.grpLapPhieu.Controls.Add(this.lblSach);
            this.grpLapPhieu.Controls.Add(this.dtpHanTra);
            this.grpLapPhieu.Controls.Add(this.lblHanTra);
            this.grpLapPhieu.Controls.Add(this.lblTinhTrangThe);
            this.grpLapPhieu.Controls.Add(this.cboDocGia);
            this.grpLapPhieu.Controls.Add(this.lblDocGia);
            this.grpLapPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.grpLapPhieu.Location = new System.Drawing.Point(12, 8);
            this.grpLapPhieu.Name = "grpLapPhieu";
            this.grpLapPhieu.Size = new System.Drawing.Size(1076, 262);
            this.grpLapPhieu.TabIndex = 0;
            this.grpLapPhieu.Text = "Lập phiếu mượn mới";
            // 
            // lblDocGia
            // 
            this.lblDocGia.AutoSize = true;
            this.lblDocGia.Location = new System.Drawing.Point(15, 30);
            this.lblDocGia.Name = "lblDocGia";
            this.lblDocGia.TabIndex = 1;
            this.lblDocGia.Text = "Độc giả:";
            // 
            // cboDocGia
            // 
            this.cboDocGia.Location = new System.Drawing.Point(110, 27);
            this.cboDocGia.Name = "cboDocGia";
            this.cboDocGia.Size = new System.Drawing.Size(300, 25);
            this.cboDocGia.TabIndex = 2;
            this.cboDocGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocGia.SelectedIndexChanged += new System.EventHandler(this.cboDocGia_SelectedIndexChanged);
            // 
            // lblTinhTrangThe
            // 
            this.lblTinhTrangThe.AutoSize = true;
            this.lblTinhTrangThe.Location = new System.Drawing.Point(420, 30);
            this.lblTinhTrangThe.Name = "lblTinhTrangThe";
            this.lblTinhTrangThe.TabIndex = 3;
            this.lblTinhTrangThe.Text = "";
            this.lblTinhTrangThe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            // 
            // lblHanTra
            // 
            this.lblHanTra.AutoSize = true;
            this.lblHanTra.Location = new System.Drawing.Point(15, 65);
            this.lblHanTra.Name = "lblHanTra";
            this.lblHanTra.TabIndex = 4;
            this.lblHanTra.Text = "Hạn trả:";
            // 
            // dtpHanTra
            // 
            this.dtpHanTra.Location = new System.Drawing.Point(110, 62);
            this.dtpHanTra.Name = "dtpHanTra";
            this.dtpHanTra.Size = new System.Drawing.Size(180, 25);
            this.dtpHanTra.TabIndex = 5;
            this.dtpHanTra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHanTra.CustomFormat = "dd/MM/yyyy";
            // 
            // lblSach
            // 
            this.lblSach.AutoSize = true;
            this.lblSach.Location = new System.Drawing.Point(15, 100);
            this.lblSach.Name = "lblSach";
            this.lblSach.TabIndex = 6;
            this.lblSach.Text = "Sách:";
            // 
            // cboSach
            // 
            this.cboSach.Location = new System.Drawing.Point(110, 97);
            this.cboSach.Name = "cboSach";
            this.cboSach.Size = new System.Drawing.Size(300, 25);
            this.cboSach.TabIndex = 7;
            this.cboSach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // lblSL
            // 
            this.lblSL.AutoSize = true;
            this.lblSL.Location = new System.Drawing.Point(420, 100);
            this.lblSL.Name = "lblSL";
            this.lblSL.TabIndex = 8;
            this.lblSL.Text = "SL:";
            // 
            // nudSLMuon
            // 
            this.nudSLMuon.Location = new System.Drawing.Point(455, 97);
            this.nudSLMuon.Name = "nudSLMuon";
            this.nudSLMuon.Size = new System.Drawing.Size(70, 25);
            this.nudSLMuon.TabIndex = 9;
            this.nudSLMuon.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.nudSLMuon.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSLMuon.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnThemSach
            // 
            this.btnThemSach.Location = new System.Drawing.Point(540, 94);
            this.btnThemSach.Name = "btnThemSach";
            this.btnThemSach.Size = new System.Drawing.Size(140, 32);
            this.btnThemSach.TabIndex = 10;
            this.btnThemSach.Text = "Thêm vào phiếu";
            this.btnThemSach.UseVisualStyleBackColor = true;
            this.btnThemSach.Click += new System.EventHandler(this.btnThemSach_Click);
            // 
            // btnBoSach
            // 
            this.btnBoSach.Location = new System.Drawing.Point(690, 94);
            this.btnBoSach.Name = "btnBoSach";
            this.btnBoSach.Size = new System.Drawing.Size(140, 32);
            this.btnBoSach.TabIndex = 11;
            this.btnBoSach.Text = "Bỏ khỏi phiếu";
            this.btnBoSach.UseVisualStyleBackColor = true;
            this.btnBoSach.Click += new System.EventHandler(this.btnBoSach_Click);
            // 
            // dgvSachMuon
            // 
            this.dgvSachMuon.Location = new System.Drawing.Point(110, 135);
            this.dgvSachMuon.Name = "dgvSachMuon";
            this.dgvSachMuon.Size = new System.Drawing.Size(720, 112);
            this.dgvSachMuon.TabIndex = 12;
            this.dgvSachMuon.AllowUserToAddRows = false;
            this.dgvSachMuon.AllowUserToDeleteRows = false;
            this.dgvSachMuon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSachMuon.BackgroundColor = System.Drawing.Color.White;
            this.dgvSachMuon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSachMuon.MultiSelect = false;
            this.dgvSachMuon.ReadOnly = true;
            this.dgvSachMuon.RowHeadersWidth = 30;
            this.dgvSachMuon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // btnLapPhieu
            // 
            this.btnLapPhieu.Location = new System.Drawing.Point(850, 135);
            this.btnLapPhieu.Name = "btnLapPhieu";
            this.btnLapPhieu.Size = new System.Drawing.Size(205, 55);
            this.btnLapPhieu.TabIndex = 13;
            this.btnLapPhieu.Text = "LẬP PHIẾU MƯỢN";
            this.btnLapPhieu.UseVisualStyleBackColor = true;
            this.btnLapPhieu.Click += new System.EventHandler(this.btnLapPhieu_Click);
            // 
            // grpDanhSach
            // 
            this.grpDanhSach.Controls.Add(this.dgvChiTiet);
            this.grpDanhSach.Controls.Add(this.dgvPhieuMuon);
            this.grpDanhSach.Controls.Add(this.btnTraSach);
            this.grpDanhSach.Controls.Add(this.btnCapNhatQuaHan);
            this.grpDanhSach.Controls.Add(this.btnLoc);
            this.grpDanhSach.Controls.Add(this.cboLocTrangThai);
            this.grpDanhSach.Controls.Add(this.lblLocTrangThai);
            this.grpDanhSach.Controls.Add(this.cboLocDocGia);
            this.grpDanhSach.Controls.Add(this.lblLocDocGia);
            this.grpDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.grpDanhSach.Location = new System.Drawing.Point(12, 278);
            this.grpDanhSach.Name = "grpDanhSach";
            this.grpDanhSach.Size = new System.Drawing.Size(1076, 392);
            this.grpDanhSach.TabIndex = 14;
            this.grpDanhSach.Text = "Danh sách phiếu mượn";
            // 
            // lblLocDocGia
            // 
            this.lblLocDocGia.AutoSize = true;
            this.lblLocDocGia.Location = new System.Drawing.Point(15, 32);
            this.lblLocDocGia.Name = "lblLocDocGia";
            this.lblLocDocGia.TabIndex = 15;
            this.lblLocDocGia.Text = "Độc giả:";
            // 
            // cboLocDocGia
            // 
            this.cboLocDocGia.Location = new System.Drawing.Point(85, 29);
            this.cboLocDocGia.Name = "cboLocDocGia";
            this.cboLocDocGia.Size = new System.Drawing.Size(260, 25);
            this.cboLocDocGia.TabIndex = 16;
            this.cboLocDocGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // lblLocTrangThai
            // 
            this.lblLocTrangThai.AutoSize = true;
            this.lblLocTrangThai.Location = new System.Drawing.Point(360, 32);
            this.lblLocTrangThai.Name = "lblLocTrangThai";
            this.lblLocTrangThai.TabIndex = 17;
            this.lblLocTrangThai.Text = "Trạng thái:";
            // 
            // cboLocTrangThai
            // 
            this.cboLocTrangThai.Location = new System.Drawing.Point(445, 29);
            this.cboLocTrangThai.Name = "cboLocTrangThai";
            this.cboLocTrangThai.Size = new System.Drawing.Size(160, 25);
            this.cboLocTrangThai.TabIndex = 18;
            this.cboLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(615, 26);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(90, 32);
            this.btnLoc.TabIndex = 19;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // btnCapNhatQuaHan
            // 
            this.btnCapNhatQuaHan.Location = new System.Drawing.Point(715, 26);
            this.btnCapNhatQuaHan.Name = "btnCapNhatQuaHan";
            this.btnCapNhatQuaHan.Size = new System.Drawing.Size(170, 32);
            this.btnCapNhatQuaHan.TabIndex = 20;
            this.btnCapNhatQuaHan.Text = "Cập nhật quá hạn";
            this.btnCapNhatQuaHan.UseVisualStyleBackColor = true;
            this.btnCapNhatQuaHan.Click += new System.EventHandler(this.btnCapNhatQuaHan_Click);
            // 
            // btnTraSach
            // 
            this.btnTraSach.Location = new System.Drawing.Point(895, 26);
            this.btnTraSach.Name = "btnTraSach";
            this.btnTraSach.Size = new System.Drawing.Size(165, 32);
            this.btnTraSach.TabIndex = 21;
            this.btnTraSach.Text = "TRẢ SÁCH";
            this.btnTraSach.UseVisualStyleBackColor = true;
            this.btnTraSach.Click += new System.EventHandler(this.btnTraSach_Click);
            // 
            // dgvPhieuMuon
            // 
            this.dgvPhieuMuon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.dgvPhieuMuon.Location = new System.Drawing.Point(15, 68);
            this.dgvPhieuMuon.Name = "dgvPhieuMuon";
            this.dgvPhieuMuon.Size = new System.Drawing.Size(640, 308);
            this.dgvPhieuMuon.TabIndex = 22;
            this.dgvPhieuMuon.AllowUserToAddRows = false;
            this.dgvPhieuMuon.AllowUserToDeleteRows = false;
            this.dgvPhieuMuon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhieuMuon.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhieuMuon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhieuMuon.MultiSelect = false;
            this.dgvPhieuMuon.ReadOnly = true;
            this.dgvPhieuMuon.RowHeadersWidth = 30;
            this.dgvPhieuMuon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhieuMuon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhieuMuon_CellClick);
            this.dgvPhieuMuon.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPhieuMuon_CellFormatting);
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChiTiet.Location = new System.Drawing.Point(670, 68);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.Size = new System.Drawing.Size(390, 308);
            this.dgvChiTiet.TabIndex = 23;
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.MultiSelect = false;
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersWidth = 30;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // FormMuonTra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.grpDanhSach);
            this.Controls.Add(this.grpLapPhieu);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1100, 680);
            this.Name = "FormMuonTra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mượn / Trả sách";
            this.Load += new System.EventHandler(this.FormMuonTra_Load);
            this.grpLapPhieu.ResumeLayout(false);
            this.grpLapPhieu.PerformLayout();
            this.grpDanhSach.ResumeLayout(false);
            this.grpDanhSach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSLMuon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSachMuon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuMuon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLapPhieu;
        private System.Windows.Forms.Label lblDocGia;
        private System.Windows.Forms.ComboBox cboDocGia;
        private System.Windows.Forms.Label lblTinhTrangThe;
        private System.Windows.Forms.Label lblHanTra;
        private System.Windows.Forms.DateTimePicker dtpHanTra;
        private System.Windows.Forms.Label lblSach;
        private System.Windows.Forms.ComboBox cboSach;
        private System.Windows.Forms.Label lblSL;
        private System.Windows.Forms.NumericUpDown nudSLMuon;
        private System.Windows.Forms.Button btnThemSach;
        private System.Windows.Forms.Button btnBoSach;
        private System.Windows.Forms.DataGridView dgvSachMuon;
        private System.Windows.Forms.Button btnLapPhieu;
        private System.Windows.Forms.GroupBox grpDanhSach;
        private System.Windows.Forms.Label lblLocDocGia;
        private System.Windows.Forms.ComboBox cboLocDocGia;
        private System.Windows.Forms.Label lblLocTrangThai;
        private System.Windows.Forms.ComboBox cboLocTrangThai;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnCapNhatQuaHan;
        private System.Windows.Forms.Button btnTraSach;
        private System.Windows.Forms.DataGridView dgvPhieuMuon;
        private System.Windows.Forms.DataGridView dgvChiTiet;
    }
}
