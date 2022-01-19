using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Contacts
{
    public class GetContactCreateRequestHandler : IRequestHandler<GetContactCreateRequest, IActionResult>
    {
        public async Task<IActionResult> Handle(GetContactCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            return request.Controller.View();
        }
    }
}
