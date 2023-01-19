using Application.Requests.CompanyContacts;
using Application.Requests.PEPassports;
using Application.Requests.Welders;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WeldingPassportsApp.Stores;

namespace WeldingPassportsApp.Controllers
{
    [Authorize]
    public class CompanyContactsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public CompanyContactsController(IMediator mediator, IWebHostEnvironment env, IMapper mapper)
        {
            _mediator = mediator;
            _env = env;
            _mapper=mapper;
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
                var query = new GetCompanyContactsIndexRequest(sortOrder, currentFilter, searchString, pageNumber, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetCompanyContactsIndex");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(string returnUrl)
        {
            try
            {
                var query = new GetCompanyContactCreateRequest(returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetCompanyContactCreate");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyContactCreateViewModel contactCreateViewModel, string returnUrl)
        {
            try
            {
                var query = new PostCompanyContactCreateRequest(contactCreateViewModel, nameof(CompanyContactsController.Details), returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in PostCompanyContactCreate");
            }
        }

        public async Task<IActionResult> Details(string id, string returnUrl)
        {
            try
            {
                var query = new GetCompanyContactDetailsRequest(id, returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetCompanyContactDetails");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, string returnUrl)
        {
            try
            {
                var request = new GetCompanyContactEditRequest(id, returnUrl, this);
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetCompanyContactEdit");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyContactEditViewModel companyContactEditViewModel, string returnUrl, string currentUrl, string submit)
        {
            switch(submit)
            {
                case CompanyContactIDStore.GetContactEditBtnID:
                    Copy2TempData(companyContactEditViewModel);
                    return RedirectToAction(nameof(ContactsController.Edit), typeof(ContactsController).GetNameOfController(), new { id = companyContactEditViewModel.ContactID, returnUrl = currentUrl });

                case CompanyContactIDStore.GetCompanyEditBtnID:
                    Copy2TempData(companyContactEditViewModel);
                    return RedirectToAction(nameof(CompaniesController.Edit), typeof(CompaniesController).GetNameOfController(), new { id = companyContactEditViewModel.CompanyID, returnUrl = currentUrl });

                case CompanyContactIDStore.GetAddressEditBtnID:
                    Copy2TempData(companyContactEditViewModel);
                    return RedirectToAction(nameof(AddressesController.Edit), typeof(AddressesController).GetNameOfController(), new { id = companyContactEditViewModel.AddressID, returnUrl = currentUrl });

                default:
                    return await Edit(companyContactEditViewModel, returnUrl);
            }
        }

        private void Copy2TempData(CompanyContactEditViewModel viewData)
        {
            if (viewData == null) return;
            TempData[nameof(CompanyContactEditViewModel)] = JsonSerializer.Serialize(viewData);

            if (viewData.ContactID != 0)
                TempData[nameof(viewData.ContactID)] = viewData.ContactID;
            if (viewData.CompanyID != 0)
                TempData[nameof(viewData.CompanyID)] = viewData.CompanyID;
            if (viewData.JobTitle != "")
                TempData[nameof(viewData.JobTitle)] = viewData.JobTitle;
            if (viewData.BusinessPhone != "")
                TempData[nameof(viewData.BusinessPhone)] = viewData.BusinessPhone;
            if (viewData.MobilePhone != "")
                TempData[nameof(viewData.MobilePhone)] = viewData.MobilePhone;
            if (viewData.Email != "")
                TempData[nameof(viewData.Email)] = viewData.Email;
            if (viewData.AddressID != 0)
                TempData[nameof(viewData.AddressID)] = viewData.AddressID;
        }

        private async Task<IActionResult> Edit(CompanyContactEditViewModel companyContactEditViewModel, string returnUrl)
        {
            try
            {
                var request = new PostCompanyContactEditRequest(companyContactEditViewModel, nameof(Details), returnUrl, this);
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in PostCompanyContactEdit");
            }
        }

        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            try
            {
                var query = new GetCompanyContactDeleteRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetCompanyContactDelete");
            }
        }
    }
}
