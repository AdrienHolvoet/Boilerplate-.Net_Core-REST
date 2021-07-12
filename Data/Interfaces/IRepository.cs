using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Boilerplate_.Net_Core_REST.Data.Models;

namespace Boilerplate_.Net_Core_REST.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T GetById(Guid id);
        T Insert(T entity);
        void Insert(List<T> entity);
        T Update(T entity);
        void Delete(Guid id);
        void Delete(T entity);
        void Delete(List<T> entity);

        /// <summary>
        /// Returns the total number of entities that satisfy the given filter
        /// </summary>
        /// <param name="filter">Lambda expression determining the filter to be applied on the entities (ex. student => student.LastName == "Smith")</param>
        /// <returns>number of entities</returns>
        int Count(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// Returns a Queryable of entities which match the given filter, ordered by the given 
        /// </summary>
        /// <param name="filter">Lambda expression determining the filter to be applied on the entities (ex. student => student.LastName == "Smith")</param>
        /// <param name="includeProperties">Comma-delimited list of navigation properties for eager loading</param>
        /// <returns>Unique entity</returns>
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> filter = null, string includeProperties = "");

        void Commit();
        void Rollback();
    }
}