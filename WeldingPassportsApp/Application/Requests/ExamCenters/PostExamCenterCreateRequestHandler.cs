using Application.Interfaces.Repositories.SQL;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.ExamCenters
{
    internal class PostExamCenterCreateRequestHandler : IRequestHandler<PostExamCenterCreateRequest, IActionResult>
    {
        private readonly IExamCentersSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostExamCenterCreateRequestHandler(IExamCentersSQLRepository repository, IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }

        public async Task<IActionResult> Handle(PostExamCenterCreateRequest request, CancellationToken cancellationToken)
        {
            if(request.Controller.ModelState.IsValid)
            {
                if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                ExamCenter newExamCenter = _mapper.Map<ExamCenter>(request.ExamCenterCreateVM);
                _repository.PostExamCentersCreate(newExamCenter);
                await _repository.SaveChangesAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View(request.ExamCenterCreateVM);
        }
    }
}
