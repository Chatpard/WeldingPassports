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
using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;

        public CertificatesSQLRepository(AppDbContext context, IRevokeSQLRepository revokeSQLRepository,IMapper mapper,
            IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings) : base(context)
        {
            _context = context;
            this._revokeSQLRepository=revokeSQLRepository;
            _mapper = mapper;
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }
        
        public async Task PostCertificateCreateAsync(CertificateCreateViewModel vm, CancellationToken cancellationToken)
        {
            var registration = _mapper.Map<Registration>(vm);
            registration.ExpiryDate = ((DateTime)vm.ExamDate).AddDays((await _context.AppSettings.FirstAsync()).MaxExpiryDays);
            registration.PreviousRegistrationID = _context.Registrations
                .Where(anyRegistration => anyRegistration.PEPassportID == registration.PEPassportID)
                .OrderByDescending(registration => registration.Examination.ExamDate)
                .FirstOrDefault()?.ID;
            //var registration = new Registration
            //{
            //    ExaminationID = test.ExaminationID,
            //    PEPassportID = vm.PEPassportID,
            //    CompanyID = vm.CompanyID,
            //    ProcessID = vm.ProcessID,
            //    RegistrationTypeID = vm.RegistrationTypeID,
            //    ExpiryDate = ((DateTime)vm.ExamDate).AddDays((await _context.AppSettings.FirstAsync()).MaxExpiryDays)
            //};
            _context.Registrations.Add(registration);
           await SaveChangesAsync(cancellationToken);
        }

        public async Task<CertificateCreateViewModel> GetCertificateCreateAsync(string examinationEncryptedID)
        {
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

            vm.ExpiryDate = ((DateTime)vm.ExamDate).AddDays((await _context.AppSettings.FirstAsync()).MaxExpiryDays);

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

            var passports = await _context.PEPassports
                .OrderBy(passport => passport.AVNumber)
                .Where(passport => passport.TrainingCenterID == trainingCenterID)
                .Where(passport => ! passport.Registrations.Where(registration => registration.ExaminationID == examinationDecryptedID).Any())
                .Select(passport => new {
                    ID = passport.ID,
                    AVNumber = $"{passport.TrainingCenter.Letter} {passport.AVNumber.ToString("D5")} ({passport.PEWelder.FirstName} {passport.PEWelder.LastName})"
                }).ToListAsync();
            vm.PEPassportAVNumberItems = new SelectList(passports, nameof(PEPassport.ID), nameof(PEPassport.AVNumber));

            var processes = await _context.Processes
                .OrderBy(process => process.ProcessName)
                .Select(process => new {
                    ID = process.ID,
                    ProcessName = process.ProcessName
                }).ToListAsync();
            vm.ProcessNameItems = new SelectList(processes, nameof(Process.ID), nameof(Process.ProcessName));

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
                CompanyID = vm.CurrentCertificateCompanyID,
                RegistrationTypeID = vm.CurrentCertificateRegistrationTypeID,
                HasPassed = vm.CurrentCertificateHasPassed
            };

            _context.Registrations.Attach(registrationChanges);
            _context.Entry(registrationChanges).Property(registration => registration.ProcessID).IsModified = true;
            _context.Entry(registrationChanges).Property(registration => registration.CompanyID).IsModified = true;
            _context.Entry(registrationChanges).Property(registration => registration.RegistrationTypeID).IsModified = true;
            _context.Entry(registrationChanges).Property(registration => registration.HasPassed).IsModified = true;

            await SaveChangesAsync(cancellationToken);

            if (vm.CurrentCertificateRevokedByCompanyContactID == null)
            {
                await _revokeSQLRepository.DeleteByCertificateEncryptedID(vm.EncryptedID);
            }
            else
            {
                var revoke = new Revoke()
                {
                    RegistrationID = registrationChanges.ID,
                    RevokeDate = vm.CurrentCertificateRevokeDate,
                    CompanyContactID = (int)vm.CurrentCertificateRevokedByCompanyContactID,
                    Comment = vm.CurrentCertificateRevokeComment
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
                        HasNext = _context.Registrations.Any(anyRegistration => anyRegistration.PreviousRegistrationID == registration.ID)
                    })
                .SingleOrDefaultAsync();
            //var vm = registration.ProjectTo<CertificateEditViewModel>(_mapper.ConfigurationProvider);
            var vm = _mapper.Map<CertificateEditViewModel>(registration);

            vm.EncryptedID = encryptedID;

            var processes = _context.Processes
                .OrderBy(process => process.ProcessName)
                .Select(process => new {
                    ID = process.ID,
                    ProcessName = process.ProcessName
                });
            vm.ProcessNameItems = new SelectList(processes, nameof(Process.ID), nameof(Process.ProcessName));

            var companies = _context.Companies
                .OrderBy(company => company.CompanyName)
                .Select(company => new {
                    ID = company.ID,
                    CompanyName = company.CompanyName
                });
            vm.CompanyNameItems = new SelectList(companies, nameof(Company.ID), nameof(Company.CompanyName));

            var registrationTypes = _context.RegistrationTypes
                .OrderBy(registrationType => registrationType.RegistrationTypeName)
                .Select(registrationType => new {
                    ID = registrationType.ID,
                    RegistrationTypeName = registrationType.RegistrationTypeName
                });
            vm.RegistrationTypeNameItems = new SelectList(registrationTypes, nameof(RegistrationType.ID), nameof(RegistrationType.RegistrationTypeName));

            var companyContacts = _context.CompanyContacts
                .OrderBy(companyContact => companyContact.Contact.FirstName)
                .Select(companyContact => new {
                    ID = companyContact.ID,
                    CompanyContactName = companyContact.Contact.FirstName + " " + companyContact.Contact.LastName
                });
            vm.CompanyContactNameItems = new SelectList(companyContacts, nameof(RegistrationType.ID), "CompanyContactName");

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
    }
}
