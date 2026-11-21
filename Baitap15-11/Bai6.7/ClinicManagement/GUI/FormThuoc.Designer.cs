namespace ClinicManagement.GUI
{
    partial class FormThuoc
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
            this.lblMaThuoc = new System.Windows.Forms.Label();
            this.txtMaThuoc = new System.Windows.Forms.TextBox();
            this.lblTenThuoc = new System.Windows.Forms.Label();
            this.txtTenThuoc = new System.Windows.Forms.TextBox();
            this.lblDonVi = new System.Windows.Forms.Label();
            this.cboDonViTinh = new System.Windows.Forms.ComboBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.nudDonGia = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuongTon = new System.Windows.Forms.Label();
            this.nudSoLuongTon = new System.Windows.Forms.NumericUpDown();
            this.lblNhapThem = new System.Windows.Forms.Label();
            this.nudNhapThem = new System.Windows.Forms.NumericUpDown();
            this.btnNhapKho = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblTim = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.dgvThuoc = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nudDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongTon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNhapThem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThuoc)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMaThuoc
            // 
            this.lblMaThuoc.AutoSize = true;
            this.lblMaThuoc.Location = new System.Drawing.Point(20, 20);
            this.lblMaThuoc.Name = "lblMaThuoc";
            this.lblMaThuoc.TabIndex = 0;
            this.lblMaThuoc.Text = "Mã thuốc:";
            // 
            // txtMaThuoc
            // 
            this.txtMaThuoc.Location = new System.Drawing.Point(130, 17);
            this.txtMaThuoc.Name = "txtMaThuoc";
            this.txtMaThuoc.Size = new System.Drawing.Size(100, 25);
            this.txtMaThuoc.TabIndex = 1;
            this.txtMaThuoc.ReadOnly = true;
            // 
            // lblTenThuoc
            // 
            this.lblTenThuoc.AutoSize = true;
            this.lblTenThuoc.Location = new System.Drawing.Point(20, 55);
            this.lblTenThuoc.Name = "lblTenThuoc";
            this.lblTenThuoc.TabIndex = 2;
            this.lblTenThuoc.Text = "Tên thuốc:";
            // 
            // txtTenThuoc
            // 
            this.txtTenThuoc.Location = new System.Drawing.Point(130, 52);
            this.txtTenThuoc.Name = "txtTenThuoc";
            this.txtTenThuoc.Size = new System.Drawing.Size(300, 25);
            this.txtTenThuoc.TabIndex = 3;
            // 
            // lblDonVi
            // 
            this.lblDonVi.AutoSize = true;
            this.lblDonVi.Location = new System.Drawing.Point(20, 90);
            this.lblDonVi.Name = "lblDonVi";
            this.lblDonVi.TabIndex = 4;
            this.lblDonVi.Text = "Đơn vị tính:";
            // 
            // cboDonViTinh
            // 
            this.cboDonViTinh.Location = new System.Drawing.Point(130, 87);
            this.cboDonViTinh.Name = "cboDonViTinh";
            this.cboDonViTinh.Size = new System.Drawing.Size(150, 25);
            this.cboDonViTinh.TabIndex = 5;
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Location = new System.Drawing.Point(460, 20);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.TabIndex = 6;
            this.lblDonGia.Text = "Đơn giá:";
            // 
            // nudDonGia
            // 
            this.nudDonGia.Location = new System.Drawing.Point(570, 17);
            this.nudDonGia.Name = "nudDonGia";
            this.nudDonGia.Size = new System.Drawing.Size(150, 25);
            this.nudDonGia.TabIndex = 7;
            this.nudDonGia.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.nudDonGia.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            this.nudDonGia.ThousandsSeparator = true;
            // 
            // lblSoLuongTon
            // 
            this.lblSoLuongTon.AutoSize = true;
            this.lblSoLuongTon.Location = new System.Drawing.Point(460, 55);
            this.lblSoLuongTon.Name = "lblSoLuongTon";
            this.lblSoLuongTon.TabIndex = 8;
            this.lblSoLuongTon.Text = "Tồn kho:";
            // 
            // nudSoLuongTon
            // 
            this.nudSoLuongTon.Location = new System.Drawing.Point(570, 52);
            this.nudSoLuongTon.Name = "nudSoLuongTon";
            this.nudSoLuongTon.Size = new System.Drawing.Size(150, 25);
            this.nudSoLuongTon.TabIndex = 9;
            this.nudSoLuongTon.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            // 
            // lblNhapThem
            // 
            this.lblNhapThem.AutoSize = true;
            this.lblNhapThem.Location = new System.Drawing.Point(460, 90);
            this.lblNhapThem.Name = "lblNhapThem";
            this.lblNhapThem.TabIndex = 10;
            this.lblNhapThem.Text = "Nhập thêm:";
            // 
            // nudNhapThem
            // 
            this.nudNhapThem.Location = new System.Drawing.Point(570, 87);
            this.nudNhapThem.Name = "nudNhapThem";
            this.nudNhapThem.Size = new System.Drawing.Size(150, 25);
            this.nudNhapThem.TabIndex = 11;
            this.nudNhapThem.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            // 
            // btnNhapKho
            // 
            this.btnNhapKho.Location = new System.Drawing.Point(730, 84);
            this.btnNhapKho.Name = "btnNhapKho";
            this.btnNhapKho.Size = new System.Drawing.Size(120, 32);
            this.btnNhapKho.TabIndex = 12;
            this.btnNhapKho.Text = "Nhập kho";
            this.btnNhapKho.UseVisualStyleBackColor = true;
            this.btnNhapKho.Click += new System.EventHandler(this.btnNhapKho_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(130, 130);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 35);
            this.btnThem.TabIndex = 13;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(240, 130);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(160, 35);
            this.btnSua.TabIndex = 14;
            this.btnSua.Text = "Sửa / Cập nhật giá";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(410, 130);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 35);
            this.btnXoa.TabIndex = 15;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(520, 130);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 35);
            this.btnLamMoi.TabIndex = 16;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // lblTim
            // 
            this.lblTim.AutoSize = true;
            this.lblTim.Location = new System.Drawing.Point(640, 138);
            this.lblTim.Name = "lblTim";
            this.lblTim.TabIndex = 17;
            this.lblTim.Text = "Tìm:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimKiem.Location = new System.Drawing.Point(680, 135);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(250, 25);
            this.txtTimKiem.TabIndex = 18;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // dgvThuoc
            // 
            this.dgvThuoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.dgvThuoc.Location = new System.Drawing.Point(20, 180);
            this.dgvThuoc.Name = "dgvThuoc";
            this.dgvThuoc.Size = new System.Drawing.Size(910, 385);
            this.dgvThuoc.TabIndex = 19;
            this.dgvThuoc.AllowUserToAddRows = false;
            this.dgvThuoc.AllowUserToDeleteRows = false;
            this.dgvThuoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThuoc.BackgroundColor = System.Drawing.Color.White;
            this.dgvThuoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThuoc.MultiSelect = false;
            this.dgvThuoc.ReadOnly = true;
            this.dgvThuoc.RowHeadersWidth = 30;
            this.dgvThuoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThuoc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThuoc_CellClick);
            // 
            // FormThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(950, 580);
            this.Controls.Add(this.dgvThuoc);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.lblTim);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnNhapKho);
            this.Controls.Add(this.nudNhapThem);
            this.Controls.Add(this.lblNhapThem);
            this.Controls.Add(this.nudSoLuongTon);
            this.Controls.Add(this.lblSoLuongTon);
            this.Controls.Add(this.nudDonGia);
            this.Controls.Add(this.lblDonGia);
            this.Controls.Add(this.cboDonViTinh);
            this.Controls.Add(this.lblDonVi);
            this.Controls.Add(this.txtTenThuoc);
            this.Controls.Add(this.lblTenThuoc);
            this.Controls.Add(this.txtMaThuoc);
            this.Controls.Add(this.lblMaThuoc);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(950, 580);
            this.Name = "FormThuoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Danh mục thuốc";
            this.Load += new System.EventHandler(this.FormThuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongTon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNhapThem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThuoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMaThuoc;
        private System.Windows.Forms.TextBox txtMaThuoc;
        private System.Windows.Forms.Label lblTenThuoc;
        private System.Windows.Forms.TextBox txtTenThuoc;
        private System.Windows.Forms.Label lblDonVi;
        private System.Windows.Forms.ComboBox cboDonViTinh;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.NumericUpDown nudDonGia;
        private System.Windows.Forms.Label lblSoLuongTon;
        private System.Windows.Forms.NumericUpDown nudSoLuongTon;
        private System.Windows.Forms.Label lblNhapThem;
        private System.Windows.Forms.NumericUpDown nudNhapThem;
        private System.Windows.Forms.Button btnNhapKho;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblTim;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.DataGridView dgvThuoc;
    }
}
