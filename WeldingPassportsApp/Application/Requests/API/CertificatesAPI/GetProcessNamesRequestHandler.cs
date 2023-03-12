using Application.Interfaces.Repositories.API;
using Application.APIModels;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.API.CertificatesAPI
{
    internal class GetProcessNamesRequestHandler : IRequestHandler<GetProcessNamesRequest, GetProcessNamesReponse>
    {
        private readonly ICertificationsAPIRepository _repository;

        public GetProcessNamesRequestHandler(ICertificationsAPIRepository repository)
        {
            _repository=repository;
        }

        public async Task<GetProcessNamesReponse> Handle(GetProcessNamesRequest request, CancellationToken cancellationToken)
        {
            return await _repository.GetProcessNamesSelectList(request.ExaminationEncryptedID, request.PePassportID, request.RegistrationID); 
        }
    }
}
