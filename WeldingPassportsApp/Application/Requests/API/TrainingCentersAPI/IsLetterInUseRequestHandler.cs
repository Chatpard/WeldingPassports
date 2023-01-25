using Application.Interfaces.Repositories.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.API.TrainingCentersAPI
{
    public class IsLetterInUseRequestHandler : IRequestHandler<IsLetterInUseRequest, JsonResult>
    {
        private readonly ITrainingCentersAPIRepository _repository;

        public IsLetterInUseRequestHandler(ITrainingCentersAPIRepository repository)
        {
            _repository = repository;
        }

        public async Task<JsonResult> Handle(IsLetterInUseRequest request, CancellationToken cancellationToken)
        {
            return new JsonResult(_repository.IsLetterInUse(request.Letter));
        }
    }
}
