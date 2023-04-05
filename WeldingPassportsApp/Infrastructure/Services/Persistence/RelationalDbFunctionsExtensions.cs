using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Services.Persistence
{
    public static class RelationalDbFunctionsExtensions
    {
        // will throw at runtime because EF tries to translate DbFunctions as well
        public static long RowNumber(this DbFunctions _, RowNumerOrderByClause orderBy)
        {
            throw new InvalidOperationException("This method is for us with Entity Framework Core only and has no in-memory implementation.");
        }

        public static long RowNumber(RowNumerOrderByClause orderBy)
        {
            throw new InvalidOperationException("This method is for us with Entity Framework Core only and has no in-memory implementation.");
        }

        public static RowNumerOrderByClause OrderBy<T>(this  DbFunctions _, T column)
        {
            throw new InvalidOperationException("This method is for us with Entity Framework Core only and has no in-memory implementation.");
        }
    }
}