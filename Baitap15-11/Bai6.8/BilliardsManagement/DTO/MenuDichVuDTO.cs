using System;

namespace BilliardsManagement.DTO
{
    public class MenuDichVuDTO
    {
        public int MaDV { get; set; }
        public string TenDV { get; set; }
        public decimal DonGia { get; set; }
        public int TonKho { get; set; }

        public bool LaDichVuThue
        {
            get { return TenDV != null && TenDV.StartsWith("Thuê", StringComparison.OrdinalIgnoreCase); }
        }

        public string HienThi
        {
            get
            {
                return TenDV + " - " + DonGia.ToString("N0") + "đ" + (LaDichVuThue ? "" : " (tồn " + TonKho + ")");
            }
        }

        public MenuDichVuDTO() { }
    }
}
