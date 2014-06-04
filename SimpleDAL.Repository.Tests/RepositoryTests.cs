using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleDAL
{
    [TestClass]
    public class RepositoryTests
    {
        private readonly string[] _sourceStringData = { "some data", "1" };
        private readonly string[] _singleEntrySourceData = new string[0];

        [TestMethod]
        public void CreateRepositoryOfType()
        {
            var repo = new Repository<string>(_sourceStringData);

            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void SelectFirstValueFromRepository()
        {
            var repo = new Repository<string>(_sourceStringData);

            var value = repo.Select(s => s).First();

            Assert.AreEqual(_sourceStringData.First(), value);
        }

        [TestMethod]
        public void GetAllSingleCharStringsFromRepository()
        {
            var repo = new Repository<string>(_sourceStringData);

            var count = repo.Where(s => s.Length == 1).Count();

            Assert.AreEqual(1, count);
        }

        [TestMethod, ExpectedException(typeof(System.InvalidOperationException))]
        public void SelectSingleValueFailsFromRepository()
        {
            var repo = new Repository<string>(_sourceStringData);

            repo.Single(s => true);

            Assert.Fail("Single should throw exception, because source data contains more than one entry.");
        }

        [TestMethod]
        public void SelectSingleOrDefaultWithNoValuesInRepo()
        {
            var repo = new Repository<string>(_singleEntrySourceData);

            var value = repo.SingleOrDefault(s => true);

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UsingCountOnRepo()
        {
            var repo = new Repository<string>(_sourceStringData);

            var totalCount = repo.Count();
            var specificCount = repo.Count(s => s == _sourceStringData[0]);

            Assert.AreEqual(_sourceStringData.Length, totalCount);
            Assert.AreEqual(1, specificCount);
        }
    }
}
