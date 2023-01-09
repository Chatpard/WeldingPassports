using Application.Interfaces.Repositories.SQL;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repositories.SQL
{
    public class ExamCentersSQLRepository : SaveChangesSQL, IExamCentersSQLRepository
    {
        private readonly AppDbContext _context;

        public ExamCentersSQLRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public SelectList ExamCenterSelectList(int? trainingCenterID = null)
        {
            var examCenters = _context.ExamCenters
                .OrderBy(examCenter => examCenter.Company.CompanyName)
                .Select(examCenter => new {
                    ID = examCenter.ID,
                    CompanyName = examCenter.Company.CompanyName
                });

            return new SelectList(examCenters, nameof(ExamCenter.ID), nameof(ExamCenter.Company.CompanyName));
        }
    }
}
