using System;
using System.Collections.Generic;
using System.Data;

namespace SimpleDAL.Database
{
    internal class FieldValueTypeMapper {
        private static readonly Dictionary<Type, Func<IDataRecord, int, object>> FieldValueTypeMappings = new Dictionary<Type, Func<IDataRecord, int, object>>()
        {
            { typeof(string), (record, ordinal) => record.GetString(ordinal) },
            { typeof(bool), (record, ordinal) => record.GetBoolean(ordinal) },
            { typeof(byte), (record, ordinal) => record.GetByte(ordinal) },
            { typeof(byte[]), (record, ordinal) => GetArray<byte>(record.GetBytes, ordinal) },
            { typeof(char), (record, ordinal) => record.GetChar(ordinal) },
            { typeof(char[]), (record, ordinal) => GetArray<char>(record.GetChars, ordinal) },
            { typeof(DateTime), (record, ordinal) => record.GetDateTime(ordinal) },
            { typeof(decimal), (record, ordinal) => record.GetDecimal(ordinal) },
            { typeof(double), (record, ordinal) => record.GetDouble(ordinal) },
            { typeof(float), (record, ordinal) => record.GetFloat(ordinal) },
            { typeof(Guid), (record, ordinal) => record.GetGuid(ordinal) },
            { typeof(short), (record, ordinal) => record.GetInt16(ordinal) },
            { typeof(int), (record, ordinal) => record.GetInt32(ordinal) },
            { typeof(long), (record, ordinal) => record.GetInt64(ordinal) }
        };

        private static T[] GetArray<T>(Func<int, long, T[], int, int, long> arrayFunc, int ordinal)
        {
            int bufSize = 1024;
            int bufPos = 0;
            T[] buf = new T[bufSize];
            long readBytes;
            long totalReadBytes = 0;

            do
            {
                readBytes = arrayFunc(ordinal, bufPos, buf, bufPos, buf.Length);
                totalReadBytes += readBytes;

                if (readBytes < bufSize)
                {
                    bufPos += bufSize;
                    bufSize = bufSize + bufSize;

                    buf = CreateExpandedBufferAndPopulate(bufSize, buf);
                }
            } while (readBytes > 0);

            buf = CreateExpandedBufferAndPopulate(totalReadBytes, buf);

            return buf;
        }

        private static T[] CreateExpandedBufferAndPopulate<T>(long bufSize, T[] buf)
        {
            T[] expandedBuf = new T[bufSize];

            buf.CopyTo(expandedBuf, 0);
            return expandedBuf;
        }

        public static object GetValue(Type type, IDataRecord record, int ordinal)
        {
            return FieldValueTypeMappings[type](record, ordinal);
        }
    }
}