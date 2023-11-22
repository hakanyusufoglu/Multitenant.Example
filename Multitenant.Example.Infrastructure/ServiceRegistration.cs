using Microsoft.Extensions.DependencyInjection;
using Multitenant.Example.Application.Abstraction;
using Multitenant.Example.Persistence.Concrete;

namespace Multitenant.Example.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection collection)
        {
            collection.AddTransient<ITenantService, TenantService>();
        }
    }
}
