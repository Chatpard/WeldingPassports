using Application.Requests.TrainingCenters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingCentersApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TrainingCentersApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetLetterByTrainingCenterId))]
        public async Task<ActionResult<char?>> GetLetterByTrainingCenterId(int id)
        {
            var query = new GetTrainingCenterLetterByTrainingCenterIdApiRequest(id, this);
            return await _mediator.Send(query);
        }
    }
}
