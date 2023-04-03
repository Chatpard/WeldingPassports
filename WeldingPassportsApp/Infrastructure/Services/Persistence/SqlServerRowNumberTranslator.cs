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

namespace Infrastructure.Services.Persistence
{
    internal class SqlServerRowNumberTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _rowNumberMethod
              = typeof(DbFunctionsExtensions).GetMethod(
                        nameof(DbFunctionsExtensions.RowNumber),
                        new[] { typeof(DbFunctions), typeof(object) });
        

        public SqlServerRowNumberTranslator()
        {
            
        }

        public SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments)
        {
            if (method != _rowNumberMethod)
                return null;

            var orderByParams = ExtractParams(arguments[1]);

            return new RowNumberExpression(orderByParams);
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
    }
}
