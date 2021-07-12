using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Boilerplate_.Net_Core_REST.Data.Models;

namespace Boilerplate_.Net_Core_REST.Business.Services.Interfaces
{
    public interface IBaseService<T> where T : BaseEntity
    {
        /// <summary>
        /// Get a selected entity by using a linq expression filter
        /// </summary>
        /// <param name="filter">linq expression filter</param>
        T GetSingle(Expression<Func<T, bool>> filter = null,
            string includeProperties = "");

        /// <summary>
        /// Get a selected entity by the object primary key ID
        /// </summary>
        /// <param name="id">Primary key ID</param>
        T GetSingleById(Guid id);

        /// <summary> 
        /// Add entity to the repository 
        /// </summary> 
        /// <param name="entity">the entity to add</param> 
        /// <returns>The added entity</returns> 
        T Add(T entity);

        /// <summary> 
        /// Add a list of entities to the repository 
        /// </summary> 
        /// <param name="entities">the entities to add</param> 
        void Add(List<T> entities);

        /// <summary> 
        /// Mark entity to be deleted within the repository 
        /// </summary> 
        /// <param name="entity">The entity to delete</param> 
        void Delete(T entity, bool permanentDelete = false);

        /// <summary>
        /// Delete range of entities
        /// </summary>
        /// <param name="entitiesToDelete"></param>
        void Delete(List<T> entitiesToDelete, bool permanentDelete = false);

        /// <summary>
        /// Mark entity to be deleted within the repository
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permanentDelete"></param>
        void DeleteById(Guid id, bool permanentDelete = false);

        /// <summary> 
        /// Updates entity within the the repository 
        /// </summary> 
        /// <param name="entity">the entity to update</param> 
        /// <returns>The updates entity</returns> 
        T Update(T entity);

        /// <summary> 
        /// Load the entities using a linq expression filter
        /// </summary> 
        /// <typeparam name="E">the entity type to load</typeparam> 
        /// <param name="where">where condition</param> 
        /// <returns>the loaded entity</returns> 
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, bool includeDisabled = false, string includeProperties = "");

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll(bool includeDisabled = false);


        /// <summary>
        /// Count using a filer
        /// </summary>
        int Count(Expression<Func<T, bool>> whereCondition, bool includeDisabled = false);

        /// <summary>
        /// All item count
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        int Count(bool includeDisabled = false);

        /// <summary>
        /// Save Changes
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Rollback Changes
        /// </summary>
        void Rollback();
    }
}