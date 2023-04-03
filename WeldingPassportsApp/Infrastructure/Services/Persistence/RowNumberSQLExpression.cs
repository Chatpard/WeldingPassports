using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Infrastructure.Services.Persistence
{
    public class RowNumberSQLExpression
    {
        public RowNumberSQLExpression(Expression value, Expression format)
        {
            //Name = "FORMAT";
            //Arguments = new[] { value, format };
        }

        //public override void Print(ExpressionPrinter expressionPrinter)
        //{
        //    expressionPrinter.Append("FORMAT(");
        //    //expressionPrinter.Visit(Arguments[0]);
        //    expressionPrinter.Append(", ");
        //    //expressionPrinter.Visit(Arguments[1]);
        //    expressionPrinter.Append(")");
        //}
    }
}
