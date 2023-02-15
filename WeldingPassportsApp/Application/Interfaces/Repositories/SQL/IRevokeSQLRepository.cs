using Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface IRevokeSQLRepository : IAppDbContext
    {
        Task<EntityEntry<Revoke>> DeleteByCertificateEncryptedID(string certificateEncryptedID);
    }
}
