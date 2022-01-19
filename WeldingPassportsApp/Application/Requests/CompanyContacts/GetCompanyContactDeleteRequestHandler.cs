using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.CompanyContacts
{
    public class GetCompanyContactDeleteRequestHandler : IRequestHandler<GetCompanyContactDeleteRequest, IActionResult>
    {
        private readonly ICompanyContactsSQLRepository _repository;

        public GetCompanyContactDeleteRequestHandler(ICompanyContactsSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetCompanyContactDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteCompanyContactByEncryptedIDAsync(request.EncryptedID, cancellationToken);
            }
            catch (DbUpdateException)
            { }

            return request.Controller.LocalRedirect(request.ReturnUrl);
        }
    }
}
