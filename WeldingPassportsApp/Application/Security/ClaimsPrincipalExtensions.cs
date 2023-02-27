using Application.Interfaces.Repositories.SQL;
using Domain;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Security
{
    public static class ClaimsPrincipalExtensions
    {
        public static readonly string[] CanReadClaimValues = new string[] { ClaimsValuesStore.CanRead, ClaimsValuesStore.CanReadEdit, ClaimsValuesStore.CanReadEditCreate, ClaimsValuesStore.CanReadEditCreateDelete, ClaimsValuesStore.CanUpdateRevokeReadEditCreateDelete };
        public static readonly string[] CanRevokeClaimValues = new string[] { ClaimsValuesStore.CanRevoke, ClaimsValuesStore.CanUpdateRevokeReadEditCreateDelete };
        public static readonly string[] CanUpdateClaimValues = new string[] { ClaimsValuesStore.CanUpdate, ClaimsValuesStore.CanUpdateRevokeReadEditCreateDelete };
        public static readonly string[] CanEditClaimValues = new string[] { ClaimsValuesStore.CanReadEdit, ClaimsValuesStore.CanReadEditCreate, ClaimsValuesStore.CanReadEditCreateDelete, ClaimsValuesStore.CanUpdateRevokeReadEditCreateDelete };
        public static readonly string[] CanUpdateRevokeReadClaimValues = new string[] { ClaimsValuesStore.CanUpdate, ClaimsValuesStore.CanRevoke, ClaimsValuesStore.CanRead, ClaimsValuesStore.CanReadEdit, ClaimsValuesStore.CanReadEditCreate, ClaimsValuesStore.CanReadEditCreateDelete, ClaimsValuesStore.CanUpdateRevokeReadEditCreateDelete };
        public static readonly string[] CanUpdateRevokeEditClaimValues = new string[] { ClaimsValuesStore.CanUpdate, ClaimsValuesStore.CanRevoke, ClaimsValuesStore.CanReadEdit, ClaimsValuesStore.CanReadEditCreate, ClaimsValuesStore.CanReadEditCreateDelete, ClaimsValuesStore.CanUpdateRevokeReadEditCreateDelete };
        public static readonly string[] CanCreateClaimValues = new string[] { ClaimsValuesStore.CanReadEditCreate, ClaimsValuesStore.CanReadEditCreateDelete, ClaimsValuesStore.CanUpdateRevokeReadEditCreateDelete };
        public static readonly string[] CanDeleteClaimValues = new string[] { ClaimsValuesStore.CanReadEditCreateDelete, ClaimsValuesStore.CanUpdateRevokeReadEditCreateDelete };
        public static readonly string[] CanUpdateRevokeDeleteClaimValues = new string[] { ClaimsValuesStore.CanUpdate, ClaimsValuesStore.CanRevoke, ClaimsValuesStore.CanReadEditCreateDelete, ClaimsValuesStore.CanUpdateRevokeReadEditCreateDelete };

        public const string CanReadClaimsGroup = "CanRead";
        public const string CanRevokeClaimsGroup = "CanRevoke";
        public const string CanUpdateClaimsGroup = "CanUpdate";
        public const string CanEditClaimsGroup = "CanEdit";
        public const string CanUpdateRevokeReadClaimsGroup = "CanUpdateRevokeRead"; 
        public const string CanUpdateRevokeEditClaimsGroup = "CanUpdateRevokeEdit";
        public const string CanCreateClaimsGroup = "CanCreate"; 
        public const string CanDeleteClaimsGroup = "CanDelete";
        public const string CanUpdateRevokeDeleteClaimsGroup = "CanUpdateRevokeDelete";

        public static Dictionary<string, string[]> ClaimsGroups()
        { 
            return new Dictionary<string, string[]> { 
                { CanReadClaimsGroup, CanReadClaimValues },
                { CanRevokeClaimsGroup, CanRevokeClaimValues },
                { CanUpdateClaimsGroup, CanUpdateClaimValues },
                { CanEditClaimsGroup, CanEditClaimValues },
                { CanUpdateRevokeReadClaimsGroup, CanUpdateRevokeReadClaimValues },
                { CanUpdateRevokeEditClaimsGroup, CanUpdateRevokeEditClaimValues },
                { CanCreateClaimsGroup, CanCreateClaimValues },
                { CanDeleteClaimsGroup, CanDeleteClaimValues },
                { CanUpdateRevokeDeleteClaimsGroup, CanUpdateRevokeDeleteClaimValues }
            };
        }
        
        public static bool CanRead(this ClaimsPrincipal claimsPrincipal, string ClaimsType = "")
        {
            return CanReadClaimValues.Contains(claimsPrincipal.FindFirst(ClaimsType)?.Value);
        }

        public static bool CanRevoke(this ClaimsPrincipal claimsPrincipal, string ClaimsType = "")
        {
            return CanRevokeClaimValues.Contains(claimsPrincipal.FindFirst(ClaimsType)?.Value);
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

        public static async Task<string?> GetTrainingCenterID(this ClaimsPrincipal claimsPrincipal, UserManager<IdentityUser> userManager, ITrainingCentersSQLRepository trainingCentersSQLRepository, IDataProtector protector)
        {
            string userId = userManager.GetUserId(claimsPrincipal);
            TrainingCenter trainingCenter = await trainingCentersSQLRepository.GetTrainingCenterByUserId(userId);

            if (trainingCenter == null) return null;

            return Convert.ToString(trainingCenter.ID);
        }

        public static async Task<string?> GetExamCenterID(this ClaimsPrincipal claimsPrincipal, UserManager<IdentityUser> userManager, IExamCentersSQLRepository examCentersSQLRepository)
        {
            string userId = userManager.GetUserId(claimsPrincipal);
            ExamCenter examCenter = await examCentersSQLRepository.GetExamCenterByUserId(userId);

            if (examCenter == null) return null;

            return Convert.ToString(examCenter.ID);
        }
    }
}
