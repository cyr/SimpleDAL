using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleDAL.Helpers
{
    [TestClass]
    public class ArrayReaderTests
    {
        private const long LARGE_BUF_SIZE = 2048;
        private const long SMALL_BUF_SIZE = 2;
        private const long DEFAULT_BUF_SIZE = 1024;

        [TestMethod]
        public void CheckArraySizeCorrectUnderDefaultSize()
        {
            var resultBuf = ArrayReader.GetArray<byte>(TestReadFuncSmall, 0);

            Assert.AreEqual(SMALL_BUF_SIZE, resultBuf.Length);
        }

        [TestMethod]
        public void CheckArraySizeCorrectOverDefaultSize()
        {
            var resultBuf = ArrayReader.GetArray<byte>(TestReadFuncLarge, 0);

            Assert.AreEqual(LARGE_BUF_SIZE, resultBuf.Length);
        }

        [TestMethod]
        public void CheckArraySizeCorrectExactlyDefaultSize()
        {
            var resultBuf = ArrayReader.GetArray<byte>(TestReadFuncDefault, 0);

            Assert.AreEqual(DEFAULT_BUF_SIZE, resultBuf.Length);
        }

        private long TestReadFuncDefault<T>(int ordinal, long fieldOffset, T[] buf, int bufOffset, int length)
        {
            T[] testBuffer = new T[DEFAULT_BUF_SIZE];

            return CopyToArray(fieldOffset, buf, testBuffer);
        }

        private long TestReadFuncLarge<T>(int ordinal, long fieldOffset, T[] buf, int bufOffset, int length)
        {
            T[] testBuffer = new T[LARGE_BUF_SIZE];

            return CopyToArray(fieldOffset, buf, testBuffer);
        }

        private long TestReadFuncSmall<T>(int ordinal, long fieldOffset, T[] buf, int bufOffset, int length)
        {
            T[] testBuffer = new T[SMALL_BUF_SIZE];

            testBuffer.CopyTo(buf, bufOffset);

            return testBuffer.Length;
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
    }
}
