using TenantService.Domain.Entities;

namespace TenantService.Infrastructure.Repositories
{
    public interface ITenantRepository
    {
        Task<Tenant> AddTenantAsync(Tenant tenant);
        Task<Tenant?> GetTenantAsync(int Id);
        Task<Tenant?> GetByDomainAsync(string domain);
        Task<Tenant> UpdateTenantAsync(Tenant tenant);
        Task SaveChangesAsync();
    }
}