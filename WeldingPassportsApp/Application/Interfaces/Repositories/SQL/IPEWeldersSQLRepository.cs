using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface IPEWeldersSQLRepository
    {
        Task<EntityEntry<PEWelder>> PostPEWelderCreateAsync(PEWelder peWelder);

        Task<PEWelderDetailsViewModel> GetPEWelderDetailsAsync(string encryptedID);

        Task<PEWelderEditViewModel> GetPEWelderEditAsync(string encryptedID);

        EntityEntry<PEWelder> PutPEWelderUpdate(PEWelder peWelderChanges);

        Task<IPaginatedList<PEWelderIndexViewModel>> GetPEWeldersIndexPaginatedAsync(int? traingsCenterId, int pageSize, int pageIndex, string searchString, string sortOrder);
                
        Task<int> DeletePEWelderByEncryptedIDAsync(string encryptedID, CancellationToken token);

        SelectList PEWelderSelectList(int? trainingCenterID = null);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
