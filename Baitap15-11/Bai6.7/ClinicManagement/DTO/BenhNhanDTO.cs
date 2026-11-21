using System.ComponentModel;

namespace ClinicManagement.DTO
{
    public class BenhNhanDTO
    {
        [DisplayName("Mã BN")]
        public int MaBN { get; set; }

        [DisplayName("Họ tên")]
        public string HoTen { get; set; }

        [DisplayName("Số điện thoại")]
        public string SoDienThoai { get; set; }

        [DisplayName("Năm sinh")]
        public int? NamSinh { get; set; }

        public BenhNhanDTO() { }

        public BenhNhanDTO(int maBN, string hoTen, string soDienThoai, int? namSinh)
        {
            MaBN = maBN;
            HoTen = hoTen;
            SoDienThoai = soDienThoai;
            NamSinh = namSinh;
        }
    }
}
