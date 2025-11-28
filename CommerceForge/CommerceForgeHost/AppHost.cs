var builder = DistributedApplication.CreateBuilder(args);



var postgres = builder.AddPostgres("CommerceForge").AddDatabase("TenantDb");
var usersApi = builder.AddProject<Projects.TenantService_API>("Tenants")
                      .WithReference(postgres);

await builder.Build().RunAsync();
