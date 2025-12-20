using System.Linq.Expressions;

namespace Shared.Infrastructure.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        Task<IList<TEntity>> AddRangeAsync(IList<TEntity> entities);
        Task<int> CountAsyncFilter(Expression<Func<TEntity, bool>> filter);
        void Delete(TEntity entity);
        void DeleteRange(IList<TEntity> entities);
        Task<int> DeleteRangeAsync(IList<TEntity> entities);
        Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? page = null, int? pageSize = null);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        Task<TEntity?> GetByKey(int id);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Query();
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entities);
    }
}