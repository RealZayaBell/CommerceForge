using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Domain.Entities.Common
{
    public class SoftDeletable : Interface.ISoftDeletable
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime DeletedOn { get; set; }

    }
}
