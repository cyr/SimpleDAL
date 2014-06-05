using System.Collections.Generic;
using System.Data.SqlClient;

namespace SimpleDAL.Database
{
    internal class MSSQLDatabase
    {
        private readonly string _connectionString;

        public MSSQLDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<MSSQLRow> ExecuteQuery(string sqlQuery)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            var cmd = connection.CreateCommand();
            cmd.CommandText = sqlQuery;

            var reader = cmd.ExecuteReader();

            var result = MSSQLRow.CreateFromReader(reader);

            return result;
        }
    }
}