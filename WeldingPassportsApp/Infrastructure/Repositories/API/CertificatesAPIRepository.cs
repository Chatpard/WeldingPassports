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

        public async Task<GetGetRegistrationTypesFromPEPassportReponse> GetRegistrationTypesFromPEPassportSelectList(int pePassportID, int? processID, DateTime examDate)
        {
            AppSettings app = await _appSettingsSQLRepository.GetAppsetingsAsync();

            IEnumerable<RegistrationType> allowedRegistrationTypes = _context.RegistrationTypes
                .Where(registrationType => registrationType.IsActive)
                .Select(registrationType => new RegistrationType
                {
                    ID = registrationType.ID,
                    RegistrationTypeName = registrationType.RegistrationTypeName
                });

            IEnumerable<CurrentRegistration> allowedRegistrationTypesQ = new Collection<CurrentRegistration>()
                {
                    new CurrentRegistration()
                    {
                        RegistrationTypeID = null,
                        ProcessID = null,
                        CompanyID = null,
                        ExpiryDate = null,
                        Examination = null,
                        HasPassed = null,
                        Revoke = null
                    }
                };

            if(pePassportID != 0)
            {
                var pePassPort = _context.PEPassports
                    .Where(pePassport => pePassport.ID == pePassportID)
                    .Include(pePassport => pePassport.Registrations)
                    .FirstOrDefault();

                if (pePassPort == null)
                {
                    return null;
                }

                var registrations = pePassPort
                    .Registrations.AsQueryable();

                if(processID!= null)
                {
                    registrations = registrations
                        .Where(registration => registration.ProcessID == processID);
                }

                if (registrations.Count() != 0)
                {
                    allowedRegistrationTypesQ = registrations.Join(
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
                        .Take(1);
                }

                var allowedRegistrationTypesQQQQ = allowedRegistrationTypesQ.Select(registration => new
                    {
                        RegistrationTypeID = (int?) registration.RegistrationTypeID,
                        ExtendableStatus =
                            registration.Revoke != null ? ExtendableStatus.Revoked :
                            EF.Functions.DateDiffDay(examDate, registration.ExpiryDate) > app.MaxInAdvanceDays ? ExtendableStatus.NotYetExtendable :
                            (EF.Functions.DateDiffDay(examDate, registration.ExpiryDate) > (app.MaxExtensionDays * -1) ? ExtendableStatus.Extendable :
                            ExtendableStatus.NoMoreExtendable),
                        HasPassed = registration.HasPassed
                    });

                allowedRegistrationTypes = allowedRegistrationTypesQQQQ.Join(
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
                        }
                    );
            }

            return new GetGetRegistrationTypesFromPEPassportReponse
                    {
                        CompanyID = allowedRegistrationTypesQ.FirstOrDefault()?.CompanyID,
                        ProcessID = allowedRegistrationTypesQ.FirstOrDefault()?.ProcessID ?? processID,
                        RegistrationsSelectList = new SelectList(allowedRegistrationTypes, nameof(RegistrationType.ID), "RegistrationTypeName")
                    };
        }

        public int DeleteRevokeByEncryptedID(string encryptedID)
        {
            _certificatesSQLRepository.DeleteByEncryptedID(encryptedID);
            return _context.SaveChanges();
        }

        public async Task<DateTime?> GetCertificateMaxExpirationDate(int? pePassportID, int? processID)
        {
            Registration leafRegistration = new Registration();

            if (pePassportID != null)
            {
                var registrations = _context?.Registrations
                    .Where(registration => registration.PEPassportID == pePassportID);

                if (processID != null)
                {
                    registrations = registrations?
                        .Where(registration => registration.ProcessID == processID);
                }

                leafRegistration = await registrations?.OrderBy(registration => registration.ExpiryDate)
                    .FirstOrDefaultAsync();
            }

            return leafRegistration?.ExpiryDate;
        }
    }
}