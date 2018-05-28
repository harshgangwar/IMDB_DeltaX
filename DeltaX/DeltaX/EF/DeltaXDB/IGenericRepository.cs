using System;
using System.Collections.Generic;

namespace DeltaX.EF.DeltaXDB
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Func<T, bool> predicate = null);
        T Get(Func<T, bool> predicate);
        T Get(long id);
        void Add(T entity);
        void Attach(T entity);
        //void Delete(T entity);
    }
}