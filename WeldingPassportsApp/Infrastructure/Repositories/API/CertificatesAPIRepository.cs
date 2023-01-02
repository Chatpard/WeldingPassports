using Application.Interfaces.Repositories.API;
using Application.Interfaces.Repositories.SQL;
using Application.SQLModels;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public CertificatesAPIRepository(AppDbContext context, IAppSettingsSQLRepository appSettingsSQLRepository)
        {
            _context=context;
            _appSettingsSQLRepository=appSettingsSQLRepository;
        }

        public async Task<GetGetRegistrationTypesFromPEPassportReponse> GetGetRegistrationTypesFromPEPassportSelectList(int pePassportID)
        {
            AppSettings app = await _appSettingsSQLRepository.GetAppsetingsAsync();

            var allowedRegistrationTypesQ = _context.PEPassports
                .Where(pePassport => pePassport.ID == pePassportID)
                .Include(pePassport => pePassport.Registrations)
                .FirstOrDefault()
                .Registrations;

            IEnumerable<CurrentRegistration> allowedRegistrationTypesQQ = null;

            if (allowedRegistrationTypesQ.Count() != 0)
            {
                allowedRegistrationTypesQQ = allowedRegistrationTypesQ.Join(
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
            else
            {
                allowedRegistrationTypesQQ = new Collection<CurrentRegistration>()
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
            }

            var allowedRegistrationTypesQQQ = allowedRegistrationTypesQQ.Select(registration => new
                {
                    RegistrationTypeID = (int?) registration.RegistrationTypeID,
                    ExtendableStatus =
                        registration.Revoke != null ? ExtendableStatus.Revoked :
                        EF.Functions.DateDiffDay(DateTime.Now, registration.ExpiryDate) > app.MaxInAdvanceDays ? ExtendableStatus.NotYetExtendable :
                        (EF.Functions.DateDiffDay(DateTime.Now, registration.ExpiryDate) > (app.MaxExtensionDays * -1) ? ExtendableStatus.Extendable :
                        ExtendableStatus.NoMoreExtendable),
                    HasPassed = registration.HasPassed
                });

            var allowedRegistrationTypes = allowedRegistrationTypesQQQ.Join(
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

            return new GetGetRegistrationTypesFromPEPassportReponse
                    {
                        CompanyID = allowedRegistrationTypesQQ.FirstOrDefault()?.CompanyID,
                        ProcessID = allowedRegistrationTypesQQ.FirstOrDefault()?.ProcessID,
                        RegistrationsSelectList = new SelectList(allowedRegistrationTypes, nameof(RegistrationType.ID), "RegistrationTypeName")
                    };
        }
    }
}