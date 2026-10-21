

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p=> p.Id)//tp convert strong type Id to GUId primary time and vice versa 
                .HasConversion(productId => productId.Value,
                dbId => ProductId.Of(dbId));

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();


        }
    }
}
