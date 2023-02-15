using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
using Application.Requests.ExamCenters;
using Application.Security;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    public class ExamCentersSQLRepository : SaveChangesSQL, IExamCentersSQLRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public ExamCentersSQLRepository(AppDbContext context, IMapper mapper,
            IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings) : base(context)
        {
            _context = context;
            _mapper=mapper;
            _protector=dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
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

        public async Task<ExamCenterDetailsViewModel> GetExamCentersDetails(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            ExamCenter examCenter = await _context.ExamCenters.Where(ec => ec.ID == decryptedID).Include(examCenter => examCenter.Company).FirstOrDefaultAsync();

            return _mapper.Map<ExamCenterDetailsViewModel>(examCenter);
        }

        public async Task<EntityEntry> PostExamCentersCreateAsync(ExamCenter examCenter)
        {
            EntityEntry newExamCenter = await _context.ExamCenters.AddAsync(examCenter);
            newExamCenter.State = EntityState.Added;
            return newExamCenter;
        }

        public async Task<ExamCenterEditViewModel> GetExamCentersEdit(string encryptedID)
        {
            if(!int.TryParse(encryptedID, out int decryptedID))
            {
                decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            }

            ExamCenter examCenter = await _context.ExamCenters
                .Where(examCenter => examCenter.ID == decryptedID).SingleOrDefaultAsync();

            return _mapper.Map<ExamCenterEditViewModel>(examCenter);
        }
        
        public EntityEntry<ExamCenter> PostExamCentersEdit(ExamCenter examCenterChanges)
        {
            EntityEntry<ExamCenter> examCenter = _context.Entry(examCenterChanges);
            examCenter.State = EntityState.Modified;
            return examCenter;
        }

        public async Task<int> DeleteExamCenterByEncryptedIDasync(string encryptedID, CancellationToken cancellationToken)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            EntityEntry examCenterEntityEntry = _context.Entry(await _context.ExamCenters.Where(ec => ec.ID == decryptedID).SingleOrDefaultAsync());
            examCenterEntityEntry.State = EntityState.Deleted;
            return await SaveChangesAsync(cancellationToken);
        }
        
        public SelectList ExamCenterSelectList()
        {
            var examCenters = _context.ExamCenters
                .Where(examCenter => examCenter.IsActive)
                .OrderBy(examCenter => examCenter.Company.CompanyName)
                .Select(examCenter => new {
                    ID = examCenter.ID,
                    CompanyName = examCenter.Company.CompanyName
                });

            return new SelectList(examCenters, nameof(ExamCenter.ID), nameof(ExamCenter.Company.CompanyName));
        }
        
        public async Task<ExamCenter> GetExamCenterByUserId(string userId)
        {
            CompanyContact companyContact = await _context.CompanyContacts.Where(companyContact => companyContact.IdentityUserId == userId).SingleOrDefaultAsync();
            if(companyContact == null) { return null; }

            ExamCenter examCenter = await _context.ExamCenters.Where(examCenter => examCenter.CompanyID == companyContact.CompanyID).SingleOrDefaultAsync();
            return examCenter;
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
