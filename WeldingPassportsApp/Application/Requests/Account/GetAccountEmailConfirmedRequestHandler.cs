using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Account
{
    public class GetAccountEmailConfirmedRequestHandler : IRequestHandler<GetAccountEmailConfirmedRequest, IActionResult>
    {
        public async Task<IActionResult> Handle(GetAccountEmailConfirmedRequest request, CancellationToken cancellationToken)
        {
            var requestCookie = request.Controller.Request.Cookies["EmailConfirmed"];
            if (requestCookie != null)
                return await Task.FromResult(request.Controller.View());
            else
                return await Task.FromResult(request.Controller.LocalRedirect("~/"));
        }
    }
}
