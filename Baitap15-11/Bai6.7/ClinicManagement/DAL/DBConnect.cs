using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace ClinicManagement.DAL
{
    public static class DBConnect
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["ClinicDB"].ConnectionString;

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static DataTable ExecuteQuery(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                DataTable dt = new DataTable();
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                return dt;
            }
        }

        public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}
