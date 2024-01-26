using Microsoft.EntityFrameworkCore;
using MultiTenant.Middleware.Api.Abstractions;
using MultiTenant.Middleware.Api.Extensions;
using MultiTenant.Middleware.Api.Implementations;
using MultiTenant.Middleware.Api.Repository;

namespace MultiTenant.Middleware.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MultiTenantContext>();
            services.AddHttpContextAccessor();
            services.AddMultTenant();
            services.AddControllers();
   
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var tenantResolver = scope.ServiceProvider.GetService<ITenantResolver>();

                if (tenantResolver != null)
                {
                    var tenants = tenantResolver.GetTenantsAsync().Result;

                    foreach (var tenant in tenants)
                    {
                        using var db = new MultiTenantContext(new TenantContext { Tenant = tenant });
                        db.Database.Migrate();
                    }
                }
            }

            app.UseMultiTenant();

            app.UseRouting();

            if (env.IsDevelopment())
            {

            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
