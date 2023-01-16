using Application.Interfaces.Repositories.SQL;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Contacts
{
    internal class GetContactEditRequestHandler : MediatR.IRequestHandler<GetContactEditRequest, IActionResult>
    {
        private readonly IContactsSQLRepository _repository;

        public GetContactEditRequestHandler(IContactsSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetContactEditRequest request, CancellationToken cancellationToken)
        {
            if(request.EncryptedID == "null")
            {
                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _repository.GetContactEditAsync(request.EncryptedID);

            return request.Controller.View(vm);
        }
    }
}
