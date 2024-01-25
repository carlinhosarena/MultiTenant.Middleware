namespace MultiTenant.Middleware.Api.Abstractions
{
    public interface ITenantResolver
    {
        Task<ITenantContext> ResolveAsync(HttpContext context);
    }
}
