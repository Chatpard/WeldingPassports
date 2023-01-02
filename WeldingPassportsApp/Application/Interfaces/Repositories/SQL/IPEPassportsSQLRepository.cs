using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface IPEPassportsSQLRepository
    {
        Task<IPaginatedList<PEPassportIndexViewModel>> GetPEPassportsIndexPaginatedAsync(int? trainingCenterID, int pageSize, int pageIndex, string searchString, string sortOrder);
        
        Task<EntityEntry<PEPassport>> PostPEPassportCreateAsync(PEPassport pePassort);
        
        Task<PEPassportDetailsViewModel> GetPEPassportDetailsAsync(string encryptedID);
        
        Task<PEPassportEditViewModel> GetPEPassportEditAsync(string encryptedID);
        
        EntityEntry<PEPassport> PostPEPassportEditasync(PEPassport pePassortChanges);

        Task<PEPassportUpdateViewModel> GetPEPassportUpdateAsync(string encryptedID);

        Task<int> DeletePEPassportByEncryptedIDAsync(string encryptedID, CancellationToken token);

        SelectList PEPassportSelectList();
        
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
