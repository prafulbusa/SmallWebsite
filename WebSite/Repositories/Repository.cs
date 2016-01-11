using System.Data.Entity.Infrastructure;
using RepositoriesFacade;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories
{
    public class Repository<T> : IRepository<T> where T:class 
    {
        private readonly DbContext context;

        public Repository(IContextFactory contextFactory)
        {
            context = contextFactory.GetDbContext();
        }

        public T Get(object id)
        {
            return context.Set<T>().Find(id);
        }

        public T GetIgnoreCache(object id)
        {
            DbEntityEntry<T> entry = context.Entry(context.Set<T>().Find(id));
            entry.State = EntityState.Detached;
            T e = context.Set<T>().Find(id);
            return e;
        }

        public IList<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T Create(T entity)
        {
            return context.Set<T>().Add(entity);
        }

        public T Update(T entity)
        {
            T updatedEntity = context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return updatedEntity;
        }

        public void Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                context.Set<T>().Attach(entity);
            }
            context.Set<T>().Remove(entity);
        }

        public void Delete(object id)
        {
            T entityToDelete = Get(id);
            Delete(entityToDelete);
        }

        public IQueryable<T> Query()
        {
            return context.Set<T>().AsQueryable();
        }

        public int Count()
        {
            return context.Set<T>().Count();
        }
        
        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }
}
