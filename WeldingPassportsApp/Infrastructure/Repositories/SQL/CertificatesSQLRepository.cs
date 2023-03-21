using Application;
using Application.Interfaces.Repositories.SQL;
using Application.Security;
using Application.SQLModels;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQL
{
    [AllowAnonymous]
    public class CertificatesSQLRepository : SaveChangesSQL, ICertificatesSQLRepository
    {
        private readonly AppDbContext _context;
        private readonly IRevokeSQLRepository _revokeSQLRepository;
        private readonly IAppSettingsSQLRepository _appSettingsSQLRepository;
        private readonly IMapper _mapper;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IDataProtectionPurposeStrings _dataProtectionPurposeStrings;
        private readonly IDataProtector _protector;

        public CertificatesSQLRepository(AppDbContext context, IRevokeSQLRepository revokeSQLRepository, IAppSettingsSQLRepository appSettingsSQLRepository, IMapper mapper,
            IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings) : base(context)
        {
            _context = context;
            _revokeSQLRepository = revokeSQLRepository;
            _appSettingsSQLRepository = appSettingsSQLRepository;
            _mapper = mapper;
            _dataProtectionProvider = dataProtectionProvider;
            _dataProtectionPurposeStrings = dataProtectionPurposeStrings;
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }
        
        public async Task PostCertificateCreateAsync(CertificateCreateViewModel vm, CancellationToken cancellationToken)
        {
            if (vm == null) throw new ArgumentNullException();
            Registration currentRegistration = await CurrentRegistration(vm.PEPassportID, vm.ProcessID, HasPassedNotNull: false);
            Registration newRegistration = _mapper.Map<Registration>(vm);
            if (currentRegistration != null && currentRegistration?.HasPassed == null)
            {
                Registration registration = await _context.Registrations.Where(certification => certification.ID == currentRegistration.ID).FirstOrDefaultAsync();
                registration.ExaminationID = newRegistration.ExaminationID;
                registration.CompanyID = newRegistration.CompanyID;
                registration.ExpiryDate = newRegistration.ExpiryDate;
                registration.CertificatePath = newRegistration.CertificatePath;
                _context.Update(registration);
            }
            else
            {
                newRegistration.PreviousRegistrationID = currentRegistration?.ID;
                _context.Registrations.Add(newRegistration);
            }
            await SaveChangesAsync(cancellationToken);
        }

        public async Task<CertificateCreateViewModel> GetCertificateCreateAsync(string examinationEncryptedID)
        {
            AppSettings appSettings = await _context.AppSettings.FirstAsync();

            var vm = new CertificateCreateViewModel();
            vm.ExaminationEncryptedID = examinationEncryptedID;

            int examinationDecryptedID = Convert.ToInt32(_protector.Unprotect(examinationEncryptedID));

            int trainingCenterID = await _context.Examinations
                .Where(e => e.ID == examinationDecryptedID)
                .Select(e => e.TrainingCenterID)
                .SingleOrDefaultAsync();

            vm.ExamDate = await _context.Examinations
                .Where(e => e.ID == examinationDecryptedID)
                .Select(e => e.ExamDate)
                .SingleOrDefaultAsync();

            vm.ExpiryDate = null; // ((DateTime)vm.ExamDate).AddDays(appSettings.MaxExpiryDays);

            vm.ExamCenterName = await _context.Examinations
                .Where(e => e.ID == examinationDecryptedID)
                .Select(e => e.ExamCenter.Company.CompanyName)
                .SingleOrDefaultAsync();

            vm.TrainingCenterName = await _context.Examinations
                .Where(e => e.ID == examinationDecryptedID)
                .Select(e => e.TrainingCenter.Company.CompanyName)
                .SingleOrDefaultAsync();

            var companies = await _context.Companies
                .OrderBy(company => company.CompanyName)
                .Select(company => new {
                    ID = company.ID,
                    CompanyName = company.CompanyName
                }).ToListAsync();
            vm.CompanyNameItems = new SelectList(companies, nameof(Company.ID), nameof(Company.CompanyName));

            //todo: PEPassportSelectList remove if ok
            //var passports = await _context.PEPassports
            //    .OrderBy(passport => passport.AVNumber)
            //    .Where(passport => passport.TrainingCenterID == trainingCenterID)
            //    .Where(passport => ! passport.Registrations.Where(registration => registration.ExaminationID == examinationDecryptedID).Any())
            //    .Where(passport => ! passport.Registrations.Where(registration => (
            //            (registration.Revoke == null) && 
            //            (((DateTime) registration.ExpiryDate).AddDays(-1 * appSettings.MaxInAdvanceDays) > vm.ExamDate))
            //        ).Any())
            //    .Where(passport => ! passport.Registrations.Where(registration =>
            //        registration.Revoke.RevokeDate > vm.ExamDate).Any())
            //    .Select(passport => new {
            //        ID = passport.ID,
            //        AVNumber = $"{passport.TrainingCenter.Letter} {passport.AVNumber.ToString("D5")} ({passport.PEWelder.FirstName} {passport.PEWelder.LastName})"
            //    }).ToListAsync();
            vm.PEPassportAVNumberItems = await GetPEPassportSelectList(examinationDecryptedID);

            var processes = await _context.Processes
                .OrderBy(process => process.ProcessName)
                .Select(process => new
                {
                    ID = process.ID,
                    ProcessName = process.ProcessName
                }).ToListAsync();
            vm.ProcessNameItems = new SelectList(processes, nameof(Domain.Models.Process.ID), nameof(Domain.Models.Process.ProcessName));

            var registrationTypes = await _context.RegistrationTypes
                .OrderBy(registrationType => registrationType.RegistrationTypeName)
                .Select(registrationType => new {
                    ID = registrationType.ID,
                    RegistrationTypeName = registrationType.RegistrationTypeName
                }).ToListAsync();
            vm.RegistrationTypeNameItems = new SelectList(registrationTypes, nameof(RegistrationType.ID), nameof(RegistrationType.RegistrationTypeName));

            return vm;
        }

        public async Task PostCertificateEditAsync(CertificateEditViewModel vm, CancellationToken cancellationToken)
        {
            var registrationChanges = new Registration()
            {
                ID = Convert.ToInt32(_protector.Unprotect(vm.EncryptedID)),
                ProcessID = vm.ProcessID,
                CompanyID = vm.CompanyID,
                RegistrationTypeID = vm.RegistrationTypeID,
                ExpiryDate = vm.ExpiryDate,
                HasPassed = vm.HasPassed
            };

            _context.Registrations.Attach(registrationChanges);
            _context.Entry(registrationChanges).Property(registration => registration.ProcessID).IsModified = true;
            _context.Entry(registrationChanges).Property(registration => registration.CompanyID).IsModified = true;
            _context.Entry(registrationChanges).Property(registration => registration.RegistrationTypeID).IsModified = true;
            _context.Entry(registrationChanges).Property(registration => registration.ExpiryDate).IsModified = true;
            _context.Entry(registrationChanges).Property(registration => registration.HasPassed).IsModified = true;

            await SaveChangesAsync(cancellationToken);

            if (vm.RevokedByCompanyContactID == null)
            {
                await _revokeSQLRepository.DeleteByCertificateEncryptedID(vm.EncryptedID);
            }
            else
            {
                var revoke = new Revoke()
                {
                    RegistrationID = registrationChanges.ID,
                    RevokeDate = vm.RevokeDate,
                    CompanyContactID = (int)vm.RevokedByCompanyContactID,
                    Comment = vm.RevokeComment
                };

                int? id = await _context
                            .Registrations
                            .Where(registration => registration.ID == registrationChanges.ID)
                            .Select(registration => registration.Revoke != null ? (int?)registration.Revoke.ID : null)
                            .SingleOrDefaultAsync();

                if (id != null)
                {
                    revoke.ID = (int)id;
                    _context.Revokes.Attach(revoke);
                    _context.Entry(revoke).Property(revoke => revoke.RevokeDate).IsModified = true;
                    _context.Entry(revoke).Property(revoke => revoke.CompanyContactID).IsModified = true;
                    _context.Entry(revoke).Property(revoke => revoke.Comment).IsModified = true;
                }
                else
                {
                    _context.Revokes.Add(revoke);
                }
            }

            await SaveChangesAsync(cancellationToken);
        }

        public async Task PostCertificateUpdateAsync(string registrationEncryptedID, bool? HasPassed, CancellationToken cancellationToken)
        {
            int registrationDecryptedID = Convert.ToInt32(_protector.Unprotect(registrationEncryptedID));
            Registration registration = await _context.Registrations.Where(registration => registration.ID == registrationDecryptedID).FirstOrDefaultAsync();
            if (registration != null)
            {
                registration.HasPassed = HasPassed;
                EntityEntry<Registration> registrationEntityEntry = _context.Entry(registration);
                registrationEntityEntry.State = EntityState.Modified;
            }
        }

        public async Task<CertificateEditViewModel> GetCertificateEditAsync(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            var registration = await _context.Registrations
                .Where(registration => registration.ID == decryptedID)
                .Include(registration => registration.PEPassport)
                .Include(registration => registration.PEPassport.PEWelder)
                .Include(registration => registration.PEPassport.TrainingCenter)
                .Include(registration => registration.PEPassport.TrainingCenter.Company)
                .Include(registration => registration.Process)
                .Include(registration => registration.Company)
                .Include(registration => registration.Examination)
                .Include(registration => registration.RegistrationType)
                .Include(registration => registration.Revoke)
                .Include(registration => registration.Revoke.CompanyContact)
                .Include(registration => registration.Revoke.CompanyContact.Contact)
                .Include(registration => registration.Examination)
                .Include(registration => registration.Examination.ExamCenter)
                .Include(registration => registration.Examination.ExamCenter.Company)
                .Include(registration => registration.PreviousRegistration)
                .Include(registration => registration.PreviousRegistration.Revoke)
                .Include(registration => registration.PreviousRegistration.Revoke.CompanyContact)
                .Include(registration => registration.PreviousRegistration.Revoke.CompanyContact.Contact)
                .Select(registration => new RegistrationHasNext
                {
                        ID = registration.ID,
                        PreviousRegistrationID = registration.PreviousRegistrationID,
                        ExaminationID = registration.ExaminationID,
                        PEPassportID = registration.PEPassportID,
                        RegistrationTypeID = registration.RegistrationTypeID,
                        ProcessID = registration.ProcessID,
                        CompanyID = registration.CompanyID,
                        ExpiryDate = registration.ExpiryDate,
                        HasPassed = registration.HasPassed,
                        CertificatePath = registration.CertificatePath,
                        Examination = registration.Examination,
                        PEPassport = registration.PEPassport,
                        RegistrationType =  registration.RegistrationType,
                        Process = registration.Process,
                        PreviousRegistration = registration.PreviousRegistration,
                        Company = registration.Company,
                        Revoke = registration.Revoke,
                        HasNext = _context.Registrations.Any(anyRegistration => anyRegistration.PreviousRegistrationID == registration.ID && anyRegistration.ProcessID == registration.ProcessID)
                    })
                .SingleOrDefaultAsync();
            var vm = _mapper.Map<CertificateEditViewModel>(registration);

            vm.EncryptedID = encryptedID;

            //todo: ProcessNameItems remove if ok
            //var processes = _context.Processes
            //    .OrderBy(process => process.ProcessName)
            //    .Select(process => new {
            //        ID = process.ID,
            //        ProcessName = process.ProcessName
            //    });
            //vm.ProcessNameItems = new SelectList(processes, nameof(Domain.Models.Process.ID), nameof(Domain.Models.Process.ProcessName));
            vm.ProcessNameItems = await GetProcessNamesSelectList(registration.ExaminationID, registration.PEPassportID, registration.ID);

            var companies = _context.Companies
                .OrderBy(company => company.CompanyName)
                .Select(company => new {
                    ID = company.ID,
                    CompanyName = company.CompanyName
                });
            vm.CompanyNameItems = new SelectList(companies, nameof(Company.ID), nameof(Company.CompanyName));

            var getRegistrationTypesSelectListResponse = await GetRegistrationTypesSelectListFromRegistration(registration);
            var registrationTypeNameItems = getRegistrationTypesSelectListResponse.RegistrationsSelectList;
            vm.RegistrationTypeNameItems = registrationTypeNameItems;

            var companyContacts = _context.CompanyContacts
                .OrderBy(companyContact => companyContact.Contact.FirstName)
                .Select(companyContact => new {
                    ID = companyContact.ID,
                    CompanyContactName = companyContact.Contact.FirstName + " " + companyContact.Contact.LastName
                });

            vm.CompanyContactNameItems = new SelectList(companyContacts, nameof(RegistrationType.ID), "CompanyContactName");

            vm.MaxExpiryDate = await GetCertificateMaxExpirationDate(vm.PEPassportID, vm.ProcessID, vm.ExamDate);

            return vm;
        }

        public async Task<CertificateDetailsViewModel> GetCertificateDetailsAsync(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));

            var vm = await _context.Registrations
                .Where(registration => registration.ID == decryptedID)
                .ProjectTo<CertificateDetailsViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            vm.EncryptedID = encryptedID;
            
            return vm;
        }
    
        public async Task<EntityEntry<Registration>> DeleteByEncryptedID(string encryptedID)
        {
            int decryptedID = Convert.ToInt32(_protector.Unprotect(encryptedID));
            Registration registration = await _context.Registrations.FindAsync(decryptedID);
            if(registration == null) { return null; };

            EntityEntry<Registration> certificateEntityEntry = _context.Registrations.Remove(registration);

            return certificateEntityEntry;
        }

        public async Task<GetRegistrationTypesFromPEPassportReponse> GetRegistrationTypesSelectListFromPEPassport(int? pePassportID, int? processID, DateTime examDate, Registration previousRegistration = null)
        {
            Registration currentRegistration = await CurrentRegistration(pePassportID, processID, previousRegistration);
            if (currentRegistration == null)
            {
                currentRegistration = new Registration()
                {
                    PEPassportID = (int) pePassportID,
                    ProcessID = (int) processID,
                };
            }

            return await GetRegistrationTypesSelectListByCurrentRegistration(currentRegistration);
        }

        private async Task<GetRegistrationTypesFromPEPassportReponse> GetRegistrationTypesSelectListFromRegistration(Registration registration)
        {
            return await GetRegistrationTypesSelectListByCurrentRegistration(registration);
        }

        private async Task<GetRegistrationTypesFromPEPassportReponse> GetRegistrationTypesSelectListByCurrentRegistration(Registration registration) {
            if (registration?.PEPassportID != null)
            {
                AppSettings app = await _appSettingsSQLRepository.GetAppsetingsAsync();

                IEnumerable<Registration> currentRegistrationsList = new List<Registration>() { registration };

                var allowedRegistrationTypesQ = currentRegistrationsList.Select(registration => new
                {
                    PreviousRegistrationTypeID = (int?) registration.PreviousRegistration?.RegistrationTypeID,
                    ExtendableStatus =
                            registration.PreviousRegistration?.Revoke != null ? ExtendableStatus.Revoked :
                            registration.PreviousRegistration?.ExpiryDate == null ? ExtendableStatus.NoMoreExtendable :
                            EF.Functions.DateDiffDay(registration.PreviousRegistration?.ExpiryDate, registration.Examination?.ExamDate) < (app.MaxInAdvanceDays * -1) ? ExtendableStatus.NotYetExtendable :
                            EF.Functions.DateDiffDay(registration.PreviousRegistration?.ExpiryDate, registration.Examination?.ExamDate) < (app.MaxExtensionDays + 1) ? ExtendableStatus.Extendable : ExtendableStatus.NoMoreExtendable,
                    HasPassed = registration.PreviousRegistration?.HasPassed,
                    IsNew = (bool?) (registration.ID == 0),
                    CurrentRegistrationTypeID = (int?) registration.RegistrationTypeID
                });

                IEnumerable<RegistrationType> allowedRegistrationT = _context.AllowedRegistrationTypes
                    .Include(allowedRegistrationTypes => allowedRegistrationTypes.AvailableRegistrationType)
                    .Where(registrationType =>
                        registrationType.ExtendableStatus == 0 &&
                        registrationType.HasPassed == null &&
                        registrationType.PreviousRegistrationTypeID == null &&
                        registrationType.IsNew == false &&
                        registrationType.CurrentRegistrationTypeID == 1)
                    .Select(registrationType => new RegistrationType
                        {
                            ID = registrationType.AvailableRegistrationType.ID,
                            RegistrationTypeName = registrationType.AvailableRegistrationType.RegistrationTypeName
                        }
                    );

                IEnumerable<RegistrationType> allowedRegistrationTypes = allowedRegistrationTypesQ
                    .Join(
                        _context.AllowedRegistrationTypes.Include(allowedRegistrationTypes => allowedRegistrationTypes.AvailableRegistrationType),
                        registrationType => new
                        {
                            PreviousRegistrationTypeID = registrationType.PreviousRegistrationTypeID != null ? registrationType.PreviousRegistrationTypeID : 0,
                            ExtendableStatus = registrationType.ExtendableStatus,
                            HasPassed = registrationType.HasPassed != null ? registrationType.HasPassed : false,
                            IsNew = registrationType.IsNew,
                            CurrentRegistrationTypeID = registrationType.CurrentRegistrationTypeID != null ? registrationType.CurrentRegistrationTypeID : 0
                        },
                        allowedRegistrationType => new
                        {
                            PreviousRegistrationTypeID = allowedRegistrationType.PreviousRegistrationTypeID != null ? allowedRegistrationType.PreviousRegistrationTypeID : 0,
                            ExtendableStatus = allowedRegistrationType.ExtendableStatus,
                            HasPassed = allowedRegistrationType.HasPassed != null ? allowedRegistrationType.HasPassed : false,
                            IsNew = allowedRegistrationType.IsNew,
                            CurrentRegistrationTypeID = allowedRegistrationType.CurrentRegistrationTypeID != null ? allowedRegistrationType.CurrentRegistrationTypeID : 0
                        },
                (registrationType, allowedRegistrationType) => new RegistrationType
                        {
                            ID = allowedRegistrationType.AvailableRegistrationType.ID,
                            RegistrationTypeName = allowedRegistrationType.AvailableRegistrationType.RegistrationTypeName,
                            CurrentRegistrationTypeID = allowedRegistrationType.CurrentRegistrationTypeID,
                            AllowedRegistrationTypeID = allowedRegistrationType.ID
                });

                return new GetRegistrationTypesFromPEPassportReponse
                {
                    RegistrationsSelectList = new SelectList(allowedRegistrationTypes, nameof(RegistrationType.ID), nameof(RegistrationType.RegistrationTypeName))
                };
            }
            else
            {
                IEnumerable<RegistrationType> allowedRegistrationTypes = _context.RegistrationTypes
                    .Where(registrationType => registrationType.IsActive)
                    .Select(registrationType => new RegistrationType
                    {
                        ID = registrationType.ID,
                        RegistrationTypeName = registrationType.RegistrationTypeName
                    });

                return new GetRegistrationTypesFromPEPassportReponse
                {
                    RegistrationsSelectList = new SelectList(allowedRegistrationTypes, nameof(RegistrationType.ID), nameof(RegistrationType.RegistrationTypeName))
                };
            }
        }

        public async Task<DateTime?> GetCertificateMaxExpirationDate(int? pePassportID, int? processID, DateTime? examDate = null)
        {
            AppSettings appSettings = await _context.AppSettings.FirstAsync();

            Registration currentRegistration = await CurrentRegistration(pePassportID, processID);

            DateTime? maxExpiryDate = new DateTime();

            if(currentRegistration?.ExpiryDate == null)
            {
                maxExpiryDate = examDate;
            }
            else
            {
                maxExpiryDate = currentRegistration?.ExpiryDate;
            }

            return maxExpiryDate?.AddDays(appSettings.MaxExpiryDays);
        }

        private async Task<Registration> CurrentRegistration(int? pePassportID, int? processID, Registration previousRegistration = null, bool HasPassedNotNull = true )
        {
            if (pePassportID == null)
            {
                return null;
            }

            if(previousRegistration != null)
            {
                return previousRegistration;
            }

            PEPassport pePassport = await _context.PEPassports
                .Where(pePassport => pePassport.ID == pePassportID)
                .Include(pePassport => pePassport.PEWelder)
                .SingleOrDefaultAsync();

            if (pePassport == null)
            {
                return null;
            }

            IQueryable<Registration> registrations = _context.Registrations;

            if (HasPassedNotNull)
            {
                registrations = registrations
                    .Where(registration => registration.HasPassed != null);
            }

            if (processID!= null)
            {
                registrations = registrations
                    .Where(registration => registration.ProcessID == processID);
            }

            registrations = registrations
                .Where(registration => registration.PEPassport.PEWelderID == pePassport.PEWelderID)
                .Where(registration => _context.Revokes.All(revoke => revoke.RegistrationID != registration.ID));

            if (registrations.Count() == 0)
            {
                return null;
            }

            return registrations
                .Join(
                        _context.Examinations.DefaultIfEmpty(),
                        registration => new
                        {
                            ExaminationID = registration.ExaminationID
                        },
                        examination => new
                        {
                            ExaminationID = examination.ID
                        },
                        (registration, examination) => new Registration
                        {
                            ID = registration.ID,
                            RegistrationTypeID = registration.RegistrationTypeID,
                            ProcessID = registration.ProcessID,
                            CompanyID = registration.CompanyID,
                            ExpiryDate= registration.ExpiryDate,
                            Examination = examination,
                            HasPassed = registration.HasPassed,
                            Revoke = registration.Revoke
                        })
                    .OrderByDescending(registration => registration.Examination.ExamDate)
                    .FirstOrDefault();
        }

        private async Task<SelectList> GetPEPassportSelectList(int examinationDecryptedID)
        {
            AppSettings appSettings = await _context.AppSettings.FirstAsync();

            IQueryable PEPassportsQuery = _context.PEWelders
                 .Join(_context.PEPassports,
                 pewelder => new
                 {
                     PEWelderID = pewelder.ID
                 },
                 pepassport => new
                 {
                     PEWelderID = pepassport.PEWelderID
                 },
                 (pewelder, pepassport) => new
                 {
                     pewelder = pewelder,
                     pepassport = pepassport
                 })
                .Join(_context.TrainingCenters,
                peWelderPEPassport => new
                {
                    TrainingCenterID = peWelderPEPassport.pepassport.TrainingCenterID
                },
                trainingCenter => new
                {
                    TrainingCenterID = trainingCenter.ID,
                },
                (peWelderPEPassport, trainingCenter) => new
                {
                    pewelder = peWelderPEPassport.pewelder,
                    pepassport = peWelderPEPassport.pepassport,
                    trainingCenter = trainingCenter
                })
                .Join(_context.Processes.DefaultIfEmpty(),
                peWelderPEPassportTrainingCenter => new
                {
                    Link = 1
                },
                process => new
                {
                    Link = 1
                },
                (peWelderPEPassportTrainingCenter, process) => new
                {
                    pewelder = peWelderPEPassportTrainingCenter.pewelder,
                    pepassport = peWelderPEPassportTrainingCenter.pepassport,
                    trainingCenter = peWelderPEPassportTrainingCenter.trainingCenter,
                    anyProcess = process
                })
                .GroupJoin(_context.Registrations,
                peWelderPEPassportTrainingCenterProcess => new
                {
                    PEPassportID = peWelderPEPassportTrainingCenterProcess.pepassport.ID,
                    ProcessID = peWelderPEPassportTrainingCenterProcess.anyProcess.ID,
                    HasPassed = true
                },
                registration => new
                {
                    PEPassportID = registration.PEPassportID,
                    ProcessID = registration.ProcessID,
                    HasPassed = (bool)registration.HasPassed || registration.ExaminationID == examinationDecryptedID
                },
                (peWelderPEPassportTrainingCenterProcess, registration) => new
                {
                    pewelder = peWelderPEPassportTrainingCenterProcess.pewelder,
                    pepassport = peWelderPEPassportTrainingCenterProcess.pepassport,
                    trainingCenter = peWelderPEPassportTrainingCenterProcess.trainingCenter,
                    anyProcess = peWelderPEPassportTrainingCenterProcess.anyProcess,
                    registration = registration
                })
                .SelectMany(peWelderPEPassportTrainingCenterProcess => peWelderPEPassportTrainingCenterProcess.registration.DefaultIfEmpty(),
                (peWelderPEPassportTrainingCenterProcess, registration) => new
                {
                    pewelder = peWelderPEPassportTrainingCenterProcess.pewelder,
                    pepassport = peWelderPEPassportTrainingCenterProcess.pepassport,
                    trainingCenter = peWelderPEPassportTrainingCenterProcess.trainingCenter,
                    anyProcess = peWelderPEPassportTrainingCenterProcess.anyProcess,
                    registration = registration
                })
                .GroupJoin(_context.Examinations,
                peWelderPEPassportTrainingCenterProcessRegistration => new
                {
                    ExaminationID = peWelderPEPassportTrainingCenterProcessRegistration.registration.ExaminationID
                },
                examination => new
                {
                    ExaminationID = examination.ID
                },
                (peWelderPEPassportTrainingCenterProcessRegistration, examination) => new
                {
                    pewelder = peWelderPEPassportTrainingCenterProcessRegistration.pewelder,
                    pepassport = peWelderPEPassportTrainingCenterProcessRegistration.pepassport,
                    trainingCenter = peWelderPEPassportTrainingCenterProcessRegistration.trainingCenter,
                    anyProcess = peWelderPEPassportTrainingCenterProcessRegistration.anyProcess,
                    registration = peWelderPEPassportTrainingCenterProcessRegistration.registration,
                    examination = examination
                })
                .SelectMany(peWelderPEPassportTrainingCenterProcessRegistration => peWelderPEPassportTrainingCenterProcessRegistration.examination.DefaultIfEmpty(),
                (peWelderPEPassportTrainingCenterProcessRegistration, examination) => new
                {
                    pewelder = peWelderPEPassportTrainingCenterProcessRegistration.pewelder,
                    pepassport = peWelderPEPassportTrainingCenterProcessRegistration.pepassport,
                    trainingCenter = peWelderPEPassportTrainingCenterProcessRegistration.trainingCenter,
                    anyProcess = peWelderPEPassportTrainingCenterProcessRegistration.anyProcess,
                    registration = peWelderPEPassportTrainingCenterProcessRegistration.registration,
                    examination = examination
                })
                .GroupJoin(_context.Processes.DefaultIfEmpty(),
                peWelderPEPassportTrainingCenterProcessRegistrationExamination => new
                {
                    ProcessID = peWelderPEPassportTrainingCenterProcessRegistrationExamination.registration.ProcessID
                },
                process => new
                {
                    ProcessID = process.ID
                },
                (peWelderPEPassportTrainingCenterProcessRegistrationExamination, process) => new
                {
                    pewelder = peWelderPEPassportTrainingCenterProcessRegistrationExamination.pewelder,
                    pepassport = peWelderPEPassportTrainingCenterProcessRegistrationExamination.pepassport,
                    trainingCenter = peWelderPEPassportTrainingCenterProcessRegistrationExamination.trainingCenter,
                    anyProcess = peWelderPEPassportTrainingCenterProcessRegistrationExamination.anyProcess,
                    registration = peWelderPEPassportTrainingCenterProcessRegistrationExamination.registration,
                    examination = peWelderPEPassportTrainingCenterProcessRegistrationExamination.examination,
                    process = process,
                })
                .SelectMany(peWelderPEPassportTrainingCenterProcessRegistrationExamination => peWelderPEPassportTrainingCenterProcessRegistrationExamination.process.DefaultIfEmpty(),
                (peWelderPEPassportTrainingCenterProcessRegistrationExamination, process) => new
                {
                    pewelder = peWelderPEPassportTrainingCenterProcessRegistrationExamination.pewelder,
                    pepassport = peWelderPEPassportTrainingCenterProcessRegistrationExamination.pepassport,
                    trainingCenter = peWelderPEPassportTrainingCenterProcessRegistrationExamination.trainingCenter,
                    anyProcess = peWelderPEPassportTrainingCenterProcessRegistrationExamination.anyProcess,
                    registration = peWelderPEPassportTrainingCenterProcessRegistrationExamination.registration,
                    examination = peWelderPEPassportTrainingCenterProcessRegistrationExamination.examination,
                    process = process,
                })
                .Where(wptpre => wptpre.trainingCenter.ID == _context.Examinations.Where(examination => examination.ID == examinationDecryptedID).SingleOrDefault().TrainingCenterID
                    && (wptpre.registration.ExpiryDate == null ||
                    (_context.Registrations
                    .Join(_context.PEPassports,
                    registration => new
                    {
                        PEPassportID = registration.PEPassportID
                    },
                    pePassport => new
                    {
                        PEPassportID = pePassport.ID
                    },
                    (registration, pePassport) => new
                    {
                        pePassport = pePassport,
                        registration = registration
                    })
                    .Join(_context.PEWelders,
                    pePassportRegistration => new
                    {
                        peWelderID = pePassportRegistration.pePassport.PEWelderID
                    },
                    peWelder => new
                    {
                        peWelderID = peWelder.ID
                    },
                    (pePassprotRegistration, peWelder) => new
                    {
                        pePassport = pePassprotRegistration.pePassport,
                        registration = pePassprotRegistration.registration,
                        peWelder = peWelder
                    })
                    .Where(pePassprotRegistrationpeWelder =>
                        pePassprotRegistrationpeWelder.registration.ProcessID == wptpre.anyProcess.ID
                        && (pePassprotRegistrationpeWelder.registration.HasPassed == true || pePassprotRegistrationpeWelder.registration.ExaminationID == examinationDecryptedID)
                        && pePassprotRegistrationpeWelder.peWelder.ID == wptpre.pewelder.ID)
                    .Max(pePassprotRegistrationpeWelder => pePassprotRegistrationpeWelder.registration.ExpiryDate)
                    == wptpre.registration.ExpiryDate) &&
                    wptpre.registration.ExpiryDate < ((DateTime)_context.Examinations.Where(examination => examination.ID == examinationDecryptedID).SingleOrDefault().ExamDate).AddDays(appSettings.MaxInAdvanceDays*-1)))
                .Select(wptpre => new
                {
                    ID = wptpre.pepassport.ID,
                    Name = wptpre.trainingCenter.Letter.ToString() + " " + wptpre.pepassport.AVNumber + " " + wptpre.pewelder.FirstName + " " + wptpre.pewelder.LastName
                }).Distinct();
            return new SelectList(PEPassportsQuery, "ID", "Name");
        }

        public async Task<SelectList> GetProcessNamesSelectList(int? examinationID, int? pePassportID, int? registrationID)
        {
            if (examinationID == null || pePassportID == null)
            {
                return null;
            }

            AppSettings appSettings = await _context.AppSettings.FirstAsync();

            IQueryable ProcessNamesQuery = _context.PEPassports
                .Join(_context.Processes,
                pePassport => new
                {
                    IsActive = true
                },
                process => new
                {
                    IsActive = process.IsActive
                },
                (pePassport, process) => new
                {
                    PEPassport = pePassport,
                    Process = process
                })
                .Join(_context.PEPassports,
                pePassportProcess => new
                {
                    PEWelderID = pePassportProcess.PEPassport.PEWelderID
                },
                pePassport => new
                {
                    PEWelderID = pePassport.PEWelderID
                },
                (pePassportProcess, pePassport) => new
                {
                    PEPassport = pePassportProcess.PEPassport,
                    Process = pePassportProcess.Process,
                    PEPassport2 = pePassport
                })
                .GroupJoin(_context.Registrations,
                pePassportProcessPEPassport2 => new
                {
                    PEPassportID = pePassportProcessPEPassport2.PEPassport.ID,
                    ProcessID = pePassportProcessPEPassport2.Process.ID
                },
                registration => new
                {
                    PEPassportID = registration.PEPassportID,
                    ProcessID = registration.ProcessID
                },
                (pePassportProcessPEPassport2, registration) => new
                {
                    PEPassport = pePassportProcessPEPassport2.PEPassport,
                    Process = pePassportProcessPEPassport2.Process,
                    PEPassport2 = pePassportProcessPEPassport2.PEPassport2,
                    Registration = registration
                })
                .SelectMany(pePassportProcessPEPassport2 => pePassportProcessPEPassport2.Registration.DefaultIfEmpty(),
                (pePassportProcessPEPassport2, registration) => new
                {
                    PEPassport = pePassportProcessPEPassport2.PEPassport,
                    Process = pePassportProcessPEPassport2.Process,
                    PEPassport2 = pePassportProcessPEPassport2.PEPassport2,
                    Registration = registration
                })
                .GroupJoin(_context.Revokes,
                pePassportProcessPEPassport2Registration => new
                {
                    RegistrationID = pePassportProcessPEPassport2Registration.Registration.ID
                },
                revoke => new
                {
                    RegistrationID = revoke.RegistrationID
                },
                (pePassportProcessPEPassport2Registration, revoke) => new
                {
                    PEPassport = pePassportProcessPEPassport2Registration.PEPassport,
                    Process = pePassportProcessPEPassport2Registration.Process,
                    PEPassport2 = pePassportProcessPEPassport2Registration.PEPassport2,
                    Registration = pePassportProcessPEPassport2Registration.Registration,
                    Revoke = revoke
                }
                )
                .SelectMany(pePassportProcessPEPassport2Registration => pePassportProcessPEPassport2Registration.Revoke.DefaultIfEmpty(),
                (pePassportProcessPEPassport2Registration, revoke) => new
                {
                    PEPassport = pePassportProcessPEPassport2Registration.PEPassport,
                    Process = pePassportProcessPEPassport2Registration.Process,
                    PEPassport2 = pePassportProcessPEPassport2Registration.PEPassport2,
                    Registration = pePassportProcessPEPassport2Registration.Registration,
                    Revoke = revoke
                }
                )
                .Where(pprp2rr => pprp2rr.PEPassport.ID == pePassportID
                    && (pprp2rr.Registration.ExpiryDate == null  || ((pprp2rr.Registration.HasPassed == null || pprp2rr.Revoke != null) && pprp2rr.Registration.ExaminationID != examinationID) 
                        || (_context.Registrations
                            .Join(_context.PEPassports,
                            registration => new
                            {
                                PEPassportID = registration.PEPassportID,
                                ProcessID = registration.ProcessID
                            },
                            pePassport => new
                            {
                                PEPassportID = pePassport.ID,
                                ProcessID = pprp2rr.Process.ID
                            },
                            (registration, pePassport) => new
                            {
                                Registration = registration,
                                PEPassport = pePassport
                            })
                            .Where(registrationPEPassport => registrationPEPassport.PEPassport.PEWelderID == pprp2rr.PEPassport.PEWelderID)
                            .Max(registrationPEPassport => registrationPEPassport.Registration.ExpiryDate)
                            == pprp2rr.Registration.ExpiryDate
                            && (pprp2rr.Registration.ExpiryDate < ((DateTime)_context.Examinations
                                .Where(examination => examination.ID == examinationID)
                                .SingleOrDefault()
                                .ExamDate).AddDays(appSettings.MaxInAdvanceDays)
                            || registrationID != null && (pprp2rr.Registration.ID == registrationID || pprp2rr.Registration.ID == null || pprp2rr.Registration.HasPassed != true))
                        )
                    )
                )
                .Select(pprp2r => new
                {
                    ID = pprp2r.Process.ID,
                    Name = pprp2r.Process.ProcessName
                });
            return new SelectList(ProcessNamesQuery, "ID", "Name");
        }
    }
}
