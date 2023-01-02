using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Examinations
{
    public class GetExaminationDeleteRequestHandler : IRequestHandler<GetExaminationDeleteRequest, IActionResult>
    {
        private readonly IExaminationsSQLRepository _repository;

        public GetExaminationDeleteRequestHandler(IExaminationsSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetExaminationDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteExaminationByEncryptedIDAsync(request.EncryptedID, cancellationToken);
            }
            catch (DbUpdateException)
            { }

            return request.Controller.LocalRedirect(request.ReturnUrl);
        }
    }
}
