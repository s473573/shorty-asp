namespace Shorty.Api.Dtos;

// link
// slug
// createdat?
// numclicks

public sealed class LinkDetailsResponse : LinkResponse
{
    public required long TotalClicks { get; init; }
}