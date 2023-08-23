using CustomerTest.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerTest.Infrastructure.Persistence.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(nameof(Order.Id));
            builder
                .HasOne(e => e.Customer)
                .WithMany(e => e.Orders)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => x.Id);
            builder.HasIndex(x => x.CustomerId);
        }
    }
}