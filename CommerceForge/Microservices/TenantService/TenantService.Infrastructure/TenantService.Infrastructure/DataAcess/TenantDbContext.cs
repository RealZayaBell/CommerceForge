using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Domain.Entities;
using TenantService.Infrastructure.DataAcess.Configurations;
using static System.Net.WebRequestMethods;

namespace TenantService.Infrastructure.DataAcess
{
    public class TenantDbContext(DbContextOptions<TenantDbContext> options) : DbContext(options)
    {
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new TenantConfiguration());
        }
    }    
}
