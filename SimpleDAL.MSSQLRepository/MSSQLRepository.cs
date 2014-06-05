using System;
using System.Collections.Generic;

namespace SimpleDAL
{
    public class MSSQLRepository<T> : IRepository<T>
    {
        public IEnumerable<T> Select(Func<T, T> func)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Where(Func<T, bool> func)
        {
            throw new NotImplementedException();
        }

        public T Single(Func<T, bool> func)
        {
            throw new NotImplementedException();
        }

        public T SingleOrDefault(Func<T, bool> func)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Count(Func<T, bool> func)
        {
            throw new NotImplementedException();
        }
    }
}
