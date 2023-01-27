using Application.ViewModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface IExaminationsSQLRepository
    {
        Task ExaminationUpdateAsync(ExaminationEditViewModel newExaminationVm, CancellationToken cancellationToken);
        Task<ExaminationEditViewModel> GetExaminationEditAsync(string encryptedID);
        Task<EntityEntry<Examination>> PostExaminationCreateAsync(ExaminationCreateViewModel vm, CancellationToken cancellationToken);
        Task<ExaminationDetailsViewModel> GetExaminationDetailsAsync(string encrytedID);
        Task<ExaminationUpdateViewModel> GetExaminationUpdateAsync(string encrytedID);
        Task<int> DeleteExaminationByEncryptedIDAsync(string encryptedID, CancellationToken token);
        Task<IPaginatedList<ExaminationIndexViewModel>> GetExaminationsIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder, int? trainingCenterId, int? examCenterId);
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
