namespace CleanArch.Application.DTOs.Common;

/// <summary>
/// Generic paged result chứa data và metadata phân trang.
/// </summary>
/// <typeparam name="T">Kiểu dữ liệu của items.</typeparam>
public class PagedResult<T>
{
    /// <summary>Danh sách items của trang hiện tại.</summary>
    public IEnumerable<T> Items { get; private set; }

    /// <summary>Tổng số bản ghi.</summary>
    public int TotalCount { get; private set; }

    /// <summary>Số trang hiện tại.</summary>
    public int PageNumber { get; private set; }

    /// <summary>Số bản ghi mỗi trang.</summary>
    public int PageSize { get; private set; }

    /// <summary>Tổng số trang.</summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>Có trang trước không.</summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>Có trang tiếp theo không.</summary>
    public bool HasNextPage => PageNumber < TotalPages;

    private PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    /// <summary>Factory method tạo PagedResult.</summary>
    public static PagedResult<T> Create(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        => new(items, totalCount, pageNumber, pageSize);
}
