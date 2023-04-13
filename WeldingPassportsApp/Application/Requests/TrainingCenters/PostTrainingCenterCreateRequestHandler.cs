using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.TrainingCenters
{
    class PostTrainingCenterCreateRequestHandler : IRequestHandler<PostTrainingCenterCreateRequest, ActionResult>
    {
        private readonly ITrainingCentersSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostTrainingCenterCreateRequestHandler(ITrainingCentersSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ActionResult> Handle(PostTrainingCenterCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                TrainingCenter newtrainingCenter = _mapper.Map<TrainingCenter>(request.TrainingCenterCreateVM);
                _repository.PostTrainingCenterCreate(newtrainingCenter);
                await _repository.SaveChangesAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View(request.TrainingCenterCreateVM);
        }
    }
}