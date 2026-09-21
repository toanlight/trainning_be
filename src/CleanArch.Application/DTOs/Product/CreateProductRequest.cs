namespace CleanArch.Application.DTOs.Product;

/// <summary>
/// Request DTO để tạo sản phẩm mới.
/// </summary>
public class CreateProductRequest
{
    /// <summary>Tên sản phẩm (bắt buộc, tối đa 200 ký tự).</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Mô tả sản phẩm (tùy chọn).</summary>
    public string? Description { get; set; }

    /// <summary>Giá sản phẩm (phải lớn hơn 0).</summary>
    public decimal Price { get; set; }

    /// <summary>Số lượng tồn kho (không âm).</summary>
    public int Quantity { get; set; }

    /// <summary>Danh mục sản phẩm (bắt buộc, tối đa 100 ký tự).</summary>
    public string Category { get; set; } = string.Empty;
}
