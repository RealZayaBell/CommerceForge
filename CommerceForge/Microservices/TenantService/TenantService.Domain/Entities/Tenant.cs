using Shared.core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Domain.Enums;

namespace TenantService.Domain.Entities
{
    public class Tenant : BaseEntitySoftDeletable
    {
        public string Name { get; set; } = default!;
        public string Domain { get; set; } = default!;
        public string AdminEmail { get; set; } = default!;
        public Plan Plan { get; set; } = Plan.Free;
        public TenantStatus Status { get; set; } = TenantStatus.Active;
    }
}
