using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsTechMart
{
    public class MainForm : Form
    {
        private TableLayoutPanel table;
        private TextBox txtProductId, txtProductName, txtUnitPrice, txtQuantity, txtSearch;
        private ComboBox cboCategory;
        private PictureBox picAvatar;
        private Button btnChooseImage, btnAdd, btnUpdate, btnDelete, btnExport;
        private DataGridView dgvProducts;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        private BindingList<Product> products = new BindingList<Product>();
        private BindingSource bs = new BindingSource();

        public MainForm()
        {
            InitializeComponent();
            bs.DataSource = products;
            dgvProducts.DataSource = bs;
            UpdateStatus();
        }

        private void InitializeComponent()
        {
            Text = "TechMart Product Manager";
            Width = 1000;
            Height = 600;

            table = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            Controls.Add(table);

            var left = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            var right = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            table.Controls.Add(left, 0, 0);
            table.Controls.Add(right, 1, 0);

            // Left controls
            var leftLayout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 10 };
            leftLayout.RowStyles.Clear();
            for (int i = 0; i < 10; i++) leftLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            left.Controls.Add(leftLayout);

            leftLayout.Controls.Add(new Label { Text = "Product ID" });
            txtProductId = new TextBox(); leftLayout.Controls.Add(txtProductId);
            leftLayout.Controls.Add(new Label { Text = "Product Name" });
            txtProductName = new TextBox(); leftLayout.Controls.Add(txtProductName);
            leftLayout.Controls.Add(new Label { Text = "Category" });
            cboCategory = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList }; cboCategory.Items.AddRange(new[] { "Điện thoại", "Laptop", "Phụ kiện" }); leftLayout.Controls.Add(cboCategory);
            leftLayout.Controls.Add(new Label { Text = "Unit Price" });
            txtUnitPrice = new TextBox(); leftLayout.Controls.Add(txtUnitPrice);
            leftLayout.Controls.Add(new Label { Text = "Quantity" });
            txtQuantity = new TextBox(); leftLayout.Controls.Add(txtQuantity);
            leftLayout.Controls.Add(new Label { Text = "Avatar" });
            var imgPanel = new FlowLayoutPanel { AutoSize = true };
            picAvatar = new PictureBox { Size = new Size(120, 90), SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle }; imgPanel.Controls.Add(picAvatar);
            btnChooseImage = new Button { Text = "Choose Image" }; btnChooseImage.Click += BtnChooseImage_Click; imgPanel.Controls.Add(btnChooseImage);
            leftLayout.Controls.Add(imgPanel);

            var btnPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
            btnAdd = new Button { Text = "Add" }; btnAdd.Click += BtnAdd_Click; btnPanel.Controls.Add(btnAdd);
            btnUpdate = new Button { Text = "Update" }; btnUpdate.Click += BtnUpdate_Click; btnPanel.Controls.Add(btnUpdate);
            btnDelete = new Button { Text = "Delete" }; btnDelete.Click += BtnDelete_Click; btnPanel.Controls.Add(btnDelete);
            leftLayout.Controls.Add(btnPanel);

            // Search box
            leftLayout.Controls.Add(new Label { Text = "Search" });
            txtSearch = new TextBox(); txtSearch.TextChanged += TxtSearch_TextChanged; leftLayout.Controls.Add(txtSearch);

            // Right: DataGridView
            dgvProducts = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã SP", DataPropertyName = "ProductId" });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên SP", DataPropertyName = "ProductName" });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Danh Mục", DataPropertyName = "Category" });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Đơn Giá", DataPropertyName = "UnitPrice", DefaultCellStyle = { Format = "N0" } });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Số Lượng", DataPropertyName = "Quantity" });
            dgvProducts.CellClick += DgvProducts_CellClick;
            right.Controls.Add(dgvProducts);

            // Bottom status and export
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel(); statusStrip.Items.Add(statusLabel);
            btnExport = new Button { Text = "Export CSV" }; btnExport.Click += BtnExport_Click; statusStrip.Items.Add(new ToolStripControlHost(btnExport));
            Controls.Add(statusStrip);
        }

        private void UpdateStatus() => statusLabel.Text = $"Tổng số sản phẩm: {products.Count}";

        private void BtnChooseImage_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp" };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            picAvatar.Image = Image.FromFile(dlg.FileName);
        }

        private bool ValidateInputs(out decimal price, out int qty)
        {
            price = 0; qty = 0;
            if (string.IsNullOrWhiteSpace(txtProductName.Text)) { MessageBox.Show("Tên SP không được để trống"); return false; }
            if (!decimal.TryParse(txtUnitPrice.Text, out price) || price <= 0) { MessageBox.Show("Đơn giá phải > 0"); return false; }
            if (!int.TryParse(txtQuantity.Text, out qty) || qty < 0) { MessageBox.Show("Số lượng >= 0"); return false; }
            return true;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out var price, out var qty)) return;
            var imgBytes = picAvatar.Image != null ? ImageToBytes(picAvatar.Image) : null;
            var p = new Product(txtProductId.Text.Trim(), txtProductName.Text.Trim(), cboCategory.Text, price, qty, imgBytes);
            products.Add(p);
            UpdateStatus();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (bs.Current is Product current)
            {
                if (!ValidateInputs(out var price, out var qty)) return;
                current.ProductId = txtProductId.Text.Trim();
                current.ProductName = txtProductName.Text.Trim();
                current.Category = cboCategory.Text;
                current.UnitPrice = price;
                current.Quantity = qty;
                current.ImageData = picAvatar.Image != null ? ImageToBytes(picAvatar.Image) : null;
                // notify bindings
                bs.ResetCurrentItem();
                UpdateStatus();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (bs.Current is Product current)
            {
                var r = MessageBox.Show("Xác nhận xóa?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    products.Remove(current);
                    UpdateStatus();
                }
            }
        }

        private void DgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            bs.Position = e.RowIndex;
            if (bs.Current is Product p)
            {
                txtProductId.Text = p.ProductId;
                txtProductName.Text = p.ProductName;
                cboCategory.Text = p.Category;
                txtUnitPrice.Text = p.UnitPrice.ToString();
                txtQuantity.Text = p.Quantity.ToString();
                picAvatar.Image = p.ImageData != null ? BytesToImage(p.ImageData) : null;
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string q = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(q)) bs.DataSource = products;
            else bs.DataSource = new BindingList<Product>(products.Where(p => p.ProductName.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0).ToList());
            dgvProducts.DataSource = bs;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog { Filter = "CSV|*.csv" };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            using var sw = new StreamWriter(sfd.FileName);
            sw.WriteLine("ProductId,ProductName,Category,UnitPrice,Quantity");
            foreach (var p in products)
            {
                sw.WriteLine($"{EscapeCsv(p.ProductId)},{EscapeCsv(p.ProductName)},{EscapeCsv(p.Category)},{p.UnitPrice:N0},{p.Quantity}");
            }
            MessageBox.Show("Export completed.");
        }

        private static string EscapeCsv(string s)
        {
            if (s == null) return string.Empty;
            if (s.Contains(',') || s.Contains('"')) return '"' + s.Replace("\"", "\"\"") + '"';
            return s;
        }

        private static byte[] ImageToBytes(Image img)
        {
            using var ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        private static Image BytesToImage(byte[] data)
        {
            using var ms = new MemoryStream(data);
            return Image.FromStream(ms);
        }
    }
}
