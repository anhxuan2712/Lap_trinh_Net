using System.ComponentModel;

namespace LibraryManagement.DTO
{
    public class ChiTietMuonDTO
    {
        [Browsable(false)]
        public int MaPhieu { get; set; }

        [DisplayName("Mã sách")]
        public int MaSach { get; set; }

        [DisplayName("Tên sách")]
        public string TenSach { get; set; }

        [DisplayName("Số lượng")]
        public int SoLuong { get; set; }

        public ChiTietMuonDTO() { }

        public ChiTietMuonDTO(int maSach, string tenSach, int soLuong)
        {
            MaSach = maSach;
            TenSach = tenSach;
            SoLuong = soLuong;
        }
    }
}
