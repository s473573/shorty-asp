namespace Shorty.Api.Dtos;

public sealed class CreateLinkRequest
{
    public required string Url { get; init; }
    public string? CustomSlug { get; init; }
}