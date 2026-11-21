namespace LibraryManagement.GUI
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnDauSach = new System.Windows.Forms.Button();
            this.btnDocGia = new System.Windows.Forms.Button();
            this.btnMuonTra = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(480, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDauSach
            // 
            this.btnDauSach.Location = new System.Drawing.Point(30, 90);
            this.btnDauSach.Name = "btnDauSach";
            this.btnDauSach.Size = new System.Drawing.Size(220, 50);
            this.btnDauSach.TabIndex = 1;
            this.btnDauSach.Text = "Quản lý Đầu sách";
            this.btnDauSach.UseVisualStyleBackColor = true;
            this.btnDauSach.Click += new System.EventHandler(this.btnDauSach_Click);
            // 
            // btnDocGia
            // 
            this.btnDocGia.Location = new System.Drawing.Point(270, 90);
            this.btnDocGia.Name = "btnDocGia";
            this.btnDocGia.Size = new System.Drawing.Size(220, 50);
            this.btnDocGia.TabIndex = 2;
            this.btnDocGia.Text = "Quản lý Độc giả";
            this.btnDocGia.UseVisualStyleBackColor = true;
            this.btnDocGia.Click += new System.EventHandler(this.btnDocGia_Click);
            // 
            // btnMuonTra
            // 
            this.btnMuonTra.Location = new System.Drawing.Point(30, 170);
            this.btnMuonTra.Name = "btnMuonTra";
            this.btnMuonTra.Size = new System.Drawing.Size(220, 50);
            this.btnMuonTra.TabIndex = 3;
            this.btnMuonTra.Text = "Mượn / Trả sách";
            this.btnMuonTra.UseVisualStyleBackColor = true;
            this.btnMuonTra.Click += new System.EventHandler(this.btnMuonTra_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(270, 170);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(220, 50);
            this.btnThongKe.TabIndex = 4;
            this.btnThongKe.Text = "Quá hạn && Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(150, 265);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(220, 45);
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(520, 360);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.btnMuonTra);
            this.Controls.Add(this.btnDocGia);
            this.Controls.Add(this.btnDauSach);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Thư viện";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnDauSach;
        private System.Windows.Forms.Button btnDocGia;
        private System.Windows.Forms.Button btnMuonTra;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnThoat;
    }
}
