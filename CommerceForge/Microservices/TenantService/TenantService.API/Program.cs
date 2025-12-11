using TenantService;
using TenantService.Infrastructure.DataAcess;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.AddNpgsqlDbContext<TenantDbContext>("TenantDb");

builder.Services.ConfigureServices(configuration);

var app = builder.Build();

app.ConfigurePipelines();