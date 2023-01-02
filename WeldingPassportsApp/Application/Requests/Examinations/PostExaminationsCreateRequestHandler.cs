using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Examinations
{
    public class PostExaminationsCreateRequestHandler : IRequestHandler<PostExaminationsCreateRequest, IActionResult>
    {
        private readonly IExaminationsSQLRepository _repository;

        public PostExaminationsCreateRequestHandler(IExaminationsSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(PostExaminationsCreateRequest request, CancellationToken cancellationToken)
        {
            await _repository.PostExaminationCreateAsync(request.Vm, cancellationToken);

            return request.Controller.LocalRedirect(request.ReturnUrl);
        }
    }
}
