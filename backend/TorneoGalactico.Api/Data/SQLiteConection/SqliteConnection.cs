using System.Data;

namespace TorneoGalactico.Api.Data.Conection
{
    public class SqliteConnection : ISqliteConnection
    {
        private readonly string _connectionString;

        public SqliteConnection(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Default") ?? "Data Source=torneo.db";
        }

        public IDbConnection onCreate()
        {
            var connection = new Microsoft.Data.Sqlite.SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}