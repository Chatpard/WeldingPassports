using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.Requests.API.PEWeldersAPI;
using System.Threading.Tasks;
using Domain.Models;

namespace WeldingPassportsApp.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class WeldersApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeldersApiController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetWeldersNotFromTrainingCenterId))]
        public async Task<ActionResult<SelectList>> GetWeldersNotFromTrainingCenterId(int? id)
        {
            var query = new GetWeldersNotFromTrainingCenterRequest(id);
            return await _mediator.Send(query);
        }
    }
}
