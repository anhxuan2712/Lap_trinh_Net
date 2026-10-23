using System;
using System.Drawing;
using System.Windows.Forms;

namespace test2._3_chieu23_10
{
    partial class frmSchoolMain
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChangePwd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStudentList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTeacherList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClassList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTileHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTileVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblManagerInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSystem,
            this.menuManagement,
            this.menuWindow});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1200, 33);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // menuSystem
            // 
            this.menuSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLogout,
            this.mnuChangePwd,
            this.mnuExit});
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Size = new System.Drawing.Size(84, 29);
            this.menuSystem.Text = "Hệ thống";
            // 
            // mnuLogout
            // 
            this.mnuLogout.Name = "mnuLogout";
            this.mnuLogout.Size = new System.Drawing.Size(180, 30);
            this.mnuLogout.Text = "Đăng xuất";
            this.mnuLogout.Click += new System.EventHandler(this.mnuLogout_Click);
            // 
            // mnuChangePwd
            // 
            this.mnuChangePwd.Name = "mnuChangePwd";
            this.mnuChangePwd.Size = new System.Drawing.Size(180, 30);
            this.mnuChangePwd.Text = "Đổi mật khẩu";
            this.mnuChangePwd.Click += new System.EventHandler(this.mnuChangePwd_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(180, 30);
            this.mnuExit.Text = "Thoát";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // menuManagement
            // 
            this.menuManagement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStudentList,
            this.mnuTeacherList,
            this.mnuClassList});
            this.menuManagement.Name = "menuManagement";
            this.menuManagement.Size = new System.Drawing.Size(95, 29);
            this.menuManagement.Text = "Quản lý";
            // 
            // mnuStudentList
            // 
            this.mnuStudentList.Name = "mnuStudentList";
            this.mnuStudentList.Size = new System.Drawing.Size(215, 30);
            this.mnuStudentList.Text = "Danh sách Sinh viên";
            this.mnuStudentList.Click += new System.EventHandler(this.mnuStudentList_Click);
            // 
            // mnuTeacherList
            // 
            this.mnuTeacherList.Name = "mnuTeacherList";
            this.mnuTeacherList.Size = new System.Drawing.Size(240, 30);
            this.mnuTeacherList.Text = "Danh sách Giảng viên";
            this.mnuTeacherList.Click += new System.EventHandler(this.mnuTeacherList_Click);
            // 
            // mnuClassList
            // 
            this.mnuClassList.Name = "mnuClassList";
            this.mnuClassList.Size = new System.Drawing.Size(240, 30);
            this.mnuClassList.Text = "Danh mục Môn học";
            this.mnuClassList.Click += new System.EventHandler(this.mnuClassList_Click);
            // 
            // menuWindow
            // 
            this.menuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCascade,
            this.mnuTileHorizontal,
            this.mnuTileVertical,
            this.mnuCloseAll});
            this.menuWindow.Name = "menuWindow";
            this.menuWindow.Size = new System.Drawing.Size(73, 29);
            this.menuWindow.Text = "Cửa sổ";
            // 
            // mnuCascade
            // 
            this.mnuCascade.Name = "mnuCascade";
            this.mnuCascade.Size = new System.Drawing.Size(227, 30);
            this.mnuCascade.Text = "Xếp tầng (Cascade)";
            this.mnuCascade.Click += new System.EventHandler(this.mnuCascade_Click);
            // 
            // mnuTileHorizontal
            // 
            this.mnuTileHorizontal.Name = "mnuTileHorizontal";
            this.mnuTileHorizontal.Size = new System.Drawing.Size(227, 30);
            this.mnuTileHorizontal.Text = "Ngang (Tile Horizontal)";
            this.mnuTileHorizontal.Click += new System.EventHandler(this.mnuTileHorizontal_Click);
            // 
            // mnuTileVertical
            // 
            this.mnuTileVertical.Name = "mnuTileVertical";
            this.mnuTileVertical.Size = new System.Drawing.Size(227, 30);
            this.mnuTileVertical.Text = "Dọc (Tile Vertical)";
            this.mnuTileVertical.Click += new System.EventHandler(this.mnuTileVertical_Click);
            // 
            // mnuCloseAll
            // 
            this.mnuCloseAll.Name = "mnuCloseAll";
            this.mnuCloseAll.Size = new System.Drawing.Size(227, 30);
            this.mnuCloseAll.Text = "Đóng tất cả";
            this.mnuCloseAll.Click += new System.EventHandler(this.mnuCloseAll_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblManagerInfo,
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 718);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1200, 32);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblManagerInfo
            // 
            this.lblManagerInfo.Name = "lblManagerInfo";
            this.lblManagerInfo.Size = new System.Drawing.Size(72, 25);
            this.lblManagerInfo.Text = "Người quản lý:";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(200, 24);
            // 
            // frmSchoolMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "frmSchoolMain";
            this.Text = "Quản lý Trường học & Đào tạo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSchoolMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuSystem;
        private System.Windows.Forms.ToolStripMenuItem mnuLogout;
        private System.Windows.Forms.ToolStripMenuItem mnuChangePwd;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem menuManagement;
        private System.Windows.Forms.ToolStripMenuItem mnuStudentList;
        private System.Windows.Forms.ToolStripMenuItem mnuTeacherList;
        private System.Windows.Forms.ToolStripMenuItem mnuClassList;
        private System.Windows.Forms.ToolStripMenuItem menuWindow;
        private System.Windows.Forms.ToolStripMenuItem mnuCascade;
        private System.Windows.Forms.ToolStripMenuItem mnuTileHorizontal;
        private System.Windows.Forms.ToolStripMenuItem mnuTileVertical;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseAll;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblManagerInfo;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
    }
}
