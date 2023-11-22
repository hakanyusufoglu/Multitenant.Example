using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Multitenant.Example.Application.Abstraction;
using Multitenant.Example.Application.Settings;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace Multitenant.Example.Persistence.Concrete
{
    public class TenantService : ITenantService
    {
        readonly TenantSettings _tenantSettings;
        readonly Tenant _tenant;
        readonly HttpContext _httpContext;

        public TenantService(IOptions<TenantSettings> tenantSettings, IHttpContextAccessor httpContextAccessor)
        {
            _tenantSettings=tenantSettings.Value;
            _httpContext = httpContextAccessor.HttpContext;
            if(_httpContext != null)
            {
                if(_httpContext.Request.Headers.TryGetValue("TenantId", out var tenantId))
                {
                    _tenant=_tenantSettings.Tenants.FirstOrDefault(x => x.TenantId == tenantId);
                    if(_tenant==null) throw new Exception("Invalid TenantId"); // Handle Exception

                    if (string.IsNullOrEmpty(_tenant.ConnectionString))
                        _tenant.ConnectionString = _tenantSettings.Defaults.ConnectionString;
                }
            }
        }
        public string GetConnectionString() =>_tenant?.ConnectionString;

        public string GetDatabaseProvider() => _tenantSettings.Defaults?.DbProvider;
        public Tenant GetTenant() => _tenant;
    }
}
