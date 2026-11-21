using System;
using System.ComponentModel;

namespace ClinicManagement.DTO
{
    public class HoSoKhamDTO
    {
        [DisplayName("Mã HS")]
        public int MaHoSo { get; set; }

        [Browsable(false)]
        public int MaBN { get; set; }

        [DisplayName("Bệnh nhân")]
        public string HoTenBN { get; set; }

        [DisplayName("Ngày khám")]
        public DateTime NgayKham { get; set; }

        [DisplayName("Triệu chứng")]
        public string TrieuChung { get; set; }

        [DisplayName("Chẩn đoán")]
        public string ChanDoan { get; set; }

        public HoSoKhamDTO() { }
    }
}
