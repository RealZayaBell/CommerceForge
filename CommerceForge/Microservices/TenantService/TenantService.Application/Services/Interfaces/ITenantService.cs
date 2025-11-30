using Shared.core.Functional;
using TenantService.Application.DTOs.Requests;
using TenantService.Application.DTOs.Response;

namespace TenantService.Application.Services.Interfaces
{
    public interface ITenantService
    {
        Task<ActionResponse<CreateTenantResponse>> CreateTenant(CreateTenantRequest tenant);
    }
}