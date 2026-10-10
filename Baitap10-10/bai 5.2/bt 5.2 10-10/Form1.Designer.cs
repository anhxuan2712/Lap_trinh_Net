namespace bt_5._2_10_10
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblAvailableServices;
        private System.Windows.Forms.ListBox lstAvailableServices;
        private System.Windows.Forms.Label lblSelectedServices;
        private System.Windows.Forms.ListBox lstSelectedServices;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Label lblSubtotalCaption;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblDiscountCaption;
        private System.Windows.Forms.NumericUpDown nudDiscount;
        private System.Windows.Forms.Label lblPaymentCaption;
        private System.Windows.Forms.Label lblPayment;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblAvailableServices = new System.Windows.Forms.Label();
            this.lstAvailableServices = new System.Windows.Forms.ListBox();
            this.lblSelectedServices = new System.Windows.Forms.Label();
            this.lstSelectedServices = new System.Windows.Forms.ListBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.lblSubtotalCaption = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblDiscountCaption = new System.Windows.Forms.Label();
            this.nudDiscount = new System.Windows.Forms.NumericUpDown();
            this.lblPaymentCaption = new System.Windows.Forms.Label();
            this.lblPayment = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(38, 33);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(79, 16);
            this.lblCategory.TabIndex = 0;
            this.lblCategory.Text = "Loại dịch vụ:";
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(125, 30);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(280, 24);
            this.cboCategory.TabIndex = 1;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.cboCategory_SelectedIndexChanged);
            // 
            // lblAvailableServices
            // 
            this.lblAvailableServices.AutoSize = true;
            this.lblAvailableServices.Location = new System.Drawing.Point(38, 78);
            this.lblAvailableServices.Name = "lblAvailableServices";
            this.lblAvailableServices.Size = new System.Drawing.Size(112, 16);
            this.lblAvailableServices.TabIndex = 2;
            this.lblAvailableServices.Text = "Dịch vụ hiện có:";
            // 
            // lstAvailableServices
            // 
            this.lstAvailableServices.FormattingEnabled = true;
            this.lstAvailableServices.ItemHeight = 16;
            this.lstAvailableServices.Location = new System.Drawing.Point(41, 101);
            this.lstAvailableServices.Name = "lstAvailableServices";
            this.lstAvailableServices.Size = new System.Drawing.Size(330, 228);
            this.lstAvailableServices.TabIndex = 3;
            this.lstAvailableServices.DoubleClick += new System.EventHandler(this.lstAvailableServices_DoubleClick);
            // 
            // lblSelectedServices
            // 
            this.lblSelectedServices.AutoSize = true;
            this.lblSelectedServices.Location = new System.Drawing.Point(522, 78);
            this.lblSelectedServices.Name = "lblSelectedServices";
            this.lblSelectedServices.Size = new System.Drawing.Size(108, 16);
            this.lblSelectedServices.TabIndex = 4;
            this.lblSelectedServices.Text = "Dịch vụ đã chọn:";
            // 
            // lstSelectedServices
            // 
            this.lstSelectedServices.FormattingEnabled = true;
            this.lstSelectedServices.ItemHeight = 16;
            this.lstSelectedServices.Location = new System.Drawing.Point(525, 101);
            this.lstSelectedServices.Name = "lstSelectedServices";
            this.lstSelectedServices.Size = new System.Drawing.Size(330, 228);
            this.lstSelectedServices.TabIndex = 5;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(405, 132);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(96, 38);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = ">";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(405, 188);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(96, 38);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "<";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(405, 244);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(96, 38);
            this.btnClearAll.TabIndex = 8;
            this.btnClearAll.Text = "<<";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // lblSubtotalCaption
            // 
            this.lblSubtotalCaption.AutoSize = true;
            this.lblSubtotalCaption.Location = new System.Drawing.Point(38, 370);
            this.lblSubtotalCaption.Name = "lblSubtotalCaption";
            this.lblSubtotalCaption.Size = new System.Drawing.Size(139, 16);
            this.lblSubtotalCaption.TabIndex = 9;
            this.lblSubtotalCaption.Text = "Tổng tiền chưa giảm:";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubtotal.Location = new System.Drawing.Point(183, 363);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(190, 30);
            this.lblSubtotal.TabIndex = 10;
            this.lblSubtotal.Text = "0 ₫";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscountCaption
            // 
            this.lblDiscountCaption.AutoSize = true;
            this.lblDiscountCaption.Location = new System.Drawing.Point(405, 370);
            this.lblDiscountCaption.Name = "lblDiscountCaption";
            this.lblDiscountCaption.Size = new System.Drawing.Size(97, 16);
            this.lblDiscountCaption.TabIndex = 11;
            this.lblDiscountCaption.Text = "Chiết khấu (%):";
            // 
            // nudDiscount
            // 
            this.nudDiscount.Location = new System.Drawing.Point(508, 367);
            this.nudDiscount.Name = "nudDiscount";
            this.nudDiscount.Size = new System.Drawing.Size(72, 22);
            this.nudDiscount.TabIndex = 12;
            this.nudDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDiscount.ValueChanged += new System.EventHandler(this.nudDiscount_ValueChanged);
            // 
            // lblPaymentCaption
            // 
            this.lblPaymentCaption.AutoSize = true;
            this.lblPaymentCaption.Location = new System.Drawing.Point(38, 424);
            this.lblPaymentCaption.Name = "lblPaymentCaption";
            this.lblPaymentCaption.Size = new System.Drawing.Size(151, 16);
            this.lblPaymentCaption.TabIndex = 13;
            this.lblPaymentCaption.Text = "Thành tiền thanh toán:";
            // 
            // lblPayment
            // 
            this.lblPayment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayment.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblPayment.Location = new System.Drawing.Point(195, 417);
            this.lblPayment.Name = "lblPayment";
            this.lblPayment.Size = new System.Drawing.Size(178, 32);
            this.lblPayment.TabIndex = 14;
            this.lblPayment.Text = "0 ₫";
            this.lblPayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 490);
            this.Controls.Add(this.lblPayment);
            this.Controls.Add(this.lblPaymentCaption);
            this.Controls.Add(this.nudDiscount);
            this.Controls.Add(this.lblDiscountCaption);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.lblSubtotalCaption);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lstSelectedServices);
            this.Controls.Add(this.lblSelectedServices);
            this.Controls.Add(this.lstAvailableServices);
            this.Controls.Add(this.lblAvailableServices);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.lblCategory);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bảng tính tiền dịch vụ và chiết khấu đơn hàng";
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

