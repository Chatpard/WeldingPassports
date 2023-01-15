using Application.Requests;
using Application.Requests.PEPassports;
using Application.Requests.TrainingCenters;
using Application.Requests.Welders;
using Application.Security;
using Application.ViewModels;
using Domain;
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
    public class TrainingCentersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public TrainingCentersController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.TrainingCenters+ClaimsPrincipalExtensions.CanReadClaimsGroup+"Policy")]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            try
            {
                var query = new GetTrainingCentersIndexRequest(sortOrder, currentFilter, searchString, pageNumber, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetTrainingCentersIndex");
            }
        }

        [HttpGet]
        [Authorize(Policy =ClaimsTypesStore.TrainingCenters+ClaimsPrincipalExtensions.CanCreateClaimsGroup+"Policy")]
        public async Task<IActionResult> Create(string returnUrl)
        {
            try
            {
                var query = new GetTrainingCenterCreateRequest(returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetTrainingCentersCreate");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrainingCenterCreateViewModel trainingCenterCreateVM, string returnUrl)
        {
            try
            {
                var query = new PostTrainingCenterCreateRequest(trainingCenterCreateVM, nameof(Details), returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in PostTrainingCentersCreate");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.TrainingCenters + ClaimsPrincipalExtensions.CanReadClaimsGroup + "Policy")]
        public async Task<IActionResult> Details(string id, string returnUrl)
        {
            try
            {
                var query = new GetTrainingCenterDetailsRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetTrainingCentersDetails");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.TrainingCenters + ClaimsPrincipalExtensions.CanEditClaimsGroup + "Policy")]
        public async Task<IActionResult> Edit(string id, string returnUrl)
        {
            try
            {
                var query = new GetTrainingCenterEditRequest(id, returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetTrainingCentersEdit");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TrainingCenterEditViewModel trainingCenterChanges, string returnUrl)
        {
            try
            {
                var query = new PostTrainingCenterEditRequest(trainingCenterChanges, returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetTrainingCentersEdit");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.TrainingCenters + ClaimsPrincipalExtensions.CanDeleteClaimsGroup + "Policy")]
        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            try
            {
                var query = new GetTrainingCenterDeleteRequest(id, returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetTrainingCentersDelete");
            }
        }
    }
}
