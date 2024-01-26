using MultiTenant.Middleware.Api.Abstractions;

namespace MultiTenant.Middleware.Api.Middlewares
{
    public class MultiTenantMiddleware
    {
        private readonly RequestDelegate _next;

        public MultiTenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var service = context.RequestServices.GetRequiredService<ITenantResolver>();
            var tenantContext = await service.ResolveAsync(context);

            if (tenantContext.Tenant is null && !context.Response.HasStarted)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Tenant header is invalid");
                return;
            }

            if (!context.Response.HasStarted)
            {
                context.Items["TenantContext"] = tenantContext;
            }

            await _next(context);
        }
    }
}
