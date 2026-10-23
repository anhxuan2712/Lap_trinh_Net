using System;

namespace test2._4_chieu_23_10
{
    public class InventoryReportItem
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public int StockQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalValue => StockQuantity * UnitPrice;
        public string Status { get; set; }
    }

    public class ProgressUpdate
    {
        public int Percentage { get; set; }
        public string Message { get; set; }
    }
}
