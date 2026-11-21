using System;
using System.ComponentModel;

namespace ClinicManagement.DTO
{
    public static class TrangThaiDon
    {
        public const string ChoLayThuoc = "Chờ lấy thuốc";
        public const string DaThanhToan = "Đã thanh toán";
    }

    public class DonThuocDTO
    {
        [DisplayName("Mã đơn")]
        public int MaDon { get; set; }

        [DisplayName("Mã HS")]
        public int MaHoSo { get; set; }

        [DisplayName("Bệnh nhân")]
        public string HoTenBN { get; set; }

        [Browsable(false)]
        public string SoDienThoai { get; set; }

        [DisplayName("Ngày khám")]
        public DateTime NgayKham { get; set; }

        [Browsable(false)]
        public string ChanDoan { get; set; }

        [DisplayName("Tiền khám")]
        public decimal TienKham { get; set; }

        [DisplayName("Tổng tiền")]
        public decimal TongTien { get; set; }

        [DisplayName("Trạng thái")]
        public string TrangThai { get; set; }

        public DonThuocDTO() { }
    }
}
