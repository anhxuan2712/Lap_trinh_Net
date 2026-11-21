using System.Collections.Generic;
using ClinicManagement.DTO;
using MySql.Data.MySqlClient;

namespace ClinicManagement.DAL
{
    public class HoSoKhamDAL
    {
        private const string SelectSql =
            "SELECT hs.MaHoSo, hs.MaBN, bn.HoTen, hs.NgayKham, hs.TrieuChung, hs.ChanDoan " +
            "FROM HoSoKham hs INNER JOIN BenhNhan bn ON hs.MaBN = bn.MaBN ";

        private static List<HoSoKhamDTO> ReadList(MySqlCommand cmd)
        {
            List<HoSoKhamDTO> list = new List<HoSoKhamDTO>();
            using (MySqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    list.Add(new HoSoKhamDTO
                    {
                        MaHoSo = rd.GetInt32("MaHoSo"),
                        MaBN = rd.GetInt32("MaBN"),
                        HoTenBN = rd.GetString("HoTen"),
                        NgayKham = rd.GetDateTime("NgayKham"),
                        TrieuChung = rd.IsDBNull(rd.GetOrdinal("TrieuChung")) ? "" : rd.GetString("TrieuChung"),
                        ChanDoan = rd.IsDBNull(rd.GetOrdinal("ChanDoan")) ? "" : rd.GetString("ChanDoan")
                    });
                }
            }
            return list;
        }

        public int Insert(int maBN, string trieuChung)
        {
            const string sql = "INSERT INTO HoSoKham (MaBN, NgayKham, TrieuChung) VALUES (@maBN, NOW(), @trieuChung)";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@maBN", maBN);
                cmd.Parameters.AddWithValue("@trieuChung", trieuChung);
                conn.Open();
                cmd.ExecuteNonQuery();
                return (int)cmd.LastInsertedId;
            }
        }

        public List<HoSoKhamDTO> GetByBenhNhan(int maBN)
        {
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(SelectSql + "WHERE hs.MaBN = @maBN ORDER BY hs.NgayKham DESC", conn))
            {
                cmd.Parameters.AddWithValue("@maBN", maBN);
                conn.Open();
                return ReadList(cmd);
            }
        }

        public List<HoSoKhamDTO> GetChoKeDon()
        {
            const string sql = SelectSql +
                "WHERE NOT EXISTS (SELECT 1 FROM DonThuoc d WHERE d.MaHoSo = hs.MaHoSo) ORDER BY hs.NgayKham";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                return ReadList(cmd);
            }
        }
    }
}
