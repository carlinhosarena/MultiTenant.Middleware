using MultiTenant.Middleware.Api.Abstractions;
using System.Collections.Concurrent;

namespace MultiTenant.Middleware.Api.Implementations
{
    public class TenantResolver : ITenantResolver
    {
        private const string DefaultSectionName = "MultiTenant:Stores:Configuration";
        private ConcurrentDictionary<string, ITenantInfo>? tenantMap;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _section;

        public TenantResolver(IConfiguration configuration)
        {
            _configuration = configuration;
            _section = _configuration.GetSection(DefaultSectionName);

            if (!_section.Exists())
            {
                throw new Exception("Section name provided to the Configuration Store is invalid.");
            }

            UpdateTenantMap();
        }

        public async Task<ITenantContext> ResolveAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenant))
            {
                return new TenantContext();
            }

            var tenantInfo = await TryGetAsync(tenant);

            if (tenantInfo is null)
            {
                return new TenantContext();
            }

            return new TenantContext
            {
                Tenant = tenantInfo
            };
        }

        public Task<IEnumerable<ITenantInfo>> GetTenantsAsync()
        {
            return Task.FromResult(tenantMap?.Values.AsEnumerable() ?? Enumerable.Empty<ITenantInfo>());
        }

        private void UpdateTenantMap()
        {
            var newMap = new ConcurrentDictionary<string, ITenantInfo>(StringComparer.OrdinalIgnoreCase);
            var tenants = _section.GetSection("Tenants").GetChildren();

            foreach (var tenantSection in tenants)
            {
                var newTenant = _section.GetSection("Defaults").Get<ITenantInfo>(options => options.BindNonPublicProperties = true) ?? new TenantInfo();
                tenantSection.Bind(newTenant, options => options.BindNonPublicProperties = true);
                newMap.TryAdd(newTenant.Identifier!, newTenant);
            }

            tenantMap = newMap;
        }

        private async Task<ITenantInfo?> TryGetAsync(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await Task.FromResult(tenantMap?.Where(kv => kv.Value.Id == id).SingleOrDefault().Value);
        }

    }
}
