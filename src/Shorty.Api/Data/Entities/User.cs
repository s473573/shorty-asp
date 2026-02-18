// id: guid
// name: str
// api_hash: later

namespace Shorty.Api.Data.Entities;

public sealed class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }

    // phase 2: api key auth
    public string? ApiKeyHash { get; set; }

    public ICollection<Link> Links { get; set; } = new List<Link>();
}
