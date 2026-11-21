using System;
using BilliardsManagement.DAL;
using BilliardsManagement.DTO;

namespace BilliardsManagement.BUS
{
    public class HoaDonBUS
    {
        public const int BlockPhut = 15;

        private readonly HoaDonDAL dal = new HoaDonDAL();

        public HoaDonDTO GetHoaDonDangMo(int maBan)
        {
            return dal.GetHoaDonDangMo(maBan);
        }

        public static int TinhSoPhut(DateTime gioBatDau, DateTime gioKetThuc, bool lamTronBlock15)
        {
            double tongPhut = Math.Max(0, (gioKetThuc - gioBatDau).TotalMinutes);
            int phut = (int)Math.Ceiling(tongPhut);
            if (!lamTronBlock15) return phut;
            int soBlock = Math.Max(1, (int)Math.Ceiling(tongPhut / BlockPhut));
            return soBlock * BlockPhut;
        }

        public static decimal TinhTienGio(int soPhut, decimal giaTheoGio)
        {
            return Math.Round(soPhut / 60m * giaTheoGio, 0, MidpointRounding.AwayFromZero);
        }

        public static string DinhDangThoiGian(TimeSpan ts)
        {
            if (ts < TimeSpan.Zero) ts = TimeSpan.Zero;
            return string.Format("{0}:{1:00}:{2:00}", (int)ts.TotalHours, ts.Minutes, ts.Seconds);
        }

        public HoaDonDTO TinhTien(HoaDonDTO hd, decimal giaTheoGio, decimal tienDichVu, DateTime gioKetThuc, bool lamTronBlock15)
        {
            hd.GioKetThuc = gioKetThuc;
            hd.SoPhutTinhTien = TinhSoPhut(hd.GioBatDau, gioKetThuc, lamTronBlock15);
            hd.TienGioChoi = TinhTienGio(hd.SoPhutTinhTien, giaTheoGio);
            hd.TienDichVu = tienDichVu;
            return hd;
        }

        public HoaDonDTO ThanhToan(HoaDonDTO hd, decimal giaTheoGio, bool lamTronBlock15)
        {
            if (hd == null) throw new Exception("Bàn chưa có hóa đơn đang mở!");
            if (hd.TrangThai) throw new Exception("Hóa đơn đã được thanh toán!");

            TinhTien(hd, giaTheoGio, hd.TienDichVu, DateTime.Now, lamTronBlock15);
            hd.TienDichVu = dal.ThanhToan(hd);
            hd.TrangThai = true;
            return hd;
        }
    }
}
