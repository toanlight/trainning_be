using CleanArch.Application.DTOs.Common;
using CleanArch.Application.DTOs.Product;

namespace CleanArch.Application.Interfaces.Services;

/// <summary>
/// Interface định nghĩa các use cases quản lý sản phẩm.
/// </summary>
public interface IProductService
{
    /// <summary>Lấy thông tin sản phẩm theo Id.</summary>
    Task<ProductDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Lấy danh sách sản phẩm có phân trang và tìm kiếm.</summary>
    Task<PagedResult<ProductDto>> GetAllAsync(PagedQuery query, CancellationToken cancellationToken = default);

    /// <summary>Tạo sản phẩm mới.</summary>
    Task<ProductDto> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken = default);

    /// <summary>Cập nhật thông tin sản phẩm.</summary>
    Task<ProductDto> UpdateAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default);

    /// <summary>Xóa mềm sản phẩm.</summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
