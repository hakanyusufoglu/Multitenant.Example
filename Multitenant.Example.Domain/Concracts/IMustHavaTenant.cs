namespace Multitenant.Example.Domain.Concracts
{
    public interface IMustHaveTenant
    {
        public string TenantId { get; set; }
    }
}
