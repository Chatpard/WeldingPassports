using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Security
{
    public static class ClaimsPrincipalExtensions
    {
        public static readonly string[] CanReadClaimValues = new string[] { ClaimsValuesStore.CanRead, ClaimsValuesStore.CanReadUpdate, ClaimsValuesStore.CanReadEdit, ClaimsValuesStore.CanReadEditCreate, ClaimsValuesStore.CanReadEditCreateDelete };
        public static readonly string[] CanUpdateClaimValues = new string[] { ClaimsValuesStore.CanReadUpdate };
        public static readonly string[] CanEditClaimValues = new string[] { ClaimsValuesStore.CanReadEdit, ClaimsValuesStore.CanReadEditCreate, ClaimsValuesStore.CanReadEditCreateDelete };
        public static readonly string[] CanCreateClaimValues = new string[] { ClaimsValuesStore.CanReadEditCreate, ClaimsValuesStore.CanReadEditCreateDelete };
        public static readonly string[] CanDeleteClaimValues = new string[] { ClaimsValuesStore.CanReadEditCreateDelete };

        public const string CanReadClaimsGroup = "CanRead";
        public const string CanUpdateClaimsGroup = "CanUpdate";
        public const string CanEditClaimsGroup = "CanEdit";
        public const string CanCreateClaimsGroup = "CanCreate"; 
        public const string CanDeleteClaimsGroup = "CanDelete";

        public static Dictionary<string, string[]> ClaimsGroups()
        { 
            return new Dictionary<string, string[]> { 
                { CanReadClaimsGroup, CanReadClaimValues },
                { CanUpdateClaimsGroup, CanUpdateClaimValues },
                { CanEditClaimsGroup, CanEditClaimValues },
                { CanCreateClaimsGroup, CanCreateClaimValues },
                { CanDeleteClaimsGroup, CanDeleteClaimValues } 
            };
        }
        
        public static bool CanRead(this ClaimsPrincipal claimsPrincipal, string ClaimsType = "")
        {
            return CanReadClaimValues.Contains(claimsPrincipal.FindFirst(ClaimsType)?.Value);
        }

        public static bool CanUpdate(this ClaimsPrincipal claimsPrincipal, string ClaimsType = "")
        {
            return CanUpdateClaimValues.Contains(claimsPrincipal.FindFirst(ClaimsType)?.Value);
        }

        public static bool CanEdit(this ClaimsPrincipal claimsPrincipal, string ClaimsType = "" )
        {
            return CanEditClaimValues.Contains(claimsPrincipal.FindFirst(ClaimsType)?.Value);
        }

        public static bool CanCreate(this ClaimsPrincipal claimsPrincipal, string ClaimsType = "")
        {
            return CanCreateClaimValues.Contains(claimsPrincipal.FindFirst(ClaimsType)?.Value);
        }

        public static bool CanDelete(this ClaimsPrincipal claimsPrincipal, string ClaimsType = "")
        {
            return CanDeleteClaimValues.Contains(claimsPrincipal.FindFirst(ClaimsType)?.Value);
        }
    }
}
