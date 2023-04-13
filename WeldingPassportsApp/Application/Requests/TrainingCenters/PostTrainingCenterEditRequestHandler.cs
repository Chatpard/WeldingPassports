using Application.Interfaces.Repositories.SQL;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.TrainingCenters
{
    class PostTrainingCenterEditRequestHandler : IRequestHandler<PostTrainingCenterEditRequest, ActionResult>
    {
        private readonly ITrainingCentersSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostTrainingCenterEditRequestHandler(ITrainingCentersSQLRepository repository, IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }

        public async Task<ActionResult> Handle(PostTrainingCenterEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                TrainingCenter trainingCenterChanges = _mapper.Map<TrainingCenter>(request.TrainingCenterEditVM);
                _repository.PostTrainingCenterEdit(trainingCenterChanges);
                await _repository.SaveChangesAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View(request.TrainingCenterEditVM);
        }
    }
}
