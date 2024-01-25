using MultiTenant.Middleware.Api.Abstractions;

namespace MultiTenant.Middleware.Api.Implementations
{
    public class TenantInfo : ITenantInfo
    {
        public string? Id { get; set; }

        public string? Identifier { get; set; }

        public string? Name { get; set; }

        public string? ConnectionString { get; set; }
    }
}
