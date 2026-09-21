namespace CleanArch.Application.DTOs.Product;

/// <summary>
/// Request DTO để cập nhật sản phẩm (Partial update — chỉ field nào có giá trị mới được cập nhật).
/// </summary>
public class UpdateProductRequest
{
    /// <summary>Tên mới (null = không thay đổi).</summary>
    public string? Name { get; set; }

    /// <summary>Mô tả mới (null = không thay đổi).</summary>
    public string? Description { get; set; }

    /// <summary>Giá mới (null = không thay đổi).</summary>
    public decimal? Price { get; set; }

    /// <summary>Số lượng mới (null = không thay đổi).</summary>
    public int? Quantity { get; set; }

    /// <summary>Danh mục mới (null = không thay đổi).</summary>
    public string? Category { get; set; }
}
