namespace ClinicManagement.GUI
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
            this.btnTiepNhan = new System.Windows.Forms.Button();
            this.btnKeDon = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnThuoc = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(480, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "PHÒNG KHÁM && NHÀ THUỐC";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTiepNhan
            // 
            this.btnTiepNhan.Location = new System.Drawing.Point(30, 90);
            this.btnTiepNhan.Name = "btnTiepNhan";
            this.btnTiepNhan.Size = new System.Drawing.Size(220, 50);
            this.btnTiepNhan.TabIndex = 1;
            this.btnTiepNhan.Text = "Tiếp nhận bệnh nhân";
            this.btnTiepNhan.UseVisualStyleBackColor = true;
            this.btnTiepNhan.Click += new System.EventHandler(this.btnTiepNhan_Click);
            // 
            // btnKeDon
            // 
            this.btnKeDon.Location = new System.Drawing.Point(270, 90);
            this.btnKeDon.Name = "btnKeDon";
            this.btnKeDon.Size = new System.Drawing.Size(220, 50);
            this.btnKeDon.TabIndex = 2;
            this.btnKeDon.Text = "Khám && Kê đơn thuốc";
            this.btnKeDon.UseVisualStyleBackColor = true;
            this.btnKeDon.Click += new System.EventHandler(this.btnKeDon_Click);
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Location = new System.Drawing.Point(30, 170);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(220, 50);
            this.btnThanhToan.TabIndex = 3;
            this.btnThanhToan.Text = "Thanh toán && Hóa đơn";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // btnThuoc
            // 
            this.btnThuoc.Location = new System.Drawing.Point(270, 170);
            this.btnThuoc.Name = "btnThuoc";
            this.btnThuoc.Size = new System.Drawing.Size(220, 50);
            this.btnThuoc.TabIndex = 4;
            this.btnThuoc.Text = "Danh mục thuốc";
            this.btnThuoc.UseVisualStyleBackColor = true;
            this.btnThuoc.Click += new System.EventHandler(this.btnThuoc_Click);
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
            this.Controls.Add(this.btnThuoc);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.btnKeDon);
            this.Controls.Add(this.btnTiepNhan);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Phòng khám";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnTiepNhan;
        private System.Windows.Forms.Button btnKeDon;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Button btnThuoc;
        private System.Windows.Forms.Button btnThoat;
    }
}
