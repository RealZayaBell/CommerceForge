var builder = DistributedApplication.CreateBuilder(args);



var postgres = builder.AddPostgres("CommerceForge")
    .WithDataVolume("TenantDbData")
    .AddDatabase("TenantDb"); 
var usersApi = builder.AddProject<Projects.TenantService_API>("TenantService")
                      .WithReference(postgres);

await builder.Build().RunAsync();
