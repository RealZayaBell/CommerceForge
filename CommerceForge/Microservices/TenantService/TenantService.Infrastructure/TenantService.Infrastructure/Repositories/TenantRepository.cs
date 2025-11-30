using Shared.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Domain.Entities;

namespace TenantService.Infrastructure.Repositories
{
    public class TenantRepository(IUnitOfWork unitOfWork) : ITenantRepository
    {
        private readonly IGenericRepository<Tenant> _tenantRepo = unitOfWork.Repository<Tenant>();

        public async Task<Tenant> AddTenantAsync(Tenant tenant)
        {
            return await _tenantRepo.AddAsync(tenant);
        }

        public async Task<Tenant?> GetByDomainAsync(string domain)
        {
            return await _tenantRepo.FindAsync(t => t.Domain == domain);
        }

        public async Task SaveChangesAsync()
        {
            await unitOfWork.Commit();
        }
    }

}