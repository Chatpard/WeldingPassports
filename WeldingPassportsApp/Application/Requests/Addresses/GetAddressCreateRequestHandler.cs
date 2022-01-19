using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Addresses
{
    class GetAddressCreateRequestHandler : IRequestHandler<GetAddressCreateRequest, IActionResult>
    {
        public GetAddressCreateRequestHandler()
        {

        }

        public async Task<IActionResult> Handle(GetAddressCreateRequest request, CancellationToken cancellationToken)
        {
            request.Controller.ViewBag.returnUrl = request.ReturnUrl;
            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            return request.Controller.View();
        }
    }
}
