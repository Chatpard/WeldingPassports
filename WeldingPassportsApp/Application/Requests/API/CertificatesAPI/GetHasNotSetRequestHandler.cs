using Application.Interfaces.Repositories.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.API.CertificatesAPI
{
    internal class GetHasNotSetRequestHandler : IRequestHandler<GetHasNotSet, ActionResult<bool?>>
    {
        private readonly ICertificationsAPIRepository _repository;

        public GetHasNotSetRequestHandler(ICertificationsAPIRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult<bool?>> Handle(GetHasNotSet request, CancellationToken cancellationToken)
        {
            bool? hasNotSet = await _repository.GetHasNotSet(request.PePassortID, request.ProcessID);
            if (hasNotSet == null)
            {
                return request.Controller.NotFound(hasNotSet);
            }
            return request.Controller.Ok(hasNotSet);
        }
    }
}
