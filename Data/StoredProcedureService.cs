using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;     // IMPORTANT for MySQL
using System.Data;

namespace BabyShop.Data
{
    public class StoredProcedureService
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public StoredProcedureService(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("BabyShopConnection");
        }

        // Execute stored procedure and return DataTable
        public DataTable ExecuteStoredProcedure(string procedureName, Dictionary<string, object> parameters = null)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(procedureName, conn))
            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }

        // Execute Insert/Update/Delete stored procedure
        public int ExecuteNonQuery(string procedureName, Dictionary<string, object> parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(procedureName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Execute scalar stored procedure
        public object ExecuteScalar(string procedureName, Dictionary<string, object> parameters = null)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(procedureName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}
