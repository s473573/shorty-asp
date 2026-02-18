namespace Shorty.Api.Dtos;

// pagenum
// size?
// total
public sealed class PagedResponse<T>
{
    public required IReadOnlyList<T> Items { get; set; }
    public required int PageNum { get; init; }
    public required int PageSize { get; init; }
    public required int TotalCount { get; init; }
}