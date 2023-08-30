using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
using Application.Requests.ExamCenters;
using Application.Security;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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

        public async Task<ExamCenterDetailsViewModel> GetExamCentersDetailsAsync(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            IQueryable<ExamCenter> query = _context.ExamCenters.Where(ec => ec.ID == decryptedID);

            return await query.ProjectTo<ExamCenterDetailsViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public EntityEntry<ExamCenter> PostExamCentersCreate(ExamCenter newExamCenter)
        {
            EntityEntry<ExamCenter> newExamCenterEntityEntry = _context.Entry(newExamCenter);
            newExamCenterEntityEntry.State = EntityState.Added;
            return newExamCenterEntityEntry;
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
            CompanyContact companyContact = await _context.CompanyContacts.Where(companyContact => companyContact.AppUserId == userId).SingleOrDefaultAsync();
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
                case "BusinessAddressPostalCode_desc":
                    examCentersQuery = examCentersQuery.OrderByDescending(trainingCenter => trainingCenter.BusinessAddressPostalCode);
                    return examCentersQuery;
                case "BusinessAddressPostalCode_asc":
                    examCentersQuery = examCentersQuery.OrderBy(trainingCenter => trainingCenter.BusinessAddressPostalCode);
                    return examCentersQuery;
                case "BusinessAddressCity_desc":
                    examCentersQuery = examCentersQuery.OrderByDescending(trainingCenter => trainingCenter.BusinessAddressCity);
                    return examCentersQuery;
                case "BusinessAddressCity_asc":
                    examCentersQuery = examCentersQuery.OrderBy(trainingCenter => trainingCenter.BusinessAddressCity);
                    return examCentersQuery;
                case "Contact_desc":
                    examCentersQuery = examCentersQuery.OrderByDescending(trainingCenter => trainingCenter.Contact);
                    return examCentersQuery;
                case "Contact_asc":
                    examCentersQuery = examCentersQuery.OrderBy(trainingCenter => trainingCenter.Contact);
                    return examCentersQuery;
                case "Email_desc":
                    examCentersQuery = examCentersQuery.OrderByDescending(trainingCenter => trainingCenter.Email);
                    return examCentersQuery;
                case "Email_asc":
                    examCentersQuery = examCentersQuery.OrderBy(trainingCenter => trainingCenter.Email);
                    return examCentersQuery;
                case "MobilePhone_desc":
                    examCentersQuery = examCentersQuery.OrderByDescending(trainingCenter => trainingCenter.MobilePhone);
                    return examCentersQuery;
                case "MobilePhone_asc":
                    examCentersQuery = examCentersQuery.OrderBy(trainingCenter => trainingCenter.MobilePhone);
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

        private bool HasDependencies(EntityEntry entityEntry)
        {
            var entityType = _context.Model.FindEntityType(typeof(ExamCenter));
            var collectionNavigations = entityType.GetNavigations()
                .Where(nav => nav.IsCollection)
                .Concat<INavigationBase>(entityType.GetSkipNavigations());

            foreach(INavigationBase collectionNavigation in collectionNavigations)
            {
                var property = entityEntry.GetType().GetProperty(collectionNavigation.PropertyInfo.Name);
                if (property.GetValue(entityEntry) is IEnumerable<object> collection && collection.Any())
                    return true;
            }

            var referenceNavigations = entityType.GetNavigations()
                .Where(nav => !nav.IsOnDependent);

            foreach(INavigation referenceNavigation in referenceNavigations)
            {
                var property = entityEntry.GetType().GetProperty(referenceNavigation.PropertyInfo.Name);
                if (property.GetValue(entityEntry) is IEnumerable<object> collection && collection.Any())
                    return true;
            }

            return false;
        }

    }
}
