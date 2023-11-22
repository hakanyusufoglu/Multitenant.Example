using Microsoft.EntityFrameworkCore;
using Multitenant.Example.Application.Abstraction;
using Multitenant.Example.Domain.Concracts;
using Multitenant.Example.Domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace Multitenant.Example.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        readonly ITenantService _tenantService;
        string tenantId;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService) : base(options)
        {
            _tenantService = tenantService;
            tenantId = _tenantService.GetTenant()?.TenantId;
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasQueryFilter(p => p.TenantId == tenantId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenantConnectionString = _tenantService.GetConnectionString();
            if(!string.IsNullOrEmpty(tenantConnectionString))
            {
                var dbProvider = _tenantService.GetDatabaseProvider();
                if(dbProvider.ToLower()=="mssql")
                    optionsBuilder.UseSqlServer(tenantConnectionString);
            }
        }
        public async override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = tenantId;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
