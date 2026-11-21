using System;
using System.Collections.Generic;
using ClinicManagement.DTO;
using MySql.Data.MySqlClient;

namespace ClinicManagement.DAL
{
    public class DonThuocDAL
    {
        public int LuuVaXuatDon(int maHoSo, string chanDoan, decimal tienKham, List<ChiTietDonThuocDTO> chiTiet)
        {
            using (MySqlConnection conn = DBConnect.GetConnection())
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                bool daRollback = false;
                try
                {
                    MySqlCommand cmdHoSo = new MySqlCommand(
                        "UPDATE HoSoKham SET ChanDoan = @chanDoan WHERE MaHoSo = @maHoSo", conn, trans);
                    cmdHoSo.Parameters.AddWithValue("@chanDoan", chanDoan);
                    cmdHoSo.Parameters.AddWithValue("@maHoSo", maHoSo);
                    cmdHoSo.ExecuteNonQuery();

                    MySqlCommand cmdDon = new MySqlCommand(
                        "INSERT INTO DonThuoc (MaHoSo, TienKham, TongTien, TrangThai) VALUES (@maHoSo, @tienKham, @tienKham, @tt)",
                        conn, trans);
                    cmdDon.Parameters.AddWithValue("@maHoSo", maHoSo);
                    cmdDon.Parameters.AddWithValue("@tienKham", tienKham);
                    cmdDon.Parameters.AddWithValue("@tt", TrangThaiDon.ChoLayThuoc);
                    cmdDon.ExecuteNonQuery();
                    int maDon = (int)cmdDon.LastInsertedId;

                    foreach (ChiTietDonThuocDTO ct in chiTiet)
                    {
                        MySqlCommand cmdCT = new MySqlCommand(
                            "INSERT INTO ChiTietDonThuoc (MaDon, MaThuoc, SoLuong, LieuDung) VALUES (@maDon, @maThuoc, @sl, @lieuDung)",
                            conn, trans);
                        cmdCT.Parameters.AddWithValue("@maDon", maDon);
                        cmdCT.Parameters.AddWithValue("@maThuoc", ct.MaThuoc);
                        cmdCT.Parameters.AddWithValue("@sl", ct.SoLuong);
                        cmdCT.Parameters.AddWithValue("@lieuDung", ct.LieuDung ?? "");
                        cmdCT.ExecuteNonQuery();

                        MySqlCommand cmdKho = new MySqlCommand(
                            "UPDATE Thuoc SET SoLuongTon = SoLuongTon - @sl WHERE MaThuoc = @maThuoc AND SoLuongTon >= @sl",
                            conn, trans);
                        cmdKho.Parameters.AddWithValue("@sl", ct.SoLuong);
                        cmdKho.Parameters.AddWithValue("@maThuoc", ct.MaThuoc);
                        if (cmdKho.ExecuteNonQuery() == 0)
                        {
                            MySqlCommand cmdTon = new MySqlCommand(
                                "SELECT SoLuongTon FROM Thuoc WHERE MaThuoc = @maThuoc", conn, trans);
                            cmdTon.Parameters.AddWithValue("@maThuoc", ct.MaThuoc);
                            object ton = cmdTon.ExecuteScalar();

                            trans.Rollback();
                            daRollback = true;
                            throw new ThieuThuocException(
                                "Thuốc \"" + ct.TenThuoc + "\" không đủ tồn kho!\n" +
                                "Yêu cầu: " + ct.SoLuong + " - Tồn hiện tại: " + Convert.ToInt32(ton ?? 0) + "\n\n" +
                                "Toàn bộ đơn thuốc đã được hủy (Rollback).");
                        }
                    }

                    MySqlCommand cmdTong = new MySqlCommand(
                        "UPDATE DonThuoc d SET d.TongTien = d.TienKham + IFNULL((" +
                        "   SELECT SUM(ct.SoLuong * t.DonGia) FROM ChiTietDonThuoc ct " +
                        "   INNER JOIN Thuoc t ON ct.MaThuoc = t.MaThuoc WHERE ct.MaDon = d.MaDon), 0) " +
                        "WHERE d.MaDon = @maDon", conn, trans);
                    cmdTong.Parameters.AddWithValue("@maDon", maDon);
                    cmdTong.ExecuteNonQuery();

                    trans.Commit();
                    return maDon;
                }
                catch
                {
                    if (!daRollback) trans.Rollback();
                    throw;
                }
            }
        }

        public List<DonThuocDTO> GetDanhSach(string trangThai)
        {
            List<DonThuocDTO> list = new List<DonThuocDTO>();
            const string sql =
                "SELECT d.MaDon, d.MaHoSo, bn.HoTen, bn.SoDienThoai, hs.NgayKham, hs.ChanDoan, d.TienKham, d.TongTien, d.TrangThai " +
                "FROM DonThuoc d INNER JOIN HoSoKham hs ON d.MaHoSo = hs.MaHoSo " +
                "INNER JOIN BenhNhan bn ON hs.MaBN = bn.MaBN " +
                "WHERE (@tt = '' OR d.TrangThai = @tt) ORDER BY d.MaDon DESC";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@tt", trangThai ?? "");
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new DonThuocDTO
                        {
                            MaDon = rd.GetInt32("MaDon"),
                            MaHoSo = rd.GetInt32("MaHoSo"),
                            HoTenBN = rd.GetString("HoTen"),
                            SoDienThoai = rd.IsDBNull(rd.GetOrdinal("SoDienThoai")) ? "" : rd.GetString("SoDienThoai"),
                            NgayKham = rd.GetDateTime("NgayKham"),
                            ChanDoan = rd.IsDBNull(rd.GetOrdinal("ChanDoan")) ? "" : rd.GetString("ChanDoan"),
                            TienKham = rd.GetDecimal("TienKham"),
                            TongTien = rd.GetDecimal("TongTien"),
                            TrangThai = rd.GetString("TrangThai")
                        });
                    }
                }
            }
            return list;
        }

        public List<ChiTietDonThuocDTO> GetChiTiet(int maDon)
        {
            List<ChiTietDonThuocDTO> list = new List<ChiTietDonThuocDTO>();
            const string sql =
                "SELECT ct.MaDon, ct.MaThuoc, t.TenThuoc, t.DonViTinh, ct.SoLuong, t.DonGia, ct.LieuDung " +
                "FROM ChiTietDonThuoc ct INNER JOIN Thuoc t ON ct.MaThuoc = t.MaThuoc WHERE ct.MaDon = @maDon";
            using (MySqlConnection conn = DBConnect.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@maDon", maDon);
                conn.Open();
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new ChiTietDonThuocDTO
                        {
                            MaDon = rd.GetInt32("MaDon"),
                            MaThuoc = rd.GetInt32("MaThuoc"),
                            TenThuoc = rd.GetString("TenThuoc"),
                            DonViTinh = rd.IsDBNull(rd.GetOrdinal("DonViTinh")) ? "" : rd.GetString("DonViTinh"),
                            SoLuong = rd.GetInt32("SoLuong"),
                            DonGia = rd.GetDecimal("DonGia"),
                            LieuDung = rd.IsDBNull(rd.GetOrdinal("LieuDung")) ? "" : rd.GetString("LieuDung")
                        });
                    }
                }
            }
            return list;
        }

        public bool ThanhToan(int maDon, decimal tongTien)
        {
            return DBConnect.ExecuteNonQuery(
                "UPDATE DonThuoc SET TongTien = @tong, TrangThai = @tt WHERE MaDon = @maDon AND TrangThai <> @tt",
                new MySqlParameter("@tong", tongTien),
                new MySqlParameter("@tt", TrangThaiDon.DaThanhToan),
                new MySqlParameter("@maDon", maDon)) > 0;
        }
    }
}
