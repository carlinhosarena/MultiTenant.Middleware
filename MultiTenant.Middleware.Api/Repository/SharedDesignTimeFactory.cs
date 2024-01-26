using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using MultiTenant.Middleware.Api.Implementations;

namespace MultiTenant.Middleware.Api.Repository
{
    public class SharedDesignTimeFactory : IDesignTimeDbContextFactory<MultiTenantContext>
    {
        public MultiTenantContext CreateDbContext(string[] args)
        {
            var tenantInfo = new TenantInfo { ConnectionString = "Data Source=Data/SharedIdentity.db" };
            var tenantContext = new TenantContext { Tenant = tenantInfo };
            var optionsBuilder = new DbContextOptionsBuilder<MultiTenantContext>();

            return new MultiTenantContext(tenantContext, optionsBuilder.Options);
        }
    }
}
