using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Shared.core.Abstractions.Entities;
using Shared.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Implementation
{
    public class GenericRepository<TEntity, TContext>(TContext context) : IGenericRepository<TEntity> where TEntity : class
    where TContext : DbContext
    {
        private IEntityType GetEntityType()
        {
            return context.Model.FindEntityType(typeof(TEntity));
        }

        #region Add Methods
        public TEntity Add(TEntity entity)
        {
            HandleSoftDeleteAdd(entity);
            context.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (await HandleSoftDeleteAddAsync(entity))
                return entity;

            await context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        private void HandleSoftDeleteAdd(TEntity entity)
        {
            if (entity is not ISoftDeletable) return;

            var key = GetEntityType().FindPrimaryKey().Properties[0];
            var keyValue = key.PropertyInfo.GetValue(entity);

            var existing = context.Set<TEntity>()
                .IgnoreQueryFilters()
                .FirstOrDefault(e => EF.Property<object>(e, key.Name).Equals(keyValue));

            if (existing is ISoftDeletable softDeleted && softDeleted.IsDeleted)
            {
                softDeleted.IsDeleted = false;
                context.Entry(existing).CurrentValues.SetValues(entity);
            }
        }

        private async Task<bool> HandleSoftDeleteAddAsync(TEntity entity)
        {
            if (entity is not ISoftDeletable)
                return false;

            var key = GetEntityType().FindPrimaryKey().Properties[0];
            var keyValue = key.PropertyInfo.GetValue(entity);

            var existing = await context.Set<TEntity>()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => EF.Property<object>(e, key.Name).Equals(keyValue));

            if (existing is ISoftDeletable softDeleted && softDeleted.IsDeleted)
            {
                softDeleted.IsDeleted = false;
                context.Entry(existing).CurrentValues.SetValues(entity);
                return true;
            }

            if (existing != null)
                throw new InvalidOperationException("Entity already exists.");

            return false;
        }
        public async Task<IList<TEntity>> AddRangeAsync(IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (!await HandleSoftDeleteAddAsync(entity))
                    await context.Set<TEntity>().AddAsync(entity);
            }

            return entities;
        }
        #endregion

        #region Update Methods
        // Methods for Update
        public TEntity Update(TEntity entity)
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        #endregion
        public async Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entities)
        {
            context.Set<TEntity>().UpdateRange(entities);
            return entities;
        }

        #region Delete Methods
        // For Delete
        public void Delete(TEntity entity)
        {
            if (entity is ISoftDeletable soft)
            {
                soft.IsDeleted = true;
                context.Update(entity);
            }
            else
            {
                context.Set<TEntity>().Remove(entity);
            }
        }

        public void DeleteRange(IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity is ISoftDeletable soft)
                    soft.IsDeleted = true;
                else
                    context.Set<TEntity>().Remove(entity);
            }
        }

        public async Task<int> DeleteRangeAsync(IList<TEntity> entities)
        {
            bool soft = entities.Any(e => e is ISoftDeletable);

            if (soft)
            {
                foreach (var entity in entities.OfType<ISoftDeletable>())
                {
                    entity.IsDeleted = true;
                }
            }
            else
            {
                context.Set<TEntity>().RemoveRange(entities);
            }

            return await Task.FromResult(0);
        }

        #endregion

        #region Querying
        // Querying

        public IQueryable<TEntity> Query()
            => context.Set<TEntity>();

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
            => context.Set<TEntity>().Where(predicate);

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
            => await context.Set<TEntity>().SingleOrDefaultAsync(match);

        public async Task<List<TEntity>> FilterAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> q = context.Set<TEntity>();

            if (filter != null)
                q = q.Where(filter);

            foreach (var include in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                q = q.Include(include.Trim());

            if (orderBy != null)
                q = orderBy(q);

            if (page.HasValue && pageSize.HasValue)
                q = q.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return await q.ToListAsync();
        }

        public async Task<int> CountAsyncFilter(Expression<Func<TEntity, bool>> filter)
            => await context.Set<TEntity>().CountAsync(filter);

        #endregion
    }
}
