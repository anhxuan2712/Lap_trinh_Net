using System.Collections.Generic;
using LibraryManagement.DTO;
using MySql.Data.MySqlClient;

namespace LibraryManagement.DAL
{
    public class DauSachDAL
    {
        private static DauSachDTO Map(MySqlDataReader rd)
        {
            return new DauSachDTO(
                rd.GetInt32("MaSach"),
                rd.GetString("TenSach"),
                rd.IsDBNull(rd.GetOrdinal("TheLoai")) ? "" : rd.GetString("TheLoai"),
                rd.GetInt32("SoLuongCon"));
        }

        public List<DauSachDTO> Search(string keyword)
        {
            List<DauSachDTO> list = new List<DauSachDTO>();
            const string sql = "SELECT MaSach, TenSach, TheLoai, SoLuongCon FROM DauSach " +
                               "WHERE (@kw = '' OR TenSach LIKE CONCAT('%', @kw, '%') OR TheLoai LIKE CONCAT('%', @kw, '%')) " +
                               "ORDER BY MaSach";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@kw", keyword ?? "");
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) list.Add(Map(rd));
                }
            }
            return list;
        }

        public List<DauSachDTO> GetAll()
        {
            return Search("");
        }

        public List<string> GetDanhSachTheLoai()
        {
            List<string> list = new List<string>();
            const string sql = "SELECT DISTINCT TheLoai FROM DauSach WHERE TheLoai IS NOT NULL AND TheLoai <> '' ORDER BY TheLoai";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) list.Add(rd.GetString(0));
                }
            }
            return list;
        }

        public bool Insert(DauSachDTO s)
        {
            const string sql = "INSERT INTO DauSach (TenSach, TheLoai, SoLuongCon) VALUES (@ten, @theLoai, @sl)";
            return DBConnect.ExecuteNonQuery(sql,
                new MySqlParameter("@ten", s.TenSach),
                new MySqlParameter("@theLoai", s.TheLoai),
                new MySqlParameter("@sl", s.SoLuongCon)) > 0;
        }

        public bool Update(DauSachDTO s)
        {
            const string sql = "UPDATE DauSach SET TenSach = @ten, TheLoai = @theLoai, SoLuongCon = @sl WHERE MaSach = @ma";
            return DBConnect.ExecuteNonQuery(sql,
                new MySqlParameter("@ten", s.TenSach),
                new MySqlParameter("@theLoai", s.TheLoai),
                new MySqlParameter("@sl", s.SoLuongCon),
                new MySqlParameter("@ma", s.MaSach)) > 0;
        }

        public bool Delete(int maSach)
        {
            return DBConnect.ExecuteNonQuery("DELETE FROM DauSach WHERE MaSach = @ma",
                new MySqlParameter("@ma", maSach)) > 0;
        }
    }
}
