using Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface IUserToApproveRepository
    {
        Task<IEnumerable<UserToApprove>> GetAllUsersToApproveAsync();
        Task<IEnumerable<UserToApproveIndex>> GetUsersToApproveIndexAsync();
        Task<UserToApprove> GetUserToApproveByEncryptedIDAsync(string UserToApproveID);
        Task<UserToApprove> GetUserToApproveByEmailAsync(string email);
        EntityEntry<UserToApprove> InsertUserToApprove(UserToApprove userToApprove);
        Task<EntityEntry<UserToApprove>> DeleteUserToApproveByEncryptedIDAsync(string userToApproveEncryptedID);
        Task<EntityEntry<UserToApprove>> DeleteUserToApproveByIDAsync(int userToApproveID);
        EntityEntry<UserToApprove> UpdateUserToApprove(UserToApprove userToApproveChanges);
        Task<AppUser> InsertAppUserByEncryptedIDAsync(string userToApproveEncryptedID, string role, CancellationToken cancellationToken);
        UserToApprove EncryptUserToApproveID(UserToApprove userToApproveEntityEntry);
        Task<string> GetUserToApproveEmailLanguageByEncryptedIDAsync(string userToApproveEncryptedID);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
