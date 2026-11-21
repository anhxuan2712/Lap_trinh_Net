namespace BilliardsManagement.GUI
{
    partial class FormMain
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
            this.grpSoDo = new System.Windows.Forms.GroupBox();
            this.flpBan = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTrong = new System.Windows.Forms.Label();
            this.lblDangChoi = new System.Windows.Forms.Label();
            this.btnTaiLaiSoDo = new System.Windows.Forms.Button();
            this.grpBan = new System.Windows.Forms.GroupBox();
            this.lblTenBan = new System.Windows.Forms.Label();
            this.lblLoaiBan = new System.Windows.Forms.Label();
            this.lblGia = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblGioBatDau = new System.Windows.Forms.Label();
            this.lblThoiGian = new System.Windows.Forms.Label();
            this.lblTienGioTamTinh = new System.Windows.Forms.Label();
            this.btnMoBan = new System.Windows.Forms.Button();
            this.grpDichVu = new System.Windows.Forms.GroupBox();
            this.lblDV = new System.Windows.Forms.Label();
            this.cboDichVu = new System.Windows.Forms.ComboBox();
            this.lblSL = new System.Windows.Forms.Label();
            this.nudSL = new System.Windows.Forms.NumericUpDown();
            this.btnThemMon = new System.Windows.Forms.Button();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.btnXoaMon = new System.Windows.Forms.Button();
            this.btnLuuDichVu = new System.Windows.Forms.Button();
            this.lblTienDV = new System.Windows.Forms.Label();
            this.grpThanhToan = new System.Windows.Forms.GroupBox();
            this.rdoBlock15 = new System.Windows.Forms.RadioButton();
            this.rdoTheoPhut = new System.Windows.Forms.RadioButton();
            this.lblChiTietTien = new System.Windows.Forms.Label();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.grpSoDo.SuspendLayout();
            this.flpBan.SuspendLayout();
            this.grpBan.SuspendLayout();
            this.grpDichVu.SuspendLayout();
            this.grpThanhToan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSoDo
            // 
            this.grpSoDo.Controls.Add(this.btnTaiLaiSoDo);
            this.grpSoDo.Controls.Add(this.lblDangChoi);
            this.grpSoDo.Controls.Add(this.lblTrong);
            this.grpSoDo.Controls.Add(this.flpBan);
            this.grpSoDo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.grpSoDo.Location = new System.Drawing.Point(12, 8);
            this.grpSoDo.Name = "grpSoDo";
            this.grpSoDo.Size = new System.Drawing.Size(560, 680);
            this.grpSoDo.TabIndex = 0;
            this.grpSoDo.Text = "Sơ đồ bàn bida";
            // 
            // flpBan
            // 
            this.flpBan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.flpBan.Location = new System.Drawing.Point(10, 26);
            this.flpBan.Name = "flpBan";
            this.flpBan.Size = new System.Drawing.Size(540, 592);
            this.flpBan.TabIndex = 1;
            this.flpBan.AutoScroll = true;
            this.flpBan.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // lblTrong
            // 
            this.lblTrong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.lblTrong.AutoSize = true;
            this.lblTrong.Location = new System.Drawing.Point(10, 634);
            this.lblTrong.Name = "lblTrong";
            this.lblTrong.TabIndex = 2;
            this.lblTrong.Text = "   Trống   ";
            this.lblTrong.BackColor = System.Drawing.Color.SeaGreen;
            this.lblTrong.ForeColor = System.Drawing.Color.White;
            this.lblTrong.Padding = new System.Windows.Forms.Padding(4);
            // 
            // lblDangChoi
            // 
            this.lblDangChoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.lblDangChoi.AutoSize = true;
            this.lblDangChoi.Location = new System.Drawing.Point(100, 634);
            this.lblDangChoi.Name = "lblDangChoi";
            this.lblDangChoi.TabIndex = 3;
            this.lblDangChoi.Text = "   Đang chơi   ";
            this.lblDangChoi.BackColor = System.Drawing.Color.OrangeRed;
            this.lblDangChoi.ForeColor = System.Drawing.Color.White;
            this.lblDangChoi.Padding = new System.Windows.Forms.Padding(4);
            // 
            // btnTaiLaiSoDo
            // 
            this.btnTaiLaiSoDo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaiLaiSoDo.Location = new System.Drawing.Point(410, 628);
            this.btnTaiLaiSoDo.Name = "btnTaiLaiSoDo";
            this.btnTaiLaiSoDo.Size = new System.Drawing.Size(140, 36);
            this.btnTaiLaiSoDo.TabIndex = 4;
            this.btnTaiLaiSoDo.Text = "Tải lại sơ đồ";
            this.btnTaiLaiSoDo.UseVisualStyleBackColor = true;
            this.btnTaiLaiSoDo.Click += new System.EventHandler(this.btnTaiLaiSoDo_Click);
            // 
            // grpBan
            // 
            this.grpBan.Controls.Add(this.btnMoBan);
            this.grpBan.Controls.Add(this.lblTienGioTamTinh);
            this.grpBan.Controls.Add(this.lblThoiGian);
            this.grpBan.Controls.Add(this.lblGioBatDau);
            this.grpBan.Controls.Add(this.lblTrangThai);
            this.grpBan.Controls.Add(this.lblGia);
            this.grpBan.Controls.Add(this.lblLoaiBan);
            this.grpBan.Controls.Add(this.lblTenBan);
            this.grpBan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.grpBan.Location = new System.Drawing.Point(580, 8);
            this.grpBan.Name = "grpBan";
            this.grpBan.Size = new System.Drawing.Size(608, 172);
            this.grpBan.TabIndex = 5;
            this.grpBan.Text = "Thông tin bàn";
            // 
            // lblTenBan
            // 
            this.lblTenBan.AutoSize = true;
            this.lblTenBan.Location = new System.Drawing.Point(15, 28);
            this.lblTenBan.Name = "lblTenBan";
            this.lblTenBan.TabIndex = 6;
            this.lblTenBan.Text = "(Chưa chọn bàn)";
            this.lblTenBan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            // 
            // lblLoaiBan
            // 
            this.lblLoaiBan.AutoSize = true;
            this.lblLoaiBan.Location = new System.Drawing.Point(15, 68);
            this.lblLoaiBan.Name = "lblLoaiBan";
            this.lblLoaiBan.TabIndex = 7;
            this.lblLoaiBan.Text = "Loại bàn: ";
            // 
            // lblGia
            // 
            this.lblGia.AutoSize = true;
            this.lblGia.Location = new System.Drawing.Point(15, 96);
            this.lblGia.Name = "lblGia";
            this.lblGia.TabIndex = 8;
            this.lblGia.Text = "Giá/giờ: ";
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(15, 124);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.TabIndex = 9;
            this.lblTrangThai.Text = "Trạng thái: ";
            // 
            // lblGioBatDau
            // 
            this.lblGioBatDau.AutoSize = true;
            this.lblGioBatDau.Location = new System.Drawing.Point(270, 68);
            this.lblGioBatDau.Name = "lblGioBatDau";
            this.lblGioBatDau.TabIndex = 10;
            this.lblGioBatDau.Text = "Giờ bắt đầu: ";
            // 
            // lblThoiGian
            // 
            this.lblThoiGian.AutoSize = true;
            this.lblThoiGian.Location = new System.Drawing.Point(270, 96);
            this.lblThoiGian.Name = "lblThoiGian";
            this.lblThoiGian.TabIndex = 11;
            this.lblThoiGian.Text = "Thời gian chơi: ";
            // 
            // lblTienGioTamTinh
            // 
            this.lblTienGioTamTinh.AutoSize = true;
            this.lblTienGioTamTinh.Location = new System.Drawing.Point(270, 124);
            this.lblTienGioTamTinh.Name = "lblTienGioTamTinh";
            this.lblTienGioTamTinh.TabIndex = 12;
            this.lblTienGioTamTinh.Text = "Tiền giờ tạm tính: ";
            // 
            // btnMoBan
            // 
            this.btnMoBan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoBan.Location = new System.Drawing.Point(432, 22);
            this.btnMoBan.Name = "btnMoBan";
            this.btnMoBan.Size = new System.Drawing.Size(160, 42);
            this.btnMoBan.TabIndex = 13;
            this.btnMoBan.Text = "MỞ BÀN";
            this.btnMoBan.UseVisualStyleBackColor = true;
            this.btnMoBan.Click += new System.EventHandler(this.btnMoBan_Click);
            // 
            // grpDichVu
            // 
            this.grpDichVu.Controls.Add(this.lblTienDV);
            this.grpDichVu.Controls.Add(this.btnLuuDichVu);
            this.grpDichVu.Controls.Add(this.btnXoaMon);
            this.grpDichVu.Controls.Add(this.dgvDichVu);
            this.grpDichVu.Controls.Add(this.btnThemMon);
            this.grpDichVu.Controls.Add(this.nudSL);
            this.grpDichVu.Controls.Add(this.lblSL);
            this.grpDichVu.Controls.Add(this.cboDichVu);
            this.grpDichVu.Controls.Add(this.lblDV);
            this.grpDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.grpDichVu.Location = new System.Drawing.Point(580, 186);
            this.grpDichVu.Name = "grpDichVu";
            this.grpDichVu.Size = new System.Drawing.Size(608, 336);
            this.grpDichVu.TabIndex = 14;
            this.grpDichVu.Text = "Dịch vụ đã gọi (SqlDataAdapter + DataTable)";
            // 
            // lblDV
            // 
            this.lblDV.AutoSize = true;
            this.lblDV.Location = new System.Drawing.Point(15, 32);
            this.lblDV.Name = "lblDV";
            this.lblDV.TabIndex = 15;
            this.lblDV.Text = "Dịch vụ:";
            // 
            // cboDichVu
            // 
            this.cboDichVu.Location = new System.Drawing.Point(85, 29);
            this.cboDichVu.Name = "cboDichVu";
            this.cboDichVu.Size = new System.Drawing.Size(260, 25);
            this.cboDichVu.TabIndex = 16;
            this.cboDichVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // lblSL
            // 
            this.lblSL.AutoSize = true;
            this.lblSL.Location = new System.Drawing.Point(355, 32);
            this.lblSL.Name = "lblSL";
            this.lblSL.TabIndex = 17;
            this.lblSL.Text = "SL:";
            // 
            // nudSL
            // 
            this.nudSL.Location = new System.Drawing.Point(385, 29);
            this.nudSL.Name = "nudSL";
            this.nudSL.Size = new System.Drawing.Size(60, 25);
            this.nudSL.TabIndex = 18;
            this.nudSL.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.nudSL.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSL.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnThemMon
            // 
            this.btnThemMon.Location = new System.Drawing.Point(455, 26);
            this.btnThemMon.Name = "btnThemMon";
            this.btnThemMon.Size = new System.Drawing.Size(138, 32);
            this.btnThemMon.TabIndex = 19;
            this.btnThemMon.Text = "Thêm món";
            this.btnThemMon.UseVisualStyleBackColor = true;
            this.btnThemMon.Click += new System.EventHandler(this.btnThemMon_Click);
            // 
            // dgvDichVu
            // 
            this.dgvDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDichVu.Location = new System.Drawing.Point(15, 66);
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.Size = new System.Drawing.Size(578, 216);
            this.dgvDichVu.TabIndex = 20;
            this.dgvDichVu.AllowUserToAddRows = false;
            this.dgvDichVu.AllowUserToDeleteRows = false;
            this.dgvDichVu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDichVu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.MultiSelect = false;
            this.dgvDichVu.ReadOnly = false;
            this.dgvDichVu.RowHeadersWidth = 30;
            this.dgvDichVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDichVu.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDichVu_CellEndEdit);
            this.dgvDichVu.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDichVu_DataError);
            // 
            // btnXoaMon
            // 
            this.btnXoaMon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.btnXoaMon.Location = new System.Drawing.Point(15, 290);
            this.btnXoaMon.Name = "btnXoaMon";
            this.btnXoaMon.Size = new System.Drawing.Size(110, 34);
            this.btnXoaMon.TabIndex = 21;
            this.btnXoaMon.Text = "Xóa món";
            this.btnXoaMon.UseVisualStyleBackColor = true;
            this.btnXoaMon.Click += new System.EventHandler(this.btnXoaMon_Click);
            // 
            // btnLuuDichVu
            // 
            this.btnLuuDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.btnLuuDichVu.Location = new System.Drawing.Point(135, 290);
            this.btnLuuDichVu.Name = "btnLuuDichVu";
            this.btnLuuDichVu.Size = new System.Drawing.Size(185, 34);
            this.btnLuuDichVu.TabIndex = 22;
            this.btnLuuDichVu.Text = "Lưu dịch vụ (Update)";
            this.btnLuuDichVu.UseVisualStyleBackColor = true;
            this.btnLuuDichVu.Click += new System.EventHandler(this.btnLuuDichVu_Click);
            // 
            // lblTienDV
            // 
            this.lblTienDV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.lblTienDV.AutoSize = true;
            this.lblTienDV.Location = new System.Drawing.Point(335, 297);
            this.lblTienDV.Name = "lblTienDV";
            this.lblTienDV.TabIndex = 23;
            this.lblTienDV.Text = "Tiền dịch vụ: 0 đ";
            this.lblTienDV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            // 
            // grpThanhToan
            // 
            this.grpThanhToan.Controls.Add(this.btnThanhToan);
            this.grpThanhToan.Controls.Add(this.lblChiTietTien);
            this.grpThanhToan.Controls.Add(this.rdoTheoPhut);
            this.grpThanhToan.Controls.Add(this.rdoBlock15);
            this.grpThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.grpThanhToan.Location = new System.Drawing.Point(580, 528);
            this.grpThanhToan.Name = "grpThanhToan";
            this.grpThanhToan.Size = new System.Drawing.Size(608, 160);
            this.grpThanhToan.TabIndex = 24;
            this.grpThanhToan.Text = "Tính tiền & Thanh toán";
            // 
            // rdoBlock15
            // 
            this.rdoBlock15.Location = new System.Drawing.Point(15, 28);
            this.rdoBlock15.Name = "rdoBlock15";
            this.rdoBlock15.Size = new System.Drawing.Size(210, 25);
            this.rdoBlock15.TabIndex = 25;
            this.rdoBlock15.Text = "Làm tròn block 15 phút";
            this.rdoBlock15.Checked = true;
            this.rdoBlock15.UseVisualStyleBackColor = true;
            this.rdoBlock15.CheckedChanged += new System.EventHandler(this.rdoCachTinh_CheckedChanged);
            // 
            // rdoTheoPhut
            // 
            this.rdoTheoPhut.Location = new System.Drawing.Point(235, 28);
            this.rdoTheoPhut.Name = "rdoTheoPhut";
            this.rdoTheoPhut.Size = new System.Drawing.Size(230, 25);
            this.rdoTheoPhut.TabIndex = 26;
            this.rdoTheoPhut.Text = "Tính chính xác theo phút";
            this.rdoTheoPhut.UseVisualStyleBackColor = true;
            this.rdoTheoPhut.CheckedChanged += new System.EventHandler(this.rdoCachTinh_CheckedChanged);
            // 
            // lblChiTietTien
            // 
            this.lblChiTietTien.Location = new System.Drawing.Point(15, 62);
            this.lblChiTietTien.Name = "lblChiTietTien";
            this.lblChiTietTien.Size = new System.Drawing.Size(400, 90);
            this.lblChiTietTien.TabIndex = 27;
            this.lblChiTietTien.Text = "";
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Right)));
            this.btnThanhToan.Location = new System.Drawing.Point(432, 92);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(160, 55);
            this.btnThanhToan.TabIndex = 28;
            this.btnThanhToan.Text = "THANH TOÁN";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.grpThanhToan);
            this.Controls.Add(this.grpDichVu);
            this.Controls.Add(this.grpBan);
            this.Controls.Add(this.grpSoDo);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý CLB Bida";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.grpSoDo.ResumeLayout(false);
            this.grpSoDo.PerformLayout();
            this.flpBan.ResumeLayout(false);
            this.flpBan.PerformLayout();
            this.grpBan.ResumeLayout(false);
            this.grpBan.PerformLayout();
            this.grpDichVu.ResumeLayout(false);
            this.grpDichVu.PerformLayout();
            this.grpThanhToan.ResumeLayout(false);
            this.grpThanhToan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSoDo;
        private System.Windows.Forms.FlowLayoutPanel flpBan;
        private System.Windows.Forms.Label lblTrong;
        private System.Windows.Forms.Label lblDangChoi;
        private System.Windows.Forms.Button btnTaiLaiSoDo;
        private System.Windows.Forms.GroupBox grpBan;
        private System.Windows.Forms.Label lblTenBan;
        private System.Windows.Forms.Label lblLoaiBan;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblGioBatDau;
        private System.Windows.Forms.Label lblThoiGian;
        private System.Windows.Forms.Label lblTienGioTamTinh;
        private System.Windows.Forms.Button btnMoBan;
        private System.Windows.Forms.GroupBox grpDichVu;
        private System.Windows.Forms.Label lblDV;
        private System.Windows.Forms.ComboBox cboDichVu;
        private System.Windows.Forms.Label lblSL;
        private System.Windows.Forms.NumericUpDown nudSL;
        private System.Windows.Forms.Button btnThemMon;
        private System.Windows.Forms.DataGridView dgvDichVu;
        private System.Windows.Forms.Button btnXoaMon;
        private System.Windows.Forms.Button btnLuuDichVu;
        private System.Windows.Forms.Label lblTienDV;
        private System.Windows.Forms.GroupBox grpThanhToan;
        private System.Windows.Forms.RadioButton rdoBlock15;
        private System.Windows.Forms.RadioButton rdoTheoPhut;
        private System.Windows.Forms.Label lblChiTietTien;
        private System.Windows.Forms.Button btnThanhToan;
    }
}
