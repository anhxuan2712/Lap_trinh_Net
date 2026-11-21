namespace LibraryManagement.GUI
{
    partial class FormDauSach
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
            this.lblMaSach = new System.Windows.Forms.Label();
            this.txtMaSach = new System.Windows.Forms.TextBox();
            this.lblTenSach = new System.Windows.Forms.Label();
            this.txtTenSach = new System.Windows.Forms.TextBox();
            this.lblTheLoai = new System.Windows.Forms.Label();
            this.cboTheLoai = new System.Windows.Forms.ComboBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblTim = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.dgvDauSach = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDauSach)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMaSach
            // 
            this.lblMaSach.AutoSize = true;
            this.lblMaSach.Location = new System.Drawing.Point(20, 20);
            this.lblMaSach.Name = "lblMaSach";
            this.lblMaSach.TabIndex = 0;
            this.lblMaSach.Text = "Mã sách:";
            // 
            // txtMaSach
            // 
            this.txtMaSach.Location = new System.Drawing.Point(120, 17);
            this.txtMaSach.Name = "txtMaSach";
            this.txtMaSach.Size = new System.Drawing.Size(120, 25);
            this.txtMaSach.TabIndex = 1;
            this.txtMaSach.ReadOnly = true;
            // 
            // lblTenSach
            // 
            this.lblTenSach.AutoSize = true;
            this.lblTenSach.Location = new System.Drawing.Point(20, 55);
            this.lblTenSach.Name = "lblTenSach";
            this.lblTenSach.TabIndex = 2;
            this.lblTenSach.Text = "Tên sách:";
            // 
            // txtTenSach
            // 
            this.txtTenSach.Location = new System.Drawing.Point(120, 52);
            this.txtTenSach.Name = "txtTenSach";
            this.txtTenSach.Size = new System.Drawing.Size(350, 25);
            this.txtTenSach.TabIndex = 3;
            // 
            // lblTheLoai
            // 
            this.lblTheLoai.AutoSize = true;
            this.lblTheLoai.Location = new System.Drawing.Point(500, 20);
            this.lblTheLoai.Name = "lblTheLoai";
            this.lblTheLoai.TabIndex = 4;
            this.lblTheLoai.Text = "Thể loại:";
            // 
            // cboTheLoai
            // 
            this.cboTheLoai.Location = new System.Drawing.Point(590, 17);
            this.cboTheLoai.Name = "cboTheLoai";
            this.cboTheLoai.Size = new System.Drawing.Size(280, 25);
            this.cboTheLoai.TabIndex = 5;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(500, 55);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.TabIndex = 6;
            this.lblSoLuong.Text = "SL còn:";
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.Location = new System.Drawing.Point(590, 52);
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(120, 25);
            this.nudSoLuong.TabIndex = 7;
            this.nudSoLuong.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(120, 95);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 35);
            this.btnThem.TabIndex = 8;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(230, 95);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 35);
            this.btnSua.TabIndex = 9;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(340, 95);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 35);
            this.btnXoa.TabIndex = 10;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(450, 95);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 35);
            this.btnLamMoi.TabIndex = 11;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // lblTim
            // 
            this.lblTim.AutoSize = true;
            this.lblTim.Location = new System.Drawing.Point(590, 103);
            this.lblTim.Name = "lblTim";
            this.lblTim.TabIndex = 12;
            this.lblTim.Text = "Tìm:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimKiem.Location = new System.Drawing.Point(630, 100);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(240, 25);
            this.txtTimKiem.TabIndex = 13;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // dgvDauSach
            // 
            this.dgvDauSach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDauSach.Location = new System.Drawing.Point(20, 145);
            this.dgvDauSach.Name = "dgvDauSach";
            this.dgvDauSach.Size = new System.Drawing.Size(860, 395);
            this.dgvDauSach.TabIndex = 14;
            this.dgvDauSach.AllowUserToAddRows = false;
            this.dgvDauSach.AllowUserToDeleteRows = false;
            this.dgvDauSach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDauSach.BackgroundColor = System.Drawing.Color.White;
            this.dgvDauSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDauSach.MultiSelect = false;
            this.dgvDauSach.ReadOnly = true;
            this.dgvDauSach.RowHeadersWidth = 30;
            this.dgvDauSach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDauSach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDauSach_CellClick);
            // 
            // FormDauSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.Controls.Add(this.dgvDauSach);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.lblTim);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.nudSoLuong);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.cboTheLoai);
            this.Controls.Add(this.lblTheLoai);
            this.Controls.Add(this.txtTenSach);
            this.Controls.Add(this.lblTenSach);
            this.Controls.Add(this.txtMaSach);
            this.Controls.Add(this.lblMaSach);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "FormDauSach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Đầu sách";
            this.Load += new System.EventHandler(this.FormDauSach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDauSach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMaSach;
        private System.Windows.Forms.TextBox txtMaSach;
        private System.Windows.Forms.Label lblTenSach;
        private System.Windows.Forms.TextBox txtTenSach;
        private System.Windows.Forms.Label lblTheLoai;
        private System.Windows.Forms.ComboBox cboTheLoai;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblTim;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.DataGridView dgvDauSach;
    }
}
