using Bai6._3.DTO;
using MySql.Data.MySqlClient;

namespace Bai6._3.DAL;

public sealed class ProjectTaskDal
{
    public void TestConnection()
    {
        using var connection = new MySqlConnection(DbConfig.ConnectionString);
        connection.Open();
    }

    public void UpdateProject(int projectId, string name)
    {
        const string sql = "UPDATE DuAn SET TenDuAn = @name WHERE MaDuAn = @projectId";
        using var connection = Open(); using var command = new MySqlCommand(sql, connection); command.Parameters.AddWithValue("@name", name); command.Parameters.AddWithValue("@projectId", projectId); command.ExecuteNonQuery();
    }

    public void DeleteProject(int projectId)
    {
        const string sql = "DELETE FROM DuAn WHERE MaDuAn = @projectId";
        using var connection = Open(); using var command = new MySqlCommand(sql, connection); command.Parameters.AddWithValue("@projectId", projectId); command.ExecuteNonQuery();
    }

    public List<ProjectDto> GetProjects()
    {
        const string sql = "SELECT MaDuAn, TenDuAn, NgayBatDau, NgayKetThuc, TrangThai FROM DuAn ORDER BY MaDuAn DESC";
        using var connection = Open(); using var command = new MySqlCommand(sql, connection); using var reader = command.ExecuteReader();
        var result = new List<ProjectDto>();
        while (reader.Read()) result.Add(new ProjectDto { Id = reader.GetInt32("MaDuAn"), Name = reader.GetString("TenDuAn"), StartDate = GetDate(reader, "NgayBatDau"), EndDate = GetDate(reader, "NgayKetThuc"), Status = reader.GetString("TrangThai") });
        return result;
    }

    public void EnsureSampleMembers()
    {
        const string sql = "INSERT IGNORE INTO ThanhVien (HoTen, Email, ChuyenMon) VALUES (@name1, @email1, @expertise1), (@name2, @email2, @expertise2), (@name3, @email3, @expertise3)";
        using var connection = Open();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@name1", "Nguyễn Minh Trí"); command.Parameters.AddWithValue("@email1", "tri@example.com"); command.Parameters.AddWithValue("@expertise1", "Backend");
        command.Parameters.AddWithValue("@name2", "Trần Minh Anh"); command.Parameters.AddWithValue("@email2", "anh@example.com"); command.Parameters.AddWithValue("@expertise2", "Frontend");
        command.Parameters.AddWithValue("@name3", "Lê Hoàng Nam"); command.Parameters.AddWithValue("@email3", "nam@example.com"); command.Parameters.AddWithValue("@expertise3", "QA");
        command.ExecuteNonQuery();
    }

    public List<MemberDto> GetMembers()
    {
        const string sql = "SELECT MaThanhVien, HoTen, Email, ChuyenMon FROM ThanhVien ORDER BY HoTen";
        using var connection = Open(); using var command = new MySqlCommand(sql, connection); using var reader = command.ExecuteReader();
        var result = new List<MemberDto>();
        while (reader.Read()) result.Add(new MemberDto { Id = reader.GetInt32("MaThanhVien"), FullName = reader.GetString("HoTen"), Email = reader.GetString("Email"), Expertise = reader.IsDBNull(reader.GetOrdinal("ChuyenMon")) ? "" : reader.GetString("ChuyenMon") });
        return result;
    }

    public List<TaskDto> GetTasks(int? projectId, string? status)
    {
        const string sql = "SELECT c.MaTask, c.MaDuAn, d.TenDuAn, c.TenTask, c.MoTa, c.HanNop, c.DoUuTien, c.TrangThai FROM CongViec c INNER JOIN DuAn d ON d.MaDuAn = c.MaDuAn WHERE (@projectId IS NULL OR c.MaDuAn = @projectId) AND (@status IS NULL OR c.TrangThai = @status) ORDER BY c.HanNop";
        using var connection = Open(); using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@projectId", projectId.HasValue ? projectId.Value : DBNull.Value); command.Parameters.AddWithValue("@status", status is null ? DBNull.Value : status);
        using var reader = command.ExecuteReader(); return ReadTasks(reader);
    }

    public List<TaskDto> GetOverdueTasks()
    {
        const string sql = "SELECT c.MaTask, c.MaDuAn, d.TenDuAn, c.TenTask, c.MoTa, c.HanNop, c.DoUuTien, c.TrangThai FROM CongViec c INNER JOIN DuAn d ON d.MaDuAn = c.MaDuAn WHERE c.HanNop IS NOT NULL AND c.HanNop < NOW() AND c.TrangThai <> 'Done' ORDER BY c.HanNop";
        using var connection = Open(); using var command = new MySqlCommand(sql, connection); using MySqlDataReader reader = command.ExecuteReader(); return ReadTasks(reader);
    }

    public List<CompletionReportDto> GetCompletionReport()
    {
        const string sql = "SELECT tv.HoTen, COUNT(pc.MaTask) AS TotalTasks, SUM(CASE WHEN c.TrangThai = 'Done' THEN 1 ELSE 0 END) AS CompletedTasks, ROUND(COALESCE(SUM(CASE WHEN c.TrangThai = 'Done' THEN 1 ELSE 0 END) * 100.0 / NULLIF(COUNT(pc.MaTask), 0), 0), 2) AS CompletionRate FROM ThanhVien tv LEFT JOIN PhanCong pc ON pc.MaThanhVien = tv.MaThanhVien LEFT JOIN CongViec c ON c.MaTask = pc.MaTask GROUP BY tv.MaThanhVien, tv.HoTen ORDER BY CompletionRate DESC";
        using var connection = Open(); using var command = new MySqlCommand(sql, connection); using var reader = command.ExecuteReader();
        var result = new List<CompletionReportDto>();
        while (reader.Read()) result.Add(new CompletionReportDto { MemberName = reader.GetString("HoTen"), TotalTasks = Convert.ToInt32(reader["TotalTasks"]), CompletedTasks = Convert.ToInt32(reader["CompletedTasks"]), CompletionRate = Convert.ToDecimal(reader["CompletionRate"]) });
        return result;
    }

    public void AddProject(ProjectDto project)
    {
        const string sql = "INSERT INTO DuAn (TenDuAn, NgayBatDau, NgayKetThuc, TrangThai) VALUES (@name, @start, @end, @status)";
        using var connection = Open(); using var command = new MySqlCommand(sql, connection); command.Parameters.AddWithValue("@name", project.Name); command.Parameters.AddWithValue("@start", project.StartDate ?? DateTime.Now); command.Parameters.AddWithValue("@end", project.EndDate.HasValue ? project.EndDate.Value : DBNull.Value); command.Parameters.AddWithValue("@status", project.Status); command.ExecuteNonQuery();
    }

    public void AddTask(TaskDto task, IEnumerable<int> memberIds)
    {
        using var connection = Open(); using var transaction = connection.BeginTransaction();
        try
        {
            const string taskSql = "INSERT INTO CongViec (MaDuAn, TenTask, MoTa, HanNop, DoUuTien, TrangThai) VALUES (@projectId, @name, @description, @dueDate, @priority, @status); SELECT LAST_INSERT_ID();";
            using var taskCommand = new MySqlCommand(taskSql, connection, transaction); taskCommand.Parameters.AddWithValue("@projectId", task.ProjectId); taskCommand.Parameters.AddWithValue("@name", task.Name); taskCommand.Parameters.AddWithValue("@description", task.Description); taskCommand.Parameters.AddWithValue("@dueDate", task.DueDate.HasValue ? task.DueDate.Value : DBNull.Value); taskCommand.Parameters.AddWithValue("@priority", task.Priority); taskCommand.Parameters.AddWithValue("@status", task.Status);
            var taskId = Convert.ToInt32(taskCommand.ExecuteScalar());
            foreach (var memberId in memberIds)
            {
                using var assignment = new MySqlCommand("INSERT INTO PhanCong (MaTask, MaThanhVien) VALUES (@taskId, @memberId)", connection, transaction); assignment.Parameters.AddWithValue("@taskId", taskId); assignment.Parameters.AddWithValue("@memberId", memberId); assignment.ExecuteNonQuery();
            }
            transaction.Commit();
        }
        catch { transaction.Rollback(); throw; }
    }

    public void UpdateTaskStatus(int taskId, string status)
    {
        const string sql = "UPDATE CongViec SET TrangThai = @status WHERE MaTask = @taskId";
        using var connection = Open(); using var command = new MySqlCommand(sql, connection); command.Parameters.AddWithValue("@status", status); command.Parameters.AddWithValue("@taskId", taskId); command.ExecuteNonQuery();
    }

    private static MySqlConnection Open() { var connection = new MySqlConnection(DbConfig.ConnectionString); connection.Open(); return connection; }
    private static DateTime? GetDate(MySqlDataReader reader, string column) => reader.IsDBNull(reader.GetOrdinal(column)) ? null : reader.GetDateTime(column);
    private static List<TaskDto> ReadTasks(MySqlDataReader reader)
    {
        var result = new List<TaskDto>();
        while (reader.Read()) result.Add(new TaskDto { Id = reader.GetInt32("MaTask"), ProjectId = reader.GetInt32("MaDuAn"), ProjectName = reader.GetString("TenDuAn"), Name = reader.GetString("TenTask"), Description = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? "" : reader.GetString("MoTa"), DueDate = GetDate(reader, "HanNop"), Priority = reader.GetString("DoUuTien"), Status = reader.GetString("TrangThai") });
        return result;
    }
}
