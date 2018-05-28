using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DeltaX.EF.DeltaXDB
{
    class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DeltaXIMDBEntities2 entities;
        DbSet<T> _objectSet;

        public GenericRepository(DeltaXIMDBEntities2 _entities)
        {
            entities = _entities;
            _objectSet = entities.Set<T>();
        }

        public IEnumerable<T> GetAll(Func<T, bool> predicate = null)
        {
            if (predicate != null)
            {
                return _objectSet.Where(predicate);
            }

            return _objectSet.AsEnumerable();
        }

        public T Get(Func<T, bool> predicate)
        {
            return _objectSet.First(predicate);
        }

        public T Get(long id)
        {
            return _objectSet.Find(id);
        }

        public void Add(T entity)
        {
            _objectSet.Add(entity);
        }

        public void Attach(T entity)
        {
            _objectSet.Attach(entity);
        }
    }
}
