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
        private readonly IGenericRepository<Tenant> _tenants = unitOfWork.Repository<Tenant>();

        public async Task<Tenant> AddTenantAsync(Tenant tenant)
        {
            return await _tenants.AddAsync(tenant);
        }

        public async Task<Tenant?> GetTenantAsync(int Id)
        {
            return await _tenants.GetByKey(Id);
        }


        public async Task<Tenant?> GetByDomainAsync(string domain)
        {
            return await _tenants.FindAsync(t => t.Domain == domain);
        }

        public async Task<Tenant> UpdateTenantAsync(Tenant tenant)
        {
            return await _tenants.UpdateAsync(tenant);
        }

        public async Task SaveChangesAsync()
        {
            await unitOfWork.Commit();
        }
    }

}