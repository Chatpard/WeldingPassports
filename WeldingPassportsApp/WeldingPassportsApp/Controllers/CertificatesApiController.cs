using Application.APIModels;
using Application.Requests.API.CertificatesAPI;
using Application.SQLModels;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CertificatesApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetProcessNames))]
        public async Task<ActionResult<GetProcessNamesReponse>>GetProcessNames(string examinationEncryptedID, int? pePassportID, int? registrationID = null)
        {
            var query = new GetProcessNamesRequest(examinationEncryptedID, pePassportID, registrationID);
            return await _mediator.Send(query);
        }

        [HttpGet(nameof(GetRegistrationTypesFromPEPassport))]
        public async Task<ActionResult<GetRegistrationTypesFromPEPassportReponse>> GetRegistrationTypesFromPEPassport(int? pePassportID, int? processID, DateTime examDate)
        {
            var query = new GetRegistrationTypesFromPEPassportRequest(pePassportID, processID, examDate);
            return await _mediator.Send(query);
        }

        [HttpGet(nameof(DeleteRevokeByEncryptedID))]
        public async Task<EntityEntry<Revoke>> DeleteRevokeByEncryptedID(string encryptedID)
        {
            var query = new DeleteCertificatesRevokeRequest(encryptedID);
            return await _mediator.Send(query);
        }

        [HttpGet(nameof(GetMaxCertificateExpirationDate))]
        public async Task<ExpiryDateAPIModel> GetMaxCertificateExpirationDate(string examDateString, int pePassportID, int processID, int? registrationTypeID = null)
        {
            var query = new GetCertificateMaxExpirationDateRequest(pePassportID, processID, registrationTypeID, examDateString);
            return await _mediator.Send(query);
        }

        [HttpGet(nameof(GetCompanyID))]
        public async Task<ActionResult<int?>> GetCompanyID(int pePassportID)
        {
            var query = new GetCompanyIDByPEPassportRequest(pePassportID, this);
            return await _mediator.Send(query);
        }

        [HttpGet(nameof(GetHasNotSet))]
        public async Task<ActionResult<bool?>> GetHasNotSet(int pePassportID, int processID)
        {
            var query = new GetHasNotSet(pePassportID, processID, this);
            return await _mediator.Send(query);
        }
    }
}