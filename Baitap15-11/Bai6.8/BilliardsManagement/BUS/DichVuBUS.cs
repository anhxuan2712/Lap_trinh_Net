using System;
using System.Collections.Generic;
using System.Data;
using BilliardsManagement.DAL;
using BilliardsManagement.DTO;

namespace BilliardsManagement.BUS
{
    public class DichVuBUS
    {
        private readonly MenuDichVuDAL menuDal = new MenuDichVuDAL();
        private readonly ChiTietHoaDonDAL chiTietDal = new ChiTietHoaDonDAL();

        public List<MenuDichVuDTO> GetMenu()
        {
            return menuDal.GetAll();
        }

        public DataTable GetDichVuDaGoi(int maHD)
        {
            DataTable dt = chiTietDal.GetDichVuDaGoi(maHD);
            if (!dt.Columns.Contains("ThanhTien"))
                dt.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * DonGia");
            return dt;
        }

        public void ThemMon(DataTable dt, int maHD, MenuDichVuDTO dv, int soLuong)
        {
            if (dt == null || maHD <= 0) throw new Exception("Bàn chưa được mở (chưa có hóa đơn)!");
            if (dv == null) throw new Exception("Vui lòng chọn dịch vụ!");
            if (soLuong <= 0) throw new Exception("Số lượng phải > 0!");

            DataRow row = TimDong(dt, dv.MaDV);
            int soLuongMoi = soLuong;
            if (row != null)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    row.RejectChanges();
                    row["SoLuong"] = soLuong;
                }
                else
                {
                    soLuongMoi = Convert.ToInt32(row["SoLuong"]) + soLuong;
                    row["SoLuong"] = soLuongMoi;
                }
            }
            else
            {
                DataRow r = dt.NewRow();
                r["MaHD"] = maHD;
                r["MaDV"] = dv.MaDV;
                r["TenDV"] = dv.TenDV;
                r["SoLuong"] = soLuong;
                r["DonGia"] = dv.DonGia;
                dt.Rows.Add(r);
            }

            if (!dv.LaDichVuThue && soLuongMoi > dv.TonKho)
                throw new CanhBaoTonKhoException("\"" + dv.TenDV + "\" chỉ còn " + dv.TonKho +
                                                 " trong kho. Hóa đơn sẽ không thanh toán được nếu vượt tồn kho.");
        }

        private static DataRow TimDong(DataTable dt, int maDV)
        {
            foreach (DataRow r in dt.Rows)
            {
                object ma = r.RowState == DataRowState.Deleted ? r["MaDV", DataRowVersion.Original] : r["MaDV"];
                if (Convert.ToInt32(ma) == maDV) return r;
            }
            return null;
        }

        public static decimal TinhTienDichVu(DataTable dt)
        {
            decimal tong = 0;
            if (dt == null) return 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;
                tong += Convert.ToInt32(r["SoLuong"]) * Convert.ToDecimal(r["DonGia"]);
            }
            return tong;
        }

        public static bool CoThayDoiChuaLuu(DataTable dt)
        {
            return dt != null && dt.GetChanges() != null;
        }

        public int LuuThayDoi(DataTable dt, int maHD)
        {
            if (dt == null) return 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;
                if (r["SoLuong"] == DBNull.Value || Convert.ToInt32(r["SoLuong"]) <= 0)
                    throw new Exception("Số lượng của \"" + r["TenDV"] + "\" phải > 0!");
            }
            int n = chiTietDal.LuuThayDoi(dt, maHD);
            dt.AcceptChanges();
            return n;
        }
    }

    public class CanhBaoTonKhoException : Exception
    {
        public CanhBaoTonKhoException(string message) : base(message) { }
    }
}
