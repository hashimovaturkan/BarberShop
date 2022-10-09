using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BarberShop.Domain;

namespace BarberShop.Persistence.Infrastructure
{
      public interface IRepository<TEntity>
        where TEntity : BaseEntity,IActive
    {
        IQueryable<TEntity> AsQueryable(bool includeActive = false);

        Task BeginTransaction();

        Task Commit();

        Task Rollback();

        Task ToTransaction(Func<Task> action);

        /// <summary>
        /// Get the entity entry
        /// </summary>
        /// <param name="id">Entity entry identifier</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="IDeleted"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entry
        /// </returns>
        Task<TEntity> GetByIdAsync(int? id, bool includeDeleted = true);

        /// <summary>
        /// Get entity entries by identifiers
        /// </summary>
        /// <param name="ids">Entity entry identifiers</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="IDeleted"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        Task<IList<TEntity>> GetByIdsAsync(IList<int> ids, bool includeDeleted = true);

        /// <summary>
        /// Get all entity queryable
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="IDeleted"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        IQueryable<TEntity> GetAllQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            bool includeDeleted = true);
        
        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <param name="func">Function to select entries</param>
        /// <param name="includeDeleted">Whether to include deleted items (applies only to <see cref="IDeleted"/> entities)</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            bool includeDeleted = true);


        /// <summary>
        /// Insert the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertAsync(TEntity entity, bool saveChanges = false);

        /// <summary>
        /// Insert entity entries
        /// </summary>
        /// <param name="entities">Entity entries</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertAsync(IList<TEntity> entities, bool saveChanges = false);

        /// <summary>
        /// Update the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateAsync(TEntity entity, bool saveChanges = false);

        /// <summary>
        /// Update entity entries
        /// </summary>
        /// <param name="entities">Entity entries</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateAsync(IList<TEntity> entities, bool saveChanges = false);

        /// <summary>
        /// Delete the entity entry
        /// </summary>
        /// <param name="entity">Entity entry</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteAsync(TEntity entity, bool saveChanges = false);

        /// <summary>
        /// Delete entity entries
        /// </summary>
        /// <param name="entities">Entity entries</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteAsync(IList<TEntity> entities, bool saveChanges = false);

        /// <summary>
        /// Delete entity entries by the passed predicate
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the number of deleted records
        /// </returns>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool saveChanges = false);

        ///// <summary>
        ///// Truncates database table
        ///// </summary>
        ///// <param name="resetIdentity">Performs reset identity column</param>
        ///// <returns>A task that represents the asynchronous operation</returns>
        //Task TruncateAsync(bool resetIdentity = false);
    }
}