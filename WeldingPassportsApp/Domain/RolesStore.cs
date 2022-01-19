using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public static class RolesStore
    {
        public static string Admin { get { return "Admin"; } }
        public static string TC { get { return "TC"; } }
        public static string DSO { get { return "DSO"; } }
        public static string EC { get { return "EC"; } }
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
