using Application.Interfaces.Repositories.SQL;
using Application.Requests.Welders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.PEPassports
{
    public class GetPEPassportDeleteRequestHandler : IRequestHandler<GetPEPassportDeleteRequest, IActionResult>
    {
        private readonly IPEPassportsSQLRepository _repository;

        public GetPEPassportDeleteRequestHandler(IPEPassportsSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetPEPassportDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeletePEPassportByEncryptedIDAsync(request.EncryptedID, cancellationToken);
            }
            catch (DbUpdateException)
            { }

            return request.Controller.LocalRedirect(request.ReturnUrl);
        }
    }
}
