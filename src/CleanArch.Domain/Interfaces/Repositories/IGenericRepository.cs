using System.Linq.Expressions;
using CleanArch.Domain.Entities.Common;

namespace CleanArch.Domain.Interfaces.Repositories;

/// <summary>
/// Generic repository interface định nghĩa các thao tác CRUD cơ bản.
/// </summary>
/// <typeparam name="T">Entity type kế thừa từ BaseEntity.</typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    /// <summary>Lấy entity theo Id.</summary>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Lấy tất cả entities (chưa bị soft delete).</summary>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>Tìm entities theo điều kiện.</summary>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>Kiểm tra tồn tại theo điều kiện.</summary>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>Đếm số lượng entities.</summary>
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);

    /// <summary>Thêm entity mới.</summary>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>Thêm nhiều entities.</summary>
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>Cập nhật entity.</summary>
    void Update(T entity);

    /// <summary>Xóa cứng entity.</summary>
    void Remove(T entity);

    /// <summary>Xóa mềm entity (đặt IsDeleted = true).</summary>
    void SoftDelete(T entity);
}
