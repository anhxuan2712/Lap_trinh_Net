using System.Data;
using MySql.Data.MySqlClient;

namespace BilliardsManagement.DAL
{
    public class ChiTietHoaDonDAL
    {
        private const string SelectSql =
            "SELECT ct.MaHD, ct.MaDV, m.TenDV, ct.SoLuong, ct.DonGia " +
            "FROM ChiTietHoaDon ct INNER JOIN MenuDichVu m ON ct.MaDV = m.MaDV " +
            "WHERE ct.MaHD = @MaHD ORDER BY m.TenDV";

        private static MySqlDataAdapter CreateAdapter(MySqlConnection conn, int maHD)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();

            da.SelectCommand = new MySqlCommand(SelectSql, conn);
            da.SelectCommand.Parameters.AddWithValue("@MaHD", maHD);

            da.InsertCommand = new MySqlCommand(
                "INSERT INTO ChiTietHoaDon (MaHD, MaDV, SoLuong, DonGia) VALUES (@MaHD, @MaDV, @SoLuong, @DonGia)", conn);
            da.InsertCommand.Parameters.Add("@MaHD", MySqlDbType.Int32, 0, "MaHD");
            da.InsertCommand.Parameters.Add("@MaDV", MySqlDbType.Int32, 0, "MaDV");
            da.InsertCommand.Parameters.Add("@SoLuong", MySqlDbType.Int32, 0, "SoLuong");
            da.InsertCommand.Parameters.Add("@DonGia", MySqlDbType.Decimal, 0, "DonGia");

            da.UpdateCommand = new MySqlCommand(
                "UPDATE ChiTietHoaDon SET SoLuong = @SoLuong WHERE MaHD = @MaHD AND MaDV = @MaDV", conn);
            da.UpdateCommand.Parameters.Add("@SoLuong", MySqlDbType.Int32, 0, "SoLuong");
            da.UpdateCommand.Parameters.Add("@MaHD", MySqlDbType.Int32, 0, "MaHD").SourceVersion = DataRowVersion.Original;
            da.UpdateCommand.Parameters.Add("@MaDV", MySqlDbType.Int32, 0, "MaDV").SourceVersion = DataRowVersion.Original;

            da.DeleteCommand = new MySqlCommand(
                "DELETE FROM ChiTietHoaDon WHERE MaHD = @MaHD AND MaDV = @MaDV", conn);
            da.DeleteCommand.Parameters.Add("@MaHD", MySqlDbType.Int32, 0, "MaHD").SourceVersion = DataRowVersion.Original;
            da.DeleteCommand.Parameters.Add("@MaDV", MySqlDbType.Int32, 0, "MaDV").SourceVersion = DataRowVersion.Original;

            return da;
        }

        public DataTable GetDichVuDaGoi(int maHD)
        {
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlDataAdapter da = CreateAdapter(conn, maHD))
            {
                DataTable dt = new DataTable("ChiTietHoaDon");
                da.Fill(dt);
                dt.PrimaryKey = new[] { dt.Columns["MaHD"], dt.Columns["MaDV"] };
                return dt;
            }
        }

        public int LuuThayDoi(DataTable dt, int maHD)
        {
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlDataAdapter da = CreateAdapter(conn, maHD))
            {
                return da.Update(dt);
            }
        }
    }
}
