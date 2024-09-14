using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.Products;

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

