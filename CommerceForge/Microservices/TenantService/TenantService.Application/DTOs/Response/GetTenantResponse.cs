using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Domain.Enums;

namespace TenantService.Application.DTOs.Response
{
    public record GetTenantResponse
    {
        public string Name { get; init; } = default!;
        public string Domain { get; init; } = default!;
        public string AdminEmail { get; init; } = default!;
        private string _plan = default!;
        private string _status = default!;
        public string Plan
        {
            get
            {
                return _plan;
            }
            init
            {
                if (!Enum.TryParse<Plan>(value, true, out _))
                {
                    throw new ArgumentException($"Invalid plan value: {value}");
                } 
                _plan = value;
            }
        }
        public string Status { get
            {
                return _status;
            }
            init
            {
                if (!Enum.TryParse<TenantStatus>(value, true, out _))
                {
                    throw new ArgumentException($"Invalid status value: {value}");
                }

                _status = value;
            }
        }
    }
}
