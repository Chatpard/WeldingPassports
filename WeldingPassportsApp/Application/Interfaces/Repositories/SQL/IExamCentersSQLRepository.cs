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
    public interface IExamCentersSQLRepository
    {
        IQueryable<ExamCenterIndexViewModel> GetExamCentersIndex();

        SelectList ExamCenterSelectList();

        Task<IPaginatedList<ExamCenterIndexViewModel>> GetExamCentersIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder);

        Task<ExamCenterDetailsViewModel> GetExamCentersDetailsAsync(string encryptedID);

        EntityEntry<ExamCenter> PostExamCentersEdit(ExamCenter examCenterChanges);

        EntityEntry<ExamCenter> PostExamCentersCreate(ExamCenter newExamCenter);

        Task<ExamCenterEditViewModel> GetExamCentersEdit(string encryptedID);

        Task<int> DeleteExamCenterByEncryptedIDasync(string encryptedID, CancellationToken cancellationToken);

        Task<ExamCenter> GetExamCenterByUserId(string userId);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
