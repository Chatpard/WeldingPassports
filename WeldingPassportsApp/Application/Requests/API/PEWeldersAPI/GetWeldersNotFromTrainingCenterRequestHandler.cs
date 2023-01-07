using Application.Interfaces.Repositories.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.API.PEWeldersAPI
{
    public class GetWeldersNotFromTrainingCenterRequestHandler : IRequestHandler<GetWeldersNotFromTrainingCenterRequest, ActionResult<SelectList>>
    {
        private readonly IPEWeldersAPIRepository _repository;

        public GetWeldersNotFromTrainingCenterRequestHandler(IPEWeldersAPIRepository repository)
        {
            _repository=repository;
        }

        public async Task<ActionResult<SelectList>> Handle(GetWeldersNotFromTrainingCenterRequest request, CancellationToken cancellationToken)
        {
            return _repository.GetWeldersNotFromTrainingCenterID(request.TrainingCenterID);
        }
    }
}
