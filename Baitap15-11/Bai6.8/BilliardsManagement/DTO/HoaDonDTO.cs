using System;

namespace BilliardsManagement.DTO
{
    public class HoaDonDTO
    {
        public int MaHD { get; set; }
        public int MaBan { get; set; }
        public DateTime GioBatDau { get; set; }
        public DateTime? GioKetThuc { get; set; }
        public decimal TienGioChoi { get; set; }
        public decimal TienDichVu { get; set; }

        public bool TrangThai { get; set; }

        public int SoPhutTinhTien { get; set; }

        public decimal TongTien
        {
            get { return TienGioChoi + TienDichVu; }
        }

        public HoaDonDTO() { }
    }
}
