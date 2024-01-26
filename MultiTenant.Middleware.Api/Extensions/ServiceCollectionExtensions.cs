using MultiTenant.Middleware.Api.Abstractions;
using MultiTenant.Middleware.Api.Implementations;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMultTenant(this IServiceCollection services)
        {
            services.AddScoped<ITenantResolver, TenantResolver>();
            services.AddScoped<ITenantContext, TenantContext>();
            services.AddScoped<ITenantContext>(sp => (sp.GetRequiredService<IHttpContextAccessor>().HttpContext!.Items["TenantContext"]! as ITenantContext)!);
            return services;
        }
    }
}
