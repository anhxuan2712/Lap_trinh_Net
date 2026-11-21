using System;
using ClinicManagement.DTO;
using MySql.Data.MySqlClient;

namespace ClinicManagement.DAL
{
    public class BenhNhanDAL
    {
        public BenhNhanDTO GetBySoDienThoai(string soDienThoai)
        {
            const string sql = "SELECT MaBN, HoTen, SoDienThoai, NamSinh FROM BenhNhan WHERE SoDienThoai = @sdt";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@sdt", MySqlDbType.VarChar, 15).Value = soDienThoai;
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new BenhNhanDTO(
                        rd.GetInt32("MaBN"),
                        rd.GetString("HoTen"),
                        rd.IsDBNull(rd.GetOrdinal("SoDienThoai")) ? "" : rd.GetString("SoDienThoai"),
                        rd.IsDBNull(rd.GetOrdinal("NamSinh")) ? (int?)null : rd.GetInt32("NamSinh"));
                }
            }
        }

        public int Insert(BenhNhanDTO bn)
        {
            const string sql = "INSERT INTO BenhNhan (HoTen, SoDienThoai, NamSinh) VALUES (@hoTen, @sdt, @namSinh)";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@hoTen", bn.HoTen);
                cmd.Parameters.AddWithValue("@sdt", bn.SoDienThoai);
                cmd.Parameters.AddWithValue("@namSinh", bn.NamSinh.HasValue ? (object)bn.NamSinh.Value : DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
                return (int)cmd.LastInsertedId;
            }
        }

        public bool Update(BenhNhanDTO bn)
        {
            const string sql = "UPDATE BenhNhan SET HoTen = @hoTen, SoDienThoai = @sdt, NamSinh = @namSinh WHERE MaBN = @ma";
            return DBConnect.ExecuteNonQuery(sql,
                new MySqlParameter("@hoTen", bn.HoTen),
                new MySqlParameter("@sdt", bn.SoDienThoai),
                new MySqlParameter("@namSinh", bn.NamSinh.HasValue ? (object)bn.NamSinh.Value : DBNull.Value),
                new MySqlParameter("@ma", bn.MaBN)) > 0;
        }

        public bool SoDienThoaiExists(string soDienThoai, int excludeMaBN)
        {
            object kq = DBConnect.ExecuteScalar(
                "SELECT COUNT(*) FROM BenhNhan WHERE SoDienThoai = @sdt AND MaBN <> @ma",
                new MySqlParameter("@sdt", soDienThoai),
                new MySqlParameter("@ma", excludeMaBN));
            return Convert.ToInt32(kq) > 0;
        }
    }
}
