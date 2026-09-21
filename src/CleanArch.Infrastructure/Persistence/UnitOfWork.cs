using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Interfaces.Repositories;
using CleanArch.Infrastructure.Persistence.Repositories;

namespace CleanArch.Infrastructure.Persistence;

/// <summary>
/// Unit of Work implementation — quản lý transaction và tập hợp repositories.
/// Đảm bảo tất cả thay đổi được commit trong một atomic operation.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IProductRepository? _products;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    // Lazy initialization — chỉ tạo repository khi cần dùng
    public IProductRepository Products
        => _products ??= new ProductRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
        => _context.Dispose();
}
