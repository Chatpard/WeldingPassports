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
        private readonly IListTrainingCentersSQLRepository _listTrainingCentersSQLRepository;
        private readonly IMapper _mapper;

        public PostTrainingCenterEditRequestHandler(ITrainingCentersSQLRepository repository, IListTrainingCentersSQLRepository listTrainingCentersSQLRepository, IMapper mapper)
        {
            _repository = repository;
            _listTrainingCentersSQLRepository = listTrainingCentersSQLRepository;
            _mapper = mapper;
        }

        public async Task<ActionResult> Handle(PostTrainingCenterEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                TrainingCenter trainingCenterChanges = _mapper.Map<TrainingCenter>(request.TrainingCenterChanges);
                _repository.PostTrainingCenterEditAsync(trainingCenterChanges);
                await _repository.SaveAsync(cancellationToken);

                await _listTrainingCentersSQLRepository.Edit(trainingCenterChanges.ID, request.TrainingCenterChanges.CompanyContactID);
                await _repository.SaveAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View();
        }
    }
}
