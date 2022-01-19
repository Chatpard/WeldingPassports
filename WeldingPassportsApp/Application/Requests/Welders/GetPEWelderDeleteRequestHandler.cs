using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Welders
{
    public class GetPEWelderDeleteRequestHandler : IRequestHandler<GetPEWelderDeleteRequest, IActionResult>
    {
        private readonly IPEWeldersSQLRepository _repository;

        public GetPEWelderDeleteRequestHandler(IPEWeldersSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetPEWelderDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeletePEWelderByEncryptedIDAsync(request.EncryptedID, cancellationToken);
            }
            catch (DbUpdateException)
            { }

            return request.Controller.LocalRedirect(request.ReturnUrl);
        }
    }
}
