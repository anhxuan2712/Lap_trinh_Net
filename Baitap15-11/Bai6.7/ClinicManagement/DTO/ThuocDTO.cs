using System.ComponentModel;

namespace ClinicManagement.DTO
{
    public class ThuocDTO
    {
        [DisplayName("Mã thuốc")]
        public int MaThuoc { get; set; }

        [DisplayName("Tên thuốc")]
        public string TenThuoc { get; set; }

        [DisplayName("Đơn vị tính")]
        public string DonViTinh { get; set; }

        [DisplayName("Đơn giá")]
        public decimal DonGia { get; set; }

        [DisplayName("Tồn kho")]
        public int SoLuongTon { get; set; }

        public ThuocDTO() { }

        public ThuocDTO(int maThuoc, string tenThuoc, string donViTinh, decimal donGia, int soLuongTon)
        {
            MaThuoc = maThuoc;
            TenThuoc = tenThuoc;
            DonViTinh = donViTinh;
            DonGia = donGia;
            SoLuongTon = soLuongTon;
        }

        [Browsable(false)]
        public string HienThi
        {
            get { return TenThuoc + " (" + DonViTinh + ") - " + DonGia.ToString("N0") + "đ"; }
        }

        public override string ToString()
        {
            return HienThi;
        }
    }
}
