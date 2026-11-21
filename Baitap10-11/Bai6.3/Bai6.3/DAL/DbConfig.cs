
namespace Bai6._3.DAL;

internal static class DbConfig
{
    public const string DefaultConnection = "Server=localhost;Port=3306;Database=ProjectTaskDB;Uid=root;Pwd=familo23;CharSet=utf8mb4;SslMode=Disabled;AllowPublicKeyRetrieval=True;";

    public static string ConnectionString =>
        Environment.GetEnvironmentVariable("PROJECT_TASK_MYSQL_CONNECTION") ?? DefaultConnection;
}
