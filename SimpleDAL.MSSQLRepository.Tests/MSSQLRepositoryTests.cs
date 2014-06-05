using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleDAL
{
    [TestClass]
    public class MSSQLRepositoryTests
    {
        [TestMethod]
        public void CreateRepositoryOfType()
        {
            var repo = new MSSQLRepository<string>();

            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void SelectFirstValueFromSelectFromRepository()
        {
            var repo = new MSSQLRepository<string>();

            var value = repo.Select(s => s).First();

            Assert.AreEqual("", value);
        }

        [TestMethod]
        public void GetAllSingleCharStringsFromRepository()
        {
            var repo = new MSSQLRepository<string>();

            int count = repo.Where(s => s.Length == 1).Count();

            Assert.AreEqual(1, count);
        }

        [TestMethod, ExpectedException(typeof(System.InvalidOperationException))]
        public void SelectSingleValueFailsFromRepository()
        {
            var repo = new MSSQLRepository<string>();

            repo.Single(s => true);

            Assert.Fail("Single should throw exception, because source data contains more than one entry.");
        }

        [TestMethod]
        public void SelectSingleOrDefaultWithNoValuesInRepo()
        {
            var repo = new MSSQLRepository<string>();

            var value = repo.SingleOrDefault(s => true);

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UsingCountOnRepo()
        {
            var repo = new MSSQLRepository<string>();

            int totalCount = repo.Count();
            int specificCount = repo.Count(s => s == "");

            Assert.AreEqual(0, totalCount);
            Assert.AreEqual(1, specificCount);
        }
    }
}
