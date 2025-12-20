using AutoMapper;
using TenantService.Application.DTOs.Response;
using TenantService.Domain.Entities;
namespace TenantService.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tenant,GetTenantResponse>();
        }
    }
}
