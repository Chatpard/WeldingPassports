using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
using Application.Security;
using Application.SQLModels;
using Application.ViewModels;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    internal class UsersSQLRepository: IUsersSQLRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UsersSQLRepository(AppDbContext dbContext, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _dbContext=dbContext;
            _userManager=userManager;
            _roleManager=roleManager;
        }

        public IQueryable<UserIndexViewModel> GetUserIndex()
        {
            return _dbContext.CompanyContacts
                //.Include(user => _dbContext.Contacts)
                //.Include(user => _dbContext.Companies)
                //.Include(user => _userManager.Users)
                //.Include(user => _roleManager.Roles)
                .Select(u => new UserIndexViewModel
                {
                    FirstName = u.Contact.FirstName,
                    LastName = u.Contact.LastName,
                    CompanyName = u.Company.CompanyName,
                    Email = "",
                    RoleName = ""
                });
        }

        public async Task<IPaginatedList<UserIndexViewModel>> GetUserIndexPagination()
        {
            return await PaginatedList<UserIndexViewModel>.CreateAsync(GetUserIndex(), 1, 8);
        }

        public SelectList AppUsersSelectList(string AppUserID = null)
        {
            var users = _dbContext.AppUsers
                .Include(user => user.CompanyContact)
                .Where(user => user.CompanyContact == null || (AppUserID != null && user.Id == AppUserID));
            var userItems = users.Select(user => new AppUsersSelectListSQLModel
            {
                ID = user.Id,
                UserName = user.UserName,
            });
            return new SelectList(userItems, nameof(AppUsersSelectListSQLModel.ID), nameof(AppUsersSelectListSQLModel.UserName));
        }
    }
}