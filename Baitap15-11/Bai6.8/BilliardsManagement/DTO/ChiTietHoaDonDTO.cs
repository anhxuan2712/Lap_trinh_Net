namespace BilliardsManagement.DTO
{
    public class ChiTietHoaDonDTO
    {
        public int MaHD { get; set; }
        public int MaDV { get; set; }
        public string TenDV { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public int TonKho { get; set; }

        public decimal ThanhTien
        {
            get { return SoLuong * DonGia; }
        }

        public ChiTietHoaDonDTO() { }
    }
}
