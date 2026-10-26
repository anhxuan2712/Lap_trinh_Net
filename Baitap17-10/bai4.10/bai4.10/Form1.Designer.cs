namespace bai4._10
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.GroupBox entryGroup;
        private System.Windows.Forms.TableLayoutPanel entryLayout;
        private System.Windows.Forms.Label loanIdLabel;
        private System.Windows.Forms.TextBox loanIdTextBox;
        private System.Windows.Forms.Label studentIdLabel;
        private System.Windows.Forms.TextBox studentIdTextBox;
        private System.Windows.Forms.Label studentNameLabel;
        private System.Windows.Forms.TextBox studentNameTextBox;
        private System.Windows.Forms.Label equipmentLabel;
        private System.Windows.Forms.ComboBox equipmentComboBox;
        private System.Windows.Forms.Label quantityLabel;
        private System.Windows.Forms.NumericUpDown quantityNumericUpDown;
        private System.Windows.Forms.Label borrowDateLabel;
        private System.Windows.Forms.DateTimePicker borrowDatePicker;
        private System.Windows.Forms.Label dueDateLabel;
        private System.Windows.Forms.DateTimePicker dueDatePicker;
        private System.Windows.Forms.Label actualReturnDateLabel;
        private System.Windows.Forms.DateTimePicker actualReturnDatePicker;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label currentStatusLabel;
        private System.Windows.Forms.FlowLayoutPanel actionPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button markReturnedButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.FlowLayoutPanel searchPanel;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label filterLabel;
        private System.Windows.Forms.ComboBox statusFilterComboBox;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.DataGridView loansDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn loanIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn studentIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn studentNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn equipmentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn borrowDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dueDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actualReturnDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel totalCountStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel overdueCountStatusLabel;
        private System.Windows.Forms.ErrorProvider errorProvider1;

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
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.entryGroup = new System.Windows.Forms.GroupBox();
            this.entryLayout = new System.Windows.Forms.TableLayoutPanel();
            this.loanIdLabel = new System.Windows.Forms.Label();
            this.loanIdTextBox = new System.Windows.Forms.TextBox();
            this.studentIdLabel = new System.Windows.Forms.Label();
            this.studentIdTextBox = new System.Windows.Forms.TextBox();
            this.studentNameLabel = new System.Windows.Forms.Label();
            this.studentNameTextBox = new System.Windows.Forms.TextBox();
            this.equipmentLabel = new System.Windows.Forms.Label();
            this.equipmentComboBox = new System.Windows.Forms.ComboBox();
            this.quantityLabel = new System.Windows.Forms.Label();
            this.quantityNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.borrowDateLabel = new System.Windows.Forms.Label();
            this.borrowDatePicker = new System.Windows.Forms.DateTimePicker();
            this.dueDateLabel = new System.Windows.Forms.Label();
            this.dueDatePicker = new System.Windows.Forms.DateTimePicker();
            this.actualReturnDateLabel = new System.Windows.Forms.Label();
            this.actualReturnDatePicker = new System.Windows.Forms.DateTimePicker();
            this.statusLabel = new System.Windows.Forms.Label();
            this.currentStatusLabel = new System.Windows.Forms.Label();
            this.actionPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.markReturnedButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.searchPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.searchLabel = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.filterLabel = new System.Windows.Forms.Label();
            this.statusFilterComboBox = new System.Windows.Forms.ComboBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.loansDataGridView = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.totalCountStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.overdueCountStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.mainLayout.SuspendLayout();
            this.entryGroup.SuspendLayout();
            this.entryLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantityNumericUpDown)).BeginInit();
            this.actionPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loansDataGridView)).BeginInit();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.entryGroup, 0, 0);
            this.mainLayout.Controls.Add(this.searchPanel, 0, 1);
            this.mainLayout.Controls.Add(this.loansDataGridView, 0, 2);
            this.mainLayout.Controls.Add(this.statusStrip, 0, 3);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Padding = new System.Windows.Forms.Padding(8);
            this.mainLayout.RowCount = 4;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.mainLayout.Size = new System.Drawing.Size(1180, 760);
            this.mainLayout.TabIndex = 0;
            // 
            // entryGroup
            // 
            this.entryGroup.Controls.Add(this.entryLayout);
            this.entryGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entryGroup.Location = new System.Drawing.Point(11, 11);
            this.entryGroup.Name = "entryGroup";
            this.entryGroup.Padding = new System.Windows.Forms.Padding(8);
            this.entryGroup.Size = new System.Drawing.Size(1158, 316);
            this.entryGroup.TabIndex = 0;
            this.entryGroup.TabStop = false;
            this.entryGroup.Text = "Thông tin phiếu mượn";
            // 
            // entryLayout
            // 
            this.entryLayout.ColumnCount = 4;
            this.entryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.entryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.entryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.entryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.entryLayout.Controls.Add(this.loanIdLabel, 0, 0);
            this.entryLayout.Controls.Add(this.loanIdTextBox, 1, 0);
            this.entryLayout.Controls.Add(this.studentIdLabel, 2, 0);
            this.entryLayout.Controls.Add(this.studentIdTextBox, 3, 0);
            this.entryLayout.Controls.Add(this.studentNameLabel, 0, 1);
            this.entryLayout.Controls.Add(this.studentNameTextBox, 1, 1);
            this.entryLayout.Controls.Add(this.equipmentLabel, 2, 1);
            this.entryLayout.Controls.Add(this.equipmentComboBox, 3, 1);
            this.entryLayout.Controls.Add(this.quantityLabel, 0, 2);
            this.entryLayout.Controls.Add(this.quantityNumericUpDown, 1, 2);
            this.entryLayout.Controls.Add(this.borrowDateLabel, 2, 2);
            this.entryLayout.Controls.Add(this.borrowDatePicker, 3, 2);
            this.entryLayout.Controls.Add(this.dueDateLabel, 0, 3);
            this.entryLayout.Controls.Add(this.dueDatePicker, 1, 3);
            this.entryLayout.Controls.Add(this.actualReturnDateLabel, 2, 3);
            this.entryLayout.Controls.Add(this.actualReturnDatePicker, 3, 3);
            this.entryLayout.Controls.Add(this.statusLabel, 0, 4);
            this.entryLayout.Controls.Add(this.currentStatusLabel, 1, 4);
            this.entryLayout.Controls.Add(this.actionPanel, 2, 4);
            this.entryLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entryLayout.Location = new System.Drawing.Point(8, 23);
            this.entryLayout.Name = "entryLayout";
            this.entryLayout.RowCount = 5;
            this.entryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.entryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.entryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.entryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.entryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.entryLayout.Size = new System.Drawing.Size(1142, 285);
            this.entryLayout.TabIndex = 0;
            // 
            // loanIdLabel
            // 
            this.loanIdLabel.Location = new System.Drawing.Point(3, 0);
            this.loanIdLabel.Name = "loanIdLabel";
            this.loanIdLabel.Size = new System.Drawing.Size(100, 23);
            this.loanIdLabel.TabIndex = 0;
            this.loanIdLabel.Text = "Mã phiếu:";
            // 
            // loanIdTextBox
            // 
            this.loanIdTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loanIdTextBox.Location = new System.Drawing.Point(151, 3);
            this.loanIdTextBox.Name = "loanIdTextBox";
            this.loanIdTextBox.Size = new System.Drawing.Size(416, 22);
            this.loanIdTextBox.TabIndex = 1;
            // 
            // studentIdLabel
            // 
            this.studentIdLabel.Location = new System.Drawing.Point(573, 0);
            this.studentIdLabel.Name = "studentIdLabel";
            this.studentIdLabel.Size = new System.Drawing.Size(100, 23);
            this.studentIdLabel.TabIndex = 2;
            this.studentIdLabel.Text = "Mã sinh viên:";
            // 
            // studentIdTextBox
            // 
            this.studentIdTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.studentIdTextBox.Location = new System.Drawing.Point(721, 3);
            this.studentIdTextBox.Name = "studentIdTextBox";
            this.studentIdTextBox.Size = new System.Drawing.Size(418, 22);
            this.studentIdTextBox.TabIndex = 3;
            // 
            // studentNameLabel
            // 
            this.studentNameLabel.Location = new System.Drawing.Point(3, 57);
            this.studentNameLabel.Name = "studentNameLabel";
            this.studentNameLabel.Size = new System.Drawing.Size(100, 23);
            this.studentNameLabel.TabIndex = 4;
            this.studentNameLabel.Text = "Họ tên:";
            // 
            // studentNameTextBox
            // 
            this.studentNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.studentNameTextBox.Location = new System.Drawing.Point(151, 60);
            this.studentNameTextBox.Name = "studentNameTextBox";
            this.studentNameTextBox.Size = new System.Drawing.Size(416, 22);
            this.studentNameTextBox.TabIndex = 5;
            // 
            // equipmentLabel
            // 
            this.equipmentLabel.Location = new System.Drawing.Point(573, 57);
            this.equipmentLabel.Name = "equipmentLabel";
            this.equipmentLabel.Size = new System.Drawing.Size(100, 23);
            this.equipmentLabel.TabIndex = 6;
            this.equipmentLabel.Text = "Thiết bị:";
            // 
            // equipmentComboBox
            // 
            this.equipmentComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.equipmentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.equipmentComboBox.Location = new System.Drawing.Point(721, 60);
            this.equipmentComboBox.Name = "equipmentComboBox";
            this.equipmentComboBox.Size = new System.Drawing.Size(418, 24);
            this.equipmentComboBox.TabIndex = 7;
            // 
            // quantityLabel
            // 
            this.quantityLabel.Location = new System.Drawing.Point(3, 114);
            this.quantityLabel.Name = "quantityLabel";
            this.quantityLabel.Size = new System.Drawing.Size(100, 23);
            this.quantityLabel.TabIndex = 8;
            this.quantityLabel.Text = "Số lượng:";
            // 
            // quantityNumericUpDown
            // 
            this.quantityNumericUpDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.quantityNumericUpDown.Location = new System.Drawing.Point(151, 117);
            this.quantityNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.quantityNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.quantityNumericUpDown.Name = "quantityNumericUpDown";
            this.quantityNumericUpDown.Size = new System.Drawing.Size(120, 22);
            this.quantityNumericUpDown.TabIndex = 9;
            this.quantityNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // borrowDateLabel
            // 
            this.borrowDateLabel.Location = new System.Drawing.Point(573, 114);
            this.borrowDateLabel.Name = "borrowDateLabel";
            this.borrowDateLabel.Size = new System.Drawing.Size(100, 23);
            this.borrowDateLabel.TabIndex = 10;
            this.borrowDateLabel.Text = "Ngày mượn:";
            // 
            // borrowDatePicker
            // 
            this.borrowDatePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borrowDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.borrowDatePicker.Location = new System.Drawing.Point(721, 117);
            this.borrowDatePicker.Name = "borrowDatePicker";
            this.borrowDatePicker.Size = new System.Drawing.Size(418, 22);
            this.borrowDatePicker.TabIndex = 11;
            // 
            // dueDateLabel
            // 
            this.dueDateLabel.Location = new System.Drawing.Point(3, 171);
            this.dueDateLabel.Name = "dueDateLabel";
            this.dueDateLabel.Size = new System.Drawing.Size(100, 23);
            this.dueDateLabel.TabIndex = 12;
            this.dueDateLabel.Text = "Hạn trả:";
            // 
            // dueDatePicker
            // 
            this.dueDatePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dueDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dueDatePicker.Location = new System.Drawing.Point(151, 174);
            this.dueDatePicker.Name = "dueDatePicker";
            this.dueDatePicker.Size = new System.Drawing.Size(416, 22);
            this.dueDatePicker.TabIndex = 13;
            // 
            // actualReturnDateLabel
            // 
            this.actualReturnDateLabel.Location = new System.Drawing.Point(573, 171);
            this.actualReturnDateLabel.Name = "actualReturnDateLabel";
            this.actualReturnDateLabel.Size = new System.Drawing.Size(100, 23);
            this.actualReturnDateLabel.TabIndex = 14;
            this.actualReturnDateLabel.Text = "Ngày trả thực tế:";
            // 
            // actualReturnDatePicker
            // 
            this.actualReturnDatePicker.Checked = false;
            this.actualReturnDatePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actualReturnDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.actualReturnDatePicker.Location = new System.Drawing.Point(721, 174);
            this.actualReturnDatePicker.Name = "actualReturnDatePicker";
            this.actualReturnDatePicker.ShowCheckBox = true;
            this.actualReturnDatePicker.Size = new System.Drawing.Size(418, 22);
            this.actualReturnDatePicker.TabIndex = 15;
            // 
            // statusLabel
            // 
            this.statusLabel.Location = new System.Drawing.Point(3, 228);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(100, 23);
            this.statusLabel.TabIndex = 16;
            this.statusLabel.Text = "Tình trạng:";
            // 
            // currentStatusLabel
            // 
            this.currentStatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentStatusLabel.Location = new System.Drawing.Point(151, 228);
            this.currentStatusLabel.Name = "currentStatusLabel";
            this.currentStatusLabel.Size = new System.Drawing.Size(416, 57);
            this.currentStatusLabel.TabIndex = 9;
            this.currentStatusLabel.Text = "Chưa chọn phiếu";
            this.currentStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // actionPanel
            // 
            this.entryLayout.SetColumnSpan(this.actionPanel, 2);
            this.actionPanel.Controls.Add(this.addButton);
            this.actionPanel.Controls.Add(this.updateButton);
            this.actionPanel.Controls.Add(this.deleteButton);
            this.actionPanel.Controls.Add(this.markReturnedButton);
            this.actionPanel.Controls.Add(this.clearButton);
            this.actionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionPanel.Location = new System.Drawing.Point(573, 231);
            this.actionPanel.Name = "actionPanel";
            this.actionPanel.Size = new System.Drawing.Size(566, 51);
            this.actionPanel.TabIndex = 0;
            this.actionPanel.WrapContents = false;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(3, 3);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(76, 30);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "Thêm";
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(85, 3);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(88, 30);
            this.updateButton.TabIndex = 1;
            this.updateButton.Text = "Cập nhật";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(179, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(68, 30);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Xóa";
            // 
            // markReturnedButton
            // 
            this.markReturnedButton.Location = new System.Drawing.Point(253, 3);
            this.markReturnedButton.Name = "markReturnedButton";
            this.markReturnedButton.Size = new System.Drawing.Size(130, 30);
            this.markReturnedButton.TabIndex = 3;
            this.markReturnedButton.Text = "Đánh dấu đã trả";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(389, 3);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(78, 30);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "Làm mới";
            // 
            // searchPanel
            // 
            this.searchPanel.Controls.Add(this.searchLabel);
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Controls.Add(this.filterLabel);
            this.searchPanel.Controls.Add(this.statusFilterComboBox);
            this.searchPanel.Controls.Add(this.exportButton);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchPanel.Location = new System.Drawing.Point(11, 333);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Padding = new System.Windows.Forms.Padding(4, 8, 4, 4);
            this.searchPanel.Size = new System.Drawing.Size(1158, 42);
            this.searchPanel.TabIndex = 1;
            this.searchPanel.WrapContents = false;
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(7, 15);
            this.searchLabel.Margin = new System.Windows.Forms.Padding(3, 7, 4, 0);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(124, 16);
            this.searchLabel.TabIndex = 0;
            this.searchLabel.Text = "Tìm mã SV / thiết bị:";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(138, 11);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(250, 22);
            this.searchTextBox.TabIndex = 0;
            // 
            // filterLabel
            // 
            this.filterLabel.AutoSize = true;
            this.filterLabel.Location = new System.Drawing.Point(409, 15);
            this.filterLabel.Margin = new System.Windows.Forms.Padding(18, 7, 4, 0);
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.Size = new System.Drawing.Size(89, 16);
            this.filterLabel.TabIndex = 1;
            this.filterLabel.Text = "Lọc trạng thái:";
            // 
            // statusFilterComboBox
            // 
            this.statusFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusFilterComboBox.Location = new System.Drawing.Point(505, 11);
            this.statusFilterComboBox.Name = "statusFilterComboBox";
            this.statusFilterComboBox.Size = new System.Drawing.Size(160, 24);
            this.statusFilterComboBox.TabIndex = 1;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(686, 8);
            this.exportButton.Margin = new System.Windows.Forms.Padding(18, 0, 3, 0);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(100, 28);
            this.exportButton.TabIndex = 2;
            this.exportButton.Text = "Xuất CSV";
            // 
            // loansDataGridView
            // 
            this.loansDataGridView.AllowUserToAddRows = false;
            this.loansDataGridView.AllowUserToDeleteRows = false;
            this.loansDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.loansDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.loansDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loansDataGridView.Location = new System.Drawing.Point(11, 381);
            this.loansDataGridView.MultiSelect = false;
            this.loansDataGridView.Name = "loansDataGridView";
            this.loansDataGridView.ReadOnly = true;
            this.loansDataGridView.RowHeadersVisible = false;
            this.loansDataGridView.RowHeadersWidth = 51;
            this.loansDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.loansDataGridView.Size = new System.Drawing.Size(1158, 343);
            this.loansDataGridView.TabIndex = 2;
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.totalCountStatusLabel,
            this.overdueCountStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(8, 727);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1164, 25);
            this.statusStrip.TabIndex = 3;
            // 
            // totalCountStatusLabel
            // 
            this.totalCountStatusLabel.Name = "totalCountStatusLabel";
            this.totalCountStatusLabel.Size = new System.Drawing.Size(118, 19);
            this.totalCountStatusLabel.Text = "Tổng số phiếu: 0";
            // 
            // overdueCountStatusLabel
            // 
            this.overdueCountStatusLabel.Margin = new System.Windows.Forms.Padding(24, 3, 0, 2);
            this.overdueCountStatusLabel.Name = "overdueCountStatusLabel";
            this.overdueCountStatusLabel.Size = new System.Drawing.Size(139, 20);
            this.overdueCountStatusLabel.Text = "Số phiếu quá hạn: 0";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.mainLayout);
            this.MinimumSize = new System.Drawing.Size(1000, 680);
            this.Name = "Form1";
            this.Text = "Lab Equipment Checkout";
            this.mainLayout.ResumeLayout(false);
            this.mainLayout.PerformLayout();
            this.entryGroup.ResumeLayout(false);
            this.entryLayout.ResumeLayout(false);
            this.entryLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantityNumericUpDown)).EndInit();
            this.actionPanel.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loansDataGridView)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}

