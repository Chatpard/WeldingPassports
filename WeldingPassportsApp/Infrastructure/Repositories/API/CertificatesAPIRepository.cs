using Application.Interfaces.Repositories.API;
using Application.Interfaces.Repositories.SQL;
using Application.Security;
using Application.SQLModels;
using Domain.Models;
using System.Threading;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Repositories.API
{
    public class CertificatesAPIRepository : ICertificationsAPIRepository
    {
        private readonly AppDbContext _context;
        private readonly IAppSettingsSQLRepository _appSettingsSQLRepository;
        private readonly ICertificatesSQLRepository _certificatesSQLRepository;

        public CertificatesAPIRepository(AppDbContext context, IAppSettingsSQLRepository appSettingsSQLRepository, ICertificatesSQLRepository certificatesSQLRepository)
        {
            _context=context;
            _appSettingsSQLRepository=appSettingsSQLRepository;
            _certificatesSQLRepository=certificatesSQLRepository;
        }

        public async Task<GetRegistrationTypesFromPEPassportReponse> GetRegistrationTypesSelectListFromPEPassport(int? pePassportID, int? processID, DateTime examDate)
        {
            CurrentRegistration currentRegistration = await CurrentRegistration(pePassportID, processID);
            IEnumerable<CurrentRegistration> currentRegistrationsList = new List<CurrentRegistration>() { currentRegistration };

            if (pePassportID != null )
            {
                AppSettings app = await _appSettingsSQLRepository.GetAppsetingsAsync();

                var allowedRegistrationTypesQ = currentRegistrationsList.Select(registration => new
                {
                    RegistrationTypeID = (int?) registration.RegistrationTypeID,
                    ExtendableStatus =
                            registration.Revoke != null ? ExtendableStatus.Revoked :
                            EF.Functions.DateDiffDay(registration.ExpiryDate, examDate) < (app.MaxInAdvanceDays * -1) ? ExtendableStatus.NotYetExtendable :
                            EF.Functions.DateDiffDay(registration.ExpiryDate, examDate) < (app.MaxExtensionDays + 1) ? ExtendableStatus.Extendable : ExtendableStatus.NoMoreExtendable,
                    HasPassed = registration.HasPassed
                });

                //IEnumerable<RegistrationType> allowedRegistrationTypes = _context.RegistrationTypes.AsEnumerable();

                IEnumerable<RegistrationType> allowedRegistrationTypes = allowedRegistrationTypesQ
                    .Join(
                        _context.AllowedRegistrationTypes.Include(allowedRegistrationTypes => allowedRegistrationTypes.AvailableRegistrationType).DefaultIfEmpty(),
                        registration => new
                        {
                            RegistrationTypeID = registration.RegistrationTypeID,
                            ExtendableStatus = registration.ExtendableStatus,
                            HasPassed = registration.HasPassed
                        },
                        allowedRegistrationTypes => new
                        {
                            RegistrationTypeID = allowedRegistrationTypes.RegistrationTypeID,
                            ExtendableStatus = allowedRegistrationTypes.ExtendableStatus,
                            HasPassed = allowedRegistrationTypes.HasPassed
                        },
                        (registration, allowedRegistrationTypes) => new RegistrationType
                        {
                            ID = allowedRegistrationTypes.AvailableRegistrationType.ID,
                            RegistrationTypeName = allowedRegistrationTypes.AvailableRegistrationType.RegistrationTypeName
                        });

                return new GetRegistrationTypesFromPEPassportReponse
                {
                    CompanyID = currentRegistration?.CompanyID,
                    ProcessID = currentRegistration?.ProcessID ?? processID,
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
                    CompanyID = currentRegistration?.CompanyID,
                    ProcessID = currentRegistration?.ProcessID ?? processID,
                    RegistrationsSelectList = new SelectList(allowedRegistrationTypes, nameof(RegistrationType.ID), nameof(RegistrationType.RegistrationTypeName))
                };
            }
        }

        public int DeleteRevokeByEncryptedID(string encryptedID)
        {
            _certificatesSQLRepository.DeleteByEncryptedID(encryptedID);
            return _context.SaveChanges();
        }

        public async Task<DateTime?> GetCertificateMaxExpirationDate(int? pePassportID, int? processID)
        {
            CurrentRegistration currentRegistration = await CurrentRegistration(pePassportID, processID);

            return currentRegistration?.ExpiryDate;
        }

        private async Task<CurrentRegistration> CurrentRegistration(int? pePassportID, int? processID)
        {
            CurrentRegistration currentRegistrations = new CurrentRegistration()
                {
                    RegistrationTypeID = null,
                    ProcessID = null,
                    CompanyID = null,
                    ExpiryDate = null,
                    Examination = null,
                    HasPassed = null,
                    Revoke = null
                };

            if (pePassportID != null)
            {
                PEPassport pePassport = await _context.PEPassports
                    .Where(pePassport => pePassport.ID == pePassportID)
                    .Include(pePassport => pePassport.PEWelder)
                    .SingleOrDefaultAsync();

                if (pePassport == null)
                {
                    return null;
                }

                var registrations = _context.Registrations
                    .Where(registration => registration.HasPassed != null);

                if (processID!= null)
                {
                    registrations = registrations
                        .Where(registration => registration.ProcessID == processID);
                }

                registrations = registrations
                    .Where(registration => registration.PEPassport.PEWelderID == pePassport.PEWelderID)
                    .Where(registration => _context.Revokes.All(revoke => revoke.RegistrationID != registration.ID));

                if (registrations.Count() != 0)
                {

                    currentRegistrations = registrations
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
                            (registration, examination) => new CurrentRegistration
                            {
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
            }
            return currentRegistrations;
        }
    }
}