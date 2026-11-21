using System.Collections.Generic;
using BilliardsManagement.DTO;
using MySql.Data.MySqlClient;

namespace BilliardsManagement.DAL
{
    public class MenuDichVuDAL
    {
        public List<MenuDichVuDTO> GetAll()
        {
            List<MenuDichVuDTO> list = new List<MenuDichVuDTO>();
            const string sql = "SELECT MaDV, TenDV, DonGia, TonKho FROM MenuDichVu ORDER BY TenDV";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new MenuDichVuDTO
                        {
                            MaDV = rd.GetInt32("MaDV"),
                            TenDV = rd.GetString("TenDV"),
                            DonGia = rd.GetDecimal("DonGia"),
                            TonKho = rd.GetInt32("TonKho")
                        });
                    }
                }
            }
            return list;
        }
    }
}
