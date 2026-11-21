using System;
using System.Collections.Generic;
using BilliardsManagement.DTO;
using MySql.Data.MySqlClient;

namespace BilliardsManagement.DAL
{
    public class HoaDonDAL
    {
        public int MoBan(int maBan)
        {
            using (MySqlConnection conn = DBConnect.GetConnection())
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                try
                {
                    MySqlCommand cmdBan = new MySqlCommand(
                        "UPDATE BanBida SET TrangThai = @dangChoi WHERE MaBan = @maBan AND TrangThai = @trong", conn, trans);
                    cmdBan.Parameters.AddWithValue("@dangChoi", TrangThaiBan.DangChoi);
                    cmdBan.Parameters.AddWithValue("@trong", TrangThaiBan.Trong);
                    cmdBan.Parameters.AddWithValue("@maBan", maBan);
                    if (cmdBan.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("Bàn này không ở trạng thái \"Trống\" (có thể đã được mở)!");

                    MySqlCommand cmdHD = new MySqlCommand(
                        "INSERT INTO HoaDon (MaBan, GioBatDau, GioKetThuc, TienGioChoi, TienDichVu, TrangThai) " +
                        "VALUES (@maBan, NOW(), NULL, 0, 0, 0)", conn, trans);
                    cmdHD.Parameters.AddWithValue("@maBan", maBan);
                    cmdHD.ExecuteNonQuery();
                    int maHD = (int)cmdHD.LastInsertedId;

                    trans.Commit();
                    return maHD;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public HoaDonDTO GetHoaDonDangMo(int maBan)
        {
            const string sql =
                "SELECT MaHD, MaBan, GioBatDau, GioKetThuc, TienGioChoi, TienDichVu, TrangThai " +
                "FROM HoaDon WHERE MaBan = @maBan AND TrangThai = 0 ORDER BY MaHD DESC LIMIT 1";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@maBan", maBan);
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new HoaDonDTO
                    {
                        MaHD = rd.GetInt32("MaHD"),
                        MaBan = rd.GetInt32("MaBan"),
                        GioBatDau = rd.GetDateTime("GioBatDau"),
                        GioKetThuc = rd.IsDBNull(rd.GetOrdinal("GioKetThuc")) ? (DateTime?)null : rd.GetDateTime("GioKetThuc"),
                        TienGioChoi = rd.GetDecimal("TienGioChoi"),
                        TienDichVu = rd.GetDecimal("TienDichVu"),
                        TrangThai = Convert.ToBoolean(rd["TrangThai"])
                    };
                }
            }
        }

        public decimal ThanhToan(HoaDonDTO hd)
        {
            using (MySqlConnection conn = DBConnect.GetConnection())
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                try
                {
                    MySqlCommand cmdTienDV = new MySqlCommand(
                        "SELECT IFNULL(SUM(SoLuong * DonGia), 0) FROM ChiTietHoaDon WHERE MaHD = @maHD", conn, trans);
                    cmdTienDV.Parameters.AddWithValue("@maHD", hd.MaHD);
                    decimal tienDichVu = Convert.ToDecimal(cmdTienDV.ExecuteScalar());

                    List<ChiTietHoaDonDTO> thieu = new List<ChiTietHoaDonDTO>();
                    MySqlCommand cmdKT = new MySqlCommand(
                        "SELECT m.TenDV, ct.SoLuong, m.TonKho FROM ChiTietHoaDon ct " +
                        "INNER JOIN MenuDichVu m ON ct.MaDV = m.MaDV " +
                        "WHERE ct.MaHD = @maHD AND m.TenDV NOT LIKE 'Thuê%' AND m.TonKho < ct.SoLuong " +
                        "FOR UPDATE", conn, trans);
                    cmdKT.Parameters.AddWithValue("@maHD", hd.MaHD);
                    using (MySqlDataReader rd = cmdKT.ExecuteReader())
                    {
                        while (rd.Read())
                            thieu.Add(new ChiTietHoaDonDTO { TenDV = rd.GetString(0), SoLuong = rd.GetInt32(1), TonKho = rd.GetInt32(2) });
                    }
                    if (thieu.Count > 0)
                    {
                        string msg = "Không đủ tồn kho cho các món sau:\n";
                        foreach (ChiTietHoaDonDTO t in thieu)
                            msg += " - " + t.TenDV + ": gọi " + t.SoLuong + ", tồn " + t.TonKho + "\n";
                        throw new InvalidOperationException(msg + "\nĐã hủy thanh toán (Rollback).");
                    }

                    MySqlCommand cmdHD = new MySqlCommand(
                        "UPDATE HoaDon SET GioKetThuc = @gioKT, TienGioChoi = @tienGio, TienDichVu = @tienDV, TrangThai = 1 " +
                        "WHERE MaHD = @maHD AND TrangThai = 0", conn, trans);
                    cmdHD.Parameters.AddWithValue("@gioKT", hd.GioKetThuc ?? DateTime.Now);
                    cmdHD.Parameters.AddWithValue("@tienGio", hd.TienGioChoi);
                    cmdHD.Parameters.AddWithValue("@tienDV", tienDichVu);
                    cmdHD.Parameters.AddWithValue("@maHD", hd.MaHD);
                    if (cmdHD.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("Hóa đơn không tồn tại hoặc đã được thanh toán!");

                    MySqlCommand cmdKho = new MySqlCommand(
                        "UPDATE MenuDichVu m INNER JOIN ChiTietHoaDon ct ON m.MaDV = ct.MaDV " +
                        "SET m.TonKho = m.TonKho - ct.SoLuong " +
                        "WHERE ct.MaHD = @maHD AND m.TenDV NOT LIKE 'Thuê%'", conn, trans);
                    cmdKho.Parameters.AddWithValue("@maHD", hd.MaHD);
                    cmdKho.ExecuteNonQuery();

                    MySqlCommand cmdBan = new MySqlCommand(
                        "UPDATE BanBida SET TrangThai = @trong WHERE MaBan = @maBan", conn, trans);
                    cmdBan.Parameters.AddWithValue("@trong", TrangThaiBan.Trong);
                    cmdBan.Parameters.AddWithValue("@maBan", hd.MaBan);
                    cmdBan.ExecuteNonQuery();

                    trans.Commit();
                    return tienDichVu;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }
    }
}
