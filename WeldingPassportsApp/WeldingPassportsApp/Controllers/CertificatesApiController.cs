using Application.Requests.API.CertificatesAPI;
using Application.SQLModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<ActionResult<GetGetRegistrationTypesFromPEPassportReponse>> GetRegistrationTypesFromPEPassport(int pePassportID)
        {
            var query = new GetRegistrationTypesFromPEPassportRequest(pePassportID);
            return await _mediator.Send(query);
        }
    }
}