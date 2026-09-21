using CleanArch.Domain.Interfaces.Repositories;

namespace CleanArch.Domain.Interfaces;

/// <summary>
/// Unit of Work pattern — quản lý transaction và tập hợp tất cả repositories.
/// Đảm bảo tất cả thay đổi được lưu trong một atomic operation.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }

    /// <summary>
    /// Lưu tất cả thay đổi vào database trong một transaction.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
