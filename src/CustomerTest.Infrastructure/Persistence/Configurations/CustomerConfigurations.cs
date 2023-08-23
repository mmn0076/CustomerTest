using CustomerTest.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerTest.Infrastructure.Persistence.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(nameof(Customer.Id));
            builder.Property(s => s.FirstName).HasMaxLength(100);
            builder.Property(s => s.LastName).HasMaxLength(100);
            builder.Property(s => s.Email).HasMaxLength(100);
            builder.Property(s => s.Address).HasMaxLength(1000);
            builder.HasIndex(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}