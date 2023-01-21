using Application.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface IExamCentersSQLRepository
    {
        IQueryable<ExamCenterIndexViewModel> GetExamCentersIndex();

        SelectList ExamCenterSelectList(int? trainingCenterID = null);

        Task<IPaginatedList<ExamCenterIndexViewModel>> GetExamCentersIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder);
    }
}
