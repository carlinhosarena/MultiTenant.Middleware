using MultiTenant.Middleware.Api.Abstractions;
using MultiTenant.Middleware.Api.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMultTenant(this IServiceCollection services)
        {
            services.AddScoped<ITenantResolver, TenantResolver>();
            return services;
        }
    }
}
