using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Infrastructure.Services.Persistence
{
    public static class ModelBuilderExtensions
    {
        private static readonly MethodInfo _rowNumberMethod = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.RowNumber), new[] { typeof(object) });

        public static ModelBuilder AddRowNumberSupport(this ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(_rowNumberMethod)
                        .HasTranslation(expression =>
                        {
                            ReadOnlyCollection<SqlExpression> orderByParams = ExtractParams(expression.First());
                            return new RowNumberExpression(orderByParams);
                        });

            return modelBuilder;
        }

        private static ReadOnlyCollection<SqlExpression> ExtractParams(SqlExpression expression)
        {
            return new List<SqlExpression> { expression }.AsReadOnly();
        }
    }
}
