using System;
using System.Collections.Generic;
using ClinicManagement.DAL;
using ClinicManagement.DTO;
using MySql.Data.MySqlClient;

namespace ClinicManagement.BUS
{
    public class ThuocBUS
    {
        private readonly ThuocDAL dal = new ThuocDAL();

        public List<ThuocDTO> GetAll()
        {
            return dal.Search("");
        }

        public List<ThuocDTO> TimKiem(string keyword)
        {
            return dal.Search((keyword ?? "").Trim());
        }

        private static void Validate(ThuocDTO t)
        {
            t.TenThuoc = (t.TenThuoc ?? "").Trim();
            t.DonViTinh = (t.DonViTinh ?? "").Trim();
            if (t.TenThuoc == "") throw new Exception("Tên thuốc không được để trống!");
            if (t.TenThuoc.Length > 150) throw new Exception("Tên thuốc tối đa 150 ký tự!");
            if (t.DonViTinh == "") throw new Exception("Vui lòng nhập đơn vị tính!");
            if (t.DonViTinh.Length > 20) throw new Exception("Đơn vị tính tối đa 20 ký tự!");
            if (t.DonGia <= 0) throw new Exception("Đơn giá phải lớn hơn 0!");
            if (t.SoLuongTon < 0) throw new Exception("Số lượng tồn phải >= 0!");
        }

        public bool Them(ThuocDTO t)
        {
            Validate(t);
            return dal.Insert(t);
        }

        public bool CapNhat(ThuocDTO t)
        {
            if (t.MaThuoc <= 0) throw new Exception("Vui lòng chọn thuốc cần cập nhật!");
            Validate(t);
            return dal.Update(t);
        }

        public bool NhapKho(int maThuoc, int soLuongNhap)
        {
            if (maThuoc <= 0) throw new Exception("Vui lòng chọn thuốc cần nhập kho!");
            if (soLuongNhap <= 0) throw new Exception("Số lượng nhập thêm phải > 0!");
            return dal.NhapKho(maThuoc, soLuongNhap);
        }

        public bool Xoa(int maThuoc)
        {
            if (maThuoc <= 0) throw new Exception("Vui lòng chọn thuốc cần xóa!");
            try
            {
                return dal.Delete(maThuoc);
            }
            catch (MySqlException ex) when (ex.Number == 1451)
            {
                throw new Exception("Không thể xóa: thuốc này đã được kê trong đơn thuốc!");
            }
        }
    }
}
