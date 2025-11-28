namespace TenantService
{
    public static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();       
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();
        }

        public static void ConfigurePipelines(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {                
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
