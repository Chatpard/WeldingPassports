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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    public class ExaminationsSQLRepository : SaveChangesSQL, IExaminationsSQLRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAppSettingsSQLRepository _appSettingsSQLRepository;
        private readonly IDataProtector _protector;

        public ExaminationsSQLRepository(AppDbContext context, IMapper mapper, IAppSettingsSQLRepository appSettingsSQLRepository,
            IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _appSettingsSQLRepository = appSettingsSQLRepository;
            _protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }

        public async Task ExaminationUpdateAsync(ExaminationEditViewModel newExaminationVm,
            CancellationToken cancellationToken)
        {
            Examination newExamination = _mapper.Map<Examination>(newExaminationVm);
            EntityEntry<Examination> newExaminationEntity = _context.Entry<Examination>(newExamination);
            newExaminationEntity.State = EntityState.Modified;
            await SaveAsync(cancellationToken);
         }

        public async Task<ExaminationEditViewModel> GetExaminationEditAsync(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            IQueryable<Examination> query =
                _context.Examinations.Where(examination => examination.ID == decryptedID);

            return await query.ProjectTo<ExaminationEditViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<EntityEntry<Examination>> PostExaminationCreateAsync(ExaminationCreateViewModel vm, CancellationToken cancellationToken)
        {
            var examination = _mapper.Map<Examination>(vm);
            EntityEntry<Examination> newPePassport = await _context.AddAsync(examination);
            await SaveAsync(cancellationToken);
            return newPePassport;
        }

        public async Task<ExaminationDetailsViewModel> GetExaminationDetailsAsync(string encryptedID)
        {
            var query = await ExaminationTrainingCenterPEPassportPEWelderRegistrationUIColorsGroup(encryptedID);
            return await query.ProjectTo<ExaminationDetailsViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }
        
        public async Task<ExaminationUpdateViewModel> GetExaminationUpdateAsync(string encryptedID)
        {
            var query = await ExaminationTrainingCenterPEPassportPEWelderRegistrationUIColorsGroup(encryptedID);
            return await query.ProjectTo<ExaminationUpdateViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<int> DeleteExaminationByEncryptedIDAsync(string encryptedID, CancellationToken token)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            _context.Examinations.Remove(new Examination { ID = decryptedID });
            return await SaveAsync(token);
        }

        public async Task<IQueryable<ExaminationTrainingCenterPEPassportPEWelderRegistrationUIColorsGroup>> ExaminationTrainingCenterPEPassportPEWelderRegistrationUIColorsGroup(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            AppSettings app = await _appSettingsSQLRepository.GetAppsetingsAsync();

            IQueryable<ExaminationTrainingCenterPEPassportPEWelderRegistrationUIColorsGroup> query = _context.Examinations
                .Where(examination => examination.ID == decryptedID)
                .Include(examination => examination.TrainingCenter.Company)
                .Include(examination => examination.Registrations)
                .Select(
                    examination => new ExaminationTrainingCenterPEPassportPEWelderRegistrationUIColorsGroup
                    {
                        Examination = examination,
                        PEPassportPEWelderRegistrationUIColors = examination.Registrations
                            .Select(registration => new
                            {
                                Registration = registration,
                                ExtendableStatus =
                                            registration.Revoke != null ? ExtendableStatus.Revoked :
                                            EF.Functions.DateDiffDay(DateTime.Now, registration.ExpiryDate) > app.MaxInAdvanceDays ? ExtendableStatus.NotYetExtendable :
                                            (EF.Functions.DateDiffDay(DateTime.Now, registration.ExpiryDate) > (app.MaxExtensionDays * -1) ? ExtendableStatus.Extendable :
                                            ExtendableStatus.NoMoreExtendable)

                            })
                            .Join(
                                _context.UIColors.DefaultIfEmpty(),
                                registrationExtendableStatus => new
                                {
                                    ExtendableStatus = registrationExtendableStatus.ExtendableStatus,
                                    HasPassed = (bool)(registrationExtendableStatus.Registration.HasPassed.HasValue ? registrationExtendableStatus.Registration.HasPassed : false)
                                },
                                uiColor => new
                                {
                                    ExtendableStatus = uiColor.ExtendableStatus,
                                    HasPassed = (bool)(uiColor.HasPassed.HasValue ? uiColor.HasPassed : false)
                                },
                                (registrationExtendableStatus, uiColor) => new RegistrationUIColorGroup
                                {
                                    Registration = registrationExtendableStatus.Registration,
                                    UIColor = uiColor
                                }
                            )
                    }

                );

            return query;
        }

        private IQueryable<ExaminationIndexViewModel> GetExaminationsIndex()
        {
            return _context.Examinations.ProjectTo<ExaminationIndexViewModel>(_mapper.ConfigurationProvider);
        }

        public async Task<IPaginatedList<ExaminationIndexViewModel>> GetExaminationsIndexPaginatedAsync(int pageSize, int pageIndex, string searchString, string sortOrder)
        {
            var weldersQuery = GetExaminationsIndex();

            weldersQuery = SearchExaminationIndex(weldersQuery, searchString);

            weldersQuery = SortExaminationIndex(weldersQuery, sortOrder);

            return await PaginatedList<ExaminationIndexViewModel>.CreateAsync(weldersQuery.AsNoTracking(), pageIndex, pageSize);
        }

        private static IQueryable<ExaminationIndexViewModel> SortExaminationIndex(IQueryable<ExaminationIndexViewModel> examinationsQuery, string sortOrder)
        {
            switch (sortOrder)
            {
                case "ExamDate_desc":
                    examinationsQuery = examinationsQuery.OrderByDescending(examination => examination.ExamDate);
                    return examinationsQuery;
                case "ExamDate_asc":
                case null:
                case "":
                    examinationsQuery = examinationsQuery.OrderBy(examination => examination.ExamDate);
                    return examinationsQuery;
                case "CompanyName_desc":
                    examinationsQuery = examinationsQuery.OrderByDescending(examination => examination.CompanyName);
                    return examinationsQuery;
                case "CompanyName_asc":
                    examinationsQuery = examinationsQuery.OrderBy(examination => examination.CompanyName);
                    return examinationsQuery;
                case "NumberOfPassports_desc":
                    examinationsQuery = examinationsQuery.OrderByDescending(examination => examination.NumberOfPassports);
                    return examinationsQuery;
                case "NumberOfPassports_asc":
                    examinationsQuery = examinationsQuery.OrderBy(examination => examination.NumberOfPassports);
                    return examinationsQuery;
                default:
                    throw new InvalidOperationException("SortOrder nout found.");
            }
        }

        private static IQueryable<ExaminationIndexViewModel> SearchExaminationIndex(IQueryable<ExaminationIndexViewModel> examinationsQuery, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                examinationsQuery = examinationsQuery.Where(examination => examination.CompanyName.Contains(searchString.ToLower()));
            }

            return examinationsQuery;
        }
    }
}
