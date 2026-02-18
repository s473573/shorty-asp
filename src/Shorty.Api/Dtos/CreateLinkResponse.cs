namespace Shorty.Api.Dtos;

public sealed class LinkResponse
{
    public required Guid Id { get; init; }
    public required string Slug { get; init; }
    public required string Url { get; init; }
    
    public bool IsActive { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}