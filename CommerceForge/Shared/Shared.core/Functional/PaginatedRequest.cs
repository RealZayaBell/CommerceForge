using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.core.Functional
{
    public class PaginatedRequest<T> : PaginatedRequest where T : class
    {
        public T Value { get; set; }
    }

    public class PaginatedRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
