using System.Data;
using Bai5._5.DTO;
using MySql.Data.MySqlClient;

namespace Bai5._5.DAL;

public sealed class InventoryDAL
{
    public const string DefaultConnectionString =
        "Server=localhost;Port=3306;Database=InventoryDB;Uid=root;Pwd=familo23;CharSet=utf8mb4;";

    private static string ConnectionString =>
        Environment.GetEnvironmentVariable("INVENTORY_DB_CONNECTION") ?? DefaultConnectionString;

    public DataTable GetVatTu(string keyword)
    {
        const string sql = """
            SELECT MaVatTu, TenVatTu, DVT, SoLuongTon
            FROM VatTu
            WHERE @Keyword = '' OR MaVatTu LIKE @Pattern OR TenVatTu LIKE @Pattern
            ORDER BY MaVatTu
            """;
        return GetData(sql,
            new MySqlParameter("@Keyword", keyword),
            new MySqlParameter("@Pattern", $"%{keyword}%"));
    }

    public DataTable GetNhaCungCap(string keyword)
    {
        const string sql = """
            SELECT MaNCC, TenNCC, DienThoai, DiaChi
            FROM NhaCungCap
            WHERE @Keyword = '' OR TenNCC LIKE @Pattern OR DienThoai LIKE @Pattern
            ORDER BY TenNCC
            """;
        return GetData(sql,
            new MySqlParameter("@Keyword", keyword),
            new MySqlParameter("@Pattern", $"%{keyword}%"));
    }

    public DataTable GetVatTuTonDuoi(decimal nguong)
    {
        const string sql = """
            SELECT MaVatTu, TenVatTu, DVT, SoLuongTon
            FROM VatTu
            WHERE SoLuongTon < @Nguong
            ORDER BY SoLuongTon, MaVatTu
            """;
        return GetData(sql, new MySqlParameter("@Nguong", MySqlDbType.Decimal) { Value = nguong });
    }

    public decimal GetTongGiaTriTonKho()
    {
        const string sql = """
            SELECT COALESCE(SUM(v.SoLuongTon * COALESCE(gia.GiaNhapBinhQuan, 0)), 0)
            FROM VatTu v
            LEFT JOIN (
                SELECT ct.MaVatTu, SUM(ct.SoLuong * ct.DonGia) / NULLIF(SUM(ct.SoLuong), 0) AS GiaNhapBinhQuan
                FROM ChiTietPhieuKho ct
                INNER JOIN PhieuKho p ON p.MaPhieu = ct.MaPhieu
                WHERE p.LoaiPhieu = 'NHAP'
                GROUP BY ct.MaVatTu
            ) gia ON gia.MaVatTu = v.MaVatTu
            """;
        using var connection = new MySqlConnection(ConnectionString);
        using var command = new MySqlCommand(sql, connection);
        connection.Open();
        return Convert.ToDecimal(command.ExecuteScalar());
    }

    public void AddVatTu(VatTuDTO vatTu)
    {
        const string sql = "INSERT INTO VatTu (MaVatTu, TenVatTu, DVT, SoLuongTon) VALUES (@MaVatTu, @TenVatTu, @DVT, @SoLuongTon)";
        Execute(sql,
            new MySqlParameter("@MaVatTu", vatTu.MaVatTu.Trim()),
            new MySqlParameter("@TenVatTu", vatTu.TenVatTu.Trim()),
            new MySqlParameter("@DVT", string.IsNullOrWhiteSpace(vatTu.DVT) ? DBNull.Value : vatTu.DVT.Trim()),
            new MySqlParameter("@SoLuongTon", MySqlDbType.Decimal) { Value = vatTu.SoLuongTon });
    }

    public void UpdateVatTu(VatTuDTO vatTu)
    {
        const string sql = "UPDATE VatTu SET TenVatTu = @TenVatTu, DVT = @DVT, SoLuongTon = @SoLuongTon WHERE MaVatTu = @MaVatTu";
        int affected = Execute(sql,
            new MySqlParameter("@TenVatTu", vatTu.TenVatTu.Trim()),
            new MySqlParameter("@DVT", string.IsNullOrWhiteSpace(vatTu.DVT) ? DBNull.Value : vatTu.DVT.Trim()),
            new MySqlParameter("@SoLuongTon", MySqlDbType.Decimal) { Value = vatTu.SoLuongTon },
            new MySqlParameter("@MaVatTu", vatTu.MaVatTu.Trim()));
        EnsureAffected(affected, "Không tìm thấy vật tư cần cập nhật.");
    }

    public void DeleteVatTu(string maVatTu)
    {
        int affected = Execute("DELETE FROM VatTu WHERE MaVatTu = @MaVatTu", new MySqlParameter("@MaVatTu", maVatTu));
        EnsureAffected(affected, "Không tìm thấy vật tư cần xóa.");
    }

    public void AddNhaCungCap(NhaCungCapDTO nhaCungCap)
    {
        const string sql = "INSERT INTO NhaCungCap (TenNCC, DienThoai, DiaChi) VALUES (@TenNCC, @DienThoai, @DiaChi)";
        Execute(sql,
            new MySqlParameter("@TenNCC", nhaCungCap.TenNCC.Trim()),
            new MySqlParameter("@DienThoai", string.IsNullOrWhiteSpace(nhaCungCap.DienThoai) ? DBNull.Value : nhaCungCap.DienThoai.Trim()),
            new MySqlParameter("@DiaChi", string.IsNullOrWhiteSpace(nhaCungCap.DiaChi) ? DBNull.Value : nhaCungCap.DiaChi.Trim()));
    }

    public void UpdateNhaCungCap(NhaCungCapDTO nhaCungCap)
    {
        const string sql = "UPDATE NhaCungCap SET TenNCC = @TenNCC, DienThoai = @DienThoai, DiaChi = @DiaChi WHERE MaNCC = @MaNCC";
        int affected = Execute(sql,
            new MySqlParameter("@TenNCC", nhaCungCap.TenNCC.Trim()),
            new MySqlParameter("@DienThoai", string.IsNullOrWhiteSpace(nhaCungCap.DienThoai) ? DBNull.Value : nhaCungCap.DienThoai.Trim()),
            new MySqlParameter("@DiaChi", string.IsNullOrWhiteSpace(nhaCungCap.DiaChi) ? DBNull.Value : nhaCungCap.DiaChi.Trim()),
            new MySqlParameter("@MaNCC", nhaCungCap.MaNCC));
        EnsureAffected(affected, "Không tìm thấy nhà cung cấp cần cập nhật.");
    }

    public void DeleteNhaCungCap(int maNCC)
    {
        int affected = Execute("DELETE FROM NhaCungCap WHERE MaNCC = @MaNCC", new MySqlParameter("@MaNCC", maNCC));
        EnsureAffected(affected, "Không tìm thấy nhà cung cấp cần xóa.");
    }

    public int SavePhieuKho(PhieuKhoDTO phieu)
    {
        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            decimal tongTien = phieu.ChiTiet.Sum(item => item.ThanhTien);
            const string receiptSql = "INSERT INTO PhieuKho (LoaiPhieu, NgayLap, MaNCC, TongTien) VALUES (@LoaiPhieu, NOW(), @MaNCC, @TongTien)";
            using var receiptCommand = new MySqlCommand(receiptSql, connection, transaction);
            receiptCommand.Parameters.AddWithValue("@LoaiPhieu", phieu.LoaiPhieu);
            receiptCommand.Parameters.AddWithValue("@MaNCC", phieu.LoaiPhieu == "NHAP" ? phieu.MaNCC!.Value : DBNull.Value);
            receiptCommand.Parameters.Add("@TongTien", MySqlDbType.Decimal).Value = tongTien;
            receiptCommand.ExecuteNonQuery();
            int maPhieu = checked((int)receiptCommand.LastInsertedId);

            const string detailSql = "INSERT INTO ChiTietPhieuKho (MaPhieu, MaVatTu, SoLuong, DonGia) VALUES (@MaPhieu, @MaVatTu, @SoLuong, @DonGia)";
            foreach (ChiTietPhieuKhoDTO item in phieu.ChiTiet)
            {
                using var detailCommand = new MySqlCommand(detailSql, connection, transaction);
                detailCommand.Parameters.AddWithValue("@MaPhieu", maPhieu);
                detailCommand.Parameters.AddWithValue("@MaVatTu", item.MaVatTu.Trim());
                detailCommand.Parameters.Add("@SoLuong", MySqlDbType.Decimal).Value = item.SoLuong;
                detailCommand.Parameters.Add("@DonGia", MySqlDbType.Decimal).Value = item.DonGia;
                detailCommand.ExecuteNonQuery();
            }

            string stockSql = phieu.LoaiPhieu == "NHAP"
                ? "UPDATE VatTu SET SoLuongTon = SoLuongTon + @SoLuong WHERE MaVatTu = @MaVatTu"
                : "UPDATE VatTu SET SoLuongTon = SoLuongTon - @SoLuong WHERE MaVatTu = @MaVatTu AND SoLuongTon >= @SoLuong";
            foreach (ChiTietPhieuKhoDTO item in phieu.ChiTiet)
            {
                using var stockCommand = new MySqlCommand(stockSql, connection, transaction);
                stockCommand.Parameters.AddWithValue("@MaVatTu", item.MaVatTu.Trim());
                stockCommand.Parameters.Add("@SoLuong", MySqlDbType.Decimal).Value = item.SoLuong;
                if (stockCommand.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Không đủ hàng trong kho.");
            }

            transaction.Commit();
            return maPhieu;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private static DataTable GetData(string sql, params MySqlParameter[] parameters)
    {
        using var connection = new MySqlConnection(ConnectionString);
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddRange(parameters);
        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    private static int Execute(string sql, params MySqlParameter[] parameters)
    {
        using var connection = new MySqlConnection(ConnectionString);
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddRange(parameters);
        connection.Open();
        return command.ExecuteNonQuery();
    }

    private static void EnsureAffected(int affected, string message)
    {
        if (affected != 1)
            throw new InvalidOperationException(message);
    }
}