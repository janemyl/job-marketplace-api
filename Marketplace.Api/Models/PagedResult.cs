namespace Marketplace.Api.Models;

/// <summary>Represents a paginated result set of items.</summary>
/// <typeparam name="T">The type of items in the result set.</typeparam>
public class PagedResult<T>
{
    /// <summary>Gets or sets the collection of items in the current page.</summary>
    public IEnumerable<T> Items { get; set; } = [];
    /// <summary>Gets or sets the total count of items across all pages.</summary>
    public int TotalCount { get; set; }
    /// <summary>Gets or sets the current page number.</summary>
    public int Page { get; set; }
    /// <summary>Gets or sets the size of each page.</summary>
    public int PageSize { get; set; }
    /// <summary>Gets the total number of pages.</summary>
    public int TotalPages => (TotalCount + PageSize - 1) / PageSize;
}
