using System;
using System.ComponentModel;

namespace LibraryManagement.DTO
{
    public class DocGiaDTO
    {
        [DisplayName("Mã độc giả")]
        public int MaDocGia { get; set; }

        [DisplayName("Họ tên")]
        public string HoTen { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Ngày hết hạn thẻ")]
        public DateTime? NgayHetHanThe { get; set; }

        [Browsable(false)]
        public bool TheConHan
        {
            get { return NgayHetHanThe.HasValue && NgayHetHanThe.Value >= DateTime.Now; }
        }

        public DocGiaDTO() { }

        public DocGiaDTO(int maDocGia, string hoTen, string email, DateTime? ngayHetHanThe)
        {
            MaDocGia = maDocGia;
            HoTen = hoTen;
            Email = email;
            NgayHetHanThe = ngayHetHanThe;
        }

        [Browsable(false)]
        public string HienThi
        {
            get { return MaDocGia <= 0 ? HoTen : MaDocGia + " - " + HoTen; }
        }

        public override string ToString()
        {
            return HienThi;
        }
    }
}
