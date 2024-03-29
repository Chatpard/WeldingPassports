﻿using Application.Requests.CompanyContacts;
using Application.Requests.Contacts;
using Application.Requests.PEPassports;
using Application.Requests.Welders;
using Application.Security;
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
    public class ContactsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public ContactsController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Contacts+ClaimsPrincipalExtensions.CanReadClaimsGroup+"Policy")]
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
                return Utilities.ErrorView(_env, this, e, "Error in GetContactsIndex");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Contacts+ClaimsPrincipalExtensions.CanCreateClaimsGroup+"Policy")]
        public async Task<IActionResult> Create(string returnUrl)
        {
            try
            {
                var query = new GetContactCreateRequest(returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetContactCreate");
            }
        }

        [HttpPost]
        [Authorize(Policy = ClaimsTypesStore.Contacts+ClaimsPrincipalExtensions.CanCreateClaimsGroup+"Policy")]
        public async Task<IActionResult> Create(ContactCreateViewModel contactCreateViewModel, string returnUrl)
        {
            try
            {
                var query = new PostContactCreateRequest(contactCreateViewModel, returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in PostContactCreate");
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
                return Utilities.ErrorView(_env, this, e, "Error in GetContactsDetails");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Contacts+ClaimsPrincipalExtensions.CanEditClaimsGroup+"Policy")]
        public async Task<IActionResult> Edit(string id, string returnUrl)
        {
            try
            {
                var request = new GetContactEditRequest(id, returnUrl, this);
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetContactEdit");
            }
        }

        [HttpPost]
        [Authorize(Policy = ClaimsTypesStore.Contacts+ClaimsPrincipalExtensions.CanEditClaimsGroup+"Policy")]
        public async Task<IActionResult> Edit(ContactEditViewModel contactEditViewModel, string returnUrl)
        {
            try
            {
                var request = new PostContactEditRequest(contactEditViewModel, nameof(Details), returnUrl, this);
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in PostContactEdit");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Contacts+ClaimsPrincipalExtensions.CanDeleteClaimsGroup+"Policy")]
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
                return Utilities.ErrorView(_env, this, e, "Error in GetContactsDelete");
            }
        }
    }
}
