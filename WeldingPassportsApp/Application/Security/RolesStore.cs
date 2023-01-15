using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Security
{
    public static class RolesStore
    {
        public const string Admin = "Admin";
        public const string TC = "TC";
        public const string DSO = "DSO";
        public const string EC = "EC";

        public static List<string> Roles
        {
            get
            {
                return new List<string> { Admin, TC, DSO, EC };
            }
        }
        public static Dictionary<string, string> ViewRoles
        {
            get 
            {
                return new Dictionary<string, string>
                {
                    { Admin, "Admin" },
                    { TC, "Training Center" },
                    { DSO, "Distribution System Operator" },
                    { EC, "Exam Center" }
                };
            }
        }
    }
}
