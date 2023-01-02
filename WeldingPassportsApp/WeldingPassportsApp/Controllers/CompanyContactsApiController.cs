using Application.Requests.API.CompanyContactsAPI;
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
    public class CompanyContactsApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyContactsApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetCompanyContactsFromCompany))]
        public async Task<ActionResult<SelectList>> GetCompanyContactsFromCompany(int companyID)
        {
            var query = new GetCompanyContactsFromCompanyRequest(companyID);
            return await _mediator.Send(query);
        }
    }
}
