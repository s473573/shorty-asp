// id
// link
// date
// tracking

namespace Shorty.Api.Data.Entities;

public sealed class Click {
   public long Id { get; set; }
   public Guid LinkId { get; set; }
   public Link Link { get; set; } = null!;
   
   public DateTimeOffset ClickedAt { get; set; } = DateTimeOffset.UtcNow;
   
   public string? UserAgent { get; set; }
   public string? Referer { get; set; }
}