using Application.Interfaces.Repositories.SQL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Addresses
{
    internal class GetAddressEditRequestHandler : MediatR.IRequestHandler<GetAddressEditRequest, IActionResult>
    {
        private readonly IAddressesSQLRepository _repository;

        public GetAddressEditRequestHandler(IAddressesSQLRepository repository)
        {
            _repository=repository;
        }

        public async Task<IActionResult> Handle(GetAddressEditRequest request, CancellationToken cancellationToken)
        {
            if (request.EncryptedID == "null")
            {
                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.returnUrl = request.ReturnUrl;

            var vm = await _repository.GetAddressEditAsync(request.EncryptedID);

            return request.Controller.View(vm);
        }
    }
}
