using Microsoft.AspNetCore.StaticAssets;
using Microsoft.EntityFrameworkCore;
using Shorty.Api.Data.Entities;

namespace Shorty.Api.Data;

public static class DbSeeder {
    public static async Task SeedAsync(AppDbContext db, CancellationToken ct = default) {
        if (!await db.Users.AnyAsync(ct)) {
            db.Users.Add(new User { Name = "local-dev" });
            await db.SaveChangesAsync(ct);
        }
    }
}