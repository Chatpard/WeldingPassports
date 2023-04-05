using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json.Schema;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Services.Persistence
{
    public sealed class RationalRowNumberTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _rowNumberMethod
              = typeof(RelationalDbFunctionsExtensions).GetMethod(
                        nameof(RelationalDbFunctionsExtensions.RowNumber),
                        new[] { typeof(DbFunctions), typeof(object) });
        
        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        public RationalRowNumberTranslator(ISqlExpressionFactory sqlExpressionFactory)
        {
            _sqlExpressionFactory=sqlExpressionFactory;
        }

        public SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            ArgumentNullException.ThrowIfNull(method);
            ArgumentNullException.ThrowIfNull(arguments);

            if (method.DeclaringType != typeof(Microsoft.EntityFrameworkCore.RelationalDbFunctionsExtensions))
                return null;

            switch (method.Name)
            {
                case nameof(RelationalDbFunctionsExtensions.OrderBy):
                {
                    var orderby = arguments.Skip(1).Select(e => new OrderingExpression(_sqlExpressionFactory.ApplyDefaultTypeMapping(e), true)).ToList();
                    return new RowNumberClauseOrderingsExpression(orderby);
                }
                case nameof(RelationalDbFunctionsExtensions.RowNumber):
                {
                    var partitionBy = arguments.Skip(1).Take(arguments.Count()-2).Select(e => _sqlExpressionFactory.ApplyDefaultTypeMapping(e)).ToList();
                    var orderings = (RowNumberClauseOrderingsExpression)arguments[^1];
                    return new RowNumberExpression(partitionBy, orderings.Orderings, RelationalTypeMapping.NullMapping);
                }
                default:
                    throw new InvalidOperationException($"Unexpected method '{method.Name}' in '{nameof(RelationalDbFunctionsExtensions)}'");
            }
        }

        //public Expression SqlTranslate(MethodCallExpression methodCallExpression)
        //{
        //    if (methodCallExpression.Method != _rowNumberMethod)
        //        return null;

        //    var orderByParam = ExtractParams(methodCallExpression.Arguments[1]);

        //    return new RowNumberExpression(orderByParam);
        //}

        private static ReadOnlyCollection<SqlExpression> ExtractParams(SqlExpression parameter)
        {
            if (parameter is SqlConstantExpression constant
                && constant.Value is IEnumerable<SqlExpression> enumerable)
            {
                return enumerable.ToList().AsReadOnly();
            }

            return new List<SqlExpression> { parameter }.AsReadOnly();
        }

        public SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments)
        {
            throw new NotImplementedException();
        }
    }
}
