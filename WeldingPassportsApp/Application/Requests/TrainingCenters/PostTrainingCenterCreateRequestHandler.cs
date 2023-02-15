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
        private readonly IListTrainingCentersSQLRepository _listTrainingCentersSQLRepository;
        private readonly IMapper _mapper;

        public PostTrainingCenterCreateRequestHandler(ITrainingCentersSQLRepository repository, IListTrainingCentersSQLRepository listTrainingCentersSQLRepository, IMapper mapper)
        {
            _repository = repository;
            _listTrainingCentersSQLRepository = listTrainingCentersSQLRepository;
            _mapper = mapper;
        }

        public async Task<ActionResult> Handle(PostTrainingCenterCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                TrainingCenter newtrainingCenter = _mapper.Map<TrainingCenter>(request.TrainingCenterCreateVM);
                await _repository.PostTrainingCenterCreateAsync(newtrainingCenter);
                await _repository.SaveChangesAsync(cancellationToken);

                EntityEntry<ListTrainingCenter> listTrainingCenter = await _listTrainingCentersSQLRepository.Create(newtrainingCenter.ID, request.TrainingCenterCreateVM.CompanyContactID);
                if (listTrainingCenter != null)
                    await _repository.SaveChangesAsync(cancellationToken);

                TrainingCenterEditViewModel newTrainingCenterVM = _mapper.Map<TrainingCenterEditViewModel>(newtrainingCenter);

                return request.Controller.RedirectToAction(request.NameOfDetailsAction, new { id = newTrainingCenterVM.EncryptedID, returnUrl = request.ReturnUrl });
            };

            return request.Controller.View();
        }
    }
}
