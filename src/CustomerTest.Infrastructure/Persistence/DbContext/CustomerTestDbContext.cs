using CustomerTest.Domain;
using CustomerTest.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace CustomerTest.Infrastructure.Persistence.DbContext
{
    public class CustomerTestDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly PublishDomainEventInterceptor _domainEventInterceptor;

        public CustomerTestDbContext(DbContextOptions options, PublishDomainEventInterceptor domainEventInterceptor) :
            base(options)
        {
            _domainEventInterceptor = domainEventInterceptor;
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_domainEventInterceptor);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(CustomerTestDbContext).Assembly);
        }
    }
}