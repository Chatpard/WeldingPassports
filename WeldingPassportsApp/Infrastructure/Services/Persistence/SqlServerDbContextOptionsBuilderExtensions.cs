using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Infrastructure.Services.Persistence
{
    public static class SqlServerDbContextOptionsBuilderExtensions
    {
        public static SqlServerDbContextOptionsBuilder AddRowNumberSupport(
            this SqlServerDbContextOptionsBuilder sqlServerOptionsBuilder  )
        {
            var infrastructure = (IRelationalDbContextOptionsBuilderInfrastructure) sqlServerOptionsBuilder;

            var builder = (IDbContextOptionsBuilderInfrastructure) infrastructure.OptionsBuilder;

            var extension = infrastructure.OptionsBuilder.Options
                .FindExtension<SqlServerDbContextOptionsExtension>()
                ?? new SqlServerDbContextOptionsExtension();
            
            builder.AddOrUpdateExtension(extension);

            return sqlServerOptionsBuilder;
        }
    }
}
