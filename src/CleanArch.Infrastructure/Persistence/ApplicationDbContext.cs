using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanArch.Infrastructure.Persistence;

/// <summary>
/// EF Core DbContext chính của ứng dụng.
/// Tự động apply soft delete filter và update timestamp.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tự động apply tất cả IEntityTypeConfiguration trong assembly này
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Global soft delete query filter — tự động lọc các bản ghi đã bị soft delete
        modelBuilder.Entity<Product>().HasQueryFilter(e => !e.IsDeleted);
    }

    /// <summary>
    /// Override SaveChangesAsync để tự động set UpdatedAt timestamp.
    /// </summary>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
