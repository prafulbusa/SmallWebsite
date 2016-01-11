using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RepositoriesFacade
{
    public interface IRepository<T>
    {
        T Get(object id);

        T GetIgnoreCache(object id);

         IList<T> GetAll();

        T Create(T entity);

        T Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        IQueryable<T> Query();

        int Count();

        void SaveChanges();
   }
}
