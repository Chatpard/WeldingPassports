using Application.Requests.Welders;
using Application.Security;
using Application.ViewModels;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    [Authorize]
    public class WeldersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;

        public WeldersController(IMediator mediator, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _env = env;
            _userManager=userManager;
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Welders+ClaimsPrincipalExtensions.CanReadClaimsGroup+"Policy")]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            try
            {
                var query = new GetPEWelderIndexRequest(sortOrder, currentFilter, searchString, pageNumber, _userManager, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetWelderIndex");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Welders+ClaimsPrincipalExtensions.CanCreateClaimsGroup+"Policy")]
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
        [Authorize(Policy = ClaimsTypesStore.Welders+ClaimsPrincipalExtensions.CanCreateClaimsGroup+"Policy")]
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

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Welders+ClaimsPrincipalExtensions.CanReadClaimsGroup+"Policy")]
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
        [Authorize(Policy = ClaimsTypesStore.Welders+ClaimsPrincipalExtensions.CanEditClaimsGroup+"Policy")]
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
        [Authorize(Policy = ClaimsTypesStore.Welders+ClaimsPrincipalExtensions.CanEditClaimsGroup+"Policy")]
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

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Welders+ClaimsPrincipalExtensions.CanDeleteClaimsGroup+"Policy")]
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
