using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Persistence.Repositories;

/// <summary>
/// Concrete repository cho Product entity với các query đặc thù.
/// </summary>
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower(), cancellationToken);

    public async Task<bool> IsNameTakenAsync(string name, CancellationToken cancellationToken = default)
        => await _dbSet.AnyAsync(p => p.Name.ToLower() == name.ToLower(), cancellationToken);

    public async Task<IEnumerable<Product>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default)
        => await _dbSet
            .Where(p => p.Category.ToLower() == category.ToLower())
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
}
