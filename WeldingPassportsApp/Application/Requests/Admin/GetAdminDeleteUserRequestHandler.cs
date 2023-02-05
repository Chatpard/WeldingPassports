using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Admin
{
    public class GetAdminDeleteUserRequestHandler : IRequestHandler<GetAdminDeleteUserRequest, IActionResult>
    {
        private readonly IUserToApproveRepository _repository;

        public GetAdminDeleteUserRequestHandler(IUserToApproveRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetAdminDeleteUserRequest request, CancellationToken cancellationToken)
        {
            await _repository.DeleteUserToApproveByEncryptedIDAsync(request.UserToApproveEncryptedID);
            await _repository.SaveChangesAsync(cancellationToken);
            return request.Controller.LocalRedirect(request.ReturnUrl);
        }
    }
}
