using Multitenant.Example.Domain.Entities;

namespace Multitenant.Example.Application.Abstraction
{
    public interface IProductService
    {
        Task<Product> CreateAsync(string name, string description, int rate);
        Task<Product> GetByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetAllAsync();
    }
}
