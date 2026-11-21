using System;
using System.Text.RegularExpressions;
using ClinicManagement.DAL;
using ClinicManagement.DTO;

namespace ClinicManagement.BUS
{
    public class BenhNhanBUS
    {
        private readonly BenhNhanDAL dal = new BenhNhanDAL();

        public static string ChuanHoaSDT(string sdt)
        {
            return Regex.Replace(sdt ?? "", @"[\s\.\-]", "");
        }

        private static void KiemTraSDT(string sdt)
        {
            if (!Regex.IsMatch(sdt, @"^0\d{9,10}$"))
                throw new Exception("Số điện thoại không hợp lệ (bắt đầu bằng 0, gồm 10-11 chữ số)!");
        }

        public BenhNhanDTO TraCuuTheoSDT(string soDienThoai)
        {
            string sdt = ChuanHoaSDT(soDienThoai);
            if (sdt == "") throw new Exception("Vui lòng nhập số điện thoại cần tra cứu!");
            return dal.GetBySoDienThoai(sdt);
        }

        private void Validate(BenhNhanDTO bn)
        {
            bn.HoTen = (bn.HoTen ?? "").Trim();
            bn.SoDienThoai = ChuanHoaSDT(bn.SoDienThoai);
            if (bn.HoTen == "") throw new Exception("Họ tên bệnh nhân không được để trống!");
            if (bn.HoTen.Length > 100) throw new Exception("Họ tên tối đa 100 ký tự!");
            KiemTraSDT(bn.SoDienThoai);
            if (bn.NamSinh.HasValue && (bn.NamSinh < 1900 || bn.NamSinh > DateTime.Now.Year))
                throw new Exception("Năm sinh không hợp lệ!");
            if (dal.SoDienThoaiExists(bn.SoDienThoai, bn.MaBN))
                throw new Exception("Số điện thoại " + bn.SoDienThoai + " đã thuộc về bệnh nhân khác!");
        }

        public int Them(BenhNhanDTO bn)
        {
            bn.MaBN = 0;
            Validate(bn);
            return dal.Insert(bn);
        }

        public bool Sua(BenhNhanDTO bn)
        {
            if (bn.MaBN <= 0) throw new Exception("Chưa chọn bệnh nhân để cập nhật!");
            Validate(bn);
            return dal.Update(bn);
        }
    }
}
