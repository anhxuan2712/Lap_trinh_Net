using System;

namespace BilliardsManagement.DTO
{
    public static class TrangThaiBan
    {
        public const string Trong = "Trống";
        public const string DangChoi = "Đang chơi";
    }

    public class BanBidaDTO
    {
        public int MaBan { get; set; }
        public string TenBan { get; set; }
        public string LoaiBan { get; set; }
        public decimal GiaTheoGio { get; set; }
        public string TrangThai { get; set; }

        public int? MaHDDangMo { get; set; }

        public DateTime? GioBatDau { get; set; }

        public bool DangChoi
        {
            get { return TrangThai == TrangThaiBan.DangChoi; }
        }

        public BanBidaDTO() { }
    }
}
