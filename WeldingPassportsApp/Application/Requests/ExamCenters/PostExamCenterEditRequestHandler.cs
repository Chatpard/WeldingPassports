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
        private readonly IMapper _mapper;

        public PostExamCenterEditRequestHandler(IExamCentersSQLRepository repository, IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }

        public async Task<IActionResult> Handle(PostExamCenterEditRequest request, CancellationToken cancellationToken)
        {
            //Todo: Validation Submit Invalid
            //if(request.Controller.ModelState.IsValid)
            //{
                if(request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                ExamCenter examCenter = _mapper.Map<ExamCenter>(request.ExamCenterEditVM);
                _repository.PostExamCentersEdit(examCenter);
                await _repository.SaveChangesAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl); 
            //}

            //return request.Controller.View(request.ExamCenterEditVM);
        }
    }
}
