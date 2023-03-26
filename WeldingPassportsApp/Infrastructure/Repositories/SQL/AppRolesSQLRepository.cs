using Application.Interfaces.Repositories.SQL;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.SQLModels;

namespace Infrastructure.Repositories.SQL
{
    public class AppRolesSQLRepository: IAppRolesSQLRepository
    {
        private readonly AppDbContext _context;

        public AppRolesSQLRepository(AppDbContext context)
        {
            _context = context;
        }

        public SelectList RoleNamesSelectList()
        {
            var RoleNames = _context.Roles.Select(role => new RoleNamesSelectListSQLModel
            {
                ID = role.Id,
                RoleName = role.RoleName,
            });
            return new SelectList(RoleNames, nameof(RoleNamesSelectListSQLModel.ID), nameof(RoleNamesSelectListSQLModel.RoleName));
        }
    }
}
