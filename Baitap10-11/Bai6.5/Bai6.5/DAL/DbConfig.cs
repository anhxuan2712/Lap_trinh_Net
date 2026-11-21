
using MySql.Data.MySqlClient;

namespace Bai6._5.DAL;

internal static class DbConfig
{
    public static string ConnectionString =>
        "Server=localhost;Port=3306;Database=KaraokeDB;Uid=root;Pwd=familo23;CharSet=utf8mb4;SslMode=Disabled;AllowPublicKeyRetrieval=True;";
}
