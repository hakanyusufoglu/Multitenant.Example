using Multitenant.Example.Application.Settings;

namespace Multitenant.Example.Application.Abstraction
{
    public interface ITenantService
    {
        string GetDatabaseProvider();
        string GetConnectionString();
        Tenant GetTenant();
    }
}
