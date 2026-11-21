
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Bai6._4.DAL;

internal static class DbConfig
{
    public static string ConnectionString
    {
        get
        {
            string connectionString =
                Environment.GetEnvironmentVariable("GARAGE_MYSQL_CONNECTION")
                ?? "";

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString =
                    "Server=localhost;Port=3306;Database=GarageDB;" +
                    "Uid=root;Pwd=familo23;CharSet=utf8mb4;" +
                    "SslMode=Disabled;AllowPublicKeyRetrieval=True;";
            }

            connectionString = Regex.Replace(
                connectionString,
                @"(SslMode\s*=\s*)(None|Disabled)",
                "$1Disabled",
                RegexOptions.IgnoreCase
            );

            var builder = new MySqlConnectionStringBuilder(connectionString)
            {
                SslMode = MySqlSslMode.Disabled,
                AllowPublicKeyRetrieval = true
            };

            return builder.ConnectionString;
        }
    }
}
