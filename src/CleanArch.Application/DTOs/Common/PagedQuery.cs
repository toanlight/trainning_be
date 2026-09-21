namespace CleanArch.Application.DTOs.Common;

/// <summary>
/// Query parameters cho phân trang và tìm kiếm.
/// </summary>
public class PagedQuery
{
    private int _pageNumber = 1;
    private int _pageSize = 10;

    /// <summary>Số trang hiện tại (bắt đầu từ 1).</summary>
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    /// <summary>Số bản ghi trên mỗi trang (tối đa 100).</summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 100 ? 100 : value < 1 ? 10 : value;
    }

    /// <summary>Từ khóa tìm kiếm (optional).</summary>
    public string? SearchTerm { get; set; }

    /// <summary>Sắp xếp theo field (optional).</summary>
    public string? SortBy { get; set; }

    /// <summary>Sắp xếp giảm dần.</summary>
    public bool SortDescending { get; set; } = false;
}
