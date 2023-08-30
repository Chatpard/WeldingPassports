using Application.Interfaces.Repositories.SQL;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.ExamCenters
{
    public class PostExamCenterEditRequestHandler: IRequestHandler<PostExamCenterEditRequest, IActionResult>
    {
        private readonly IExamCentersSQLRepository _repository;
        private readonly ICompaniesSQLRepository _companiesSQLRepository;
        private readonly ICompanyContactsSQLRepository _companyContactsSQLRepository;
        private readonly IMapper _mapper;

        public PostExamCenterEditRequestHandler(IExamCentersSQLRepository repository, ICompaniesSQLRepository companiesSQLRepository,
            ICompanyContactsSQLRepository companyContactsSQLRepository, IMapper mapper)
        {
            _repository=repository;
            _companiesSQLRepository=companiesSQLRepository;
            _companyContactsSQLRepository=companyContactsSQLRepository;
            _mapper=mapper;
        }

        public async Task<IActionResult> Handle(PostExamCenterEditRequest request, CancellationToken cancellationToken)
        {
            if(request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ModelState.Remove("submit");
            if (request.Controller.ModelState.IsValid)
            {

                ExamCenter examCenter = _mapper.Map<ExamCenter>(request.ExamCenterEditVM);
                _repository.PostExamCentersEdit(examCenter);
                await _repository.SaveChangesAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl); 
            }

            request.ExamCenterEditVM.CompanySelectList = _companiesSQLRepository.CompanySelectList(unasigned: true, CompanyID: request.ExamCenterEditVM.CompanyID);
            request.ExamCenterEditVM.CompanyContactSelectList = _companyContactsSQLRepository.CompanyContactExamCenterSelectList(request.ExamCenterEditVM.EncryptedID);

            return request.Controller.View(request.ExamCenterEditVM);
        }
    }
}
