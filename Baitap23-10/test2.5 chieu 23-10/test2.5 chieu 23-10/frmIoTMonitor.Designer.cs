using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace test2._5_chieu_23_10
{
    partial class frmIoTMonitor
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpControls = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.grpRealtime = new System.Windows.Forms.GroupBox();
            this.lblTempCaption = new System.Windows.Forms.Label();
            this.lblTemperature = new System.Windows.Forms.Label();
            this.grpLogs = new System.Windows.Forms.GroupBox();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusSeparator = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusCycles = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpControls.SuspendLayout();
            this.grpRealtime.SuspendLayout();
            this.grpLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // grpControls
            // 
            this.grpControls.Controls.Add(this.btnStop);
            this.grpControls.Controls.Add(this.btnStart);
            this.grpControls.Location = new System.Drawing.Point(12, 12);
            this.grpControls.Name = "grpControls";
            this.grpControls.Size = new System.Drawing.Size(760, 65);
            this.grpControls.TabIndex = 0;
            this.grpControls.TabStop = false;
            this.grpControls.Text = "Khu vực điều khiển";

            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(20, 24);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(150, 30);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Bắt đầu Giám sát";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(190, 24);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(150, 30);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Dừng Giám sát";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);

            // 
            // grpRealtime
            // 
            this.grpRealtime.Controls.Add(this.lblTempCaption);
            this.grpRealtime.Controls.Add(this.lblTemperature);
            this.grpRealtime.Location = new System.Drawing.Point(12, 85);
            this.grpRealtime.Name = "grpRealtime";
            this.grpRealtime.Size = new System.Drawing.Size(760, 80);
            this.grpRealtime.TabIndex = 1;
            this.grpRealtime.TabStop = false;
            this.grpRealtime.Text = "Khu vực hiển thị real-time";

            // 
            // lblTempCaption
            // 
            this.lblTempCaption.AutoSize = true;
            this.lblTempCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTempCaption.Location = new System.Drawing.Point(20, 32);
            this.lblTempCaption.Name = "lblTempCaption";
            this.lblTempCaption.Size = new System.Drawing.Size(133, 18);
            this.lblTempCaption.TabIndex = 0;
            this.lblTempCaption.Text = "Nhiệt độ cảm biến: ";

            // 
            // lblTemperature
            // 
            this.lblTemperature.AutoSize = true;
            this.lblTemperature.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemperature.ForeColor = System.Drawing.Color.Green;
            this.lblTemperature.Location = new System.Drawing.Point(165, 22);
            this.lblTemperature.Name = "lblTemperature";
            this.lblTemperature.Size = new System.Drawing.Size(97, 36);
            this.lblTemperature.TabIndex = 1;
            this.lblTemperature.Text = "--.- °C";

            // 
            // grpLogs
            // 
            this.grpLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLogs.Controls.Add(this.dgvLogs);
            this.grpLogs.Location = new System.Drawing.Point(12, 175);
            this.grpLogs.Name = "grpLogs";
            this.grpLogs.Size = new System.Drawing.Size(760, 300);
            this.grpLogs.TabIndex = 2;
            this.grpLogs.TabStop = false;
            this.grpLogs.Text = "Nhật ký ghi nhận các mốc nhiệt độ theo thời gian real-time (dgvLogs)";

            // 
            // dgvLogs
            // 
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogs.Location = new System.Drawing.Point(3, 16);
            this.dgvLogs.MultiSelect = false;
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.ReadOnly = true;
            this.dgvLogs.RowHeadersVisible = false;
            this.dgvLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLogs.Size = new System.Drawing.Size(754, 281);
            this.dgvLogs.TabIndex = 0;

            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusConnection,
            this.lblStatusSeparator,
            this.lblStatusCycles});
            this.statusStrip.Location = new System.Drawing.Point(0, 489);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip";

            // 
            // lblStatusConnection
            // 
            this.lblStatusConnection.Name = "lblStatusConnection";
            this.lblStatusConnection.Size = new System.Drawing.Size(127, 17);
            this.lblStatusConnection.Text = "Trạng thái: Chưa kết nối";

            // 
            // lblStatusSeparator
            // 
            this.lblStatusSeparator.Name = "lblStatusSeparator";
            this.lblStatusSeparator.Size = new System.Drawing.Size(10, 17);
            this.lblStatusSeparator.Text = "|";

            // 
            // lblStatusCycles
            // 
            this.lblStatusCycles.Name = "lblStatusCycles";
            this.lblStatusCycles.Size = new System.Drawing.Size(161, 17);
            this.lblStatusCycles.Text = "Số lần nhận chu kỳ tín hiệu: 0";

            // 
            // frmIoTMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.grpLogs);
            this.Controls.Add(this.grpRealtime);
            this.Controls.Add(this.grpControls);
            this.Name = "frmIoTMonitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mô phỏng Giám sát Hệ thống Cảnh báo IoT Real-time";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIoTMonitor_FormClosing);
            this.Load += new System.EventHandler(this.frmIoTMonitor_Load);
            this.grpControls.ResumeLayout(false);
            this.grpRealtime.ResumeLayout(false);
            this.grpRealtime.PerformLayout();
            this.grpLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.GroupBox grpControls;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox grpRealtime;
        private System.Windows.Forms.Label lblTempCaption;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.GroupBox grpLogs;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusConnection;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusSeparator;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusCycles;
    }
}
