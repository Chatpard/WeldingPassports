using Application.Requests.Certificates;
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
    public class CertificatesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public CertificatesController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Certificates+ClaimsPrincipalExtensions.CanUpdateRevokeReadClaimsGroup+"Policy")]
        public async Task<IActionResult> Details(string id, string returnUrl)
        {
            try
            {
                var query = new GetCertificatesDetailsRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask; 
                return Utilities.ErrorView(_env, this, e, "Error in GetCertificatesDetails");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Certificates+ClaimsPrincipalExtensions.CanCreateClaimsGroup+"Policy")]
        public async Task<IActionResult> Create(string id, string returnUrl)
        {
            try
            {
                var request = new GetCertificatesCreateRequest(id, returnUrl, this);

                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                await Task.CompletedTask; 
                return Utilities.ErrorView(_env, this, e, "Error in GetCertificatesCreate");
            }
        }

        [HttpPost]
        [Authorize(Policy = ClaimsTypesStore.Certificates+ClaimsPrincipalExtensions.CanCreateClaimsGroup+"Policy")]
        public async Task<IActionResult> Create(string id, CertificateCreateViewModel vm, string returnUrl)
        {
            try
            {
                var request = new PostCertificatesCreateRequest(id, vm, returnUrl, this);

                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                await Task.CompletedTask; 
                return Utilities.ErrorView(_env, this, e, "Error in PostCertificatesCreate");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Certificates+ClaimsPrincipalExtensions.CanUpdateRevokeEditClaimsGroup+"Policy") ]
        public async Task<IActionResult> Edit(string id, string returnUrl)
        {
            try
            {
                var query = new GetCertificatesEditRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask; 
                return Utilities.ErrorView(_env, this, e, "Error in GetCertificatesEdit");
            }
        }

        [HttpPost]
        [Authorize(Policy = ClaimsTypesStore.Certificates+ClaimsPrincipalExtensions.CanUpdateRevokeEditClaimsGroup+"Policy")]
        public async Task<IActionResult> Edit(CertificateEditViewModel vm, string returnUrl)
        {
            try
            {
                var query = new PostCertificatesEditRequest(vm, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask; 
                return Utilities.ErrorView(_env, this, e, "Error in PostCertificatesEdit");
            }
        }

        [HttpGet]
        [Authorize(Policy = ClaimsTypesStore.Certificates+ClaimsPrincipalExtensions.CanDeleteClaimsGroup+"Policy")]
        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            try
            {
                var query = new GetCertificatesDeleteRequest(id, returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetCertificatesDelete");
            }
        }
    }
}
