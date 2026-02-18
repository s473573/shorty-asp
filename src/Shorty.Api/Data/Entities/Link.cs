// id
// url
// slug

namespace Shorty.Api.Data.Entities;

public sealed class Link {
    public Guid Id {get; set; } = Guid.NewGuid();

    public Guid UserId {get; set; }
    public User User {get; set; } = null!;

    public required string Slug {get; set; }
    public required string Url { get; set; }
    
    public bool IsActive {get; set; } = true;
    public DateTimeOffset createdAt {get; set; } = DateTimeOffset.UtcNow;
    
    public ICollection<Click> Clicks { get; set; } = new List<Click>();
}
