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
    public interface ITrainingCentersSQLRepository
    {
        IQueryable<TrainingCenterIndexViewModel> GetTrainingCentersIndex();

        Task<TrainingCenterDetailsViewModel> GetTrainingCenterDetailsAsync(string encryptedID);

        Task<TrainingCenterEditViewModel> GetTrainingCenterEditAsync(string encryptedID);

        Task<EntityEntry<TrainingCenter>> PostTrainingCenterCreateAsync(TrainingCenter trainingCenterChanges);

        EntityEntry<TrainingCenter> PostTrainingCenterEditAsync(TrainingCenter trainingCenterChanges);

        Task<int> DeleteTrainingCenterByEncryptedIDAsync(string encryptedID, CancellationToken token);

        SelectList TrainingCenterSelectList(int? trainingCenterId = null);

        Dictionary<int, char> LetterDictionary();

        Task<IPaginatedList<TrainingCenterIndexViewModel>> GetTrainingCentersIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder);

        Task<TrainingCenter> GetTrainingCenterByUserId(string userId);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
