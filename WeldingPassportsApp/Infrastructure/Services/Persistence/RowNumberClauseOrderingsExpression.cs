using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Persistence
{
    public sealed class RowNumberClauseOrderingsExpression : SqlExpression, INotNullableSqlExpression
    {
        

        public RowNumberClauseOrderingsExpression(IReadOnlyList<OrderingExpression> orderings) : base(typeof(RowNumerOrderByClause),RelationalTypeMapping.NullMapping)
        {
            Orderings=orderings ?? throw new ArgumentNullException(nameof(orderings));
        }

        public IReadOnlyList<OrderingExpression> Orderings { get; }

        protected override Expression Accept(ExpressionVisitor visitor)
        {
            if (visitor is QuerySqlGenerator)
                throw new NotSupportedException($"The EF function '{nameof(RelationalDbFunctionsExtensions.RowNumber)}' contains some expressions not supported by the Entity Framework. One of the reason is the creation of new objects like: 'new {{ e.MyPropery, e.MyOtherPropery }}'");

            return base.Accept(visitor);
        }

        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            var visited = visitor.VisitExpressions(Orderings);
            return ReferenceEquals(visited, Orderings) ? this : new RowNumberClauseOrderingsExpression(visited);
        }

        protected override void Print(ExpressionPrinter expressionPrinter)
        {
            ArgumentNullException.ThrowIfNull(expressionPrinter);
            expressionPrinter.VisitCollection(Orderings);
        }
    }
}
