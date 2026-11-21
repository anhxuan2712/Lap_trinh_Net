using System;
using System.Collections.Generic;
using System.Linq;
using ClinicManagement.DAL;
using ClinicManagement.DTO;

namespace ClinicManagement.BUS
{
    public class DonThuocBUS
    {
        private readonly DonThuocDAL dal = new DonThuocDAL();

        public static decimal TinhTongTien(decimal tienKham, IEnumerable<ChiTietDonThuocDTO> chiTiet)
        {
            return tienKham + (chiTiet == null ? 0 : chiTiet.Sum(x => x.SoLuong * x.DonGia));
        }

        public int LuuVaXuatDon(int maHoSo, string chanDoan, decimal tienKham, List<ChiTietDonThuocDTO> chiTiet)
        {
            chanDoan = (chanDoan ?? "").Trim();
            if (maHoSo <= 0) throw new Exception("Vui lòng chọn hồ sơ khám!");
            if (chanDoan == "") throw new Exception("Bác sĩ chưa nhập chẩn đoán!");
            if (chanDoan.Length > 250) throw new Exception("Chẩn đoán tối đa 250 ký tự!");
            if (tienKham < 0) throw new Exception("Tiền khám không hợp lệ!");
            if (chiTiet == null || chiTiet.Count == 0) throw new Exception("Đơn thuốc chưa có thuốc nào!");
            foreach (ChiTietDonThuocDTO ct in chiTiet)
            {
                if (ct.SoLuong <= 0) throw new Exception("Số lượng thuốc \"" + ct.TenThuoc + "\" phải > 0!");
                if (!string.IsNullOrEmpty(ct.LieuDung) && ct.LieuDung.Length > 100)
                    throw new Exception("Liều dùng của \"" + ct.TenThuoc + "\" tối đa 100 ký tự!");
            }
            return dal.LuuVaXuatDon(maHoSo, chanDoan, tienKham, chiTiet);
        }

        public List<DonThuocDTO> GetDanhSach(string trangThai)
        {
            return dal.GetDanhSach(trangThai ?? "");
        }

        public List<ChiTietDonThuocDTO> GetChiTiet(int maDon)
        {
            return dal.GetChiTiet(maDon);
        }

        public decimal ThanhToan(DonThuocDTO don)
        {
            if (don == null) throw new Exception("Vui lòng chọn đơn thuốc!");
            if (don.TrangThai == TrangThaiDon.DaThanhToan) throw new Exception("Đơn thuốc này đã được thanh toán!");
            decimal tong = TinhTongTien(don.TienKham, dal.GetChiTiet(don.MaDon));
            if (!dal.ThanhToan(don.MaDon, tong))
                throw new Exception("Thanh toán thất bại: đơn không tồn tại hoặc đã thanh toán.");
            don.TongTien = tong;
            don.TrangThai = TrangThaiDon.DaThanhToan;
            return tong;
        }
    }
}
