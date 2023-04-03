using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
using Application.Security;
using Application.SQLModels;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    public class PEPassportsSQLRepository : SaveChangesSQL, IPEPassportsSQLRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAppSettingsSQLRepository _appSettingsSQLRepository;
        private readonly IDataProtector _protector;

        public PEPassportsSQLRepository(AppDbContext context, IMapper mapper, IAppSettingsSQLRepository appSettingsSQLRepository,
            IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _appSettingsSQLRepository = appSettingsSQLRepository;
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }

        private async Task<IQueryable<PEPassportIndexViewModel>> GetPEPassportsIndexAsync(int? trainingCenterID)
        {
            AppSettings app = await _appSettingsSQLRepository.GetAppsetingsAsync();

            IQueryable<PEPassportRegistrationGroup> pePassportRegistrationQuery =
                _context.PEPassports
                    .Select(
                        pePassport => new PEPassportRegistrationGroup
                        {
                            PEPassport = pePassport,
                            Registration =
                                pePassport.Registrations.OrderByDescending(registration => registration.Examination.ExamDate).FirstOrDefault()
                        });
            if(trainingCenterID != null)
            {
                pePassportRegistrationQuery =
                    pePassportRegistrationQuery
                        .Where(pePassportRegistration => pePassportRegistration.PEPassport.TrainingCenterID == trainingCenterID);
            }
            IQueryable<PEPassportRegistrationUIColorGroup> pePassportRegistrationUIColorQuery =
                pePassportRegistrationQuery
                    .Select(
                        pePassportRegistration => new
                        {
                            PEPassport = pePassportRegistration.PEPassport,
                            Registration = pePassportRegistration.Registration,
                            ExtendableStatus =
                                pePassportRegistration.Registration.Revoke != null ? ExtendableStatus.Revoked :
                                EF.Functions.DateDiffDay(DateTime.Now, pePassportRegistration.Registration.ExpiryDate) > app.MaxInAdvanceDays ? ExtendableStatus.NotYetExtendable :
                                (EF.Functions.DateDiffDay(DateTime.Now, pePassportRegistration.Registration.ExpiryDate) > (app.MaxExtensionDays * -1) ? ExtendableStatus.Extendable :
                                ExtendableStatus.NoMoreExtendable)
                        })
                    .Join(
                        _context.UIColors.DefaultIfEmpty(),
                        pePassportRegistration => new
                        {
                            ExtendableStatus = pePassportRegistration.ExtendableStatus,
                            HasPassed = (bool)(pePassportRegistration.Registration.HasPassed.HasValue ? pePassportRegistration.Registration.HasPassed : false)
                        },
                        uiColor => new
                        {
                            ExtendableStatus = uiColor.ExtendableStatus,
                            HasPassed = (bool)(uiColor.HasPassed.HasValue ? uiColor.HasPassed : false)
                        },
                        (pePassportRegistration, uiColor) => new PEPassportRegistrationUIColorGroup
                        {
                            PEPassport = pePassportRegistration.PEPassport,
                            Registration = pePassportRegistration.Registration,
                            UIColor = uiColor
                        }
                    );

            var temp = pePassportRegistrationUIColorQuery.ProjectTo<PEPassportIndexViewModel>(_mapper.ConfigurationProvider);

            return pePassportRegistrationUIColorQuery.ProjectTo<PEPassportIndexViewModel>(_mapper.ConfigurationProvider);
        }

        public async Task<EntityEntry<PEPassport>> PostPEPassportCreateAsync(PEPassport pePassort)
        {
            EntityEntry<PEPassport> newPePassport = await _context.AddAsync(pePassort);
            newPePassport.State = EntityState.Added;
            return newPePassport;
        }

        public async Task<PEPassportDetailsViewModel> GetPEPassportDetailsAsync(string encryptedID)
        {
            var query = await GetPEPassportPEWelderRegistrationUIColorsGroup(encryptedID);
            return await query.ProjectTo<PEPassportDetailsViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<PEPassportEditViewModel> GetPEPassportEditAsync(string encryptedID)
        {

            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            var test =
                _context.PEPassports.Select(x => new { RowNumber = EF.Functions.RowNumber(x.AVNumber) });
            var test1 = await test.FirstOrDefaultAsync();
            if (test1 != null)
            {
                var num = test1.RowNumber;
            }

            IQueryable<PEPassport> query =
                _context.PEPassports.Where(passport => passport.ID == decryptedID);
    
            return await query.ProjectTo<PEPassportEditViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public EntityEntry<PEPassport> PostPEPassportEditasync(PEPassport pePassortChanges)
        {
            EntityEntry<PEPassport> pePassport = _context.Entry<PEPassport>(pePassortChanges);
            pePassport.State = EntityState.Modified;
            return pePassport;
        }

        public async Task<PEPassportUpdateViewModel> GetPEPassportUpdateAsync(string encryptedID)
        {
            var query = await GetPEPassportPEWelderRegistrationUIColorsGroup(encryptedID);
            return await query.ProjectTo<PEPassportUpdateViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<int> DeletePEPassportByEncryptedIDAsync(string encryptedID, CancellationToken token)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            _context.PEPassports.Remove(new PEPassport { ID = decryptedID });
            return await SaveChangesAsync(token);
        }

        private async Task<IQueryable<PEPassportPEWelderRegistrationUIColorsGroup>> GetPEPassportPEWelderRegistrationUIColorsGroup(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            AppSettings app = await _appSettingsSQLRepository.GetAppsetingsAsync();

            IQueryable<PEPassportPEWelderRegistrationUIColorsGroup> query =
                _context.PEPassports
                    .Where(passport => passport.ID == decryptedID)
                    .Include(passport => passport.Registrations)
                    .Select(
                        passport => new PEPassportPEWelderRegistrationUIColorsGroup
                        {
                            PEPassport = passport,
                            PEWelder = passport.PEWelder,
                            SelectedCertificationEncryptedID = "",
                            RegistrationUIColors = passport.Registrations
                                .Select(
                                    registration => new
                                    {
                                        Registration = registration,
                                        ExtendableStatus =
                                            registration.Revoke != null ? ExtendableStatus.Revoked :
                                            EF.Functions.DateDiffDay(DateTime.Now, registration.ExpiryDate) > app.MaxInAdvanceDays ? ExtendableStatus.NotYetExtendable :
                                            (EF.Functions.DateDiffDay(DateTime.Now, registration.ExpiryDate) > (app.MaxExtensionDays * -1) ? ExtendableStatus.Extendable :
                                            ExtendableStatus.NoMoreExtendable),
                                        HasNextOrRevoked = _context.Registrations.Any(anyRegistration => anyRegistration.PreviousRegistrationID == registration.ID) || registration.Revoke != null
                                    })
                                .Join(
                                    _context.UIColors.DefaultIfEmpty(),
                                    registrationExtendableStatus => new
                                    {
                                        ExtendableStatus = registrationExtendableStatus.ExtendableStatus,
                                        HasPassed = (bool)(registrationExtendableStatus.Registration.HasPassed.HasValue ? registrationExtendableStatus.Registration.HasPassed : false)
                                    },
                                    uicolor => new
                                    {
                                        ExtendableStatus = uicolor.ExtendableStatus,
                                        HasPassed = (bool)(uicolor.HasPassed.HasValue ? uicolor.HasPassed : false)
                                    },
                                    (registrationExtendableStatus, uicolor) => new RegistrationUIColorGroup
                                    {
                                        Registration = registrationExtendableStatus.Registration,
                                        UIColor = uicolor,
                                        HasNextOrRevoked = registrationExtendableStatus.HasNextOrRevoked
                                    }
                                )
                        }
                    );

            return query;
        }

        public SelectList PEPassportSelectList()
        {
            return new SelectList(_context.PEPassports);
        }

        public async Task<IPaginatedList<PEPassportIndexViewModel>> GetPEPassportsIndexPaginatedAsync(int? trainingCenterId, int pageSize, int pageIndex, string searchString, string sortOrder)
        {
            var passportsQuery = await GetPEPassportsIndexAsync(trainingCenterId);

            passportsQuery = SearchPEPassportIndex(passportsQuery, searchString);

            passportsQuery = SortPEPassportIndex(passportsQuery, sortOrder);

            return await PaginatedList<PEPassportIndexViewModel>.CreateAsync(passportsQuery.AsNoTracking(), pageIndex, pageSize);
        }

        private static IQueryable<PEPassportIndexViewModel> SortPEPassportIndex(IQueryable<PEPassportIndexViewModel> passportsQuery, string sortOrder)
        {
            switch (sortOrder)
            {
                case "AVNumber_desc":
                    passportsQuery = passportsQuery.OrderByDescending(passport => passport.Letter).ThenBy(passport => passport.AVNumber);
                    return passportsQuery;
                case "AVNumber_asc":
                case null:
                case "":
                    passportsQuery = passportsQuery.OrderBy(passport => passport.Letter).ThenBy(passport => passport.AVNumber);
                    return passportsQuery;
                case "FirstName_desc":
                    passportsQuery = passportsQuery.OrderByDescending(passport => passport.FirstName);
                    return passportsQuery;
                case "FirstName_asc":
                    passportsQuery = passportsQuery.OrderBy(passport => passport.FirstName);
                    return passportsQuery;
                case "LastName_desc":
                    passportsQuery = passportsQuery.OrderByDescending(passport => passport.LastName);
                    return passportsQuery;
                case "LastName_asc":
                    passportsQuery = passportsQuery.OrderBy(passport => passport.LastName);
                    return passportsQuery;
                case "CompanyName_desc":
                    passportsQuery = passportsQuery.OrderByDescending(passport => passport.CompanyName);
                    return passportsQuery;
                case "CompanyName_asc":
                    passportsQuery = passportsQuery.OrderBy(passport => passport.CompanyName);
                    return passportsQuery;
                case "ExpiryDate_desc":
                    passportsQuery = passportsQuery.OrderByDescending(passport => passport.ExpiryDate);
                    return passportsQuery;
                case "ExpiryDate_asc":
                    passportsQuery = passportsQuery.OrderBy(passport => passport.ExpiryDate);
                    return passportsQuery;
                default:
                    throw new InvalidOperationException("SortOrder nout found.");
            }
        }

        private static IQueryable<PEPassportIndexViewModel> SearchPEPassportIndex(IQueryable<PEPassportIndexViewModel> passportsQuery, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                passportsQuery = passportsQuery.Where(passport => (passport.Letter.ToString() + passport.AVNumber.ToString()).ToLower().Contains(searchString.ToLower())
                    || passport.Letter.ToString().ToLower().Contains(searchString.ToLower())
                    || passport.AVNumber.ToString().ToLower().Contains(searchString.ToLower())
                    || passport.FirstName.ToLower().Contains(searchString.ToLower())
                    || passport.LastName.ToLower().Contains(searchString.ToLower())
                    || passport.CompanyName.ToLower().Contains(searchString.ToLower()));
            }

            return passportsQuery;
        }

    }
}
