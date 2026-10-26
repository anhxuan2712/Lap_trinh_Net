namespace bai4._11
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.panelImageInfo = new System.Windows.Forms.Panel();
            this.labelImage = new System.Windows.Forms.Label();
            this.pictureBoxDevice = new System.Windows.Forms.PictureBox();
            this.buttonSelectImage = new System.Windows.Forms.Button();
            this.panelFormInfo = new System.Windows.Forms.Panel();
            this.labelOrderID = new System.Windows.Forms.Label();
            this.textBoxOrderID = new System.Windows.Forms.TextBox();
            this.labelCustomerName = new System.Windows.Forms.Label();
            this.textBoxCustomerName = new System.Windows.Forms.TextBox();
            this.labelPhone = new System.Windows.Forms.Label();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.labelDeviceType = new System.Windows.Forms.Label();
            this.comboBoxDeviceType = new System.Windows.Forms.ComboBox();
            this.labelErrorDescription = new System.Windows.Forms.Label();
            this.textBoxErrorDescription = new System.Windows.Forms.TextBox();
            this.labelReceivedDate = new System.Windows.Forms.Label();
            this.dateTimePickerReceivedDate = new System.Windows.Forms.DateTimePicker();
            this.labelEstimatedCost = new System.Windows.Forms.Label();
            this.textBoxEstimatedCost = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.panelGridAndSearch = new System.Windows.Forms.Panel();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.labelSearch = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelFilter = new System.Windows.Forms.Label();
            this.comboBoxFilterStatus = new System.Windows.Forms.ComboBox();
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.panelImageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDevice)).BeginInit();
            this.panelFormInfo.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelGridAndSearch.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportCSVToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exportCSVToolStripMenuItem
            // 
            this.exportCSVToolStripMenuItem.Name = "exportCSVToolStripMenuItem";
            this.exportCSVToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.exportCSVToolStripMenuItem.Text = "Export to CSV";
            this.exportCSVToolStripMenuItem.Click += new System.EventHandler(this.ExportCSVToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelCount,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabelTotal});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(83, 17);
            this.toolStripStatusLabel1.Text = "Order Count:";
            // 
            // toolStripStatusLabelCount
            // 
            this.toolStripStatusLabelCount.Name = "toolStripStatusLabelCount";
            this.toolStripStatusLabelCount.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabel1.Text = "0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(125, 17);
            this.toolStripStatusLabel2.Text = "Total Incomplete Cost:";
            // 
            // toolStripStatusLabelTotal
            // 
            this.toolStripStatusLabelTotal.Name = "toolStripStatusLabelTotal";
            this.toolStripStatusLabelTotal.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabelTotal.Text = "0";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.splitContainerMain);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 24);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1200, 615);
            this.panelMain.TabIndex = 2;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainerMain.Panel1.Controls.Add(this.panelInfo);
            this.splitContainerMain.Panel2.Controls.Add(this.panelGridAndSearch);
            this.splitContainerMain.Size = new System.Drawing.Size(1200, 615);
            this.splitContainerMain.SplitterDistance = 250;
            this.splitContainerMain.TabIndex = 0;
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelInfo.Controls.Add(this.panelImageInfo);
            this.panelInfo.Controls.Add(this.panelFormInfo);
            this.panelInfo.Controls.Add(this.panelButtons);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfo.Location = new System.Drawing.Point(0, 0);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(1200, 250);
            this.panelInfo.TabIndex = 0;
            // 
            // panelImageInfo
            // 
            this.panelImageInfo.BackColor = System.Drawing.Color.White;
            this.panelImageInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelImageInfo.Controls.Add(this.labelImage);
            this.panelImageInfo.Controls.Add(this.pictureBoxDevice);
            this.panelImageInfo.Controls.Add(this.buttonSelectImage);
            this.panelImageInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelImageInfo.Location = new System.Drawing.Point(950, 0);
            this.panelImageInfo.Name = "panelImageInfo";
            this.panelImageInfo.Padding = new System.Windows.Forms.Padding(10);
            this.panelImageInfo.Size = new System.Drawing.Size(250, 190);
            this.panelImageInfo.TabIndex = 2;
            // 
            // labelImage
            // 
            this.labelImage.AutoSize = true;
            this.labelImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImage.Location = new System.Drawing.Point(10, 10);
            this.labelImage.Name = "labelImage";
            this.labelImage.Size = new System.Drawing.Size(108, 13);
            this.labelImage.TabIndex = 0;
            this.labelImage.Text = "Device Image:";
            // 
            // pictureBoxDevice
            // 
            this.pictureBoxDevice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxDevice.Location = new System.Drawing.Point(10, 30);
            this.pictureBoxDevice.Name = "pictureBoxDevice";
            this.pictureBoxDevice.Size = new System.Drawing.Size(230, 140);
            this.pictureBoxDevice.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDevice.TabIndex = 1;
            this.pictureBoxDevice.TabStop = false;
            // 
            // buttonSelectImage
            // 
            this.buttonSelectImage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonSelectImage.Location = new System.Drawing.Point(10, 170);
            this.buttonSelectImage.Name = "buttonSelectImage";
            this.buttonSelectImage.Size = new System.Drawing.Size(228, 22);
            this.buttonSelectImage.TabIndex = 2;
            this.buttonSelectImage.Text = "Select Image";
            this.buttonSelectImage.UseVisualStyleBackColor = true;
            this.buttonSelectImage.Click += new System.EventHandler(this.ButtonSelectImage_Click);
            // 
            // panelFormInfo
            // 
            this.panelFormInfo.AutoScroll = true;
            this.panelFormInfo.BackColor = System.Drawing.Color.White;
            this.panelFormInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFormInfo.Controls.Add(this.labelOrderID);
            this.panelFormInfo.Controls.Add(this.textBoxOrderID);
            this.panelFormInfo.Controls.Add(this.labelCustomerName);
            this.panelFormInfo.Controls.Add(this.textBoxCustomerName);
            this.panelFormInfo.Controls.Add(this.labelPhone);
            this.panelFormInfo.Controls.Add(this.textBoxPhone);
            this.panelFormInfo.Controls.Add(this.labelDeviceType);
            this.panelFormInfo.Controls.Add(this.comboBoxDeviceType);
            this.panelFormInfo.Controls.Add(this.labelErrorDescription);
            this.panelFormInfo.Controls.Add(this.textBoxErrorDescription);
            this.panelFormInfo.Controls.Add(this.labelReceivedDate);
            this.panelFormInfo.Controls.Add(this.dateTimePickerReceivedDate);
            this.panelFormInfo.Controls.Add(this.labelEstimatedCost);
            this.panelFormInfo.Controls.Add(this.textBoxEstimatedCost);
            this.panelFormInfo.Controls.Add(this.labelStatus);
            this.panelFormInfo.Controls.Add(this.comboBoxStatus);
            this.panelFormInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFormInfo.Location = new System.Drawing.Point(0, 0);
            this.panelFormInfo.Name = "panelFormInfo";
            this.panelFormInfo.Padding = new System.Windows.Forms.Padding(10);
            this.panelFormInfo.Size = new System.Drawing.Size(600, 190);
            this.panelFormInfo.TabIndex = 1;
            // 
            // labelOrderID
            // 
            this.labelOrderID.AutoSize = true;
            this.labelOrderID.Location = new System.Drawing.Point(10, 12);
            this.labelOrderID.Name = "labelOrderID";
            this.labelOrderID.Size = new System.Drawing.Size(52, 13);
            this.labelOrderID.TabIndex = 0;
            this.labelOrderID.Text = "Order ID:";
            // 
            // textBoxOrderID
            // 
            this.textBoxOrderID.Location = new System.Drawing.Point(10, 28);
            this.textBoxOrderID.Name = "textBoxOrderID";
            this.textBoxOrderID.Size = new System.Drawing.Size(200, 20);
            this.textBoxOrderID.TabIndex = 1;
            // 
            // labelCustomerName
            // 
            this.labelCustomerName.AutoSize = true;
            this.labelCustomerName.Location = new System.Drawing.Point(220, 12);
            this.labelCustomerName.Name = "labelCustomerName";
            this.labelCustomerName.Size = new System.Drawing.Size(87, 13);
            this.labelCustomerName.TabIndex = 2;
            this.labelCustomerName.Text = "Customer Name:";
            // 
            // textBoxCustomerName
            // 
            this.textBoxCustomerName.Location = new System.Drawing.Point(220, 28);
            this.textBoxCustomerName.Name = "textBoxCustomerName";
            this.textBoxCustomerName.Size = new System.Drawing.Size(200, 20);
            this.textBoxCustomerName.TabIndex = 3;
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.Location = new System.Drawing.Point(430, 12);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(44, 13);
            this.labelPhone.TabIndex = 4;
            this.labelPhone.Text = "Phone:";
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.Location = new System.Drawing.Point(430, 28);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(150, 20);
            this.textBoxPhone.TabIndex = 5;
            // 
            // labelDeviceType
            // 
            this.labelDeviceType.AutoSize = true;
            this.labelDeviceType.Location = new System.Drawing.Point(10, 54);
            this.labelDeviceType.Name = "labelDeviceType";
            this.labelDeviceType.Size = new System.Drawing.Size(71, 13);
            this.labelDeviceType.TabIndex = 6;
            this.labelDeviceType.Text = "Device Type:";
            // 
            // comboBoxDeviceType
            // 
            this.comboBoxDeviceType.FormattingEnabled = true;
            this.comboBoxDeviceType.Items.AddRange(new object[] {
            "Laptop",
            "Điện thoại",
            "Máy tính bảng",
            "Máy in",
            "Router",
            "Khác"});
            this.comboBoxDeviceType.Location = new System.Drawing.Point(10, 70);
            this.comboBoxDeviceType.Name = "comboBoxDeviceType";
            this.comboBoxDeviceType.Size = new System.Drawing.Size(200, 21);
            this.comboBoxDeviceType.TabIndex = 7;
            // 
            // labelErrorDescription
            // 
            this.labelErrorDescription.AutoSize = true;
            this.labelErrorDescription.Location = new System.Drawing.Point(220, 54);
            this.labelErrorDescription.Name = "labelErrorDescription";
            this.labelErrorDescription.Size = new System.Drawing.Size(106, 13);
            this.labelErrorDescription.TabIndex = 8;
            this.labelErrorDescription.Text = "Error Description:";
            // 
            // textBoxErrorDescription
            // 
            this.textBoxErrorDescription.Location = new System.Drawing.Point(220, 70);
            this.textBoxErrorDescription.Multiline = true;
            this.textBoxErrorDescription.Name = "textBoxErrorDescription";
            this.textBoxErrorDescription.Size = new System.Drawing.Size(360, 50);
            this.textBoxErrorDescription.TabIndex = 9;
            // 
            // labelReceivedDate
            // 
            this.labelReceivedDate.AutoSize = true;
            this.labelReceivedDate.Location = new System.Drawing.Point(10, 96);
            this.labelReceivedDate.Name = "labelReceivedDate";
            this.labelReceivedDate.Size = new System.Drawing.Size(78, 13);
            this.labelReceivedDate.TabIndex = 10;
            this.labelReceivedDate.Text = "Received Date:";
            // 
            // dateTimePickerReceivedDate
            // 
            this.dateTimePickerReceivedDate.Location = new System.Drawing.Point(10, 112);
            this.dateTimePickerReceivedDate.Name = "dateTimePickerReceivedDate";
            this.dateTimePickerReceivedDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerReceivedDate.TabIndex = 11;
            this.dateTimePickerReceivedDate.Value = new System.DateTime(2025, 1, 1, 0, 0, 0, 0);
            // 
            // labelEstimatedCost
            // 
            this.labelEstimatedCost.AutoSize = true;
            this.labelEstimatedCost.Location = new System.Drawing.Point(220, 130);
            this.labelEstimatedCost.Name = "labelEstimatedCost";
            this.labelEstimatedCost.Size = new System.Drawing.Size(88, 13);
            this.labelEstimatedCost.TabIndex = 12;
            this.labelEstimatedCost.Text = "Estimated Cost:";
            // 
            // textBoxEstimatedCost
            // 
            this.textBoxEstimatedCost.Location = new System.Drawing.Point(220, 146);
            this.textBoxEstimatedCost.Name = "textBoxEstimatedCost";
            this.textBoxEstimatedCost.Size = new System.Drawing.Size(100, 20);
            this.textBoxEstimatedCost.TabIndex = 13;
            this.textBoxEstimatedCost.Text = "0";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(330, 130);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(41, 13);
            this.labelStatus.TabIndex = 14;
            this.labelStatus.Text = "Status:";
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "Mới tiếp nhận",
            "Đang sửa",
            "Chờ linh kiện",
            "Hoàn tất",
            "Đã hủy"});
            this.comboBoxStatus.Location = new System.Drawing.Point(330, 146);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(120, 21);
            this.comboBoxStatus.TabIndex = 15;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelButtons.Controls.Add(this.buttonAdd);
            this.panelButtons.Controls.Add(this.buttonUpdate);
            this.panelButtons.Controls.Add(this.buttonDelete);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 190);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1200, 60);
            this.panelButtons.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(10, 10);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 40);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(95, 10);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 40);
            this.buttonUpdate.TabIndex = 1;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(180, 10);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 40);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // panelGridAndSearch
            // 
            this.panelGridAndSearch.Controls.Add(this.dataGridViewOrders);
            this.panelGridAndSearch.Controls.Add(this.panelSearch);
            this.panelGridAndSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridAndSearch.Location = new System.Drawing.Point(0, 0);
            this.panelGridAndSearch.Name = "panelGridAndSearch";
            this.panelGridAndSearch.Size = new System.Drawing.Size(1200, 361);
            this.panelGridAndSearch.TabIndex = 0;
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelSearch.Controls.Add(this.labelSearch);
            this.panelSearch.Controls.Add(this.textBoxSearch);
            this.panelSearch.Controls.Add(this.labelFilter);
            this.panelSearch.Controls.Add(this.comboBoxFilterStatus);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Padding = new System.Windows.Forms.Padding(10);
            this.panelSearch.Size = new System.Drawing.Size(1200, 60);
            this.panelSearch.TabIndex = 1;
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(10, 15);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(44, 13);
            this.labelSearch.TabIndex = 0;
            this.labelSearch.Text = "Search:";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(60, 12);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(300, 20);
            this.textBoxSearch.TabIndex = 1;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.TextBoxSearch_TextChanged);
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.Location = new System.Drawing.Point(410, 15);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(36, 13);
            this.labelFilter.TabIndex = 2;
            this.labelFilter.Text = "Filter:";
            // 
            // comboBoxFilterStatus
            // 
            this.comboBoxFilterStatus.FormattingEnabled = true;
            this.comboBoxFilterStatus.Items.AddRange(new object[] {
            "All",
            "Mới tiếp nhận",
            "Đang sửa",
            "Chờ linh kiện",
            "Hoàn tất",
            "Đã hủy"});
            this.comboBoxFilterStatus.Location = new System.Drawing.Point(452, 12);
            this.comboBoxFilterStatus.Name = "comboBoxFilterStatus";
            this.comboBoxFilterStatus.Size = new System.Drawing.Size(150, 21);
            this.comboBoxFilterStatus.TabIndex = 3;
            this.comboBoxFilterStatus.SelectedIndexChanged += new System.EventHandler(this.ComboBoxFilterStatus_SelectedIndexChanged);
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.AutoGenerateColumns = false;
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewOrders.Location = new System.Drawing.Point(0, 60);
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.ReadOnly = true;
            this.dataGridViewOrders.RowHeadersVisible = false;
            this.dataGridViewOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOrders.Size = new System.Drawing.Size(1200, 301);
            this.dataGridViewOrders.TabIndex = 0;
            this.dataGridViewOrders.SelectionChanged += new System.EventHandler(this.DataGridViewOrders_SelectionChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "CSV Files|*.csv|All Files|*.*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 661);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Repair Service Tracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.panelImageInfo.ResumeLayout(false);
            this.panelImageInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDevice)).EndInit();
            this.panelFormInfo.ResumeLayout(false);
            this.panelFormInfo.ResumeLayout(false);
            this.panelFormInfo.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelGridAndSearch.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTotal;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Panel panelFormInfo;
        private System.Windows.Forms.Label labelOrderID;
        private System.Windows.Forms.TextBox textBoxOrderID;
        private System.Windows.Forms.Label labelCustomerName;
        private System.Windows.Forms.TextBox textBoxCustomerName;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.Label labelDeviceType;
        private System.Windows.Forms.ComboBox comboBoxDeviceType;
        private System.Windows.Forms.Label labelErrorDescription;
        private System.Windows.Forms.TextBox textBoxErrorDescription;
        private System.Windows.Forms.Label labelReceivedDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerReceivedDate;
        private System.Windows.Forms.Label labelEstimatedCost;
        private System.Windows.Forms.TextBox textBoxEstimatedCost;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Panel panelGridAndSearch;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.ComboBox comboBoxFilterStatus;
        private System.Windows.Forms.DataGridView dataGridViewOrders;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panelImageInfo;
        private System.Windows.Forms.Label labelImage;
        private System.Windows.Forms.PictureBox pictureBoxDevice;
        private System.Windows.Forms.Button buttonSelectImage;
    }
}

