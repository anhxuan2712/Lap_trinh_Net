using System.Collections.Generic;
using ClinicManagement.DTO;
using MySql.Data.MySqlClient;

namespace ClinicManagement.DAL
{
    public class ThuocDAL
    {
        public List<ThuocDTO> Search(string keyword)
        {
            List<ThuocDTO> list = new List<ThuocDTO>();
            const string sql = "SELECT MaThuoc, TenThuoc, DonViTinh, DonGia, SoLuongTon FROM Thuoc " +
                               "WHERE (@kw = '' OR TenThuoc LIKE CONCAT('%', @kw, '%')) ORDER BY TenThuoc";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@kw", keyword ?? "");
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new ThuocDTO(
                            rd.GetInt32("MaThuoc"),
                            rd.GetString("TenThuoc"),
                            rd.IsDBNull(rd.GetOrdinal("DonViTinh")) ? "" : rd.GetString("DonViTinh"),
                            rd.GetDecimal("DonGia"),
                            rd.GetInt32("SoLuongTon")));
                    }
                }
            }
            return list;
        }

        public bool Insert(ThuocDTO t)
        {
            const string sql = "INSERT INTO Thuoc (TenThuoc, DonViTinh, DonGia, SoLuongTon) VALUES (@ten, @dvt, @gia, @ton)";
            return DBConnect.ExecuteNonQuery(sql,
                new MySqlParameter("@ten", t.TenThuoc),
                new MySqlParameter("@dvt", t.DonViTinh),
                new MySqlParameter("@gia", t.DonGia),
                new MySqlParameter("@ton", t.SoLuongTon)) > 0;
        }

        public bool Update(ThuocDTO t)
        {
            const string sql = "UPDATE Thuoc SET TenThuoc = @ten, DonViTinh = @dvt, DonGia = @gia WHERE MaThuoc = @ma";
            return DBConnect.ExecuteNonQuery(sql,
                new MySqlParameter("@ten", t.TenThuoc),
                new MySqlParameter("@dvt", t.DonViTinh),
                new MySqlParameter("@gia", t.DonGia),
                new MySqlParameter("@ma", t.MaThuoc)) > 0;
        }

        public bool NhapKho(int maThuoc, int soLuongNhap)
        {
            return DBConnect.ExecuteNonQuery(
                "UPDATE Thuoc SET SoLuongTon = SoLuongTon + @sl WHERE MaThuoc = @ma",
                new MySqlParameter("@sl", soLuongNhap),
                new MySqlParameter("@ma", maThuoc)) > 0;
        }

        public bool Delete(int maThuoc)
        {
            return DBConnect.ExecuteNonQuery("DELETE FROM Thuoc WHERE MaThuoc = @ma",
                new MySqlParameter("@ma", maThuoc)) > 0;
        }
    }
}
