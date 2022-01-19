using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Admin
{
    public class GetAdminUsersToApproveIndexRequestHandler : IRequestHandler<GetAdminUsersToApproveIndexRequest, IActionResult>
    {
        private readonly IUserToApproveRepository _repository;
        private readonly IMapper _mapper;

        public GetAdminUsersToApproveIndexRequestHandler(IUserToApproveRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetAdminUsersToApproveIndexRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<UserToApproveIndex> userToApproveIndexList = await _repository.GetUsersToApproveIndexAsync();
            var listVm = new List<UserToApproveIndexViewModel>();

            foreach (UserToApproveIndex userToApproveIndex in userToApproveIndexList)
            {
                var userVm = _mapper.Map<UserToApproveIndexViewModel>(userToApproveIndex);
                listVm.Add(userVm);
            }

            var controller = request.Controller;
            var actionDescriptor = controller.ControllerContext.ActionDescriptor;

            controller.ViewBag.ReturnUrl = controller.Url.Action(actionDescriptor.ActionName, actionDescriptor.ControllerName,
                controller.Request.Scheme);

            return request.Controller.View(listVm);
        }
    }
}
