using Application.Requests.API.CertificatesAPI;
using Application.SQLModels;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    [AllowAnonymous]
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
        public async Task<DateTime?> GetMaxCertificateExpirationDate(string examDateString, int pePassportID, int processID, int? registrationTypeID = null)
        {
            var query = new GetCertificateMaxExpirationDateRequest(pePassportID, processID, registrationTypeID, examDateString);
            return await _mediator.Send(query);
        }
    }
}