using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services.Persistence
{
    public class RelationalMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public IEnumerable<IMethodCallTranslator> Translators { get;  }

        public RelationalMethodCallTranslatorPlugin(ISqlExpressionFactory sqlExpressionFactory)
        {
            Translators = new List<IMethodCallTranslator>
            {
                new RationalRowNumberTranslator(sqlExpressionFactory)
            };
        }
    }
}
