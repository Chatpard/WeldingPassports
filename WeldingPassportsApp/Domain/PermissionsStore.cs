using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public static class PermissionsStore
    {
        public static string All { get { return "All"; } }
        public static string Own { get { return "Own"; } }

        public static string CanRead { get { return "CanRead"; } }
        public static string CanReadEdit { get { return "CanEdit"; } }
        public static string CanReadEditDelete { get { return "CanDelete"; } }

        public static List<string> PermissionsViews
        {
            get
            {
                return new List<string> { CanRead, CanReadEdit, CanReadEditDelete };
            }
        }

        public static List<string> PermissionsScope
        {
            get
            {
                return new List<string> { All, Own };
            }
        }

    }
}
