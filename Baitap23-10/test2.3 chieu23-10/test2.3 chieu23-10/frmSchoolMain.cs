using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace test2._3_chieu23_10
{
    public partial class frmSchoolMain : Form
    {
        public frmSchoolMain()
        {
            InitializeComponent();
        }

        private void frmSchoolMain_Load(object sender, EventArgs e)
        {
            // Dummy manager info
            lblManagerInfo.Text = "Cán bộ quản lý: Nguyễn Văn A";
        }

        // System menu actions
        private void mnuLogout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đăng xuất không thực hiện trong demo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuChangePwd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đổi mật khẩu không thực hiện trong demo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Management menu actions
        private void mnuStudentList_Click(object sender, EventArgs e)
        {
            ShowChildForm(typeof(frmStudentList));
        }

        private void mnuTeacherList_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Danh sách Giảng viên đang hoàn thiện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuClassList_Click(object sender, EventArgs e)
        {
            ShowChildForm(typeof(frmCourseList));
        }

        // Window arrangement actions
        private void mnuCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void mnuTileHorizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void mnuTileVertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void mnuCloseAll_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren)
            {
                child.Close();
            }
        }

        // Helper to open single instance of a child form
        private void ShowChildForm(Type formType)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child.GetType() == formType)
                {
                    child.Activate();
                    return;
                }
            }
            Form frm = (Form)Activator.CreateInstance(formType);
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
