using System;

namespace SimpleDAL.Helpers
{
    internal class ArrayReader {
        public static T[] GetArray<T>(Func<int, long, T[], int, int, long> arrayFunc, int ordinal)
        {
            int bufSize = 1024;
            int bufPos = 0;
            T[] buf = new T[bufSize];
            long totalReadBytes = 0;
            bool needsExpanding = false;

            do
            {
                long readBytes = arrayFunc(ordinal, bufPos, buf, bufPos, buf.Length);
                totalReadBytes += readBytes;

                needsExpanding = totalReadBytes == bufSize;

                if (needsExpanding)
                {
                    bufPos += bufSize;
                    bufSize = bufSize + bufSize;

                    buf = CreateExpandedBufferAndPopulate(bufSize, buf);
                }
            } while (needsExpanding);

            buf = CreateExpandedBufferAndPopulate(totalReadBytes, buf);

            return buf;
        }

        private static T[] CreateExpandedBufferAndPopulate<T>(long bufSize, T[] buf)
        {
            T[] resizedBuf = new T[bufSize];

            FillArrayFromDifferentSizedArray(buf, resizedBuf);

            return resizedBuf;
        }

        private static void FillArrayFromDifferentSizedArray<T>(T[] buf, T[] resizedBuf)
        {
            var endBufPos = buf.Length > resizedBuf.Length ? resizedBuf.Length : buf.Length;

            for (int i = 0; i < endBufPos; ++i)
            {
                resizedBuf[i] = buf[i];
            }
        }
    }
}