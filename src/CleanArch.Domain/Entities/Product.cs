using CleanArch.Domain.Entities.Common;

namespace CleanArch.Domain.Entities;

/// <summary>
/// Entity đại diện cho một sản phẩm trong hệ thống.
/// </summary>
public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; } = string.Empty;
}
