using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleDAL.Database
{
    [TestClass]
    public class MSSQLDatabaseTests
    {
        [TestMethod]
        public void LoadRowFromDatabase()
        {
            var db = new MSSQLDatabase(@"mini\sqlexpress");

            var result = db.ExecuteQuery("select * from Videos");

            Assert.AreEqual(result.First().First().Value, 1);
        }
    }
}
