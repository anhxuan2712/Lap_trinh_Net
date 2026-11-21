using System;
using System.Collections.Generic;
using LibraryManagement.DAL;
using LibraryManagement.DTO;
using MySql.Data.MySqlClient;

namespace LibraryManagement.BUS
{
    public class DauSachBUS
    {
        private readonly DauSachDAL dal = new DauSachDAL();

        public List<DauSachDTO> GetAll()
        {
            return dal.GetAll();
        }

        public List<DauSachDTO> TimKiem(string keyword)
        {
            return dal.Search((keyword ?? "").Trim());
        }

        public List<string> GetDanhSachTheLoai()
        {
            return dal.GetDanhSachTheLoai();
        }

        private static void Validate(DauSachDTO s)
        {
            if (string.IsNullOrWhiteSpace(s.TenSach))
                throw new Exception("Tên sách không được để trống!");
            if (s.TenSach.Length > 200)
                throw new Exception("Tên sách tối đa 200 ký tự!");
            if (!string.IsNullOrEmpty(s.TheLoai) && s.TheLoai.Length > 50)
                throw new Exception("Thể loại tối đa 50 ký tự!");
            if (s.SoLuongCon < 0)
                throw new Exception("Số lượng còn phải >= 0!");
        }

        public bool Them(DauSachDTO s)
        {
            s.TenSach = (s.TenSach ?? "").Trim();
            s.TheLoai = (s.TheLoai ?? "").Trim();
            Validate(s);
            return dal.Insert(s);
        }

        public bool Sua(DauSachDTO s)
        {
            if (s.MaSach <= 0) throw new Exception("Vui lòng chọn đầu sách cần sửa!");
            s.TenSach = (s.TenSach ?? "").Trim();
            s.TheLoai = (s.TheLoai ?? "").Trim();
            Validate(s);
            return dal.Update(s);
        }

        public bool Xoa(int maSach)
        {
            if (maSach <= 0) throw new Exception("Vui lòng chọn đầu sách cần xóa!");
            try
            {
                return dal.Delete(maSach);
            }
            catch (MySqlException ex) when (ex.Number == 1451)
            {
                throw new Exception("Không thể xóa: đầu sách này đã có trong phiếu mượn!");
            }
        }
    }
}
