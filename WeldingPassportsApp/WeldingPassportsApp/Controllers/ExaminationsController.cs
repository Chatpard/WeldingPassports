﻿using Application.Requests.Examinations;
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
    public class ExaminationsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;

        public ExaminationsController(IMediator mediator, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _env = env;
            _userManager=userManager;
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Examinations+ClaimsPrincipalExtensions.CanReadClaimsGroup+"Policy")]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            try
            {
                var query = new GetExaminationsIndexRequest(sortOrder, currentFilter, searchString, pageNumber, typeof(ExaminationsController).GetNameOfController(), nameof(ExaminationsController.Details), _userManager, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetExaminationsIndex");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Examinations+ClaimsPrincipalExtensions.CanCreateClaimsGroup+"Policy")]
        public async Task<IActionResult> Create(string returnUrl)
        {
            try
            {
                var query = new GetExaminationsCreateRequest(returnUrl, _userManager, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetExaminationsCreate");
            }
        }

        [HttpPost]
        [Authorize(Policy = ClaimsTypesStore.Examinations+ClaimsPrincipalExtensions.CanCreateClaimsGroup+"Policy")]
        public async Task<IActionResult> Create(ExaminationCreateViewModel examination, string returnUrl)
        {
            try
            {
                var query = new PostExaminationsCreateRequest(examination, returnUrl, typeof(ExaminationsController).GetNameOfController(), nameof(ExaminationsController.Edit), this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in PostExaminationCreate");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Examinations+ClaimsPrincipalExtensions.CanReadClaimsGroup+"Policy")]
        public async Task<IActionResult> Details(string id, string returnUrl)
        {
            try
            {
                var query = new GetExaminationDetailsRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetExaminationsDetails");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Examinations+ClaimsPrincipalExtensions.CanUpdateRevokeEditClaimsGroup+"Policy")]
        public async Task<IActionResult> Edit(string id, string returnUrl)
        {
            try
            {
                var query = new GetExaminationEditRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                
                return Utilities.ErrorView(_env, this, e, "Error in GetExaminationsEdit");
            }
        }

        [HttpPost]
        [Authorize(Policy = ClaimsTypesStore.Examinations+ClaimsPrincipalExtensions.CanUpdateRevokeEditClaimsGroup+"Policy")]
        public async Task<IActionResult> Edit(ExaminationEditViewModel newExaminationVm, string returnUrl)
        {
            try
            {
                var query = new PostExaminationEditRequest(newExaminationVm, nameof(Details), returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                
                return Utilities.ErrorView(_env, this, e, "Error in PostExaminationEdit");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Certificates+ClaimsPrincipalExtensions.CanUpdateClaimsGroup+"Policy")]
        public async Task<IActionResult> Update(string id, string returnUrl)
        {
            try
            {
                var query = new GetExaminationUpdateRequest(id, returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetExaminationUpdate");
            }
        }

        [HttpPost]
        [Authorize(Policy = ClaimsTypesStore.Certificates+ClaimsPrincipalExtensions.CanUpdateClaimsGroup+"Policy")]
        public async Task<IActionResult> Update(ExaminationUpdateViewModel examinationUpdates, string returnUrl )
        {
            try
            {
                var query = new PostExaminationUpdateRequest(examinationUpdates, returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in PostExaminationUpdate");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Examinations+ClaimsPrincipalExtensions.CanDeleteClaimsGroup+"Policy")]
        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            try
            {
                var query = new GetExaminationDeleteRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetExaminationsDelete");
            }
        }
    }
}
