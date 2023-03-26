using Application.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface IUsersSQLRepository
    {
        Task<IPaginatedList<UserIndexViewModel>> GetUserIndexPagination();

        SelectList AppUsersSelectList(string AppUserID = null);
    }
}
