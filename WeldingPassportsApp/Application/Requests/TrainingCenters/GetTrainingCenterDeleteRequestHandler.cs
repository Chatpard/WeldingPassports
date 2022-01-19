using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.TrainingCenters
{
    public class GetTrainingCenterDeleteRequestHandler : IRequestHandler<GetTrainingCenterDeleteRequest, IActionResult>
    {
        private readonly ITrainingCentersSQLRepository _repository;

        public GetTrainingCenterDeleteRequestHandler(ITrainingCentersSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetTrainingCenterDeleteRequest request, CancellationToken cancellationToken)
        {
            //try
            //{
                await _repository.DeleteTrainingCenterByEncryptedIDAsync(request.EncryptedID, cancellationToken);
            //}
            //catch (DbUpdateException)
            //{ }

            return request.Controller.LocalRedirect(request.ReturnUrl);
        }
    }
}
