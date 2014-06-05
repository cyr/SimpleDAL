using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SimpleDAL.Database
{
    internal class MSSQLRow : IList<MSSQLField>
    {
        private readonly List<MSSQLField> _fields;

        public MSSQLRow(IEnumerable<MSSQLField> fields)
        {
            _fields = fields.ToList();
        }

        public static IEnumerable<MSSQLRow> CreateFromReader(IDataReader dataReader)
        {
            while (dataReader.Read())
            {
                yield return new MSSQLRow(GetFieldsFromReader(dataReader));
            }
        }

        private static IEnumerable<MSSQLField> GetFieldsFromReader(IDataRecord dataRecord)
        {
            for (int i = 0; i < dataRecord.FieldCount; ++i)
            {
                yield return MSSQLFieldFactory.CreateField(dataRecord, i);
            }
        }

        public IEnumerator<MSSQLField> GetEnumerator()
        {
            return _fields.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(MSSQLField item)
        {
            _fields.Add(item);
        }

        public void Clear()
        {
            _fields.Clear();
        }

        public bool Contains(MSSQLField item)
        {
            return _fields.Contains(item);
        }

        public void CopyTo(MSSQLField[] array, int arrayIndex)
        {
            _fields.CopyTo(array, arrayIndex);
        }

        public bool Remove(MSSQLField item)
        {
            return _fields.Remove(item);
        }

        public int Count
        {
            get { return _fields.Count; }  
        }

        public bool IsReadOnly
        {
            get { return (_fields as ICollection<MSSQLField>).IsReadOnly; }
        }

        public int IndexOf(MSSQLField item)
        {
            return _fields.IndexOf(item);
        }

        public void Insert(int index, MSSQLField item)
        {
            _fields.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _fields.RemoveAt(index);
        }

        public MSSQLField this[int index]
        {
            get { return _fields[index]; }
            set { _fields[index] = value; }
        }
    }
}