using System;
using System.Collections.Generic;
using LibraryManagement.DTO;
using MySql.Data.MySqlClient;

namespace LibraryManagement.DAL
{
    public class DocGiaDAL
    {
        private static DocGiaDTO Map(MySqlDataReader rd)
        {
            return new DocGiaDTO(
                rd.GetInt32("MaDocGia"),
                rd.GetString("HoTen"),
                rd.IsDBNull(rd.GetOrdinal("Email")) ? "" : rd.GetString("Email"),
                rd.IsDBNull(rd.GetOrdinal("NgayHetHanThe")) ? (DateTime?)null : rd.GetDateTime("NgayHetHanThe"));
        }

        public List<DocGiaDTO> GetAll()
        {
            List<DocGiaDTO> list = new List<DocGiaDTO>();
            const string sql = "SELECT MaDocGia, HoTen, Email, NgayHetHanThe FROM DocGia ORDER BY MaDocGia";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) list.Add(Map(rd));
                }
            }
            return list;
        }

        public DocGiaDTO GetById(int maDocGia)
        {
            const string sql = "SELECT MaDocGia, HoTen, Email, NgayHetHanThe FROM DocGia WHERE MaDocGia = @ma";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ma", maDocGia);
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    return rd.Read() ? Map(rd) : null;
                }
            }
        }

        public bool Insert(DocGiaDTO dg)
        {
            const string sql = "INSERT INTO DocGia (HoTen, Email, NgayHetHanThe) VALUES (@hoTen, @email, @ngay)";
            return DBConnect.ExecuteNonQuery(sql,
                new MySqlParameter("@hoTen", dg.HoTen),
                new MySqlParameter("@email", string.IsNullOrWhiteSpace(dg.Email) ? (object)DBNull.Value : dg.Email),
                new MySqlParameter("@ngay", dg.NgayHetHanThe.HasValue ? (object)dg.NgayHetHanThe.Value : DBNull.Value)) > 0;
        }

        public bool Update(DocGiaDTO dg)
        {
            const string sql = "UPDATE DocGia SET HoTen = @hoTen, Email = @email, NgayHetHanThe = @ngay WHERE MaDocGia = @ma";
            return DBConnect.ExecuteNonQuery(sql,
                new MySqlParameter("@hoTen", dg.HoTen),
                new MySqlParameter("@email", string.IsNullOrWhiteSpace(dg.Email) ? (object)DBNull.Value : dg.Email),
                new MySqlParameter("@ngay", dg.NgayHetHanThe.HasValue ? (object)dg.NgayHetHanThe.Value : DBNull.Value),
                new MySqlParameter("@ma", dg.MaDocGia)) > 0;
        }

        public bool Delete(int maDocGia)
        {
            return DBConnect.ExecuteNonQuery("DELETE FROM DocGia WHERE MaDocGia = @ma",
                new MySqlParameter("@ma", maDocGia)) > 0;
        }

        public bool EmailExists(string email, int excludeMaDocGia)
        {
            object kq = DBConnect.ExecuteScalar(
                "SELECT COUNT(*) FROM DocGia WHERE Email = @email AND MaDocGia <> @ma",
                new MySqlParameter("@email", email),
                new MySqlParameter("@ma", excludeMaDocGia));
            return Convert.ToInt32(kq) > 0;
        }

        public bool KiemTraTheConHan(int maDocGia)
        {
            object kq = DBConnect.ExecuteScalar(
                "SELECT COUNT(*) FROM DocGia WHERE MaDocGia = @ma AND NgayHetHanThe >= NOW()",
                new MySqlParameter("@ma", maDocGia));
            return Convert.ToInt32(kq) > 0;
        }
    }
}
