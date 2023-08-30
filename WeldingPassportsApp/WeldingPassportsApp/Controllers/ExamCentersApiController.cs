using Application.Requests.API.ExamCentersAPI;
using Application.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExamCentersApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamCentersApiController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.ExamCenters + ClaimsPrincipalExtensions.CanCreateClaimsGroup + "Policy")]
        public async Task<JsonResult> IsCompanyIDInUse(int companyID)
        {
            var query = new IsCompanyIDInUseRequest(companyID);
            return await _mediator.Send(query);
        }
    }
}
