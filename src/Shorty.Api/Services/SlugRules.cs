using System.Text.RegularExpressions;

namespace Shorty.Api.Services;

public static class SlugRules
{
    public static readonly HashSet<string> Reserved = new(StringComparer.OrdinalIgnoreCase)
    {
        "api", "health", "admin"
    };
    
    public static readonly Regex Allowed = new("^[a-z0-9-]{3,64}$", RegexOptions.Compiled);
    
    public static string Normalize(string slug) => slug.Trim().ToLowerInvariant();
    
    public static bool IsReserved(string slug) => Reserved.Contains(slug);
    
    public static bool IsValidFormat(string slug) => Allowed.IsMatch(slug);
}