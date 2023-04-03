using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Services.Persistence
{
    public static class DbFunctionsExtensions
    {
        // will throw at runtime because EF tries to translate DbFunctions as well
        public static long RowNumber(this DbFunctions _, object orderBy)
        {
            throw new InvalidOperationException();
        }

        public static long RowNumber(object orderBy)
        {
            throw new InvalidOperationException();
        }

        [DbFunction("Right", "dbo")]
        public static string Right(string input, int length)
        {
            throw new NotImplementedException();
        }
    }
}