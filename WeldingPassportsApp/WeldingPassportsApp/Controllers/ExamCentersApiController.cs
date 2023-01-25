using Application.Requests.API.ExamCentersAPI;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamCentersApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamCentersApiController(IMediator mediator)
        {
            _mediator=mediator;
        }

        public async Task<JsonResult> IsCompanyIDInUse(int companyID)
        {
            var query = new IsCompanyIDInUseRequest(companyID);
            return await _mediator.Send(query);
        }
    }
}
