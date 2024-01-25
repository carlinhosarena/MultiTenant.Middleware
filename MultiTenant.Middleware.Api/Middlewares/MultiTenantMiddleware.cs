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
            var tenant = context.Request.Headers["X-Tenant-Id"];
            
            if (string.IsNullOrEmpty(tenant))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Tenant header is missing");
                return;
            }

            var service = context.RequestServices.GetRequiredService<ITenantResolver>();
            var tenantContext = await service.ResolveAsync(context);

            context.Items["TenantContext"] = tenantContext;

            await _next(context);
        }
    }
}
