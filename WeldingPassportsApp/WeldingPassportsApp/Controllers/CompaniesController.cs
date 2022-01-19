using Application.Requests.Companies;
using Application.Requests.CompanyContacts;
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
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public CompaniesController(IMediator mediator, IWebHostEnvironment env)
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
                var query = new GetCompaniesIndexRequest(sortOrder, currentFilter, searchString, pageNumber, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetCompaniesIndexIndex");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(string returnUrl)
        {
            try
            {
                var query = new GetCompanyCreateRequest(returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetCompanyCreate");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyCreateViewModel companyVM, string returnUrl)
        {
            try
            {
                var query = new PostCompanyCreateRequest(companyVM, nameof(Details), returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in PostCompanyCreate");
            }
        }

        public async Task<IActionResult> Details(string id, string returnUrl)
        {
            try
            {
                var query = new GetCompanyDetailsRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetCompanyDetails");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, string returnUrl)
        {
            try
            {
                var query = new GetCompanyEditRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetCompanyEdit");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyEditViewModel company, string returnUrl)
        {
            try
            {
                var query = new PostCompanyEditRequest(company, nameof(Details), returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in PostCompanyEdit");
            }
        }

        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            try
            {
                var query = new GetCompanyDeleteRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetCompaniesDelete");
            }
        }
    }
}
