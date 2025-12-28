using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.core.Functional
{
    public class TenantContext
    {
        public int TenantId { get; set; }
        public string Schema { get; set; }
    }
}
