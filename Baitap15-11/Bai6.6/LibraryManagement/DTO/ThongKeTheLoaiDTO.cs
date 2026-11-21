using System.ComponentModel;

namespace LibraryManagement.DTO
{
    public class ThongKeTheLoaiDTO
    {
        [DisplayName("Thể loại")]
        public string TheLoai { get; set; }

        [DisplayName("Số lượt mượn (phiếu)")]
        public int SoLuotMuon { get; set; }

        [DisplayName("Tổng số cuốn")]
        public int TongSoCuon { get; set; }
    }
}
