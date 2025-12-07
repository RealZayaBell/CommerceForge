using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Domain.Entities;

namespace TenantService.Infrastructure.DataAcess.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public TenantConfiguration() { }
        public void Configure(EntityTypeBuilder<Tenant> modelBuilider)
        {
            modelBuilider.HasKey(a => a.Id);
            modelBuilider.Property(a => a.Plan)
                .HasConversion<string>();
            modelBuilider.Property(a => a.Status)
                .HasConversion<string>();
        }
    }
}
