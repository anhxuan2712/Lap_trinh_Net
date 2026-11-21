namespace ClinicManagement.GUI
{
    partial class FormThanhToan
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
            this.grpDon = new System.Windows.Forms.GroupBox();
            this.lblLocTT = new System.Windows.Forms.Label();
            this.cboLocTrangThai = new System.Windows.Forms.ComboBox();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.dgvDon = new System.Windows.Forms.DataGridView();
            this.grpChiTiet = new System.Windows.Forms.GroupBox();
            this.lblThongTinBN = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.lblTienKhamTT = new System.Windows.Forms.Label();
            this.lblTienThuoc = new System.Windows.Forms.Label();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnInHoaDon = new System.Windows.Forms.Button();
            this.grpDon.SuspendLayout();
            this.grpChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDon
            // 
            this.grpDon.Controls.Add(this.dgvDon);
            this.grpDon.Controls.Add(this.btnTaiLai);
            this.grpDon.Controls.Add(this.cboLocTrangThai);
            this.grpDon.Controls.Add(this.lblLocTT);
            this.grpDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.grpDon.Location = new System.Drawing.Point(12, 8);
            this.grpDon.Name = "grpDon";
            this.grpDon.Size = new System.Drawing.Size(560, 620);
            this.grpDon.TabIndex = 0;
            this.grpDon.Text = "Danh sách đơn thuốc";
            // 
            // lblLocTT
            // 
            this.lblLocTT.AutoSize = true;
            this.lblLocTT.Location = new System.Drawing.Point(15, 32);
            this.lblLocTT.Name = "lblLocTT";
            this.lblLocTT.TabIndex = 1;
            this.lblLocTT.Text = "Trạng thái:";
            // 
            // cboLocTrangThai
            // 
            this.cboLocTrangThai.Location = new System.Drawing.Point(100, 29);
            this.cboLocTrangThai.Name = "cboLocTrangThai";
            this.cboLocTrangThai.Size = new System.Drawing.Size(180, 25);
            this.cboLocTrangThai.TabIndex = 2;
            this.cboLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.Location = new System.Drawing.Point(290, 26);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(100, 32);
            this.btnTaiLai.TabIndex = 3;
            this.btnTaiLai.Text = "Tải lại";
            this.btnTaiLai.UseVisualStyleBackColor = true;
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            // 
            // dgvDon
            // 
            this.dgvDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDon.Location = new System.Drawing.Point(15, 66);
            this.dgvDon.Name = "dgvDon";
            this.dgvDon.Size = new System.Drawing.Size(530, 538);
            this.dgvDon.TabIndex = 4;
            this.dgvDon.AllowUserToAddRows = false;
            this.dgvDon.AllowUserToDeleteRows = false;
            this.dgvDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDon.BackgroundColor = System.Drawing.Color.White;
            this.dgvDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDon.MultiSelect = false;
            this.dgvDon.ReadOnly = true;
            this.dgvDon.RowHeadersWidth = 30;
            this.dgvDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDon_CellClick);
            this.dgvDon.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDon_CellFormatting);
            // 
            // grpChiTiet
            // 
            this.grpChiTiet.Controls.Add(this.btnInHoaDon);
            this.grpChiTiet.Controls.Add(this.btnThanhToan);
            this.grpChiTiet.Controls.Add(this.lblTongCong);
            this.grpChiTiet.Controls.Add(this.lblTienThuoc);
            this.grpChiTiet.Controls.Add(this.lblTienKhamTT);
            this.grpChiTiet.Controls.Add(this.dgvChiTiet);
            this.grpChiTiet.Controls.Add(this.lblThongTinBN);
            this.grpChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.grpChiTiet.Location = new System.Drawing.Point(580, 8);
            this.grpChiTiet.Name = "grpChiTiet";
            this.grpChiTiet.Size = new System.Drawing.Size(458, 620);
            this.grpChiTiet.TabIndex = 5;
            this.grpChiTiet.Text = "Chi tiết đơn / Hóa đơn";
            // 
            // lblThongTinBN
            // 
            this.lblThongTinBN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.lblThongTinBN.Location = new System.Drawing.Point(15, 28);
            this.lblThongTinBN.Name = "lblThongTinBN";
            this.lblThongTinBN.Size = new System.Drawing.Size(428, 72);
            this.lblThongTinBN.TabIndex = 6;
            this.lblThongTinBN.Text = "";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChiTiet.Location = new System.Drawing.Point(15, 105);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.Size = new System.Drawing.Size(428, 300);
            this.dgvChiTiet.TabIndex = 7;
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
            // lblTienKhamTT
            // 
            this.lblTienKhamTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.lblTienKhamTT.AutoSize = true;
            this.lblTienKhamTT.Location = new System.Drawing.Point(15, 420);
            this.lblTienKhamTT.Name = "lblTienKhamTT";
            this.lblTienKhamTT.TabIndex = 8;
            this.lblTienKhamTT.Text = "";
            // 
            // lblTienThuoc
            // 
            this.lblTienThuoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.lblTienThuoc.AutoSize = true;
            this.lblTienThuoc.Location = new System.Drawing.Point(15, 450);
            this.lblTienThuoc.Name = "lblTienThuoc";
            this.lblTienThuoc.TabIndex = 9;
            this.lblTienThuoc.Text = "";
            // 
            // lblTongCong
            // 
            this.lblTongCong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Location = new System.Drawing.Point(15, 482);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.TabIndex = 10;
            this.lblTongCong.Text = "";
            this.lblTongCong.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTongCong.ForeColor = System.Drawing.Color.Firebrick;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.btnThanhToan.Location = new System.Drawing.Point(15, 540);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(205, 55);
            this.btnThanhToan.TabIndex = 11;
            this.btnThanhToan.Text = "THANH TOÁN";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // btnInHoaDon
            // 
            this.btnInHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left)));
            this.btnInHoaDon.Location = new System.Drawing.Point(238, 540);
            this.btnInHoaDon.Name = "btnInHoaDon";
            this.btnInHoaDon.Size = new System.Drawing.Size(205, 55);
            this.btnInHoaDon.TabIndex = 12;
            this.btnInHoaDon.Text = "IN HÓA ĐƠN";
            this.btnInHoaDon.UseVisualStyleBackColor = true;
            this.btnInHoaDon.Click += new System.EventHandler(this.btnInHoaDon_Click);
            // 
            // FormThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1050, 640);
            this.Controls.Add(this.grpChiTiet);
            this.Controls.Add(this.grpDon);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1050, 640);
            this.Name = "FormThanhToan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thanh toán & In hóa đơn";
            this.Load += new System.EventHandler(this.FormThanhToan_Load);
            this.grpDon.ResumeLayout(false);
            this.grpDon.PerformLayout();
            this.grpChiTiet.ResumeLayout(false);
            this.grpChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDon;
        private System.Windows.Forms.Label lblLocTT;
        private System.Windows.Forms.ComboBox cboLocTrangThai;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.DataGridView dgvDon;
        private System.Windows.Forms.GroupBox grpChiTiet;
        private System.Windows.Forms.Label lblThongTinBN;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Label lblTienKhamTT;
        private System.Windows.Forms.Label lblTienThuoc;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Button btnInHoaDon;
    }
}
