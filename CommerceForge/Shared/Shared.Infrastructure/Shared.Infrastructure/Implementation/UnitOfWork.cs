using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Implementation
{
    public class UnitOfWork<TContext>(TContext dbContext) : IUnitOfWork where TContext : DbContext
    {
        public Dictionary<Type, object> Repositories { get; } = new();

        public async Task<int> Commit()
        {
            return await dbContext.SaveChangesAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (Repositories.TryGetValue(typeof(TEntity), out var repo))
            {
                return (IGenericRepository<TEntity>)repo;
            }

            var newRepo = new GenericRepository<TEntity, TContext>(dbContext);
            Repositories.Add(typeof(TEntity), newRepo);
            return newRepo;
        }

        public void Rollback()
        {
            foreach (var entry in dbContext.ChangeTracker.Entries())
            {
                entry.Reload();                
            }
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                await action();
                await Commit();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

}
