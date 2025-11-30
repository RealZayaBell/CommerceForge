namespace Shared.Infrastructure.Interface
{
    public interface IUnitOfWork
    {
        Dictionary<Type, object> Repositories { get; }

        Task<int> Commit();
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        void Rollback();
        Task ExecuteInTransactionAsync(Func<Task> action);
    }
}