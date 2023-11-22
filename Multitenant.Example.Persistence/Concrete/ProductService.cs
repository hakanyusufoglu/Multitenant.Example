using Microsoft.EntityFrameworkCore;
using Multitenant.Example.Application.Abstraction;
using Multitenant.Example.Domain.Entities;

namespace Multitenant.Example.Persistence.Concrete
{
    public class ProductService : IProductService
    {
        readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(string name, string description, int rate)
        {
            Product product = new(name,description, rate);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
