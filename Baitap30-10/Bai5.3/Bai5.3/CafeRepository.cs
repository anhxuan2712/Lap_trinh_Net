
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Bai5._3;

public sealed class CafeRepository
{
    public const string ConnectionString =
        "Server=localhost;Port=3306;Database=CafePOSDB;Uid=root;Pwd=familo23;CharSet=utf8mb4;";

    public DataTable LoadTables()
    {
        const string sql = """
            SELECT MaBan, TenBan, TrangThai
            FROM BanAn
            ORDER BY MaBan
            """;

        return GetData(sql);
    }

    public DataTable LoadCategories()
    {
        const string sql = """
            SELECT MaDanhMuc, TenDanhMuc
            FROM DanhMuc
            ORDER BY MaDanhMuc
            """;

        return GetData(sql);
    }

    public DataTable LoadMenu()
    {
        const string sql = """
            SELECT MaMon, TenMon, MaDanhMuc, DonGia
            FROM MonAn
            WHERE TrangThai = 1
            ORDER BY MaMon
            """;

        return GetData(sql);
    }

    public DataTable LoadOrder(int tableId)
    {
        const string sql = """
            SELECT
                ct.MaMon,
                m.TenMon,
                ct.SoLuong,
                ct.DonGia,
                ct.SoLuong * ct.DonGia AS ThanhTien
            FROM ChiTietHoaDon ct
            INNER JOIN HoaDon hd ON ct.MaHoaDon = hd.MaHoaDon
            INNER JOIN MonAn m ON ct.MaMon = m.MaMon
            WHERE hd.MaBan = @MaBan
                AND hd.TrangThai = 0
            ORDER BY ct.MaMon
            """;

        return GetData(sql, new MySqlParameter("@MaBan", tableId));
    }

    public void AddItem(int tableId, int menuId, int quantity)
    {
        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            const string checkMenuSql = """
                SELECT DonGia
                FROM MonAn
                WHERE MaMon = @MaMon AND TrangThai = 1
                """;

            using var checkMenu = new MySqlCommand(
                checkMenuSql, connection, transaction);

            checkMenu.Parameters.AddWithValue("@MaMon", menuId);

            var priceResult = checkMenu.ExecuteScalar();

            if (priceResult == null || priceResult == DBNull.Value)
                throw new Exception("Món ăn không tồn tại hoặc đã ngừng kinh doanh.");

            decimal price = Convert.ToDecimal(priceResult);

            const string findOrderSql = """
                SELECT MaHoaDon
                FROM HoaDon
                WHERE MaBan = @MaBan AND TrangThai = 0
                LIMIT 1
                """;

            using var findOrder = new MySqlCommand(
                findOrderSql, connection, transaction);

            findOrder.Parameters.AddWithValue("@MaBan", tableId);

            var orderResult = findOrder.ExecuteScalar();

            int orderId;

            if (orderResult == null || orderResult == DBNull.Value)
            {
                const string insertOrderSql = """
                    INSERT INTO HoaDon
                        (MaBan, GioVao, TongTien, TrangThai)
                    VALUES
                        (@MaBan, NOW(), 0, 0)
                    """;

                using var insertOrder = new MySqlCommand(
                    insertOrderSql, connection, transaction);

                insertOrder.Parameters.AddWithValue("@MaBan", tableId);
                insertOrder.ExecuteNonQuery();

                orderId = Convert.ToInt32(insertOrder.LastInsertedId);
            }
            else
            {
                orderId = Convert.ToInt32(orderResult);
            }

            const string checkDetailSql = """
                SELECT SoLuong
                FROM ChiTietHoaDon
                WHERE MaHoaDon = @MaHoaDon
                    AND MaMon = @MaMon
                LIMIT 1
                """;

            using var checkDetail = new MySqlCommand(
                checkDetailSql, connection, transaction);

            checkDetail.Parameters.AddWithValue("@MaHoaDon", orderId);
            checkDetail.Parameters.AddWithValue("@MaMon", menuId);

            var detailResult = checkDetail.ExecuteScalar();

            if (detailResult == null || detailResult == DBNull.Value)
            {
                const string insertDetailSql = """
                    INSERT INTO ChiTietHoaDon
                        (MaHoaDon, MaMon, SoLuong, DonGia)
                    VALUES
                        (@MaHoaDon, @MaMon, @SoLuong, @DonGia)
                    """;

                using var insertDetail = new MySqlCommand(
                    insertDetailSql, connection, transaction);

                insertDetail.Parameters.AddWithValue("@MaHoaDon", orderId);
                insertDetail.Parameters.AddWithValue("@MaMon", menuId);
                insertDetail.Parameters.AddWithValue("@SoLuong", quantity);
                insertDetail.Parameters.AddWithValue("@DonGia", price);

                insertDetail.ExecuteNonQuery();
            }
            else
            {
                const string updateDetailSql = """
                    UPDATE ChiTietHoaDon
                    SET SoLuong = SoLuong + @SoLuong
                    WHERE MaHoaDon = @MaHoaDon
                        AND MaMon = @MaMon
                    """;

                using var updateDetail = new MySqlCommand(
                    updateDetailSql, connection, transaction);

                updateDetail.Parameters.AddWithValue("@SoLuong", quantity);
                updateDetail.Parameters.AddWithValue("@MaHoaDon", orderId);
                updateDetail.Parameters.AddWithValue("@MaMon", menuId);

                updateDetail.ExecuteNonQuery();
            }

            const string updateTableSql = """
                UPDATE BanAn
                SET TrangThai = 'Có khách'
                WHERE MaBan = @MaBan
                """;

            using var updateTable = new MySqlCommand(
                updateTableSql, connection, transaction);

            updateTable.Parameters.AddWithValue("@MaBan", tableId);
            updateTable.ExecuteNonQuery();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void Pay(int tableId)
    {
        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            const string updateOrderSql = """
                UPDATE HoaDon hd
                SET
                    hd.TongTien = (
                        SELECT COALESCE(SUM(ct.SoLuong * ct.DonGia), 0)
                        FROM ChiTietHoaDon ct
                        WHERE ct.MaHoaDon = hd.MaHoaDon
                    ),
                    hd.TrangThai = 1,
                    hd.GioRa = NOW()
                WHERE hd.MaBan = @MaBan
                    AND hd.TrangThai = 0
                """;

            using var updateOrder = new MySqlCommand(
                updateOrderSql, connection, transaction);

            updateOrder.Parameters.AddWithValue("@MaBan", tableId);

            int affected = updateOrder.ExecuteNonQuery();

            if (affected == 0)
                throw new Exception("Bàn chưa có hóa đơn cần thanh toán.");

            const string updateTableSql = """
                UPDATE BanAn
                SET TrangThai = 'Trống'
                WHERE MaBan = @MaBan
                """;

            using var updateTable = new MySqlCommand(
                updateTableSql, connection, transaction);

            updateTable.Parameters.AddWithValue("@MaBan", tableId);
            updateTable.ExecuteNonQuery();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void SaveMenu(
        int menuId,
        string name,
        int categoryId,
        decimal price)
    {
        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();

        if (menuId == 0)
        {
            const string sql = """
                INSERT INTO MonAn
                    (TenMon, MaDanhMuc, DonGia, TrangThai)
                VALUES
                    (@TenMon, @MaDanhMuc, @DonGia, 1)
                """;

            using var command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("@TenMon", name);
            command.Parameters.AddWithValue("@MaDanhMuc", categoryId);
            command.Parameters.AddWithValue("@DonGia", price);

            command.ExecuteNonQuery();
        }
        else
        {
            const string sql = """
                UPDATE MonAn
                SET
                    TenMon = @TenMon,
                    MaDanhMuc = @MaDanhMuc,
                    DonGia = @DonGia
                WHERE MaMon = @MaMon
                    AND TrangThai = 1
                """;

            using var command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("@TenMon", name);
            command.Parameters.AddWithValue("@MaDanhMuc", categoryId);
            command.Parameters.AddWithValue("@DonGia", price);
            command.Parameters.AddWithValue("@MaMon", menuId);

            command.ExecuteNonQuery();
        }
    }

    public void DeleteMenu(int menuId)
    {
        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();

        const string sql = """
            UPDATE MonAn
            SET TrangThai = 0
            WHERE MaMon = @MaMon
                AND TrangThai = 1
            """;

        using var command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@MaMon", menuId);

        int affected = command.ExecuteNonQuery();

        if (affected == 0)
            throw new Exception("Món ăn không tồn tại hoặc đã ngừng kinh doanh.");
    }

    public void MoveTable(int currentTableId, int targetTableId)
    {
        if (currentTableId == targetTableId)
            throw new Exception("Không thể chuyển sang chính bàn hiện tại.");

        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            const string checkTargetSql = """
                SELECT TrangThai
                FROM BanAn
                WHERE MaBan = @MaBan
                FOR UPDATE
                """;

            using var checkTarget = new MySqlCommand(
                checkTargetSql, connection, transaction);

            checkTarget.Parameters.AddWithValue("@MaBan", targetTableId);

            var targetStatus = checkTarget.ExecuteScalar();

            if (targetStatus == null || targetStatus == DBNull.Value)
                throw new Exception("Bàn đích không tồn tại.");

            if (Convert.ToString(targetStatus) != "Trống")
                throw new Exception("Bàn đích không còn trống.");

            const string updateOrderSql = """
                UPDATE HoaDon
                SET MaBan = @TargetBan
                WHERE MaBan = @CurrentBan
                    AND TrangThai = 0
                """;

            using var updateOrder = new MySqlCommand(
                updateOrderSql, connection, transaction);

            updateOrder.Parameters.AddWithValue("@TargetBan", targetTableId);
            updateOrder.Parameters.AddWithValue("@CurrentBan", currentTableId);

            int affected = updateOrder.ExecuteNonQuery();

            if (affected == 0)
                throw new Exception("Bàn hiện tại chưa có hóa đơn để chuyển.");

            const string updateCurrentSql = """
                UPDATE BanAn
                SET TrangThai = 'Trống'
                WHERE MaBan = @MaBan
                """;

            using var updateCurrent = new MySqlCommand(
                updateCurrentSql, connection, transaction);

            updateCurrent.Parameters.AddWithValue("@MaBan", currentTableId);
            updateCurrent.ExecuteNonQuery();

            const string updateTargetSql = """
                UPDATE BanAn
                SET TrangThai = 'Có khách'
                WHERE MaBan = @MaBan
                """;

            using var updateTarget = new MySqlCommand(
                updateTargetSql, connection, transaction);

            updateTarget.Parameters.AddWithValue("@MaBan", targetTableId);
            updateTarget.ExecuteNonQuery();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private DataTable GetData(
        string sql,
        params MySqlParameter[] parameters)
    {
        using var connection = new MySqlConnection(ConnectionString);
        using var command = new MySqlCommand(sql, connection);

        if (parameters.Length > 0)
            command.Parameters.AddRange(parameters);

        using var adapter = new MySqlDataAdapter(command);

        var table = new DataTable();
        adapter.Fill(table);

        return table;
    }
}
