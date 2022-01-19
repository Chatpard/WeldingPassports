using Application.Requests.Admin;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    [Authorize(Policy = "AdminRolePolicy")]
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public AdminController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> AppSettingsDetails()
        {
            try
            {
                var query = new GetAdminAppSettingsDetailsRequest(this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetAdminDetails");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AppSettingsEdit(int id)
        {
            try
            {
                var query = new GetAdminAppSettingsEditRequest(id, this);

                return await _mediator.Send(query);
            }
            catch(Exception e) 
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetAdminEdit");
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AppSettingsEdit(AppSettingsViewModel appSettingsChanges)
        {
            try
            {
                var query = new PostAdminAppSettingsEditRequest(appSettingsChanges, nameof(AppSettingsDetails), this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in PostAdminEdit");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UsersToApproveIndex()
        {
            try
            {
                var request = new GetAdminUsersToApproveIndexRequest(this);

                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetAdminUsersToApproveIndex");
            }
        }

        public async Task<IActionResult> UserToApproveDetails(string id)
        {
            try
            {
                var request = new GetAdminUserToApproveDetailsRequest(id, nameof(UsersToApproveIndex), this);

                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetAdminUserToApproveDetails");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ApproveUser(string id, string role)
        {
            try
            {
                var request = new PostAdminApproveUserRequest(id, role, nameof(UsersToApproveIndex),
                    nameof(AccountController.Login), typeof(AccountController).GetNameOfController(), this);

                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in PostAdminApproveUser");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id, string returnUrl)
        {
            try
            {
                var request = new GetAdminDeleteUserRequest(id, returnUrl, this);

                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetAdminDeleteUser");
            }
        }
    }
}
