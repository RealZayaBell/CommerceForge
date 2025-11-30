using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.core.Abstractions.Entities
{
    public class SoftDeletable : ISoftDeletable
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime DeletedOn { get; set; }

    }
}
