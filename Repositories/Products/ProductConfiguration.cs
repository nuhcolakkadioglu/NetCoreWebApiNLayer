using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repositories.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(builder => builder.Id);

        builder.Property(builder => builder.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(m => m.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(builder => builder.Stock)
         .IsRequired();
    }
}

