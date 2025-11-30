using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Domain.Entities;
using static System.Net.WebRequestMethods;

namespace TenantService.Infrastructure.DataAcess
{
    public class TenantDbCotext(DbContextOptions<TenantDbCotext> options) : DbContext(options)
    {
        public DbSet<Tenant> Tenants { get; set; }

    }

}
