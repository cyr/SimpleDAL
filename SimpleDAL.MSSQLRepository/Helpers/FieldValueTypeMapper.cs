using System;
using System.Collections.Generic;
using System.Data;

namespace SimpleDAL.Helpers
{
    internal class FieldValueTypeMapper {
        private static readonly Dictionary<Type, Func<IDataRecord, int, object>> FieldValueTypeMappings = new Dictionary<Type, Func<IDataRecord, int, object>>()
        {
            { typeof(string), (record, ordinal) => record.GetString(ordinal) },
            { typeof(bool), (record, ordinal) => record.GetBoolean(ordinal) },
            { typeof(byte), (record, ordinal) => record.GetByte(ordinal) },
            { typeof(byte[]), (record, ordinal) => ArrayReader.GetArray<byte>(record.GetBytes, ordinal) },
            { typeof(char), (record, ordinal) => record.GetChar(ordinal) },
            { typeof(char[]), (record, ordinal) => ArrayReader.GetArray<char>(record.GetChars, ordinal) },
            { typeof(DateTime), (record, ordinal) => record.GetDateTime(ordinal) },
            { typeof(decimal), (record, ordinal) => record.GetDecimal(ordinal) },
            { typeof(double), (record, ordinal) => record.GetDouble(ordinal) },
            { typeof(float), (record, ordinal) => record.GetFloat(ordinal) },
            { typeof(Guid), (record, ordinal) => record.GetGuid(ordinal) },
            { typeof(short), (record, ordinal) => record.GetInt16(ordinal) },
            { typeof(int), (record, ordinal) => record.GetInt32(ordinal) },
            { typeof(long), (record, ordinal) => record.GetInt64(ordinal) }
        };

        public static object GetValue(Type type, IDataRecord record, int ordinal)
        {
            return FieldValueTypeMappings[type](record, ordinal);
        }
    }
}