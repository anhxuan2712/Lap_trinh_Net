namespace bai4._9
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel countStatusLabel;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.FlowLayoutPanel filterPanel;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.CheckBox filterDateCheckBox;
        private System.Windows.Forms.DateTimePicker filterDatePicker;
        private System.Windows.Forms.Label filterStatusLabel;
        private System.Windows.Forms.ComboBox filterStatusComboBox;
        private System.Windows.Forms.Button clearFiltersButton;
        private System.Windows.Forms.TableLayoutPanel inputLayout;
        private System.Windows.Forms.Label appointmentIdLabel;
        private System.Windows.Forms.TextBox appointmentIdTextBox;
        private System.Windows.Forms.Label patientNameLabel;
        private System.Windows.Forms.TextBox patientNameTextBox;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.TextBox phoneTextBox;
        private System.Windows.Forms.Label appointmentDateLabel;
        private System.Windows.Forms.DateTimePicker appointmentDatePicker;
        private System.Windows.Forms.Label doctorLabel;
        private System.Windows.Forms.ComboBox doctorComboBox;
        private System.Windows.Forms.Label appointmentStatusLabel;
        private System.Windows.Forms.ComboBox appointmentStatusComboBox;
        private System.Windows.Forms.Label reasonLabel;
        private System.Windows.Forms.TextBox reasonTextBox;
        private System.Windows.Forms.FlowLayoutPanel actionPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.DataGridView appointmentsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn appointmentIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn patientNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn appointmentDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn doctorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn reasonColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn appointmentStatusColumn;
        private System.Windows.Forms.BindingSource appointmentsBindingSource;
        private System.Windows.Forms.ErrorProvider validationErrorProvider;
        private System.Windows.Forms.SaveFileDialog exportSaveFileDialog;

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
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.countStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.filterPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.searchLabel = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.filterDateCheckBox = new System.Windows.Forms.CheckBox();
            this.filterDatePicker = new System.Windows.Forms.DateTimePicker();
            this.filterStatusLabel = new System.Windows.Forms.Label();
            this.filterStatusComboBox = new System.Windows.Forms.ComboBox();
            this.clearFiltersButton = new System.Windows.Forms.Button();
            this.inputLayout = new System.Windows.Forms.TableLayoutPanel();
            this.appointmentIdLabel = new System.Windows.Forms.Label();
            this.appointmentIdTextBox = new System.Windows.Forms.TextBox();
            this.patientNameLabel = new System.Windows.Forms.Label();
            this.patientNameTextBox = new System.Windows.Forms.TextBox();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.phoneTextBox = new System.Windows.Forms.TextBox();
            this.appointmentDateLabel = new System.Windows.Forms.Label();
            this.appointmentDatePicker = new System.Windows.Forms.DateTimePicker();
            this.doctorLabel = new System.Windows.Forms.Label();
            this.doctorComboBox = new System.Windows.Forms.ComboBox();
            this.appointmentStatusLabel = new System.Windows.Forms.Label();
            this.appointmentStatusComboBox = new System.Windows.Forms.ComboBox();
            this.reasonLabel = new System.Windows.Forms.Label();
            this.reasonTextBox = new System.Windows.Forms.TextBox();
            this.actionPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.appointmentsDataGridView = new System.Windows.Forms.DataGridView();
            this.appointmentIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patientNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appointmentDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doctorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reasonColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appointmentStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appointmentsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.validationErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.exportSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.validationErrorProvider)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.mainLayout.SuspendLayout();
            this.filterPanel.SuspendLayout();
            this.inputLayout.SuspendLayout();
            this.actionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.fileMenuItem });
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1180, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.exportMenuItem });
            this.fileMenuItem.Text = "Tệp";
            this.exportMenuItem.Text = "Xuất danh sách đang hiển thị ra CSV...";
            this.exportMenuItem.Click += new System.EventHandler(this.ExportMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.countStatusLabel });
            this.statusStrip.Location = new System.Drawing.Point(0, 739);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1180, 22);
            this.statusStrip.TabIndex = 1;
            this.countStatusLabel.Text = "Số lịch hẹn: 0";
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.filterPanel, 0, 0);
            this.mainLayout.Controls.Add(this.inputLayout, 0, 1);
            this.mainLayout.Controls.Add(this.appointmentsDataGridView, 0, 2);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 24);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Padding = new System.Windows.Forms.Padding(10);
            this.mainLayout.RowCount = 3;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 270F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Size = new System.Drawing.Size(1180, 715);
            this.mainLayout.TabIndex = 2;
            // 
            // filterPanel
            // 
            this.filterPanel.Controls.Add(this.searchLabel);
            this.filterPanel.Controls.Add(this.searchTextBox);
            this.filterPanel.Controls.Add(this.filterDateCheckBox);
            this.filterPanel.Controls.Add(this.filterDatePicker);
            this.filterPanel.Controls.Add(this.filterStatusLabel);
            this.filterPanel.Controls.Add(this.filterStatusComboBox);
            this.filterPanel.Controls.Add(this.clearFiltersButton);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterPanel.WrapContents = false;
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.TabIndex = 0;
            this.searchLabel.AutoSize = true;
            this.searchLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            this.searchLabel.Text = "Tìm tên / SĐT:";
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(3, 5, 14, 3);
            this.searchTextBox.Size = new System.Drawing.Size(220, 23);
            this.searchTextBox.TextChanged += new System.EventHandler(this.FilterCriteriaChanged);
            this.filterDateCheckBox.AutoSize = true;
            this.filterDateCheckBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.filterDateCheckBox.Text = "Ngày hẹn:";
            this.filterDateCheckBox.CheckedChanged += new System.EventHandler(this.FilterCriteriaChanged);
            this.filterDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.filterDatePicker.Margin = new System.Windows.Forms.Padding(3, 5, 14, 3);
            this.filterDatePicker.Width = 120;
            this.filterDatePicker.Enabled = false;
            this.filterDatePicker.ValueChanged += new System.EventHandler(this.FilterCriteriaChanged);
            this.filterStatusLabel.AutoSize = true;
            this.filterStatusLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            this.filterStatusLabel.Text = "Trạng thái:";
            this.filterStatusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterStatusComboBox.Items.AddRange(new object[] { "Tất cả", "Chờ khám", "Đang khám", "Hoàn tất", "Đã hủy" });
            this.filterStatusComboBox.Margin = new System.Windows.Forms.Padding(3, 5, 14, 3);
            this.filterStatusComboBox.Width = 130;
            this.filterStatusComboBox.SelectedIndex = 0;
            this.filterStatusComboBox.SelectedIndexChanged += new System.EventHandler(this.FilterCriteriaChanged);
            this.clearFiltersButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.clearFiltersButton.Text = "Xóa bộ lọc";
            this.clearFiltersButton.AutoSize = true;
            this.clearFiltersButton.Click += new System.EventHandler(this.ClearFiltersButton_Click);
            // 
            // inputLayout
            // 
            this.inputLayout.ColumnCount = 4;
            this.inputLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.inputLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.inputLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.inputLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.inputLayout.RowCount = 5;
            this.inputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.inputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.inputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.inputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.inputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.inputLayout.Controls.Add(this.appointmentIdLabel, 0, 0);
            this.inputLayout.Controls.Add(this.appointmentIdTextBox, 1, 0);
            this.inputLayout.Controls.Add(this.patientNameLabel, 2, 0);
            this.inputLayout.Controls.Add(this.patientNameTextBox, 3, 0);
            this.inputLayout.Controls.Add(this.phoneLabel, 0, 1);
            this.inputLayout.Controls.Add(this.phoneTextBox, 1, 1);
            this.inputLayout.Controls.Add(this.appointmentDateLabel, 2, 1);
            this.inputLayout.Controls.Add(this.appointmentDatePicker, 3, 1);
            this.inputLayout.Controls.Add(this.doctorLabel, 0, 2);
            this.inputLayout.Controls.Add(this.doctorComboBox, 1, 2);
            this.inputLayout.Controls.Add(this.appointmentStatusLabel, 2, 2);
            this.inputLayout.Controls.Add(this.appointmentStatusComboBox, 3, 2);
            this.inputLayout.Controls.Add(this.reasonLabel, 0, 3);
            this.inputLayout.Controls.Add(this.reasonTextBox, 1, 3);
            this.inputLayout.SetColumnSpan(this.reasonTextBox, 3);
            this.inputLayout.Controls.Add(this.actionPanel, 0, 4);
            this.inputLayout.SetColumnSpan(this.actionPanel, 4);
            this.inputLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputLayout.Padding = new System.Windows.Forms.Padding(4);
            this.inputLayout.Name = "inputLayout";
            this.inputLayout.TabIndex = 1;
            this.appointmentIdLabel.Text = "Mã lịch hẹn *";
            this.appointmentIdLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appointmentIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.patientNameLabel.Text = "Họ tên bệnh nhân *";
            this.patientNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.phoneLabel.Text = "Số điện thoại *";
            this.phoneLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.phoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.appointmentDateLabel.Text = "Ngày giờ hẹn *";
            this.appointmentDateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appointmentDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.doctorLabel.Text = "Bác sĩ";
            this.doctorLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doctorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.appointmentStatusLabel.Text = "Trạng thái";
            this.appointmentStatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appointmentStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.reasonLabel.Text = "Lý do khám *";
            this.reasonLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reasonLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.appointmentIdTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appointmentIdTextBox.Margin = new System.Windows.Forms.Padding(5, 8, 12, 5);
            this.patientNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientNameTextBox.Margin = new System.Windows.Forms.Padding(5, 8, 12, 5);
            this.phoneTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.phoneTextBox.Margin = new System.Windows.Forms.Padding(5, 8, 12, 5);
            this.appointmentDatePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appointmentDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.appointmentDatePicker.CustomFormat = "dd/MM/yyyy HH:mm";
            this.appointmentDatePicker.ShowUpDown = true;
            this.appointmentDatePicker.Margin = new System.Windows.Forms.Padding(5, 8, 12, 5);
            this.doctorComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doctorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.doctorComboBox.Items.AddRange(new object[] { "Bác sĩ An", "Bác sĩ Bình", "Bác sĩ Chi", "Bác sĩ Dũng" });
            this.doctorComboBox.Margin = new System.Windows.Forms.Padding(5, 8, 12, 5);
            this.appointmentStatusComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appointmentStatusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.appointmentStatusComboBox.Items.AddRange(new object[] { "Chờ khám", "Đang khám", "Hoàn tất", "Đã hủy" });
            this.appointmentStatusComboBox.Margin = new System.Windows.Forms.Padding(5, 8, 12, 5);
            this.reasonTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reasonTextBox.Multiline = true;
            this.reasonTextBox.Margin = new System.Windows.Forms.Padding(5, 5, 12, 5);
            this.actionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.actionPanel.WrapContents = false;
            this.addButton.Text = "Thêm lịch hẹn";
            this.addButton.AutoSize = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            this.updateButton.Text = "Cập nhật";
            this.updateButton.AutoSize = true;
            this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            this.deleteButton.Text = "Xóa";
            this.deleteButton.AutoSize = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            this.clearButton.Text = "Làm mới";
            this.clearButton.AutoSize = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            this.actionPanel.Controls.Add(this.clearButton);
            this.actionPanel.Controls.Add(this.deleteButton);
            this.actionPanel.Controls.Add(this.updateButton);
            this.actionPanel.Controls.Add(this.addButton);
            // 
            // appointmentsDataGridView
            // 
            this.appointmentsDataGridView.AllowUserToAddRows = false;
            this.appointmentsDataGridView.AllowUserToDeleteRows = false;
            this.appointmentsDataGridView.AutoGenerateColumns = false;
            this.appointmentsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.appointmentIdColumn, this.patientNameColumn, this.phoneColumn,
                this.appointmentDateColumn, this.doctorColumn, this.reasonColumn, this.appointmentStatusColumn });
            this.appointmentsDataGridView.DataSource = this.appointmentsBindingSource;
            this.appointmentsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appointmentsDataGridView.ReadOnly = true;
            this.appointmentsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.appointmentsDataGridView.MultiSelect = false;
            this.appointmentsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.appointmentsDataGridView.RowHeadersVisible = false;
            this.appointmentsDataGridView.Name = "appointmentsDataGridView";
            this.appointmentsDataGridView.TabIndex = 2;
            this.appointmentsDataGridView.SelectionChanged += new System.EventHandler(this.AppointmentsDataGridView_SelectionChanged);
            this.appointmentIdColumn.DataPropertyName = "AppointmentId";
            this.appointmentIdColumn.HeaderText = "Mã lịch hẹn";
            this.appointmentIdColumn.MinimumWidth = 85;
            this.patientNameColumn.DataPropertyName = "PatientName";
            this.patientNameColumn.HeaderText = "Bệnh nhân";
            this.patientNameColumn.MinimumWidth = 130;
            this.phoneColumn.DataPropertyName = "Phone";
            this.phoneColumn.HeaderText = "Số điện thoại";
            this.phoneColumn.MinimumWidth = 105;
            this.appointmentDateColumn.DataPropertyName = "AppointmentDate";
            this.appointmentDateColumn.HeaderText = "Ngày giờ hẹn";
            this.appointmentDateColumn.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            this.appointmentDateColumn.MinimumWidth = 130;
            this.doctorColumn.DataPropertyName = "Doctor";
            this.doctorColumn.HeaderText = "Bác sĩ";
            this.doctorColumn.MinimumWidth = 90;
            this.reasonColumn.DataPropertyName = "Reason";
            this.reasonColumn.HeaderText = "Lý do khám";
            this.reasonColumn.MinimumWidth = 120;
            this.appointmentStatusColumn.DataPropertyName = "Status";
            this.appointmentStatusColumn.HeaderText = "Trạng thái";
            this.appointmentStatusColumn.MinimumWidth = 90;
            this.validationErrorProvider.ContainerControl = this;
            this.exportSaveFileDialog.DefaultExt = "csv";
            this.exportSaveFileDialog.Filter = "CSV (*.csv)|*.csv";
            this.exportSaveFileDialog.Title = "Xuất danh sách lịch hẹn";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 761);
            this.Controls.Add(this.mainLayout);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(980, 650);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clinic Appointment Manager - Quản lý lịch hẹn phòng khám";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.appointmentsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.validationErrorProvider)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mainLayout.ResumeLayout(false);
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.inputLayout.ResumeLayout(false);
            this.inputLayout.PerformLayout();
            this.actionPanel.ResumeLayout(false);
            this.actionPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

