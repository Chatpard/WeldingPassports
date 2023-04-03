using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Services.Persistence
{
    public class RowNumberExpression : SqlExpression
    {
        private readonly IReadOnlyCollection<SqlExpression> _orderBy;

        //public override ExpressionType NodeType => ExpressionType.Extension;
        public override Type Type => typeof(long);
        public override bool CanReduce => false;

        public RowNumberExpression(IReadOnlyCollection<SqlExpression> orderBy) : base(orderBy.FirstOrDefault().Type, orderBy.FirstOrDefault().TypeMapping)
        {
            //ICollection<SqlExpression> _orderByTemp = new Collection<SqlExpression>();
            //var operantVisitor = new OperandVisitor();
            //foreach (var orderByItem in orderBy)
            //{
            //    operantVisitor.Visit(orderByItem);
            //    var test = operantVisitor.Operand;
            //    _orderByTemp.Add(operantVisitor.Operand);
            //}
            //_orderBy = (IReadOnlyCollection<SqlExpression>) _orderByTemp;
            _orderBy = orderBy;
        }

        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            var visitedOrderBy = visitor.VisitExpressions((IReadOnlyList<SqlExpression>) _orderBy);

            if (ReferenceEquals(_orderBy, visitedOrderBy))
                return this;

            return new RowNumberExpression(visitedOrderBy);
        }

        protected override Expression Accept(ExpressionVisitor visitor)
        {
            if (!(visitor.GetType().FullName.Contains("SqlTypeMappingVerifyingExpressionVisitor")))
            {
                Expression myexp = base.Accept(visitor);
                return myexp;
            }
            else
            {
                return null;
            }

            visitor.Visit(new SqlFragmentExpression("ROW_NUMBER() OVER( ORDER BY "));

            RenderColumns(visitor, _orderBy);

            visitor.Visit(new SqlFragmentExpression(")"));

            return this;
        }

        private static void RenderColumns(ExpressionVisitor visitor, IEnumerable<Expression> columns)
        {
            var insertComma = false;

            foreach (var column in columns)
            {
                if (insertComma)
                    visitor.Visit(new SqlFragmentExpression(", "));

                visitor.Visit(column);
                insertComma = true;
            }

        }

        public override void Print(ExpressionPrinter expressionPrinter)
        {
            throw new NotImplementedException();
        }
    }
}
