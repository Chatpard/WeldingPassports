using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        
        Task<int> DeleteTrainingCenterByEncryptedIDAsync(string encryptedID, CancellationToken token);

        SelectList TrainingCenterSelectList();

        Task<IPaginatedList<TrainingCenterIndexViewModel>> GetTrainingCentersIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder);
        
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
