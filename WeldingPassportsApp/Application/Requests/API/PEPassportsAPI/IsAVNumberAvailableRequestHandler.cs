using Application.Interfaces.Repositories.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.API.PEPassportsAPI
{
    public class IsAVNumberAvailableRequestHandler : IRequestHandler<IsAVNumberAvailableRequest, ActionResult<bool>>
    {
        private readonly IPEPassportsAPIRepository _repository;

        public IsAVNumberAvailableRequestHandler(IPEPassportsAPIRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult<bool>> Handle(IsAVNumberAvailableRequest request, CancellationToken cancellationToken)
        {
            bool IsAvailableAVNumber =  _repository.IsAvailableAVNumber(request.AVNumber, request.Letter);

            return request.Controller.Ok(IsAvailableAVNumber);
        }
    }
}