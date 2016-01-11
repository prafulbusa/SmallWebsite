using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RepositoriesFacade;
using ServicesFacade;

namespace Services
{
    public abstract class Service<T> : IService<T>
    {
        protected abstract IRepository<T> Repository { get; }

        public T Get(object id)
        {
            return Repository.Get(id);
        }

        public T GetIgnoreCache(object id)
        {
            return Repository.GetIgnoreCache(id);
        }

        public IList<T> GetAll()
        {
            return Repository.GetAll();
        }

        public T Create(T entity)
        {
            return Repository.Create(entity);
        }

        public T Update(T entity)
        {
            return Repository.Update(entity);
        }

        public void Delete(T entity)
        {
            Repository.Delete(entity);
        }

        public void Delete(object id)
        {
            Repository.Delete(id);
        }

        public IQueryable<T> Query()
        {
            return Repository.Query();
        }

        public int Count()
        {
            return Repository.Count();
        }

        public void SaveChanges()
        {
            Repository.SaveChanges();
        }
    }
}
