using Application.Interfaces.Repositories.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.API.TrainingCentersAPI
{
    public class IsLetterInUseRequestHandler : RequestHandler<IsLetterInUseRequest, JsonResult>
    {
        private readonly ITrainingCentersAPIRepository _repository;

        public IsLetterInUseRequestHandler(ITrainingCentersAPIRepository repository)
        {
            _repository = repository;
        }

        protected override JsonResult Handle(IsLetterInUseRequest request)
        {
            return new JsonResult(_repository.IsLetterInUse(request.Letter));
        }
    }
}
