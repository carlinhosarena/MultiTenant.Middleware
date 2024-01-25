namespace MultiTenant.Middleware.Api.Abstractions
{
    public interface ITenantContext
    {
        ITenantInfo? Tenant { get; }

        bool HasResolved => Tenant != null;
    }
}
