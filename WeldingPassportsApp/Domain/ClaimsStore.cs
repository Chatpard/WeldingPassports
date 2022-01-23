using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public static class ClaimsStore
    {
        public static string Scope { get { return "Scope"; } }
        public static string PEPassports { get { return "PEPassports"; } }
        public static string Welders { get { return "Welders"; } }
        public static string Examinations { get { return "Examinations"; } }
        public static string Certificates { get { return "Certificates"; } }

        public static List<string> Claims()
        {
            return new List<string> { PEPassports, Welders, Examinations, Certificates };
        }

        public static Dictionary<string, string> Claims(string role)
        {
            switch (role)
            {
                case RolesStore.Admin:
                    return new Dictionary<string, string>
                        {
                            {Scope, PermissionsStore.All },
                            {PEPassports, PermissionsStore.CanReadEditDelete },
                            {Welders, PermissionsStore.CanReadEditDelete },
                            {Examinations, PermissionsStore.CanReadEditDelete },
                            {Certificates, PermissionsStore.CanReadEditDelete }
                        };
                case RolesStore.TC:
                    return new Dictionary<string, string>
                        {
                            {Scope, PermissionsStore.Own },
                            {PEPassports, PermissionsStore.CanReadEditDelete },
                            {Welders, PermissionsStore.CanReadEditDelete },
                            {Examinations, PermissionsStore.CanReadEditDelete },
                            {Certificates, PermissionsStore.CanReadEditDelete }
                        };
                case RolesStore.DSO:
                    return new Dictionary<string, string>
                        {
                            {Scope, PermissionsStore.All },
                            {PEPassports, PermissionsStore.CanRead },
                            {Welders, PermissionsStore.CanRead },
                            {Examinations, PermissionsStore.CanRead },
                            {Certificates, PermissionsStore.CanRead }
                        };
                case RolesStore.EC:
                    return new Dictionary<string, string>
                        {
                            {Scope, PermissionsStore.Own },
                            {PEPassports, PermissionsStore.CanRead },
                            {Welders, PermissionsStore.CanRead },
                            {Examinations, PermissionsStore.CanRead },
                            {Certificates, PermissionsStore.CanReadEdit }
                        };
            }
            return new Dictionary<string, string>();
        }
    }
}
