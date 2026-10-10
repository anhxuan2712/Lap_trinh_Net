using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace bai_5._3_10_10
{
    public partial class Form1 : Form
    {
        private readonly List<Product> products = new List<Product>();

        public Form1()
        {
            InitializeComponent();
            bindingSourceProducts.DataSource = products;
            dgvProducts.DataSource = bindingSourceProducts;
            SetColumnHeaders();
        }

        private void SetColumnHeaders()
        {
            dgvProducts.AutoGenerateColumns = true;
            dgvProducts.DataBindingComplete += (sender, e) =>
            {
                if (dgvProducts.Columns[nameof(Product.ProductId)] != null)
                    dgvProducts.Columns[nameof(Product.ProductId)].HeaderText = "Mã SP";
                if (dgvProducts.Columns[nameof(Product.ProductName)] != null)
                    dgvProducts.Columns[nameof(Product.ProductName)].HeaderText = "Tên SP";
                if (dgvProducts.Columns[nameof(Product.UnitPrice)] != null)
                    dgvProducts.Columns[nameof(Product.UnitPrice)].HeaderText = "Đơn giá";
                if (dgvProducts.Columns[nameof(Product.Quantity)] != null)
                    dgvProducts.Columns[nameof(Product.Quantity)].HeaderText = "Số lượng";
                if (dgvProducts.Columns[nameof(Product.Category)] != null)
                    dgvProducts.Columns[nameof(Product.Category)].HeaderText = "Danh mục";
            };
        }

        private bool TryReadProduct(out Product product)
        {
            product = null;
            string productId = txtProductId.Text.Trim();
            string productName = txtProductName.Text.Trim();
            string category = txtCategory.Text.Trim();

            if (string.IsNullOrWhiteSpace(productId) || string.IsNullOrWhiteSpace(productName))
            {
                MessageBox.Show("Vui lòng nhập mã và tên sản phẩm.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            decimal unitPrice;
            if (!decimal.TryParse(txtUnitPrice.Text.Trim(), NumberStyles.Number,
                    CultureInfo.CurrentCulture, out unitPrice) || unitPrice < 0)
            {
                MessageBox.Show("Đơn giá phải là số không âm.", "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnitPrice.Focus();
                return false;
            }

            int quantity;
            if (!int.TryParse(txtQuantity.Text.Trim(), out quantity) || quantity < 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên không âm.", "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }

            product = new Product
            {
                ProductId = productId,
                ProductName = productName,
                UnitPrice = unitPrice,
                Quantity = quantity,
                Category = category
            };
            return true;
        }

        private void RefreshProducts()
        {
            string searchText = txtSearch.Text.Trim();
            List<Product> displayedProducts = string.IsNullOrWhiteSpace(searchText)
                ? products
                : products.Where(p => p.ProductName.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();

            bindingSourceProducts.DataSource = displayedProducts;
            bindingSourceProducts.ResetBindings(false);
            if (displayedProducts.Count == 0)
                ClearInputs();
        }

        private Product GetSelectedProduct()
        {
            return bindingSourceProducts.Current as Product;
        }

        private void ClearInputs()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            txtCategory.Clear();
            txtProductId.Focus();
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            Product selectedProduct = GetSelectedProduct();
            if (selectedProduct == null)
                return;

            txtProductId.Text = selectedProduct.ProductId;
            txtProductName.Text = selectedProduct.ProductName;
            txtUnitPrice.Text = selectedProduct.UnitPrice.ToString(CultureInfo.CurrentCulture);
            txtQuantity.Text = selectedProduct.Quantity.ToString();
            txtCategory.Text = selectedProduct.Category;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Product product;
            if (!TryReadProduct(out product))
                return;

            if (products.Any(p => string.Equals(p.ProductId, product.ProductId, StringComparison.CurrentCultureIgnoreCase)))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại.", "Không thể thêm",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductId.Focus();
                return;
            }

            products.Add(product);
            RefreshProducts();
            ClearInputs();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Product selectedProduct = GetSelectedProduct();
            if (selectedProduct == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa.", "Chưa chọn sản phẩm",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Product updatedProduct;
            if (!TryReadProduct(out updatedProduct))
                return;

            if (products.Any(p => !ReferenceEquals(p, selectedProduct) &&
                string.Equals(p.ProductId, updatedProduct.ProductId, StringComparison.CurrentCultureIgnoreCase)))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại.", "Không thể sửa",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductId.Focus();
                return;
            }

            selectedProduct.ProductId = updatedProduct.ProductId;
            selectedProduct.ProductName = updatedProduct.ProductName;
            selectedProduct.UnitPrice = updatedProduct.UnitPrice;
            selectedProduct.Quantity = updatedProduct.Quantity;
            selectedProduct.Category = updatedProduct.Category;
            RefreshProducts();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Product selectedProduct = GetSelectedProduct();
            if (selectedProduct == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.", "Chưa chọn sản phẩm",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            products.Remove(selectedProduct);
            RefreshProducts();
            ClearInputs();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RefreshProducts();
        }
    }
}
