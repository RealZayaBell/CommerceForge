using TenantService.Domain.Entities;

namespace TenantService.Infrastructure.Repositories
{
    public interface ITenantRepository
    {
        Task<Tenant> AddTenantAsync(Tenant tenant);
        Task<Tenant?> GetByDomainAsync(string domain);
        Task SaveChangesAsync();
    }
}