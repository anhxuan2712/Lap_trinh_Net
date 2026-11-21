using System;
using System.Collections.Generic;
using BilliardsManagement.DTO;
using MySql.Data.MySqlClient;

namespace BilliardsManagement.DAL
{
    public class BanBidaDAL
    {
        public List<BanBidaDTO> GetAll()
        {
            List<BanBidaDTO> list = new List<BanBidaDTO>();
            const string sql =
                "SELECT b.MaBan, b.TenBan, b.LoaiBan, b.GiaTheoGio, b.TrangThai, hd.MaHD, hd.GioBatDau " +
                "FROM BanBida b " +
                "LEFT JOIN HoaDon hd ON hd.MaBan = b.MaBan AND hd.TrangThai = 0 " +
                "ORDER BY b.MaBan";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new BanBidaDTO
                        {
                            MaBan = rd.GetInt32("MaBan"),
                            TenBan = rd.GetString("TenBan"),
                            LoaiBan = rd.IsDBNull(rd.GetOrdinal("LoaiBan")) ? "" : rd.GetString("LoaiBan"),
                            GiaTheoGio = rd.GetDecimal("GiaTheoGio"),
                            TrangThai = rd.GetString("TrangThai"),
                            MaHDDangMo = rd.IsDBNull(rd.GetOrdinal("MaHD")) ? (int?)null : rd.GetInt32("MaHD"),
                            GioBatDau = rd.IsDBNull(rd.GetOrdinal("GioBatDau")) ? (DateTime?)null : rd.GetDateTime("GioBatDau")
                        });
                    }
                }
            }
            return list;
        }
    }
}
