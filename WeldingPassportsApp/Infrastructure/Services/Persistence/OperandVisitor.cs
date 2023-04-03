using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Services.Persistence
{
    public class OperandVisitor : SqlExpressionVisitor
    {
        public SqlExpression Operand { get; private set; }

        protected override Expression VisitCase(CaseExpression caseExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitColumn(ColumnExpression columnExpression)
        {
            Operand = columnExpression;
            return Operand;
        }

        protected override Expression VisitCrossApply(CrossApplyExpression crossApplyExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitCrossJoin(CrossJoinExpression crossJoinExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitExcept(ExceptExpression exceptExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitExists(ExistsExpression existsExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitFromSql(FromSqlExpression fromSqlExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitIn(InExpression inExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitInnerJoin(InnerJoinExpression innerJoinExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitIntersect(IntersectExpression intersectExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitLeftJoin(LeftJoinExpression leftJoinExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitLike(LikeExpression likeExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitOrdering(OrderingExpression orderingExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitOuterApply(OuterApplyExpression outerApplyExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitProjection(ProjectionExpression projectionExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitRowNumber(Microsoft.EntityFrameworkCore.Query.SqlExpressions.RowNumberExpression rowNumberExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitSelect(SelectExpression selectExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitSqlBinary(SqlBinaryExpression sqlBinaryExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitSqlConstant(SqlConstantExpression sqlConstantExpression)
        {
            Operand = sqlConstantExpression;
            return sqlConstantExpression;
        }

        protected override Expression VisitSqlFragment(SqlFragmentExpression sqlFragmentExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitSqlFunction(SqlFunctionExpression sqlFunctionExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitSqlParameter(SqlParameterExpression sqlParameterExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitSqlUnary(SqlUnaryExpression sqlCastExpression)
        {
            Operand = sqlCastExpression.Operand;
            return sqlCastExpression;
        }

        protected override Expression VisitSubSelect(ScalarSubqueryExpression scalarSubqueryExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitTable(TableExpression tableExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitUnion(UnionExpression unionExpression)
        {
            throw new NotImplementedException();
        }
    }
}
