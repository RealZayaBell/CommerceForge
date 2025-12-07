using TenantService.Application.Services.Interfaces;
using TenantService.Application.Services.Implementation;
using TenantService.Infrastructure.Repositories;
using Shared.Infrastructure.Interface;
using Shared.Infrastructure.Implementation;
using TenantService.Infrastructure.DataAcess;
using Microsoft.EntityFrameworkCore;

namespace TenantService.API.ServiceExtensions
{
    public static class ConfigurationExtension
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<TenantDbContext>>();
            services.AddScoped<ITenantService, TenantServices>();
            services.AddScoped<ITenantRepository, TenantRepository>();
        }

        public static void AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetConnectionString("postgres") is not { Length: > 0 })
                services.AddDbContext<TenantDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("TenantConnection")));
        }


    }
}
