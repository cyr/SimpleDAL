using System;
using System.Collections.Generic;
using System.Data;

namespace SimpleDAL.Database
{
    internal class MSSQLFieldFactory
    {
        public static MSSQLField CreateField(IDataRecord record, int ordinal)
        {
            Type type = Type.GetType(record.GetDataTypeName(ordinal));

            return new MSSQLField
            {
                Value = GetValue(type, record, ordinal),
                Type = type
            };
        }

        private static object GetValue(Type type, IDataRecord record, int ordinal)
        {
            return FieldValueTypeMapper.GetValue(type, record, ordinal);
        }
    }
}