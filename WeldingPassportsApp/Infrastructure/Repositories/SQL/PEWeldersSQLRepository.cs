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
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    public class PEWeldersSQLRepository : SaveChangesSQL, IPEWeldersSQLRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAppSettingsSQLRepository _appSettingsSQLRepository;
        private readonly IDataProtector _protector;

        public PEWeldersSQLRepository(AppDbContext context, IMapper mapper, IAppSettingsSQLRepository appSettingsSQLRepository,
            IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _appSettingsSQLRepository = appSettingsSQLRepository;
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }

        private async Task<IQueryable<PEWelderIndexViewModel>> GetPEWeldersIndexAsync()
        {
            AppSettings app = await _appSettingsSQLRepository.GetAppsetingsAsync();

            IQueryable<PEWelderRegistrationUIColorGroup> peWelderRegistrationUIColorQuery =
                _context.PEWelders.Select(
                        peWelder => new
                        {
                            PEWelder = peWelder,
                            Registration = 
                                peWelder.PEPassports.SelectMany(
                                    pePassport => pePassport.Registrations,
                                    (pePassport, registration) => registration
                                )
                                .OrderByDescending(registration => registration.Examination.ExamDate).FirstOrDefault(),
                        })
                        .Select(
                            peWelderRegistration => new
                            {
                                PEWelder = peWelderRegistration.PEWelder,
                                Registration = peWelderRegistration.Registration,
                                ExtendableStatus =
                                        peWelderRegistration.Registration.Revoke != null ? ExtendableStatus.Revoked :
                                        EF.Functions.DateDiffDay(DateTime.Now, peWelderRegistration.Registration.ExpiryDate) > app.MaxInAdvanceDays ? ExtendableStatus.NotYetExtendable :
                                        (EF.Functions.DateDiffDay(DateTime.Now, peWelderRegistration.Registration.ExpiryDate) > (app.MaxExtensionDays * -1) ? ExtendableStatus.Extendable :
                                        ExtendableStatus.NoMoreExtendable)
                            }
                        )
                        .Join(
                            _context.UIColors.DefaultIfEmpty(),
                            peWelderRegistrationExtendableStatus => new
                            {
                                ExtendableStatus = peWelderRegistrationExtendableStatus.ExtendableStatus,
                                HasPassed = (bool)(peWelderRegistrationExtendableStatus.Registration.HasPassed.HasValue ? peWelderRegistrationExtendableStatus.Registration.HasPassed : false)
                            },
                            uiColor => new
                            {
                                ExtendableStatus = uiColor.ExtendableStatus,
                                HasPassed = (bool)(uiColor.HasPassed.HasValue ? uiColor.HasPassed : false)
                            },
                            (peWelderRegistration, uiColor) =>
                                new PEWelderRegistrationUIColorGroup
                                {
                                    PEWelder = peWelderRegistration.PEWelder,
                                    Registration = peWelderRegistration.Registration,
                                    UIColor = uiColor
                                }
                        );

            return peWelderRegistrationUIColorQuery.ProjectTo<PEWelderIndexViewModel>(_mapper.ConfigurationProvider);
        }

        public async Task<EntityEntry<PEWelder>> PostPEWelderCreateAsync(PEWelder peWelder)
        {
            EntityEntry<PEWelder> newPEWelder = await _context.PEWelders.AddAsync(peWelder);
            newPEWelder.State = EntityState.Added;
            return newPEWelder;
        }

        public async Task<PEWelderDetailsViewModel> GetPEWelderDetailsAsync(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            AppSettings app = await _appSettingsSQLRepository.GetAppsetingsAsync();

            IQueryable<PEWelderListRegistrationUIColorGroup> query = _context.PEWelders
                .Where(welder => welder.ID == decryptedID)
                .Select(
                    welder => new PEWelderListRegistrationUIColorGroup
                    {
                        PEWelder = welder,
                        PEPassportRegistrationUIColorGroupList =
                            welder.PEPassports
                                .Select(
                                    passport => new 
                                    {
                                        PEPassport = passport,
                                        Registration = passport.Registrations.OrderByDescending(registration => registration.Examination.ExamDate).FirstOrDefault()
                                    })
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
                                    }
                                )
                                .Join(
                                    _context.UIColors.DefaultIfEmpty(),
                                    pePassportRegistrationExtendableStatus => new
                                    {
                                        ExtendableStatus = pePassportRegistrationExtendableStatus.ExtendableStatus,
                                        HasPassed = (bool)(pePassportRegistrationExtendableStatus.Registration.HasPassed.HasValue ? pePassportRegistrationExtendableStatus.Registration.HasPassed : false)
                                    },
                                    uiColor => new
                                    {
                                        ExtendableStatus = uiColor.ExtendableStatus,
                                        HasPassed = (bool)(uiColor.HasPassed.HasValue ? uiColor.HasPassed : false)
                                    },
                                    (pePassportRegistrationExtendableStatus, uiColor) => new PEPassportRegistrationUIColorGroup
                                    {
                                        PEPassport = pePassportRegistrationExtendableStatus.PEPassport,
                                        Registration = pePassportRegistrationExtendableStatus.Registration,
                                        UIColor = uiColor
                                    }
                                )
                    }
                );

            return await query.ProjectTo<PEWelderDetailsViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<PEWelderEditViewModel> GetPEWelderEditAsync(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            IQueryable<PEWelder> query = _context.PEWelders
                .Where(peWelder => peWelder.ID == decryptedID);

            return await query.ProjectTo<PEWelderEditViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public EntityEntry<PEWelder> PutPEWelderUpdate(PEWelder peWelderChanges)
        {
            EntityEntry<PEWelder> peWelder = _context.Entry<PEWelder>(peWelderChanges);
            peWelder.State = EntityState.Modified;
            return peWelder;
        }

        public async Task<int> DeletePEWelderByEncryptedIDAsync(string encryptedID, CancellationToken token)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            _context.PEWelders.Remove(new PEWelder { ID = decryptedID });
            return await SaveAsync(token);
        }

        public async Task<IPaginatedList<PEWelderIndexViewModel>> GetPEWeldersIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder)
        {
            var weldersQuery = await GetPEWeldersIndexAsync();

            weldersQuery = SearchPEWelderIndex(weldersQuery, searchString);

            weldersQuery = SortPEWelderIndex(weldersQuery, sortOrder);

            return await PaginatedList<PEWelderIndexViewModel>.CreateAsync(weldersQuery.AsNoTracking(), pageIndex, pageSize);
        }

        private static IQueryable<PEWelderIndexViewModel> SortPEWelderIndex(IQueryable<PEWelderIndexViewModel> weldersQuery, string sortOrder)
        {
            switch (sortOrder)
            {
                case "FirstName_desc":
                    weldersQuery = weldersQuery.OrderByDescending(welder => welder.FirstName);
                    return weldersQuery;
                case "FirstName_asc":
                case null:
                case "":
                    weldersQuery = weldersQuery.OrderBy(welder => welder.FirstName);
                    return weldersQuery;
                case "LastName_desc":
                    weldersQuery = weldersQuery.OrderByDescending(welder => welder.LastName);
                    return weldersQuery;
                case "LastName_asc":
                    weldersQuery = weldersQuery.OrderBy(welder => welder.LastName);
                    return weldersQuery;
                case "AVNumber_desc":
                    weldersQuery = weldersQuery.OrderByDescending(welder => welder.AVNumber);
                    return weldersQuery;
                case "AVNumber_asc":
                    weldersQuery = weldersQuery.OrderBy(welder => welder.AVNumber);
                    return weldersQuery;
                default:
                    throw new InvalidOperationException("SortOrder nout found.");
            }
        }

        private static IQueryable<PEWelderIndexViewModel> SearchPEWelderIndex(IQueryable<PEWelderIndexViewModel> weldersQuery, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                weldersQuery = weldersQuery.Where(welder => welder.FirstName.ToLower().Contains(searchString.ToLower())
                    || welder.LastName.ToLower().Contains(searchString.ToLower())
                    || welder.AVNumber.ToString().ToLower().Contains(searchString.ToLower()));
            }

            return weldersQuery;
        }

        public SelectList PEWelderSelectList()
        {
            var peWelders = _context.PEWelders
                .OrderBy(peWelder => peWelder.LastName).ThenBy(peWelder => peWelder.FirstName)
                .Select(peWelder => new 
                {
                    ID = peWelder.ID,
                    Name =  peWelder.FirstName + " " + peWelder.LastName
                });

            var list = new SelectList(peWelders, nameof(PEWelder.ID), "Name");
            return list;
        }
    }
}
