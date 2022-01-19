using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Account
{
    public class GetAccountRegistrationSuccessRequestHandler : IRequestHandler<GetAccountRegistrationSuccessRequest, IActionResult>
    {
        private readonly IConfiguration _config;

        public GetAccountRegistrationSuccessRequestHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IActionResult> Handle(GetAccountRegistrationSuccessRequest request, CancellationToken cancellationToken)
        {
            var cookie = request.Controller.Request.Cookies["RegistrationSuccess"];
            if (cookie != null)
                return await Task.FromResult(request.Controller.View());
            else
                return await Task.FromResult(request.Controller.LocalRedirect("~/"));
        }
    }
}
