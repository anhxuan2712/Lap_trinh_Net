using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LibraryManagement.DAL;
using LibraryManagement.DTO;
using MySql.Data.MySqlClient;

namespace LibraryManagement.BUS
{
    public class DocGiaBUS
    {
        private readonly DocGiaDAL dal = new DocGiaDAL();

        public List<DocGiaDTO> GetAll()
        {
            return dal.GetAll();
        }

        private void Validate(DocGiaDTO dg)
        {
            if (string.IsNullOrWhiteSpace(dg.HoTen))
                throw new Exception("Họ tên độc giả không được để trống!");
            if (dg.HoTen.Length > 100)
                throw new Exception("Họ tên tối đa 100 ký tự!");
            if (!string.IsNullOrWhiteSpace(dg.Email))
            {
                if (!Regex.IsMatch(dg.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    throw new Exception("Email không đúng định dạng!");
                if (dal.EmailExists(dg.Email, dg.MaDocGia))
                    throw new Exception("Email \"" + dg.Email + "\" đã được sử dụng!");
            }
            if (!dg.NgayHetHanThe.HasValue)
                throw new Exception("Vui lòng chọn ngày hết hạn thẻ!");
        }

        public bool Them(DocGiaDTO dg)
        {
            dg.HoTen = (dg.HoTen ?? "").Trim();
            dg.Email = (dg.Email ?? "").Trim();
            Validate(dg);
            return dal.Insert(dg);
        }

        public bool Sua(DocGiaDTO dg)
        {
            if (dg.MaDocGia <= 0) throw new Exception("Vui lòng chọn độc giả cần sửa!");
            dg.HoTen = (dg.HoTen ?? "").Trim();
            dg.Email = (dg.Email ?? "").Trim();
            Validate(dg);
            return dal.Update(dg);
        }

        public bool Xoa(int maDocGia)
        {
            if (maDocGia <= 0) throw new Exception("Vui lòng chọn độc giả cần xóa!");
            try
            {
                return dal.Delete(maDocGia);
            }
            catch (MySqlException ex) when (ex.Number == 1451)
            {
                throw new Exception("Không thể xóa: độc giả này đã có phiếu mượn sách!");
            }
        }

        public bool KiemTraTheConHan(int maDocGia)
        {
            return dal.KiemTraTheConHan(maDocGia);
        }
    }
}
