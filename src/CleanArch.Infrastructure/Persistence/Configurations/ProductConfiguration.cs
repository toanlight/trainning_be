using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core Fluent API configuration cho Product entity.
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Quantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(p => p.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Index để tìm kiếm nhanh theo tên (unique trong context active records)
        builder.HasIndex(p => p.Name)
            .HasDatabaseName("IX_Products_Name");

        // Index để filter theo category
        builder.HasIndex(p => p.Category)
            .HasDatabaseName("IX_Products_Category");

        // Index để filter soft delete
        builder.HasIndex(p => p.IsDeleted)
            .HasDatabaseName("IX_Products_IsDeleted");
    }
}
