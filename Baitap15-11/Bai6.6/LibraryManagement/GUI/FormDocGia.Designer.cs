namespace LibraryManagement.GUI
{
    partial class FormDocGia
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
            this.lblMaDocGia = new System.Windows.Forms.Label();
            this.txtMaDocGia = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblNgayHetHan = new System.Windows.Forms.Label();
            this.dtpNgayHetHan = new System.Windows.Forms.DateTimePicker();
            this.lblTinhTrangThe = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnGiaHan = new System.Windows.Forms.Button();
            this.dgvDocGia = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocGia)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMaDocGia
            // 
            this.lblMaDocGia.AutoSize = true;
            this.lblMaDocGia.Location = new System.Drawing.Point(20, 20);
            this.lblMaDocGia.Name = "lblMaDocGia";
            this.lblMaDocGia.TabIndex = 0;
            this.lblMaDocGia.Text = "Mã độc giả:";
            // 
            // txtMaDocGia
            // 
            this.txtMaDocGia.Location = new System.Drawing.Point(130, 17);
            this.txtMaDocGia.Name = "txtMaDocGia";
            this.txtMaDocGia.Size = new System.Drawing.Size(120, 25);
            this.txtMaDocGia.TabIndex = 1;
            this.txtMaDocGia.ReadOnly = true;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(20, 55);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.TabIndex = 2;
            this.lblHoTen.Text = "Họ tên:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(130, 52);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(340, 25);
            this.txtHoTen.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(500, 20);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(620, 17);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(260, 25);
            this.txtEmail.TabIndex = 5;
            // 
            // lblNgayHetHan
            // 
            this.lblNgayHetHan.AutoSize = true;
            this.lblNgayHetHan.Location = new System.Drawing.Point(500, 55);
            this.lblNgayHetHan.Name = "lblNgayHetHan";
            this.lblNgayHetHan.TabIndex = 6;
            this.lblNgayHetHan.Text = "Hạn thẻ:";
            // 
            // dtpNgayHetHan
            // 
            this.dtpNgayHetHan.Location = new System.Drawing.Point(620, 52);
            this.dtpNgayHetHan.Name = "dtpNgayHetHan";
            this.dtpNgayHetHan.Size = new System.Drawing.Size(160, 25);
            this.dtpNgayHetHan.TabIndex = 7;
            this.dtpNgayHetHan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayHetHan.CustomFormat = "dd/MM/yyyy";
            // 
            // lblTinhTrangThe
            // 
            this.lblTinhTrangThe.AutoSize = true;
            this.lblTinhTrangThe.Location = new System.Drawing.Point(790, 55);
            this.lblTinhTrangThe.Name = "lblTinhTrangThe";
            this.lblTinhTrangThe.TabIndex = 8;
            this.lblTinhTrangThe.Text = "";
            this.lblTinhTrangThe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(130, 95);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 35);
            this.btnThem.TabIndex = 9;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(240, 95);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 35);
            this.btnSua.TabIndex = 10;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(350, 95);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 35);
            this.btnXoa.TabIndex = 11;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(460, 95);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 35);
            this.btnLamMoi.TabIndex = 12;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnGiaHan
            // 
            this.btnGiaHan.Location = new System.Drawing.Point(570, 95);
            this.btnGiaHan.Name = "btnGiaHan";
            this.btnGiaHan.Size = new System.Drawing.Size(150, 35);
            this.btnGiaHan.TabIndex = 13;
            this.btnGiaHan.Text = "Gia hạn +1 năm";
            this.btnGiaHan.UseVisualStyleBackColor = true;
            this.btnGiaHan.Click += new System.EventHandler(this.btnGiaHan_Click);
            // 
            // dgvDocGia
            // 
            this.dgvDocGia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top) | (System.Windows.Forms.AnchorStyles.Bottom) | (System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDocGia.Location = new System.Drawing.Point(20, 145);
            this.dgvDocGia.Name = "dgvDocGia";
            this.dgvDocGia.Size = new System.Drawing.Size(860, 395);
            this.dgvDocGia.TabIndex = 14;
            this.dgvDocGia.AllowUserToAddRows = false;
            this.dgvDocGia.AllowUserToDeleteRows = false;
            this.dgvDocGia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDocGia.BackgroundColor = System.Drawing.Color.White;
            this.dgvDocGia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocGia.MultiSelect = false;
            this.dgvDocGia.ReadOnly = true;
            this.dgvDocGia.RowHeadersWidth = 30;
            this.dgvDocGia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDocGia.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDocGia_CellClick);
            this.dgvDocGia.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDocGia_CellFormatting);
            // 
            // FormDocGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.Controls.Add(this.dgvDocGia);
            this.Controls.Add(this.btnGiaHan);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.lblTinhTrangThe);
            this.Controls.Add(this.dtpNgayHetHan);
            this.Controls.Add(this.lblNgayHetHan);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblHoTen);
            this.Controls.Add(this.txtMaDocGia);
            this.Controls.Add(this.lblMaDocGia);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "FormDocGia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Độc giả";
            this.Load += new System.EventHandler(this.FormDocGia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocGia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMaDocGia;
        private System.Windows.Forms.TextBox txtMaDocGia;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblNgayHetHan;
        private System.Windows.Forms.DateTimePicker dtpNgayHetHan;
        private System.Windows.Forms.Label lblTinhTrangThe;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnGiaHan;
        private System.Windows.Forms.DataGridView dgvDocGia;
    }
}
