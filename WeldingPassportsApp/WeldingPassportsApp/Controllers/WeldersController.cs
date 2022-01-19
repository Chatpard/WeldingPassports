using Application.Requests.Welders;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    [Authorize]
    public class WeldersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public WeldersController(IMediator mediator, IWebHostEnvironment env)
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
                var query = new GetPEWelderIndexRequest(sortOrder, currentFilter, searchString, pageNumber, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetWelderIndex");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(string returnUrl)
        {
            try
            {
                var request =  new GetPEWelderCreateRequest(returnUrl, this);
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetWeldersCreate");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PEWelderCreateViewModel peWelderCreateViewModel, string returnUrl)
        {
            try
            {
                var request = new PostPEWelderCreateRequest(peWelderCreateViewModel, nameof(Details), returnUrl, this);
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetWeldersCreate");
            }
        }

        public async Task<IActionResult> Details(string id, string returnUrl)
        {
            try
            {
                var query = new GetPEWelderDetailsRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetWeldersDetails");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, string returnUrl)
        {
            try
            {
                var request = new GetPEWelderEditRequest(id, returnUrl, this);
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetWelderEdit");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PEWelderEditViewModel peWelderChanges, string returnUrl)
        {
            try
            {
                var request = new PostPEWelderEditRequest(peWelderChanges, nameof(Details), returnUrl, this);
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in PostWelderEdit");
            }
        }

        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            try
            {
                var query = new GetPEWelderDeleteRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetWelderDelete");
            }
        }
    }
}
