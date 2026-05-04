namespace Marketplace.Api.Models;

/// <summary>Record representing query parameters for pagination and sorting.</summary>
public record QueryParams(
    int Page = 1,
    int PageSize = 10,
    string? SortBy = null,
    string? Order = "asc"
);
