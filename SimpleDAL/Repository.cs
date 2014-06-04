using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleDAL
{
    public class Repository<T> {
        private readonly T[] _source;

        public Repository(T[] source)
        {
            _source = source;
        }

        public IEnumerable<T> Select(Func<T, T> func)
        {
            return _source.Select(func);
        }

        public IEnumerable<T> Where(Func<T, bool> func)
        {
            return _source.Where(func);
        }

        public T Single(Func<T, bool> func)
        {
            return _source.Single(func);
        }

        public T SingleOrDefault(Func<T, bool> func)
        {
            return _source.SingleOrDefault(func);
        }

        public int Count()
        {
            return _source.Count();
        }

        public int Count(Func<T, bool> func)
        {
            return _source.Count(func);
        }
    }
}
