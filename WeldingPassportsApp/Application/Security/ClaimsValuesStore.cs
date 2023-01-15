using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Security
{
    public static class ClaimsValuesStore
    {
        public const string CanRead = "CanRead";
        public const string CanReadUpdate = "CanReadUpdate";
        public const string CanReadEdit = "CanReadEdit";
        public const string CanReadEditCreate = "CanReadEditCreate";
        public const string CanReadEditCreateDelete = "CanReadEditCreateDelete";

        public static List<string> PermissionsViews
        {
            get
            {
                return new List<string> { CanRead, CanReadUpdate, CanReadEdit, CanReadEditCreate, CanReadEditCreateDelete };
            }
        }
    }
}
