using Application.Interfaces.Repositories.SQL;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.SQLModels;
using Application.Security;
using Domain.Models;

namespace Infrastructure.Repositories.SQL
{
    public class AppRolesSQLRepository: IAppRolesSQLRepository
    {
        private readonly AppDbContext _context;

        public AppRolesSQLRepository(AppDbContext context)
        {
            _context = context;
        }

        public SelectList RoleNamesSelectList(int? companyID = null)
        {
            IQueryable<AppRole> roles = _context.Roles;

            if(companyID != null)
            {
                if(_context.TrainingCenters.Any(trainingCenter => trainingCenter.CompanyID == companyID))
                {
                    roles = roles.Where(role => role.Name == RolesStore.TC);
                }
                else
                {
                    roles = roles.Where(role => role.Name != RolesStore.TC);
                }
                
                if (_context.ExamCenters.Any(examCenter => examCenter.CompanyID == companyID))
                {
                    roles = roles.Where(role => role.Name == RolesStore.EC);
                }
                else
                {
                    roles = roles.Where(role => role.Name != RolesStore.EC);
                }
            }

            var roleNames = roles.Select(role => new RoleNamesSelectListSQLModel
            {
                ID = role.Id,
                RoleName = role.RoleName,
            });
            return new SelectList(roleNames, nameof(RoleNamesSelectListSQLModel.ID), nameof(RoleNamesSelectListSQLModel.RoleName));
        }
    }
}
