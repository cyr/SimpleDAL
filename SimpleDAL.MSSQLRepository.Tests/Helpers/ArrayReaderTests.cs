using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleDAL.Helpers
{
    [TestClass]
    public class ArrayReaderTests
    {
        private const long LARGE_BUF_SIZE = 2048;
        private const long SMALL_BUF_SIZE = 2;

        private long TestReadFuncSmall<T>(int ordinal, long fieldOffset, T[] buf, int bufOffset, int length)
        {
            T[] testBuffer = new T[SMALL_BUF_SIZE];

            testBuffer.CopyTo(buf, bufOffset);

            return testBuffer.Length;
        }

        [TestMethod]
        public void CheckArraySizeCorrectUnderDefaultSize()
        {
            var resultBuf = ArrayReader.GetArray<byte>(TestReadFuncSmall, 0);

            Assert.AreEqual(SMALL_BUF_SIZE, resultBuf.Length);
        }

        private long TestReadFuncLarge<T>(int ordinal, long fieldOffset, T[] buf, int bufOffset, int length)
        {
            T[] testBuffer = new T[LARGE_BUF_SIZE];

            return CopyToArray(fieldOffset, buf, testBuffer);
        }

        private static long CopyToArray<T>(long offset, T[] buf, T[] testBuffer)
        {
            if (offset >= testBuffer.Length)
                return 0;

            long i;
            long copiedElements = 0;
            for (i = offset; i < buf.Length; ++i)
            {
                buf[i] = testBuffer[i];
                ++copiedElements;
            }
            return copiedElements;
        }

        [TestMethod]
        public void CheckArraySizeCorrectOverDefaultSize()
        {
            var resultBuf = ArrayReader.GetArray<byte>(TestReadFuncLarge, 0);

            Assert.AreEqual(LARGE_BUF_SIZE, resultBuf.Length);
        }
    }
}
