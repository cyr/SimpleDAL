using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleDAL.Database
{
    [TestClass]
    public class MSSQLDatabaseTests
    {
        [TestMethod]
        public void LoadRowFromDatabase()
        {
            MSSQLDatabase db = new MSSQLDatabase();

            MSSQLDatabaseResult result = db.ExecuteQuery("select * from Videos");
        }
    }

    public class MSSQLDatabaseResult {}

    public class MSSQLDatabase
    {
        public MSSQLDatabaseResult ExecuteQuery(string sqlQuery)
        {
            SqlConnection connection = new SqlConnection("");

            var cmd = connection.CreateCommand();
            cmd.CommandText = sqlQuery;

            var reader = cmd.ExecuteReader();
        }
    }
}
