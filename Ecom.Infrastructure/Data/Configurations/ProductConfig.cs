using Ecom.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(300);
            builder.Property(propertyExpression: x => x.NewPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasData(new Product() { Id = 1, Name = "test", Description = "test", CategoryId = 1, NewPrice = 50 });


        }
    }
}
