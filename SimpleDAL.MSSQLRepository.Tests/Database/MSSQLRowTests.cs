using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SimpleDAL.Database
{
    [TestClass]
    public class MSSQLRowTests
    {
        [TestMethod]
        public void CreateResult()
        {
            bool hasCalledRead = false;

            var readerMock = new Mock<IDataReader>();
            readerMock.Setup(r => r.Read()).Returns(() =>
            {
                if (!hasCalledRead)
                {
                    hasCalledRead = true;
                    return true;
                }

                return false;
            });
            readerMock.Setup(r => r.FieldCount).Returns(2);
            readerMock.Setup(r => r.GetInt32(0)).Returns(1);
            readerMock.Setup(r => r.GetDataTypeName(0)).Returns("System.Int32");
            readerMock.Setup(r => r.GetString(1)).Returns("Pulp fiction");
            readerMock.Setup(r => r.GetDataTypeName(1)).Returns("System.String");

            var res = MSSQLRow.CreateFromReader(readerMock.Object);

            Assert.AreEqual(2, res.First().Count());
        }
    }
}