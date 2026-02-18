using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Shorty.Api.Data;

public static class DbErrors
{
    public static bool IsUniqueConstraint(DbUpdateException ex)
        => ex.InnerException is SqliteException se && se.SqliteErrorCode == 19;
}
