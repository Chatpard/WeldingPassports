using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Welders
{
    public class GetPEWelderCreateRequestHandler : IRequestHandler<GetPEWelderCreateRequest, IActionResult>
    {
        private readonly IPEWeldersSQLRepository _peWeldersSQLRepository;

        public GetPEWelderCreateRequestHandler(IPEWeldersSQLRepository peWeldersSQLRepository)
        {
            _peWeldersSQLRepository = peWeldersSQLRepository;
        }

        public async Task<IActionResult> Handle(GetPEWelderCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            return request.Controller.View();
        }
    }
}
