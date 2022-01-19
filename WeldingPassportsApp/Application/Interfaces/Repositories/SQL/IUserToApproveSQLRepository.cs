using Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface IUserToApproveSQLRepository
    {
        Task<IEnumerable<UserToApprove>> GetAllUsersToApproveAsync();
        Task<IEnumerable<UserToApproveIndex>> GetUsersToApproveIndexAsync();
        Task<UserToApprove> GetUserToApproveByIDAsync(string UserToApproveID);
        EntityEntry<UserToApprove> InsertUserToApprove(UserToApprove userToApprove);
        Task<EntityEntry<UserToApprove>> DeleteUserToApproveAsync(int userToApproveID);
        EntityEntry<UserToApprove> UpdateUserToApprove(UserToApprove userToApproveChanges);
        Task<EntityEntry<CompanyContact>> InsertAppUserAsync(int userToApproveID, string role, CancellationToken cancellationToken);
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
