namespace MultiTenant.Middleware.Api.Abstractions
{
    public interface ITenantInfo
    {
        string? Id { get; set; }
        string? Identifier { get; set; }
        string? Name { get; set; }
        string? ConnectionString { get; set; }
    }
}
