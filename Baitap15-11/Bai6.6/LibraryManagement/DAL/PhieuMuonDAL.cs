using System;
using System.Collections.Generic;
using LibraryManagement.DTO;
using MySql.Data.MySqlClient;

namespace LibraryManagement.DAL
{
    public class PhieuMuonDAL
    {
        public List<PhieuMuonDTO> GetDanhSach(int maDocGia, string trangThai)
        {
            List<PhieuMuonDTO> list = new List<PhieuMuonDTO>();
            const string sql =
                "SELECT pm.MaPhieu, pm.MaDocGia, dg.HoTen, pm.NgayMuon, pm.HanTra, pm.TrangThai " +
                "FROM PhieuMuon pm INNER JOIN DocGia dg ON pm.MaDocGia = dg.MaDocGia " +
                "WHERE (@ma = 0 OR pm.MaDocGia = @ma) AND (@tt = '' OR pm.TrangThai = @tt) " +
                "ORDER BY pm.MaPhieu DESC";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ma", maDocGia);
                cmd.Parameters.AddWithValue("@tt", trangThai ?? "");
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new PhieuMuonDTO
                        {
                            MaPhieu = rd.GetInt32("MaPhieu"),
                            MaDocGia = rd.GetInt32("MaDocGia"),
                            HoTenDocGia = rd.GetString("HoTen"),
                            NgayMuon = rd.GetDateTime("NgayMuon"),
                            HanTra = rd.GetDateTime("HanTra"),
                            TrangThai = rd.GetString("TrangThai")
                        });
                    }
                }
            }
            return list;
        }

        public List<ChiTietMuonDTO> GetChiTiet(int maPhieu)
        {
            List<ChiTietMuonDTO> list = new List<ChiTietMuonDTO>();
            const string sql =
                "SELECT ct.MaPhieu, ct.MaSach, ds.TenSach, ct.SoLuong " +
                "FROM ChiTietMuon ct INNER JOIN DauSach ds ON ct.MaSach = ds.MaSach WHERE ct.MaPhieu = @ma";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ma", maPhieu);
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new ChiTietMuonDTO
                        {
                            MaPhieu = rd.GetInt32("MaPhieu"),
                            MaSach = rd.GetInt32("MaSach"),
                            TenSach = rd.GetString("TenSach"),
                            SoLuong = rd.GetInt32("SoLuong")
                        });
                    }
                }
            }
            return list;
        }

        public int LapPhieu(PhieuMuonDTO phieu, List<ChiTietMuonDTO> chiTiet)
        {
            using (MySqlConnection conn = DBConnect.GetConnection())
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                try
                {
                    MySqlCommand cmdPhieu = new MySqlCommand(
                        "INSERT INTO PhieuMuon (MaDocGia, NgayMuon, HanTra, TrangThai) VALUES (@maDG, NOW(), @hanTra, @tt)",
                        conn, trans);
                    cmdPhieu.Parameters.AddWithValue("@maDG", phieu.MaDocGia);
                    cmdPhieu.Parameters.AddWithValue("@hanTra", phieu.HanTra);
                    cmdPhieu.Parameters.AddWithValue("@tt", TrangThaiPhieu.DangMuon);
                    cmdPhieu.ExecuteNonQuery();
                    int maPhieu = (int)cmdPhieu.LastInsertedId;

                    foreach (ChiTietMuonDTO ct in chiTiet)
                    {
                        MySqlCommand cmdTru = new MySqlCommand(
                            "UPDATE DauSach SET SoLuongCon = SoLuongCon - @sl WHERE MaSach = @ms AND SoLuongCon >= @sl",
                            conn, trans);
                        cmdTru.Parameters.AddWithValue("@sl", ct.SoLuong);
                        cmdTru.Parameters.AddWithValue("@ms", ct.MaSach);
                        if (cmdTru.ExecuteNonQuery() == 0)
                        {
                            throw new InvalidOperationException("Sách \"" + ct.TenSach + "\" không đủ số lượng trong kho!");
                        }

                        MySqlCommand cmdCT = new MySqlCommand(
                            "INSERT INTO ChiTietMuon (MaPhieu, MaSach, SoLuong) VALUES (@mp, @ms, @sl)", conn, trans);
                        cmdCT.Parameters.AddWithValue("@mp", maPhieu);
                        cmdCT.Parameters.AddWithValue("@ms", ct.MaSach);
                        cmdCT.Parameters.AddWithValue("@sl", ct.SoLuong);
                        cmdCT.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return maPhieu;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public bool TraSach(int maPhieu)
        {
            using (MySqlConnection conn = DBConnect.GetConnection())
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                try
                {
                    MySqlCommand cmdPhieu = new MySqlCommand(
                        "UPDATE PhieuMuon SET TrangThai = @daTra WHERE MaPhieu = @ma AND TrangThai <> @daTra",
                        conn, trans);
                    cmdPhieu.Parameters.AddWithValue("@daTra", TrangThaiPhieu.DaTra);
                    cmdPhieu.Parameters.AddWithValue("@ma", maPhieu);
                    if (cmdPhieu.ExecuteNonQuery() == 0)
                    {
                        trans.Rollback();
                        return false;
                    }

                    MySqlCommand cmdKho = new MySqlCommand(
                        "UPDATE DauSach ds INNER JOIN ChiTietMuon ct ON ds.MaSach = ct.MaSach " +
                        "SET ds.SoLuongCon = ds.SoLuongCon + ct.SoLuong WHERE ct.MaPhieu = @ma",
                        conn, trans);
                    cmdKho.Parameters.AddWithValue("@ma", maPhieu);
                    cmdKho.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public int CapNhatQuaHan()
        {
            return DBConnect.ExecuteNonQuery(
                "UPDATE PhieuMuon SET TrangThai = @quaHan WHERE TrangThai = @dangMuon AND HanTra < NOW()",
                new MySqlParameter("@quaHan", TrangThaiPhieu.QuaHan),
                new MySqlParameter("@dangMuon", TrangThaiPhieu.DangMuon));
        }

        public List<PhieuMuonDTO> GetPhieuQuaHan()
        {
            List<PhieuMuonDTO> list = new List<PhieuMuonDTO>();
            const string sql =
                "SELECT pm.MaPhieu, pm.MaDocGia, dg.HoTen, dg.Email, pm.NgayMuon, pm.HanTra, pm.TrangThai, " +
                "       DATEDIFF(NOW(), pm.HanTra) AS SoNgayQuaHan " +
                "FROM PhieuMuon pm INNER JOIN DocGia dg ON pm.MaDocGia = dg.MaDocGia " +
                "WHERE pm.HanTra < NOW() AND pm.TrangThai <> @daTra " +
                "ORDER BY SoNgayQuaHan DESC";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@daTra", TrangThaiPhieu.DaTra);
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new PhieuMuonDTO
                        {
                            MaPhieu = rd.GetInt32("MaPhieu"),
                            MaDocGia = rd.GetInt32("MaDocGia"),
                            HoTenDocGia = rd.GetString("HoTen"),
                            Email = rd.IsDBNull(rd.GetOrdinal("Email")) ? "" : rd.GetString("Email"),
                            NgayMuon = rd.GetDateTime("NgayMuon"),
                            HanTra = rd.GetDateTime("HanTra"),
                            TrangThai = rd.GetString("TrangThai"),
                            SoNgayQuaHan = Convert.ToInt32(rd["SoNgayQuaHan"])
                        });
                    }
                }
            }
            return list;
        }

        public List<ThongKeTheLoaiDTO> ThongKeTheoTheLoai()
        {
            List<ThongKeTheLoaiDTO> list = new List<ThongKeTheLoaiDTO>();
            const string sql =
                "SELECT IFNULL(ds.TheLoai, '(Chưa phân loại)') AS TheLoai, " +
                "       COUNT(DISTINCT ct.MaPhieu) AS SoLuotMuon, SUM(ct.SoLuong) AS TongSoCuon " +
                "FROM ChiTietMuon ct INNER JOIN DauSach ds ON ct.MaSach = ds.MaSach " +
                "GROUP BY ds.TheLoai ORDER BY SoLuotMuon DESC";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new ThongKeTheLoaiDTO
                        {
                            TheLoai = rd.GetString("TheLoai"),
                            SoLuotMuon = Convert.ToInt32(rd["SoLuotMuon"]),
                            TongSoCuon = Convert.ToInt32(rd["TongSoCuon"])
                        });
                    }
                }
            }
            return list;
        }

        public int DemLuotMuonTheoTheLoai(string theLoai)
        {
            object kq = DBConnect.ExecuteScalar(
                "SELECT COUNT(DISTINCT ct.MaPhieu) FROM ChiTietMuon ct " +
                "INNER JOIN DauSach ds ON ct.MaSach = ds.MaSach WHERE ds.TheLoai = @tl",
                new MySqlParameter("@tl", theLoai));
            return Convert.ToInt32(kq);
        }

        public int DemPhieu(string trangThai)
        {
            object kq = DBConnect.ExecuteScalar(
                "SELECT COUNT(*) FROM PhieuMuon WHERE (@tt = '' OR TrangThai = @tt)",
                new MySqlParameter("@tt", trangThai ?? ""));
            return Convert.ToInt32(kq);
        }
    }
}
