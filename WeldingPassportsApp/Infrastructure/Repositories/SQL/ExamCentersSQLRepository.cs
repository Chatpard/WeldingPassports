using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
using Application.Requests.ExamCenters;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    public class ExamCentersSQLRepository : SaveChangesSQL, IExamCentersSQLRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ExamCentersSQLRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper=mapper;
        }

        public IQueryable<ExamCenterIndexViewModel> GetExamCentersIndex()
        {
            IQueryable<ExamCenter> query = _context.ExamCenters.AsQueryable();
            return _mapper.ProjectTo<ExamCenterIndexViewModel>(query);
        }

        public async Task<IPaginatedList<ExamCenterIndexViewModel>> GetExamCentersIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder)
        {
            var examCentersQuery = GetExamCentersIndex();

            examCentersQuery = SearchExamCenterIndex(examCentersQuery, searchString);

            examCentersQuery = SortExamCenterIndex(examCentersQuery, sortOrder);

            return await PaginatedList<ExamCenterIndexViewModel>.CreateAsync(examCentersQuery.AsNoTracking(), pageIndex, pageSize);
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
        private static IQueryable<ExamCenterIndexViewModel> SortExamCenterIndex(IQueryable<ExamCenterIndexViewModel> examCentersQuery, string sortOrder)
        {
            switch (sortOrder)
            {
                case "CompanyName_desc":
                    examCentersQuery = examCentersQuery.OrderByDescending(examCenter => examCenter.CompanyName);
                    return examCentersQuery;
                case "CompanyName_asc":
                case null:
                case "":
                    examCentersQuery = examCentersQuery.OrderBy(examCenter => examCenter.CompanyName);
                    return examCentersQuery;
                case "IsActive_desc":
                    examCentersQuery = examCentersQuery.OrderByDescending(examCenter => examCenter.IsActive);
                    return examCentersQuery;
                case "IsActive_asc":
                    examCentersQuery = examCentersQuery.OrderBy(examCenter => examCenter.IsActive);
                    return examCentersQuery;
                default:
                    throw new InvalidOperationException("SortOrder nout found.");
            }
        }

        private static IQueryable<ExamCenterIndexViewModel> SearchExamCenterIndex(IQueryable<ExamCenterIndexViewModel> examCentersQuery, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                examCentersQuery = examCentersQuery.Where(examCenter => examCenter.CompanyName.ToLower().Contains(searchString.ToLower()));
            }

            return examCentersQuery;
        }
    }
}
