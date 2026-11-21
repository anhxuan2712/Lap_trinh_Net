using System.ComponentModel;

namespace LibraryManagement.DTO
{
    public class DauSachDTO
    {
        [DisplayName("Mã sách")]
        public int MaSach { get; set; }

        [DisplayName("Tên sách")]
        public string TenSach { get; set; }

        [DisplayName("Thể loại")]
        public string TheLoai { get; set; }

        [DisplayName("SL còn")]
        public int SoLuongCon { get; set; }

        public DauSachDTO() { }

        public DauSachDTO(int maSach, string tenSach, string theLoai, int soLuongCon)
        {
            MaSach = maSach;
            TenSach = tenSach;
            TheLoai = theLoai;
            SoLuongCon = soLuongCon;
        }

        [Browsable(false)]
        public string HienThi
        {
            get { return TenSach + " (còn " + SoLuongCon + ")"; }
        }

        public override string ToString()
        {
            return HienThi;
        }
    }
}
