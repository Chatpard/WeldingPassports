using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    public class TrainingCentersSQLRepository : SaveChangesSQL, ITrainingCentersSQLRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAppSettingsSQLRepository _appSettingsSQLRepository;
        private readonly IDataProtector _protector;

        public TrainingCentersSQLRepository(AppDbContext context, IMapper mapper, IAppSettingsSQLRepository appSettingsSQLRepository,
            IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _appSettingsSQLRepository = appSettingsSQLRepository;
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }

        public IQueryable<TrainingCenterIndexViewModel> GetTrainingCentersIndex()
        {
            IQueryable<TrainingCenter> query = _context.TrainingCenters;

            return query.ProjectTo<TrainingCenterIndexViewModel>(_mapper.ConfigurationProvider);
        }

        public async Task<TrainingCenterDetailsViewModel> GetTrainingCenterDetailsAsync(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            IQueryable<TrainingCenter> query = _context.TrainingCenters.Where(trainingCenter => trainingCenter.ID == decryptedID);

            return await query.ProjectTo<TrainingCenterDetailsViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<TrainingCenterEditViewModel> GetTrainingCenterEditAsync(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            IQueryable<TrainingCenter> query = _context.TrainingCenters.Where(trainingCenter => trainingCenter.ID == decryptedID);

            return await query.ProjectTo<TrainingCenterEditViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<EntityEntry<TrainingCenter>> PostTrainingCenterCreateAsync(TrainingCenter trainingCenterChanges)
        {
            EntityEntry<TrainingCenter> trainingCenter = await _context.TrainingCenters.AddAsync(trainingCenterChanges);
            trainingCenter.State = EntityState.Added;
            return trainingCenter;
        }

        public EntityEntry<TrainingCenter> PostTrainingCenterEditAsync(TrainingCenter trainingCenterChanges)
        {
            EntityEntry<TrainingCenter> trainingCenter = _context.TrainingCenters.Update(trainingCenterChanges);
            trainingCenter.State = EntityState.Modified;
            return trainingCenter;
        }

        public async Task<int> DeleteTrainingCenterByEncryptedIDAsync(string encryptedID, CancellationToken token)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            _context.TrainingCenters.Remove(new TrainingCenter { ID = decryptedID });

            return await SaveAsync(token);
        }

        public SelectList TrainingCenterSelectList(int? trainingsCenterId = null)
        {
            var trainingCenters = _context.TrainingCenters
                .Where(trainingCenter =>  trainingCenter.IsActive);
            if (trainingsCenterId != null)
            {
                trainingCenters = trainingCenters
                    .Where(trainingCenter => trainingCenter.ID == trainingsCenterId);
            }
            var trainingCentersList = trainingCenters
                .OrderBy(trainingCenter => trainingCenter.Company.CompanyName)
                .Select(trainingCenter => new {
                    ID = trainingCenter.ID,
                    CompanyName = trainingCenter.Company.CompanyName
                });

            return new SelectList(trainingCentersList, nameof(TrainingCenter.ID), nameof(TrainingCenter.Company.CompanyName));
        }

        public Dictionary<int, char> LetterDictionary()
        {
            Dictionary<int, char> letterDictionary = new Dictionary<int, char>();

            List<TrainingCenter> trainingCentersList = _context.TrainingCenters.ToList();
            foreach (TrainingCenter trainingCenter in trainingCentersList)
            {
                letterDictionary.Add(trainingCenter.ID, trainingCenter.Letter);
            }

            return letterDictionary;
        }

        public async Task<IPaginatedList<TrainingCenterIndexViewModel>> GetTrainingCentersIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder)
        {
            var trainingCentersQuery = GetTrainingCentersIndex();

            trainingCentersQuery = SearchTrainingCenterIndex(trainingCentersQuery, searchString);

            trainingCentersQuery = SortTrainingCenterIndex(trainingCentersQuery, sortOrder);

            return await PaginatedList<TrainingCenterIndexViewModel>.CreateAsync(trainingCentersQuery.AsNoTracking(), pageIndex, pageSize);
        }

        public async Task<TrainingCenter> GetTrainingCenterByUserId (string userId)
        {
            CompanyContact companyContact = await _context.CompanyContacts.Where(companyContact => companyContact.IdentityUserId == userId).Include(companyContact => companyContact.Company).SingleOrDefaultAsync();
            if(companyContact == null) return null;

            TrainingCenter trainingCenter = await _context.TrainingCenters.Where(trainingCenter => trainingCenter.CompanyID == companyContact.CompanyID).FirstOrDefaultAsync();
            return trainingCenter;
        }

        private static IQueryable<TrainingCenterIndexViewModel> SortTrainingCenterIndex(IQueryable<TrainingCenterIndexViewModel> trainingCentersQuery, string sortOrder)
        {
            switch (sortOrder)
            {
                case "Letter_desc":
                    trainingCentersQuery = trainingCentersQuery.OrderByDescending(trainingCenter => trainingCenter.Letter);
                    return trainingCentersQuery;
                case "Letter_asc":
                case null:
                case "":
                    trainingCentersQuery = trainingCentersQuery.OrderBy(trainingCenter => trainingCenter.Letter);
                    return trainingCentersQuery;
                case "CompanyName_desc":
                    trainingCentersQuery = trainingCentersQuery.OrderByDescending(trainingCenter => trainingCenter.CompanyName);
                    return trainingCentersQuery;
                case "CompanyName_asc":
                    trainingCentersQuery = trainingCentersQuery.OrderBy(trainingCenter => trainingCenter.CompanyName);
                    return trainingCentersQuery;
                case "BusinessAddressPostalCode_desc":
                    trainingCentersQuery = trainingCentersQuery.OrderByDescending(trainingCenter => trainingCenter.BusinessAddressPostalCode);
                    return trainingCentersQuery;
                case "BusinessAddressPostalCode_asc":
                    trainingCentersQuery = trainingCentersQuery.OrderBy(trainingCenter => trainingCenter.BusinessAddressPostalCode);
                    return trainingCentersQuery;
                case "BusinessAddressCity_desc":
                    trainingCentersQuery = trainingCentersQuery.OrderByDescending(trainingCenter => trainingCenter.BusinessAddressCity);
                    return trainingCentersQuery;
                case "BusinessAddressCity_asc":
                    trainingCentersQuery = trainingCentersQuery.OrderBy(trainingCenter => trainingCenter.BusinessAddressCity);
                    return trainingCentersQuery;
                case "Contact_desc":
                    trainingCentersQuery = trainingCentersQuery.OrderByDescending(trainingCenter => trainingCenter.Contact);
                    return trainingCentersQuery;
                case "Contact_asc":
                    trainingCentersQuery = trainingCentersQuery.OrderBy(trainingCenter => trainingCenter.Contact);
                    return trainingCentersQuery;
                case "Email_desc":
                    trainingCentersQuery = trainingCentersQuery.OrderByDescending(trainingCenter => trainingCenter.Email);
                    return trainingCentersQuery;
                case "Email_asc":
                    trainingCentersQuery = trainingCentersQuery.OrderBy(trainingCenter => trainingCenter.Email);
                    return trainingCentersQuery;
                case "MobilePhone_desc":
                    trainingCentersQuery = trainingCentersQuery.OrderByDescending(trainingCenter => trainingCenter.MobilePhone);
                    return trainingCentersQuery;
                case "MobilePhone_asc":
                    trainingCentersQuery = trainingCentersQuery.OrderBy(trainingCenter => trainingCenter.MobilePhone);
                    return trainingCentersQuery;

                default:
                    throw new InvalidOperationException("SortOrder nout found.");
            }
        }

        private static IQueryable<TrainingCenterIndexViewModel> SearchTrainingCenterIndex(IQueryable<TrainingCenterIndexViewModel> trainingCentersQuery, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                trainingCentersQuery = trainingCentersQuery.Where(trainingCenter => trainingCenter.CompanyName.ToLower().Contains(searchString.ToLower())
                    || trainingCenter.BusinessAddressPostalCode.ToLower().Contains(searchString.ToLower())
                    || trainingCenter.BusinessAddressCity.ToLower().Contains(searchString.ToLower())
                    || trainingCenter.Contact.ToLower().Contains(searchString.ToLower())
                    || trainingCenter.Email.ToLower().Contains(searchString.ToLower()));
            }

            return trainingCentersQuery;
        }
    }
}
