using Multitenant.Example.Domain.Concracts;

namespace Multitenant.Example.Domain.Entities
{
    public class Product : BaseEntity, IMustHaveTenant
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Rate { get; private set; }
        public string TenantId { get; set; }
        public Product()
        {
        }
        public Product(string name, string description, int rate)
        {
            Name = name;
            Description = description;
            Rate = rate;
        }
    }
}
