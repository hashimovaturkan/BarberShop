using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BarberShop.Persistence;
using BarberShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace BarberShop.Persistence.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity, IActive
    {
        public readonly BarberShopDbContext DbContext;
        private DbSet<TEntity> Table => DbContext.Set<TEntity>();
        private DatabaseFacade Database => DbContext.Database;
        private IDbContextTransaction Transaction => Database.CurrentTransaction;

        public Repository(BarberShopDbContext dbContext)
            => DbContext = dbContext;

        /// <summary>
        /// Return active data by default
        /// </summary>
        /// <param name="includeActive"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> AsQueryable(bool includeActive = true) =>
            AddDeletedFilter(Table, !includeActive);

        public async Task BeginTransaction()
        {
            if (Transaction is null) await Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            if (Transaction is not null) await Database.CommitTransactionAsync();
        }

        public async Task Rollback()
        {
            if (Transaction is not null) await Database.RollbackTransactionAsync();
        }

        public async Task ToTransaction(Func<Task> action)
        {
            try
            {
                await BeginTransaction();
                await action.Invoke();
                await Commit();
            }
            catch (Exception e)
            {
                await Rollback();
                Console.WriteLine(e);
                throw;
            }
        }


        private async Task<int> SaveChangesAsync(bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            if (saveChanges)
                return await DbContext.SaveChangesAsync(cancellationToken);

            return default;
        }

        public async Task DeleteAsync(TEntity entity, bool saveChanges = false)
        {
            switch (entity)
            {
                case null:
                    throw new ArgumentNullException(nameof(entity));

                case IActive softDeletedEntity:
                    softDeletedEntity.IsActive = false;
                    await UpdateAsync(entity, saveChanges);
                    break;
            }
        }

        public async Task DeleteAsync(IList<TEntity> entities, bool saveChanges = false)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (entities.OfType<IActive>().Any())
            {
                foreach (var entity in entities)
                    if (entity is IActive softDeletedEntity)
                    {
                        softDeletedEntity.IsActive = false;
                        await UpdateAsync(entity);
                    }
            }

            await SaveChangesAsync(saveChanges);
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool saveChanges = false)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var entityList = await Table.Where(predicate).ToListAsync();
            entityList.ForEach(entity => entity.IsActive = false);

            await SaveChangesAsync(saveChanges);
        }

        public IQueryable<TEntity> GetAllQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            bool includeDeleted = true)
        {
            var query = AddDeletedFilter(Table.AsQueryable(), includeDeleted);
            query = func != null ? func(query) : query;

            return query;
        }


        protected virtual IQueryable<TEntity> AddDeletedFilter(IQueryable<TEntity> query, in bool includeDeleted)
        {
            if (includeDeleted)
                return query;

            query = query.Where(x => x.IsActive);

            return query;
        }

        public async Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            bool includeDeleted = true) =>
            await GetAllQuery(func, includeDeleted).ToListAsync();


        public Task<TEntity> GetByIdAsync(int? id, bool includeDeleted = true)
        {
            if (!id.HasValue || id == 0)
                return null;

            async Task<TEntity> getEntityAsync()
            {
                return await AddDeletedFilter(Table, includeDeleted)
                    .FirstOrDefaultAsync(entity => entity.Id == id.Value);
            }

            return getEntityAsync();
        }

        public async Task<IList<TEntity>> GetByIdsAsync(IList<int> ids, bool includeDeleted = true)
        {
            if (!ids?.Any() ?? true)
                return new List<TEntity>();

            async Task<IList<TEntity>> getByIdsAsync()
            {
                var query = AddDeletedFilter(Table, includeDeleted);

                //get entries
                var entries = await query.Where(entry => ids.Contains(entry.Id)).ToListAsync();

                //sort by passed identifiers
                var sortedEntries = new List<TEntity>();
                foreach (var id in ids)
                {
                    var sortedEntry = entries.FirstOrDefault(entry => entry.Id == id);
                    if (sortedEntry != null)
                        sortedEntries.Add(sortedEntry);
                }

                return sortedEntries;
            }

            return await getByIdsAsync();
        }

        public async Task InsertAsync(TEntity entity, bool saveChanges = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await Table.AddAsync(entity);
            await SaveChangesAsync(saveChanges);
        }

        public async Task InsertAsync(IList<TEntity> entities, bool saveChanges = false)
        {
            if (!entities?.Any() ?? true)
                throw new ArgumentNullException(nameof(entities));

            await Table.AddRangeAsync(entities);
            await SaveChangesAsync(saveChanges);
        }

        public async Task UpdateAsync(TEntity entity, bool saveChanges = false)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            switch (DbContext.Entry(entity).State)
            {
                case EntityState.Added:
                case EntityState.Deleted:
                    throw new InvalidOperationException("EntityState not valid for update");

                case EntityState.Detached:
                    Table.Update(entity);
                    break;

                case EntityState.Unchanged:
                case EntityState.Modified:
                    break;

                default:
                    throw new InvalidOperationException("EntityState has not value");
            }

            await SaveChangesAsync(saveChanges);
        }

        public async Task UpdateAsync(IList<TEntity> entities, bool saveChanges = false)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            if (!entities.Any())
                return;

            async Task UpdateAll()
            {
                foreach (var entity in entities)
                    await UpdateAsync(entity, saveChanges);
            }

            await (saveChanges ? ToTransaction(UpdateAll) : UpdateAll());
        }
    }
}