using Application.Requests.PEPassports;
using Application.Requests.Welders;
using Application.Security;
using Application.ViewModels;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
        [Authorize(Policy = ClaimsTypesStore.PEPassports+ClaimsPrincipalExtensions.CanReadClaimsGroup+"Policy")]
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
        [Authorize(Policy = ClaimsTypesStore.PEPassports+ClaimsPrincipalExtensions.CanCreateClaimsGroup+"Policy")]
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
        [Authorize(Policy = ClaimsTypesStore.PEPassports+ClaimsPrincipalExtensions.CanCreateClaimsGroup+"Policy")]
        public async Task<IActionResult> Create(PEPassportCreateViewModel pePassportCreateVM, string returnUrl)
        {
            try
            {
                var query = new PostPEPassportCreateRequest(pePassportCreateVM, nameof(Details), returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;

                return Utilities.ErrorView(_env, this, e, "Error in GetPEPassportCreate");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.PEPassports+ClaimsPrincipalExtensions.CanReadClaimsGroup+"Policy")]
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
        [Authorize(Policy = ClaimsTypesStore.PEPassports+ClaimsPrincipalExtensions.CanEditClaimsGroup+"Policy")]
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
                var query = new PostPEPassportEditRequest(pepassportChanges, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;

                return Utilities.ErrorView(_env, this, e, "Error in PostPEPassportEdit");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Certificates+ClaimsPrincipalExtensions.CanUpdateClaimsGroup+"Policy")]
        public async Task<IActionResult> Update(string id, string returnUrl)
        {
            try
            {
                var query = new GetPEPassportUpdateRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;

                return Utilities.ErrorView(_env, this, e, "Error in GetPEPassportUpdate");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(PEPassportUpdateViewModel pePassportUpdates, string returnUrl)
        {
            try
            {
                var query = new PostPEPassportUpdateRequest(pePassportUpdates, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;

                return Utilities.ErrorView(_env, this, e, "Error in PostPassportUpdate");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.PEPassports+ClaimsPrincipalExtensions.CanDeleteClaimsGroup+"Policy")]
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
