using System;
using System.ComponentModel;

namespace LibraryManagement.DTO
{
    public static class TrangThaiPhieu
    {
        public const string DangMuon = "Đang mượn";
        public const string DaTra = "Đã trả";
        public const string QuaHan = "Quá hạn";

        public static readonly string[] TatCa = { DangMuon, DaTra, QuaHan };
    }

    public class PhieuMuonDTO
    {
        [DisplayName("Mã phiếu")]
        public int MaPhieu { get; set; }

        [DisplayName("Mã ĐG")]
        public int MaDocGia { get; set; }

        [DisplayName("Độc giả")]
        public string HoTenDocGia { get; set; }

        [DisplayName("Ngày mượn")]
        public DateTime NgayMuon { get; set; }

        [DisplayName("Hạn trả")]
        public DateTime HanTra { get; set; }

        [DisplayName("Trạng thái")]
        public string TrangThai { get; set; }

        [Browsable(false)]
        public int SoNgayQuaHan { get; set; }

        [Browsable(false)]
        public string Email { get; set; }

        public PhieuMuonDTO() { }
    }
}
