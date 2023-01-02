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
    public class PostExaminationEditRequestHandler : IRequestHandler<PostExaminationEditRequest, IActionResult>
    {
        private readonly IExaminationsSQLRepository _repository;

        public PostExaminationEditRequestHandler(IExaminationsSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(PostExaminationEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                await _repository.ExaminationUpdateAsync(request.NewExaminationVm, cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View();
        }
    }
}
