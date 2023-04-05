using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services.Persistence
{
    internal class SqlServerDbContextOptionsExtension : IDbContextOptionsExtension
    {
        public string LogFragment => "'RowNumberSupport'=true";

        public DbContextOptionsExtensionInfo Info => new SqlServerDbContextOptionsExtensionInfo(this);

        public void ApplyServices(IServiceCollection services)
        {
            services.AddSingleton<IMethodCallTranslatorPlugin, RelationalMethodCallTranslatorPlugin>();
        }

        public long GetServiceProviderHashCode()
        {
            return 0;
        }

        public void Validate(IDbContextOptions options)
        {
            
        }
    }
}
