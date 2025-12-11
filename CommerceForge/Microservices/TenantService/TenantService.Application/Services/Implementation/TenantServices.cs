using Microsoft.Extensions.Configuration;
using Npgsql;
using Shared.core.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.DTOs.Requests;
using TenantService.Application.DTOs.Response;
using TenantService.Application.Services.Interfaces;
using TenantService.Domain.Entities;
using TenantService.Domain.Enums;
using TenantService.Infrastructure.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TenantService.Application.Services.Implementation
{
    public class TenantServices(ITenantRepository tenantRepo, IConfiguration configuration) : ITenantService
    {
        public async Task<ActionResponse<CreateTenantResponse>> CreateTenant(CreateTenantRequest tenant)
        {
            var response = new ActionResponse<CreateTenantResponse>();
            try
            {
                var existing = await tenantRepo.GetByDomainAsync(tenant.Domain);
                if (existing != null)
                {
                    response = ActionResponse<CreateTenantResponse>.Failed("Domain already registered");
                    response.FailureReasons = ["Domain already registered"];
                    return response;
                }
                var newTenant = new Tenant
                {
                    Name = tenant.Name,
                    Domain = tenant.Domain,
                    AdminEmail = tenant.AdminEmail,
                    Plan = Enum.Parse<Plan>(tenant.Plan),
                    Schema = tenant.Domain.Split(".")[0]
                };
                using (var sqlWriter = new NpgsqlConnection(configuration.GetConnectionString("TenantDb")))
                {
                    sqlWriter.Open();
                    using var command = sqlWriter.CreateCommand();
                    command.CommandText = $"CREATE SCHEMA IF NOT EXISTS \"{newTenant.Schema}\";";
                    await command.ExecuteNonQueryAsync();
                }

                await tenantRepo.AddTenantAsync(newTenant);
                await tenantRepo.SaveChangesAsync();

                // TODO: Call Identity Service to create tenant admin user (I'll use memory message bus with a OnTenantCreationEvent)
                // Another TODO: Send welcome email to tenant admin (I'll use observer pattern)


                var tenantResponse = new CreateTenantResponse(
                newTenant.Id,
                $"https://{newTenant.Domain}/CommerceForge/{newTenant.Id}",
                newTenant.Name,
                newTenant.Domain,
                newTenant.AdminEmail,
                newTenant.Plan.ToString());

                response = ActionResponse<CreateTenantResponse>.Success(tenantResponse, "Tenant created successfully");
            }
            catch (Exception ex)
            {
                // Place logs here. I'll use serilog later for now, imma just choose consoel.writeline
                Console.WriteLine(ex.Message);
                response = ActionResponse<CreateTenantResponse>.Failed("An error occurred");
                response.FailureReasons = ["Server error"];
                return response;
            }
            return response;
        }
    }
}
