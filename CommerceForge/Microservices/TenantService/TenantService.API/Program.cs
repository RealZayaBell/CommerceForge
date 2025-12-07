using TenantService;
using TenantService.Infrastructure.DataAcess;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<TenantDbContext>("TenantDb");

builder.Services.ConfigureServices();

var app = builder.Build();

app.ConfigurePipelines();