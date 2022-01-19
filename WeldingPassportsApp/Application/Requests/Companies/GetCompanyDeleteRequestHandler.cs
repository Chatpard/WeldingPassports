using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Companies
{
    public class GetCompanyDeleteRequestHandler : IRequestHandler<GetCompanyDeleteRequest, IActionResult>
    {
        private readonly ICompaniesSQLRepository _repository;

        public GetCompanyDeleteRequestHandler(ICompaniesSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetCompanyDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteCompanyByEncryptedIDAsync(request.EncryptedID, cancellationToken);
            }
            catch (DbUpdateException)
            { }

            return request.Controller.LocalRedirect(request.ReturnUrl);
        }
    }
}
