using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using AutoMapper;
using Domain;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Admin
{
    public class GetAdminUserToApproveDetailsRequestHandler : IRequestHandler<GetAdminUserToApproveDetailsRequest, IActionResult>
    {
        private readonly IUserToApproveRepository _userToApproveRepository;
        private readonly ICompaniesSQLRepository _companiesSQLRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAdminUserToApproveDetailsRequestHandler> _localizer;

        public GetAdminUserToApproveDetailsRequestHandler(IUserToApproveRepository UserToApproveRepository,
            ICompaniesSQLRepository companiesSQLRepository, IMapper mapper, IStringLocalizer<GetAdminUserToApproveDetailsRequestHandler> localizer)
        {
            _userToApproveRepository = UserToApproveRepository;
            _companiesSQLRepository = companiesSQLRepository;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IActionResult> Handle(GetAdminUserToApproveDetailsRequest request, CancellationToken cancellationToken)
        {
            UserToApprove user = await _userToApproveRepository.GetUserToApproveByEncryptedIDAsync(request.Id);

            var vm = _mapper.Map<UserToApproveViewModel>(user);

            foreach (var role in RolesStore.Roles)
            {
                vm.RoleItems.Add(new SelectListItem() { Value = role, Text = _localizer[RolesStore.ViewRoles[role]] });
            }

            var companies = await _companiesSQLRepository.GetAllCompaniesAsync();

            for (int i = 0; i < companies.Count(); i++)
            {
                vm.CompanyNameItems.Add(new SelectListItem() { Value = Convert.ToString(i), Text = companies.ElementAt(i).CompanyName });
            }

            var controller = request.Controller;
            var actionDescriptor = controller.ControllerContext.ActionDescriptor;
            
            controller.ViewBag.ReturnUrl = controller.Url.Action(request.NameOfUsersToApproveIndexAction, actionDescriptor.ControllerName, controller.Request.Scheme);

            return request.Controller.View(vm);
        }
    }
}
