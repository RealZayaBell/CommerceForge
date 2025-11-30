using TenantService.Application.Services.Interfaces;
using TenantService.Application.Services.Implementation;
using TenantService.Infrastructure.Repositories;
using Shared.Infrastructure.Interface;
using Shared.Infrastructure.Implementation;
using TenantService.Infrastructure.DataAcess;

namespace TenantService.API.ServiceExtensions
{
    public static class ConfigurationExtension
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork<TenantDbCotext>>();
            services.AddScoped<ITenantService, TenantServices>();
            services.AddScoped<ITenantRepository, TenantRepository>();

        }
    }
}
