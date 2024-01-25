using MultiTenant.Middleware.Api.Middlewares;

namespace MultiTenant.Middleware.Api.Extensions
{
    public static class MultiTenantApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMultiTenant(this IApplicationBuilder builder)
           => builder.UseMiddleware<MultiTenantMiddleware>();
    }
}
