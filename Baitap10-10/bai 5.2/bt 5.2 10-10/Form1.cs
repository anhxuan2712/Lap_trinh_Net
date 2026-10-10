using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace bt_5._2_10_10
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, List<ServiceItem>> servicesByCategory = new Dictionary<string, List<ServiceItem>>
        {
            { "Khám bệnh", new List<ServiceItem>
                {
                    new ServiceItem("Khám tổng quát", 150000m, "Khám bệnh"),
                    new ServiceItem("Khám chuyên khoa", 100000m, "Khám bệnh")
                }
            },
            { "Xét nghiệm", new List<ServiceItem>
                {
                    new ServiceItem("Xét nghiệm công thức máu", 120000m, "Xét nghiệm"),
                    new ServiceItem("Xét nghiệm đường huyết", 50000m, "Xét nghiệm"),
                    new ServiceItem("Xét nghiệm nước tiểu", 70000m, "Xét nghiệm")
                }
            },
            { "Chụp X-Quang", new List<ServiceItem>
                {
                    new ServiceItem("X-Quang ngực", 200000m, "Chụp X-Quang"),
                    new ServiceItem("X-Quang bàn tay", 150000m, "Chụp X-Quang")
                }
            },
            { "Vắc-xin", new List<ServiceItem>
                {
                    new ServiceItem("Vắc-xin cúm", 350000m, "Vắc-xin"),
                    new ServiceItem("Vắc-xin viêm gan B", 250000m, "Vắc-xin")
                }
            }
        };

        public Form1()
        {
            InitializeComponent();
            nudDiscount.Maximum = 100;
            cboCategory.DataSource = servicesByCategory.Keys.ToList();
            UpdateTotals();
        }

        private void cboCategory_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadAvailableServices();
        }

        private void LoadAvailableServices()
        {
            lstAvailableServices.Items.Clear();
            string category = cboCategory.SelectedItem as string;
            if (category == null)
            {
                return;
            }

            foreach (ServiceItem service in servicesByCategory[category])
            {
                if (!lstSelectedServices.Items.Contains(service))
                {
                    lstAvailableServices.Items.Add(service);
                }
            }
        }

        private void btnSelect_Click(object sender, System.EventArgs e)
        {
            MoveSelectedService();
        }

        private void lstAvailableServices_DoubleClick(object sender, System.EventArgs e)
        {
            MoveSelectedService();
        }

        private void MoveSelectedService()
        {
            ServiceItem service = lstAvailableServices.SelectedItem as ServiceItem;
            if (service == null)
            {
                return;
            }

            lstSelectedServices.Items.Add(service);
            lstAvailableServices.Items.Remove(service);
            UpdateTotals();
        }

        private void btnRemove_Click(object sender, System.EventArgs e)
        {
            ServiceItem service = lstSelectedServices.SelectedItem as ServiceItem;
            if (service == null)
            {
                return;
            }

            lstSelectedServices.Items.Remove(service);
            if (service.Category == cboCategory.SelectedItem as string)
            {
                lstAvailableServices.Items.Add(service);
            }

            UpdateTotals();
        }

        private void btnClearAll_Click(object sender, System.EventArgs e)
        {
            lstSelectedServices.Items.Clear();
            LoadAvailableServices();
            UpdateTotals();
        }

        private void nudDiscount_ValueChanged(object sender, System.EventArgs e)
        {
            UpdateTotals();
        }

        private void UpdateTotals()
        {
            decimal subtotal = lstSelectedServices.Items
                .Cast<ServiceItem>()
                .Sum(service => service.Price);
            decimal discount = subtotal * nudDiscount.Value / 100m;
            decimal payment = subtotal - discount;

            lblSubtotal.Text = FormatCurrency(subtotal);
            lblPayment.Text = FormatCurrency(payment);
        }

        private static string FormatCurrency(decimal amount)
        {
            return amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " ₫";
        }

        private sealed class ServiceItem
        {
            public ServiceItem(string name, decimal price, string category)
            {
                Name = name;
                Price = price;
                Category = category;
            }

            public string Name { get; private set; }
            public decimal Price { get; private set; }
            public string Category { get; private set; }

            public override string ToString()
            {
                return Name + " - " + FormatCurrency(Price);
            }
        }
    }
}
