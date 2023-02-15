using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Security
{
    public static class ClaimsValuesStore
    {
        public const string CanRead = "CanRead";
        public const string CanRevoke = "CanRevoke";
        public const string CanUpdate = "CanUpdate";
        public const string CanReadEdit = "CanReadEdit";
        public const string CanReadEditCreate = "CanReadEditCreate";
        public const string CanReadEditCreateDelete = "CanReadEditCreateDelete";
        public const string CanUpdateRevokeReadEditCreateDelete = "CanUpdateRevokeReadEditCreateDelete";

        public static List<string> PermissionsViews
        {
            get
            {
                return new List<string> { CanRead, CanRevoke, CanUpdate, CanReadEdit, CanReadEditCreate, CanReadEditCreateDelete, CanUpdateRevokeReadEditCreateDelete };
            }
        }
    }
}
