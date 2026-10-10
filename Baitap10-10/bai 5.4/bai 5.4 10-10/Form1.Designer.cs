namespace bai_5._4_10_10
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Label lblView;
        private System.Windows.Forms.ComboBox cboViewMode;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView tvDepartments;
        private System.Windows.Forms.ListView lsvEmployees;
        private System.Windows.Forms.ColumnHeader colEmployeeId;
        private System.Windows.Forms.ColumnHeader colFullName;
        private System.Windows.Forms.ColumnHeader colPosition;
        private System.Windows.Forms.ColumnHeader colHireDate;
        private System.Windows.Forms.ImageList imageListTree;
        private System.Windows.Forms.ImageList imageListEmployees;

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
            this.components = new System.ComponentModel.Container();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.lblView = new System.Windows.Forms.Label();
            this.cboViewMode = new System.Windows.Forms.ComboBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tvDepartments = new System.Windows.Forms.TreeView();
            this.lsvEmployees = new System.Windows.Forms.ListView();
            this.colEmployeeId = new System.Windows.Forms.ColumnHeader();
            this.colFullName = new System.Windows.Forms.ColumnHeader();
            this.colPosition = new System.Windows.Forms.ColumnHeader();
            this.colHireDate = new System.Windows.Forms.ColumnHeader();
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.imageListEmployees = new System.Windows.Forms.ImageList(this.components);
            this.pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlToolbar
            //
            this.pnlToolbar.Controls.Add(this.lblView);
            this.pnlToolbar.Controls.Add(this.cboViewMode);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Height = 42;
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(8);
            //
            // lblView
            //
            this.lblView.AutoSize = true;
            this.lblView.Location = new System.Drawing.Point(8, 13);
            this.lblView.Name = "lblView";
            this.lblView.Size = new System.Drawing.Size(78, 15);
            this.lblView.Text = "Chế độ xem:";
            //
            // cboViewMode
            //
            this.cboViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboViewMode.FormattingEnabled = true;
            this.cboViewMode.Items.AddRange(new object[] {
                "Details",
                "SmallIcon",
                "LargeIcon",
                "Tile"});
            this.cboViewMode.Location = new System.Drawing.Point(95, 9);
            this.cboViewMode.Name = "cboViewMode";
            this.cboViewMode.Size = new System.Drawing.Size(140, 23);
            this.cboViewMode.SelectedIndexChanged += new System.EventHandler(this.cboViewMode_SelectedIndexChanged);
            //
            // splitContainer
            //
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 42);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Panel1.Controls.Add(this.tvDepartments);
            this.splitContainer.Panel2.Controls.Add(this.lsvEmployees);
            this.splitContainer.Size = new System.Drawing.Size(900, 508);
            this.splitContainer.SplitterDistance = 270;
            //
            // tvDepartments
            //
            this.tvDepartments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDepartments.HideSelection = false;
            this.tvDepartments.ImageList = this.imageListTree;
            this.tvDepartments.Location = new System.Drawing.Point(0, 0);
            this.tvDepartments.Name = "tvDepartments";
            this.tvDepartments.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDepartments_AfterSelect);
            //
            // lsvEmployees
            //
            this.lsvEmployees.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colEmployeeId,
                this.colFullName,
                this.colPosition,
                this.colHireDate});
            this.lsvEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvEmployees.FullRowSelect = true;
            this.lsvEmployees.GridLines = true;
            this.lsvEmployees.HideSelection = false;
            this.lsvEmployees.LargeImageList = this.imageListEmployees;
            this.lsvEmployees.Location = new System.Drawing.Point(0, 0);
            this.lsvEmployees.Name = "lsvEmployees";
            this.lsvEmployees.SmallImageList = this.imageListTree;
            this.lsvEmployees.UseCompatibleStateImageBehavior = false;
            this.lsvEmployees.View = System.Windows.Forms.View.Details;
            //
            // colEmployeeId
            //
            this.colEmployeeId.Text = "Mã NV";
            this.colEmployeeId.Width = 85;
            //
            // colFullName
            //
            this.colFullName.Text = "Họ Tên";
            this.colFullName.Width = 160;
            //
            // colPosition
            //
            this.colPosition.Text = "Chức vụ";
            this.colPosition.Width = 150;
            //
            // colHireDate
            //
            this.colHireDate.Text = "Ngày vào làm";
            this.colHireDate.Width = 110;
            //
            // imageListTree
            //
            this.imageListTree.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListTree.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            //
            // imageListEmployees
            //
            this.imageListEmployees.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListEmployees.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListEmployees.TransparentColor = System.Drawing.Color.Transparent;
            //
            // Form1
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlToolbar);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trình quản lý nhân viên";
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}

