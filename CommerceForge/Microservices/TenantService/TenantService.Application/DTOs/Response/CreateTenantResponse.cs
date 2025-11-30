using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Application.DTOs.Response
{
    public record CreateTenantResponse(
        long TenantId,
        string ResourceUri,
        string Name,
        string Domain,
        string AdminEmail,
        string Plan
    )
    {
        public string GetTenantInfo()
        {
            return $"{Name} ({Domain}) - Admin: {AdminEmail}";
        }
    };
}
