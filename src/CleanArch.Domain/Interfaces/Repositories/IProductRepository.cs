using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface cho Product entity với các query đặc thù.
/// </summary>
public interface IProductRepository : IGenericRepository<Product>
{
    /// <summary>Lấy product theo tên.</summary>
    Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>Kiểm tra xem tên product đã tồn tại chưa.</summary>
    Task<bool> IsNameTakenAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>Lấy danh sách products theo category.</summary>
    Task<IEnumerable<Product>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);
}
