using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Application.DTOs.Requests
{
    public record UpdateTenantRequest(
[MinLength(3)]
string? Name,

[RegularExpression(@"^[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
string? Domain,

[EmailAddress]
string? AdminEmail,

[MinLength(2)]
string? AdminFirstname,

[MinLength(2)]
string? AdminLastname
);
}
