using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services.Persistence
{
    public class SqlServerMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public IEnumerable<IMethodCallTranslator> Translators { get;  }

        public SqlServerMethodCallTranslatorPlugin()
        {
            Translators = new List<IMethodCallTranslator>
            {
                new SqlServerRowNumberTranslator()
            };
        }
    }
}
