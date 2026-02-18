using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shorty.Api.Data;
using Shorty.Api.Data.Entities;
using Shorty.Api.Services;

namespace Shorty.Api.Controllers;

[ApiController]
public sealed class RedirectController : ControllerBase
{
    private readonly AppDbContext _db;

    public RedirectController(AppDbContext db) => _db = db;

    [HttpGet("{slug}")]
    public async Task<IActionResult> Go(string slug, CancellationToken ct)
    {
        slug = SlugRules.Normalize(slug);

        // prevents creating dead links like /health
        if (SlugRules.IsReserved(slug))
            return NotFound();

        var link = await _db.Links
            .AsTracking()
            .FirstOrDefaultAsync(l => l.Slug == slug && l.IsActive, ct);

        if (link is null) return NotFound();

        _db.Clicks.Add(new Click
        {
            LinkId = link.Id,
            ClickedAt = DateTimeOffset.UtcNow,
            UserAgent = Request.Headers.UserAgent.ToString(),
            Referer = Request.Headers.Referer.ToString()
        });

        await _db.SaveChangesAsync(ct);

        return Redirect(link.Url); // 302
    }
}
