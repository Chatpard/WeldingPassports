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
        private readonly ICompaniesSQLRepository _companiesSQLRepository;
        private readonly ICompanyContactsSQLRepository _companyContactsSQLRepository;
        private readonly IMapper _mapper;

        public PostTrainingCenterCreateRequestHandler(ITrainingCentersSQLRepository repository, ICompaniesSQLRepository companiesSQLRepository, ICompanyContactsSQLRepository companyContactsSQLRepository, IMapper mapper)
        {
            _repository = repository;
            _companiesSQLRepository=companiesSQLRepository;
            _companyContactsSQLRepository=companyContactsSQLRepository;
            _mapper = mapper;
        }

        public async Task<ActionResult> Handle(PostTrainingCenterCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            if (request.Controller.ModelState.IsValid)
            {

                TrainingCenter newtrainingCenter = _mapper.Map<TrainingCenter>(request.TrainingCenterCreateVM);
                _repository.PostTrainingCenterCreate(newtrainingCenter);
                await _repository.SaveChangesAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            request.TrainingCenterCreateVM.CompanySelectList = _companiesSQLRepository.CompanySelectList(unasigned: true);
            request.TrainingCenterCreateVM.CompanyContactSelectList = _companyContactsSQLRepository.CompanyContactTrainingCenterSelectList();

            return request.Controller.View(request.TrainingCenterCreateVM);
        }
    }
}