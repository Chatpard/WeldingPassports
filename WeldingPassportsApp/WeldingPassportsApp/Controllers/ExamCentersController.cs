using Application.Requests.ExamCenters;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeldingPassportsApp.Stores;
using System.Text.Json;
using System;
using Microsoft.AspNetCore.Hosting;
using Application.Security;

namespace WeldingPassportsApp.Controllers
{
    [Authorize]
    public class ExamCentersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public ExamCentersController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator=mediator;
            _env=env;
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.ExamCenters + ClaimsPrincipalExtensions.CanReadClaimsGroup + "Policy")]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            var query = new GetExamCentersIndexRequest(sortOrder, currentFilter, searchString, pageNumber, this);
            return await _mediator.Send(query);
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.ExamCenters + ClaimsPrincipalExtensions.CanCreateClaimsGroup + "Policy")]
        public async Task<IActionResult> Create(string returnUrl)
        {
            try
            {
                var query = new GetExamCenterCreateRequest(returnUrl, this);
                return await _mediator.Send(query);
            }
            catch(Exception ex)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, ex, "Error in GetExamCentersCreate");
            }
        }

        [HttpPost]
        [Authorize(Policy = ClaimsTypesStore.ExamCenters + ClaimsPrincipalExtensions.CanCreateClaimsGroup + "Policy")]
        public async Task<IActionResult> Create(ExamCenterCreateViewModel examCenterCreateVM, string returnUrl)
        {
            try
            {
                var query = new PostExamCenterCreateRequest(examCenterCreateVM, returnUrl, this);
                return await _mediator.Send(query);
            }
            catch(Exception ex)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, ex, "Error in PostExamCentersCreate");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.ExamCenters + ClaimsPrincipalExtensions.CanReadClaimsGroup + "Policy")]
        public async Task<IActionResult> Details(string id, string returnUrl)
        {
            try
            {
                var query = new GetExamCenterDetailsRequest(id, returnUrl, this);
                return await _mediator.Send(query);
            }
            catch(Exception ex)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, ex, "Error is GetExamCentersDetails");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.ExamCenters + ClaimsPrincipalExtensions.CanEditClaimsGroup + "Policy")]
        public async Task<IActionResult> Edit(string id, string returnUrl)
        {
            try
            {
                var query = new GetExamCenterEditRequest(id, returnUrl, this);
                return await _mediator.Send(query);
            }
            catch(Exception ex)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, ex, "Error in GetExamCentersEdit");
            }
        }

        [HttpPost]
        [Authorize(Policy = ClaimsTypesStore.ExamCenters + ClaimsPrincipalExtensions.CanEditClaimsGroup + "Policy")]
        public async Task<IActionResult> Edit(ExamCenterEditViewModel examCenterEditViewModel, string returnUrl, string currentUrl, string submit)
        {
            switch (submit)
            {
                case ExamCenterIDStore.GetCompanyEditBtnID:
                    Copy2TempData(examCenterEditViewModel);
                    return RedirectToAction(nameof(CompaniesController.Edit), typeof(CompaniesController).GetNameOfController(), new { id = examCenterEditViewModel.CompanyID, returnUrl = currentUrl });

                default:
                    return await Edit(examCenterEditViewModel, returnUrl);
            }
        }

        private void Copy2TempData(ExamCenterEditViewModel viewData)
        {
            if (viewData == null) return;
            TempData[nameof(ExamCenterEditViewModel)] = JsonSerializer.Serialize(viewData);

            if (viewData.CompanyID != 0)
                TempData[nameof(viewData.CompanyID)] = viewData.CompanyID;
            TempData[nameof(viewData.IsActive)] = viewData.IsActive;
        }

        private async Task<IActionResult> Edit(ExamCenterEditViewModel examCenterEditViewModel, string returnUrl)
        {
            try
            {
                var query = new PostExamCenterEditRequest(examCenterEditViewModel, returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception ex)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, ex, "Error in PostExamCentersEdit");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.ExamCenters + ClaimsPrincipalExtensions.CanDeleteClaimsGroup + "Policy")]
        public async Task<IActionResult> Delete(string id, string returnUrl, ExamCenterIndexViewModel examCenterIndexVM)
        {
            try
            {
                var query = new GetExamCenterDeleteRequest(id, returnUrl, examCenterIndexVM, this);
                return await _mediator.Send(query);
            }
            catch (Exception ex)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, ex, "Error in PostExamCentersDelete");
            }
        }
    }
}
