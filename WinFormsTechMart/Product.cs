using System;

namespace WinFormsTechMart
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public byte[] ImageData { get; set; } // optional store image bytes

        public Product() { }

        public Product(string id, string name, string category, decimal price, int qty, byte[] img = null)
        {
            ProductId = id;
            ProductName = name;
            Category = category;
            UnitPrice = price;
            Quantity = qty;
            ImageData = img;
        }
    }
}
