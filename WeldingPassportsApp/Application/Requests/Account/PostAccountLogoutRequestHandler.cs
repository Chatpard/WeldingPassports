using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Account
{
    public class PostAccountLogoutRequestHandler : IRequestHandler<PostAccountLogoutRequest, IActionResult>
    {
        public PostAccountLogoutRequestHandler()
        {
            
        }
        public async Task<IActionResult> Handle(PostAccountLogoutRequest request, CancellationToken cancellationToken)
        {
            await request.SignInManager.SignOutAsync();
            return request.Controller.RedirectToAction(request.NameOfIndexAction, request.NameOfPEPassportsController);
        }
    }
}
