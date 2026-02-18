using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shorty.Api.Data;
using Shorty.Api.Data.Entities;
using Shorty.Api.Dtos;
using Shorty.Api.Services;

[ApiController]
[Route("api/links")]
public sealed class LinksController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly ISlugGenerator _slugger;
    
    public LinksController(AppDbContext db, ISlugGenerator slugger)
    {
        _db = db;
        _slugger = slugger;
    }
    
    [HttpPost]
    public async Task<ActionResult<LinkResponse>> Create([FromBody] CreateLinkRequest request, CancellationToken ct)
    {
        if (!TryValidateTargetUrl(request.Url, out var normalizedUrl))
            return ValidationProblem("Provided Url must be an absolute http/s URL.");

        var user = await _db.Users.OrderBy(u => u.Name).FirstAsync(ct);

        var cSlug = request.CustomSlug;
        bool isSet = !string.IsNullOrWhiteSpace(cSlug);
        bool isValid = isSet && SlugRules.IsReserved(cSlug!) && SlugRules.IsValidFormat(cSlug!);

        if (isSet && !isValid) return ValidationProblem("CustomSlug must be an alphanumeric, hyphenated string");

        var slug = isSet && isValid ? SlugRules.Normalize(cSlug!) : _slugger.Generate(chunks:2);
        // todo: handle gen-collisions
        
        var link = new Link
        {
            UserId = user.Id,
            Slug = slug,
            Url = normalizedUrl,
        };
        
        _db.Links.Add(link);
        
        try
        {
            await _db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException e) when (DbErrors.IsUniqueConstraint(e))
        {
            return Conflict(new {message = "Slug already exists."});
        }
        
        return CreatedAtAction(nameof(GetById), new {id = link.Id }, ToDto(link));
    }
    
    [HttpGet]
    public async Task<ActionResult<LinkResponse>> GetById(Guid id, CancellationToken ct)
    {
        var link = await _db.Links.FirstOrDefaultAsync(l => l.Id == id, ct);
        if (link is null) return NotFound();
        return Ok(ToDto(link));
    }
    
    private static LinkResponse ToDto(Link l) => new()
    {
        Id = l.Id,
        Slug = l.Slug,
        Url = l.Url,
        IsActive = l.IsActive,
        CreatedAt = l.createdAt
    };
    
    private bool TryValidateTargetUrl(string u, out string normalized)
    {
        normalized = "";
        if (!Uri.TryCreate(u.Trim(), UriKind.Absolute, out var uri))
            return false;
        if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            return false;
        
        normalized = uri.ToString();
        return true;
    }
}