using System;
using System.Collections.Generic;

namespace SimpleDAL.Repository
{
    public interface IRepository<T> {
        IEnumerable<T> Select(Func<T, T> func);
        IEnumerable<T> Where(Func<T, bool> func);
        T Single(Func<T, bool> func);
        T SingleOrDefault(Func<T, bool> func);
        int Count();
        int Count(Func<T, bool> func);
    }
}