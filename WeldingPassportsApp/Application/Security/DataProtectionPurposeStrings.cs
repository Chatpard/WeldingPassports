using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Security
{
    public interface IDataProtectionPurposeStrings
    {
        string IdRouteValue { get; }
    }

    public class DataProtectionPurposeStrings : IDataProtectionPurposeStrings
    {
        private readonly IConfiguration _config;

        public DataProtectionPurposeStrings(IConfiguration config)
        {
            _config = config;
        }

        public string IdRouteValue
        { 
            get
            {
                return _config["IdRouteValue"];
            }
        }
    }
}
