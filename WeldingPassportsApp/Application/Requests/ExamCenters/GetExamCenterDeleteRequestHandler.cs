using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace Application.Requests.ExamCenters
{
    public class GetExamCenterDeleteRequestHandler : IRequestHandler<GetExamCenterDeleteRequest, IActionResult>
    {
        private readonly IExamCentersSQLRepository _repository;

        public GetExamCenterDeleteRequestHandler(IExamCentersSQLRepository repository)
        {
            _repository=repository;
        }

        public async Task<IActionResult> Handle(GetExamCenterDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteExamCenterByEncryptedIDasync(request.EncryptedID, cancellationToken);
            }
            catch (DbUpdateException)
            {
                var modelStateError = new ModelStateErr()
                {
                    Key = "",
                    ErrorMessage = "This Exam Center is still referenced and can not be deleted."
                };
                request.Controller.TempData["ModelStateError"] = JsonSerializer.Serialize( modelStateError );
            }

            return request.Controller.LocalRedirect(request.ReturnUrl);
        }
        private class ModelStateErr
        {
            public string Key { get; set; }

            public string ErrorMessage { get; set; }
        }
    }
}
