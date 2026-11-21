using System.ComponentModel;

namespace ClinicManagement.DTO
{
    public class ChiTietDonThuocDTO
    {
        [Browsable(false)]
        public int MaDon { get; set; }

        [DisplayName("Mã thuốc")]
        public int MaThuoc { get; set; }

        [DisplayName("Tên thuốc")]
        public string TenThuoc { get; set; }

        [DisplayName("ĐVT")]
        public string DonViTinh { get; set; }

        [DisplayName("Số lượng")]
        public int SoLuong { get; set; }

        [DisplayName("Đơn giá")]
        public decimal DonGia { get; set; }

        [DisplayName("Thành tiền")]
        public decimal ThanhTien
        {
            get { return SoLuong * DonGia; }
        }

        [DisplayName("Liều dùng")]
        public string LieuDung { get; set; }

        public ChiTietDonThuocDTO() { }
    }
}
