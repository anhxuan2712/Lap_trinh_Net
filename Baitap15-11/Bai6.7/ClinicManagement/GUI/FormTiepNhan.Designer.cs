namespace ClinicManagement.GUI
{
    partial class FormTiepNhan
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
            this.grpTraCuu = new System.Windows.Forms.GroupBox();
            this.lblSDTTra = new System.Windows.Forms.Label();
            this.txtSDTTra = new System.Windows.Forms.TextBox();
            this.btnTraCuu = new System.Windows.Forms.Button();
            this.lblKetQuaTra = new System.Windows.Forms.Label();
            this.grpBenhNhan = new System.Windows.Forms.GroupBox();
            this.lblMaBN = new System.Windows.Forms.Label();
            this.txtMaBN = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblNamSinh = new System.Windows.Forms.Label();
            this.nudNamSinh = new System.Windows.Forms.NumericUpDown();
            this.btnNhapMoi = new System.Windows.Forms.Button();
            this.btnLuuBN = new System.Windows.Forms.Button();
            this.btnCapNhatBN = new System.Windows.Forms.Button();
            this.grpHoSo = new System.Windows.Forms.GroupBox();
            this.lblTrieuChung = new System.Windows.Forms.Label();
            this.txtTrieuChung = new System.Windows.Forms.TextBox();
            this.btnTaoHoSo = new System.Windows.Forms.Button();
            this.grpLichSu = new System.Windows.Forms.GroupBox();
            this.dgvHoSo = new System.Windows.Forms.DataGridView();
            this.grpTraCuu.SuspendLayout();
            this.grpBenhNhan.SuspendLayout();
            this.grpHoSo.SuspendLayout();
            this.grpLichSu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNamSinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoSo)).BeginInit();
            this.SuspendLayout();
            // 
            // grpTraCuu
            // 
            this.grpTraCuu.Controls.Add(this.lblKetQuaTra);
            this.grpTraCuu.Controls.Add(this.btnTraCuu);
            this.grpTraCuu.Controls.Add(this.txtSDTTra);
            this.grpTraCuu.Controls.Add(this.lblSDTTra);
            this.grpTraCuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.grpTraCuu.Location = new System.Drawing.Point(12, 8);
            this.grpTraCuu.Name = "grpTraCuu";
            this.grpTraCuu.Size = new System.Drawing.Size(976, 72);
            this.grpTraCuu.TabIndex = 0;
            this.grpTraCuu.Text = "Tra cứu bệnh nhân theo số điện thoại";
            // 
            // lblSDTTra
            // 
            this.lblSDTTra.AutoSize = true;
            this.lblSDTTra.Location = new System.Drawing.Point(15, 32);
            this.lblSDTTra.Name = "lblSDTTra";
            this.lblSDTTra.TabIndex = 1;
            this.lblSDTTra.Text = "Số điện thoại:";
            // 
            // txtSDTTra
            // 
            this.txtSDTTra.Location = new System.Drawing.Point(130, 29);
            this.txtSDTTra.Name = "txtSDTTra";
            this.txtSDTTra.Size = new System.Drawing.Size(200, 25);
            this.txtSDTTra.TabIndex = 2;
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.Location = new System.Drawing.Point(345, 26);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(110, 32);
            this.btnTraCuu.TabIndex = 3;
            this.btnTraCuu.Text = "Tra cứu";
            this.btnTraCuu.UseVisualStyleBackColor = true;
            this.btnTraCuu.Click += new System.EventHandler(this.btnTraCuu_Click);
            // 
            // lblKetQuaTra
            // 
            this.lblKetQuaTra.AutoSize = true;
            this.lblKetQuaTra.Location = new System.Drawing.Point(470, 33);
            this.lblKetQuaTra.Name = "lblKetQuaTra";
            this.lblKetQuaTra.TabIndex = 4;
            this.lblKetQuaTra.Text = "";
            this.lblKetQuaTra.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            // 
            // grpBenhNhan
            // 
            this.grpBenhNhan.Controls.Add(this.btnCapNhatBN);
            this.grpBenhNhan.Controls.Add(this.btnLuuBN);
            this.grpBenhNhan.Controls.Add(this.btnNhapMoi);
            this.grpBenhNhan.Controls.Add(this.nudNamSinh);
            this.grpBenhNhan.Controls.Add(this.lblNamSinh);
            this.grpBenhNhan.Controls.Add(this.txtSDT);
            this.grpBenhNhan.Controls.Add(this.lblSDT);
            this.grpBenhNhan.Controls.Add(this.txtHoTen);
            this.grpBenhNhan.Controls.Add(this.lblHoTen);
            this.grpBenhNhan.Controls.Add(this.txtMaBN);
            this.grpBenhNhan.Controls.Add(this.lblMaBN);
            this.grpBenhNhan.Location = new System.Drawing.Point(12, 88);
            this.grpBenhNhan.Name = "grpBenhNhan";
            this.grpBenhNhan.Size = new System.Drawing.Size(480, 252);
            this.grpBenhNhan.TabIndex = 5;
            this.grpBenhNhan.Text = "Thông tin bệnh nhân";
            // 
            // lblMaBN
            // 
            this.lblMaBN.AutoSize = true;
            this.lblMaBN.Location = new System.Drawing.Point(15, 35);
            this.lblMaBN.Name = "lblMaBN";
            this.lblMaBN.TabIndex = 6;
            this.lblMaBN.Text = "Mã BN:";
            // 
            // txtMaBN
            // 
            this.txtMaBN.Location = new System.Drawing.Point(120, 32);
            this.txtMaBN.Name = "txtMaBN";
            this.txtMaBN.Size = new System.Drawing.Size(120, 25);
            this.txtMaBN.TabIndex = 7;
            this.txtMaBN.ReadOnly = true;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(15, 70);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.TabIndex = 8;
            this.lblHoTen.Text = "Họ tên:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(120, 67);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(340, 25);
            this.txtHoTen.TabIndex = 9;
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(15, 105);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.TabIndex = 10;
            this.lblSDT.Text = "Điện thoại:";
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(120, 102);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(200, 25);
            this.txtSDT.TabIndex = 11;
            // 
            // lblNamSinh
            // 
            this.lblNamSinh.AutoSize = true;
            this.lblNamSinh.Location = new System.Drawing.Point(15, 140);
            this.lblNamSinh.Name = "lblNamSinh";
            this.lblNamSinh.TabIndex = 12;
            this.lblNamSinh.Text = "Năm sinh:";
            // 
            // nudNamSinh
            // 
            this.nudNamSinh.Location = new System.Drawing.Point(120, 137);
            this.nudNamSinh.Name = "nudNamSinh";
            this.nudNamSinh.Size = new System.Drawing.Size(100, 25);
            this.nudNamSinh.TabIndex = 13;
            this.nudNamSinh.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            this.nudNamSinh.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            this.nudNamSinh.Value = new decimal(new int[] { 1990, 0, 0, 0 });
            // 
            // btnNhapMoi
            // 
            this.btnNhapMoi.Location = new System.Drawing.Point(120, 185);
            this.btnNhapMoi.Name = "btnNhapMoi";
            this.btnNhapMoi.Size = new System.Drawing.Size(100, 38);
            this.btnNhapMoi.TabIndex = 14;
            this.btnNhapMoi.Text = "Nhập mới";
            this.btnNhapMoi.UseVisualStyleBackColor = true;
            this.btnNhapMoi.Click += new System.EventHandler(this.btnNhapMoi_Click);
            // 
            // btnLuuBN
            // 
            this.btnLuuBN.Location = new System.Drawing.Point(230, 185);
            this.btnLuuBN.Name = "btnLuuBN";
            this.btnLuuBN.Size = new System.Drawing.Size(115, 38);
            this.btnLuuBN.TabIndex = 15;
            this.btnLuuBN.Text = "Lưu BN mới";
            this.btnLuuBN.UseVisualStyleBackColor = true;
            this.btnLuuBN.Click += new System.EventHandler(this.btnLuuBN_Click);
            // 
            // btnCapNhatBN
            // 
            this.btnCapNhatBN.Location = new System.Drawing.Point(355, 185);
            this.btnCapNhatBN.Name = "btnCapNhatBN";
            this.btnCapNhatBN.Size = new System.Drawing.Size(105, 38);
            this.btnCapNhatBN.TabIndex = 16;
            this.btnCapNhatBN.Text = "Cập nhật";
            this.btnCapNhatBN.UseVisualStyleBackColor = true;
            this.btnCapNhatBN.Click += new System.EventHandler(this.btnCapNhatBN_Click);
            // 
            // grpHoSo
            // 
            this.grpHoSo.Controls.Add(this.btnTaoHoSo);
            this.grpHoSo.Controls.Add(this.txtTrieuChung);
            this.grpHoSo.Controls.Add(this.lblTrieuChung);
            this.grpHoSo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.grpHoSo.Location = new System.Drawing.Point(500, 88);
            this.grpHoSo.Name = "grpHoSo";
            this.grpHoSo.Size = new System.Drawing.Size(488, 252);
            this.grpHoSo.TabIndex = 17;
            this.grpHoSo.Text = "Tạo hồ sơ tiếp nhận khám";
            // 
            // lblTrieuChung
            // 
            this.lblTrieuChung.AutoSize = true;
            this.lblTrieuChung.Location = new System.Drawing.Point(15, 32);
            this.lblTrieuChung.Name = "lblTrieuChung";
            this.lblTrieuChung.TabIndex = 18;
            this.lblTrieuChung.Text = "Triệu chứng:";
            // 
            // txtTrieuChung
            // 
            this.txtTrieuChung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.txtTrieuChung.Location = new System.Drawing.Point(15, 57);
            this.txtTrieuChung.Name = "txtTrieuChung";
            this.txtTrieuChung.Size = new System.Drawing.Size(455, 120);
            this.txtTrieuChung.TabIndex = 19;
            this.txtTrieuChung.Multiline = true;
            this.txtTrieuChung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // 
            // btnTaoHoSo
            // 
            this.btnTaoHoSo.Location = new System.Drawing.Point(15, 190);
            this.btnTaoHoSo.Name = "btnTaoHoSo";
            this.btnTaoHoSo.Size = new System.Drawing.Size(220, 42);
            this.btnTaoHoSo.TabIndex = 20;
            this.btnTaoHoSo.Text = "TẠO HỒ SƠ KHÁM";
            this.btnTaoHoSo.UseVisualStyleBackColor = true;
            this.btnTaoHoSo.Click += new System.EventHandler(this.btnTaoHoSo_Click);
            // 
            // grpLichSu
            // 
            this.grpLichSu.Controls.Add(this.dgvHoSo);
            this.grpLichSu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.grpLichSu.Location = new System.Drawing.Point(12, 348);
            this.grpLichSu.Name = "grpLichSu";
            this.grpLichSu.Size = new System.Drawing.Size(976, 260);
            this.grpLichSu.TabIndex = 21;
            this.grpLichSu.Text = "Lịch sử hồ sơ khám của bệnh nhân";
            // 
            // dgvHoSo
            // 
            this.dgvHoSo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHoSo.Location = new System.Drawing.Point(15, 28);
            this.dgvHoSo.Name = "dgvHoSo";
            this.dgvHoSo.Size = new System.Drawing.Size(945, 216);
            this.dgvHoSo.TabIndex = 22;
            this.dgvHoSo.AllowUserToAddRows = false;
            this.dgvHoSo.AllowUserToDeleteRows = false;
            this.dgvHoSo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoSo.BackgroundColor = System.Drawing.Color.White;
            this.dgvHoSo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoSo.MultiSelect = false;
            this.dgvHoSo.ReadOnly = true;
            this.dgvHoSo.RowHeadersWidth = 30;
            this.dgvHoSo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // FormTiepNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Controls.Add(this.grpLichSu);
            this.Controls.Add(this.grpHoSo);
            this.Controls.Add(this.grpBenhNhan);
            this.Controls.Add(this.grpTraCuu);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1000, 620);
            this.Name = "FormTiepNhan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tiếp nhận bệnh nhân";
            this.Load += new System.EventHandler(this.FormTiepNhan_Load);
            this.grpTraCuu.ResumeLayout(false);
            this.grpTraCuu.PerformLayout();
            this.grpBenhNhan.ResumeLayout(false);
            this.grpBenhNhan.PerformLayout();
            this.grpHoSo.ResumeLayout(false);
            this.grpHoSo.PerformLayout();
            this.grpLichSu.ResumeLayout(false);
            this.grpLichSu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNamSinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoSo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTraCuu;
        private System.Windows.Forms.Label lblSDTTra;
        private System.Windows.Forms.TextBox txtSDTTra;
        private System.Windows.Forms.Button btnTraCuu;
        private System.Windows.Forms.Label lblKetQuaTra;
        private System.Windows.Forms.GroupBox grpBenhNhan;
        private System.Windows.Forms.Label lblMaBN;
        private System.Windows.Forms.TextBox txtMaBN;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblNamSinh;
        private System.Windows.Forms.NumericUpDown nudNamSinh;
        private System.Windows.Forms.Button btnNhapMoi;
        private System.Windows.Forms.Button btnLuuBN;
        private System.Windows.Forms.Button btnCapNhatBN;
        private System.Windows.Forms.GroupBox grpHoSo;
        private System.Windows.Forms.Label lblTrieuChung;
        private System.Windows.Forms.TextBox txtTrieuChung;
        private System.Windows.Forms.Button btnTaoHoSo;
        private System.Windows.Forms.GroupBox grpLichSu;
        private System.Windows.Forms.DataGridView dgvHoSo;
    }
}
