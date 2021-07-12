using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Interfaces;
using Boilerplate_REST.Data.Models;

namespace Boilerplate_REST.Business.Services.Implementations
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        #region Repository Setup        

        private IRepository<T> _repository;
        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        #endregion
        public T Add(T entity)
        {
            return _repository.Insert(entity);
        }

        public void Add(List<T> entities)
        {
            _repository.Insert(entities);
        }

        public void SaveChanges()
        {
            _repository.Commit();
        }

        public int Count(Expression<Func<T, bool>> filter, bool includeDisabled = false)
        {
            if (includeDisabled)
            {
                return _repository.GetQueryable(filter).Count();
            }
            return _repository.GetQueryable(filter).Where(x => x.IsEnabled).Count();
        }

        public int Count(bool includeDisabled = false)
        {
            if (includeDisabled)
            {
                return _repository.Count();
            }
            return _repository.GetQueryable(x => x.IsEnabled).Count();
        }

        public void Delete(T entity, bool permanentDelete = false)
        {
            if (permanentDelete)
            {
                _repository.Delete(entity);
            }
            else
            {
                //Soft Delete
                entity.IsEnabled = false;
                Update(entity);
            }
        }

        public void Delete(List<T> entitiesToDelete, bool permanentDelete = false)
        {
            foreach (var entity in entitiesToDelete)
            {
                this.Delete(entity, permanentDelete);
            }

        }

        public void DeleteById(Guid id, bool permanentDelete = false)
        {
            var entity = GetSingleById(id);
            Delete(entity, permanentDelete);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, bool includeDisabled = false, string includeProperties = "")
        {
            if (includeDisabled)
            {
                _repository.GetQueryable(filter, includeProperties);
            }
            return _repository.GetQueryable(filter, includeProperties).Where(x => x.IsEnabled);
        }

        public IList<T> GetAll(bool includeDisabled = false)
        {
            if (includeDisabled)
            {
                return _repository.GetAll().ToList();
            }
            return _repository.GetAll().Where(x => x.IsEnabled).ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> filter = null, bool includeDisabled = false, string includeProperties = "")
        {
            return this.Get(filter, includeDisabled, includeProperties).SingleOrDefault();
        }

        public T GetSingle(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public T GetSingleById(Guid id)
        {
            return this._repository.GetById(id);
        }

        public void Rollback()
        {
            _repository.Rollback();
        }

        public T Update(T entity)
        {
            return _repository.Update(entity);
        }
    }
}