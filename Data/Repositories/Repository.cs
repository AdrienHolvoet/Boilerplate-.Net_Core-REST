using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Boilerplate.Data.Contexts;
using Boilerplate.Data.Interfaces;
using Boilerplate.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace Boilerplate.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext _context;
        private DbSet<T> dbSet;
        public Repository(DatabaseContext context)
        {
            this._context = context;
            dbSet = context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return dbSet.AsQueryable();
        }

        public virtual IQueryable<T> GetQueryable(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!String.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query;
        }
        public T GetById(Guid id)
        {
            return dbSet.SingleOrDefault(s => s.Id == id);
        }
        public T Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            var currentDate = DateTime.Now;
            entity.CreatedAt = currentDate;
            entity.UpdatedAt = currentDate;

            dbSet.Add(entity);

            return entity;
        }

        public virtual void Insert(List<T> entities)
        {
            if (entities == null) throw new ArgumentNullException("entity");

            var currentDate = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.CreatedAt = currentDate;
                entity.UpdatedAt = currentDate;
            }


            dbSet.AddRange(entities);
        }

        public virtual T Update(T entityToUpdate)
        {

            if (entityToUpdate == null) throw new ArgumentNullException("entity");

            T entity = this.GetById(entityToUpdate.Id);  // You need to have access to key

            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.UpdatedAt = DateTime.Now;
            _context.Entry(entity).CurrentValues.SetValues(entityToUpdate);

            return entityToUpdate;
        }
        public virtual void Delete(Guid id)
        {
            T entity = dbSet.SingleOrDefault(s => s.Id == id);
            dbSet.Remove(entity);
        }

        public virtual void Delete(T entityToDelete)
        {
            dbSet.Remove(entityToDelete);
        }

        public virtual void Delete(List<T> entitiesToDelete)
        {
            dbSet.RemoveRange(entitiesToDelete);
        }

        public virtual int Count(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public virtual void Commit()
        { _context.SaveChanges(); }

        public virtual void Rollback()
        { _context.Dispose(); }
    }
}