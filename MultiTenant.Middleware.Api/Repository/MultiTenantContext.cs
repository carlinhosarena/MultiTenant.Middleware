using Microsoft.EntityFrameworkCore;
using MultiTenant.Middleware.Api.Abstractions;
using MultiTenant.Middleware.Api.Model;

namespace MultiTenant.Middleware.Api.Repository
{
    public class MultiTenantContext : DbContext
    {
        private readonly ITenantContext _tenantContext;


        public MultiTenantContext(ITenantContext tenantContext)
        {
            _tenantContext = tenantContext;
        }

        public MultiTenantContext(ITenantContext tenantContext, DbContextOptions<MultiTenantContext> options) : base(options)
        {
            _tenantContext = tenantContext;
        }

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_tenantContext!.Tenant!.ConnectionString!);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .HasData(
                    new Product { Id = 1, Name = "Product 1" },
                    new Product { Id = 2, Name = "Product 2" },
                    new Product { Id = 3, Name = "Product 3" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
