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
        private readonly ICompaniesSQLRepository _companiesSQLRepository;
        private readonly ICompanyContactsSQLRepository _companyContactsSQLRepository;
        private readonly IMapper _mapper;

        public PostTrainingCenterEditRequestHandler(ITrainingCentersSQLRepository repository, ICompaniesSQLRepository companiesSQLRepository,
            ICompanyContactsSQLRepository companyContactsSQLRepository, IMapper mapper)
        {
            _repository=repository;
            _companiesSQLRepository=companiesSQLRepository;
            _companyContactsSQLRepository=companyContactsSQLRepository;
            _mapper=mapper;
        }

        public async Task<ActionResult> Handle(PostTrainingCenterEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            if (request.Controller.ModelState.IsValid)
            {
                TrainingCenter trainingCenterChanges = _mapper.Map<TrainingCenter>(request.TrainingCenterEditVM);
                _repository.PostTrainingCenterEdit(trainingCenterChanges);
                await _repository.SaveChangesAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            request.TrainingCenterEditVM.CompanySelectList = _companiesSQLRepository.CompanySelectList(unasigned: true, CompanyID: request.TrainingCenterEditVM.CompanyID);
            request.TrainingCenterEditVM.CompanyContactSelectList = _companyContactsSQLRepository.CompanyContactTrainingCenterSelectList(request.TrainingCenterEditVM.EncryptedID);

            return request.Controller.View(request.TrainingCenterEditVM);
        }
    }
}
