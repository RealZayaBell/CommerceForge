using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Application.DTOs.Requests;

public record CreateTenantRequest
{
    public record CreateTenantDto(
    [Required]
    [MinLength(3)]
    string Name,

    [Required]
    [RegularExpression(@"^[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    string Domain,

    [Required]
    [EmailAddress]
    string AdminEmail,

    [Required]
    [MaxLength(50)]
    string Plan
    );
}
