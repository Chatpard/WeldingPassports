using Application.Requests.PEPassports;
using Application.Requests.Welders;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    [Authorize]
    public class PEPassportsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public PEPassportsController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            try
            {
                var query = new GetPEPassportsIndexRequest(sortOrder, currentFilter, searchString, pageNumber, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                
                return Utilities.ErrorView(_env, this, e, "Error in GetPEPassportsIndex");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(string returnUrl)
        {
            try
            {
                var query = new GetPEPassportCreateRequest(returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                
                return Utilities.ErrorView(_env, this, e, "Error in GetPEPassportCreate");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PEPassportCreateViewModel pePassport, string returnUrl)
        {
            try
            {
                var query = new PostPEPassportCreateRequest(pePassport, nameof(Details), returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                
                return Utilities.ErrorView(_env, this, e, "Error in GetPEPassportCreate");
            }
        }

        public async Task<IActionResult> Details(string id, string returnUrl)
        {
            try
            {
                var query = new GetPEPassportDetailsRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                
                return Utilities.ErrorView(_env, this, e, "Error in GetPEPassportsDetails");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, string returnUrl)
        {
            try
            {
                var query = new GetPEPassportEditRequest(id, returnUrl, this);
                
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;

                return Utilities.ErrorView(_env, this, e, "Error in GetPEPassportEdit");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PEPassportEditViewModel pepassportChanges, string returnUrl)
        {
            try
            {
                var query = new PostPEPassportEditRequest(pepassportChanges, nameof(Details), returnUrl, this);
                
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                
                return Utilities.ErrorView(_env, this, e, "Error in PostPEPassportEdit");
            }
        }

        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            try
            {
                var query = new GetPEPassportDeleteRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                
                return Utilities.ErrorView(_env, this, e, "Error in GetWeldersDelete");
            }
        }
    }
}
