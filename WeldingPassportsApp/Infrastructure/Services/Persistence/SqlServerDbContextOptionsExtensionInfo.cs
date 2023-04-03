using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Infrastructure.Services.Persistence
{
    public class SqlServerDbContextOptionsExtensionInfo : DbContextOptionsExtensionInfo
    {
        public SqlServerDbContextOptionsExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
        {
        }

        public override bool IsDatabaseProvider => false;

        public override string LogFragment => "Using SqlServer Extension";

        public override int GetServiceProviderHashCode()
        {
            return 0;
        }

        public override void PopulateDebugInfo([NotNullAttribute] IDictionary<string, string> debugInfo)
        {
            
        }

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
        {
            return true;
        }
    }
}
