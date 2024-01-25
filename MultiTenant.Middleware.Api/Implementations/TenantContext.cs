using MultiTenant.Middleware.Api.Abstractions;

namespace MultiTenant.Middleware.Api.Implementations
{
    public class TenantContext : ITenantContext
    {
        public ITenantInfo? Tenant { get; set; }
    }
}
