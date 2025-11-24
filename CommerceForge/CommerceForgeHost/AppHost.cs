var builder = DistributedApplication.CreateBuilder(args);



var postgres = builder.AddPostgres("db");
var usersApi = builder.AddProject<Projects.TenantService>("Tenants")
                      .WithReference(postgres);




builder.Build().Run();
