﻿using Application.Interfaces.Repositories.API;
using Application.SQLModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.API.CertificatesAPI
{
    public class GetRegistrationTypesFromPEPassportRequestHandler : IRequestHandler<GetRegistrationTypesFromPEPassportRequest, ActionResult<GetRegistrationTypesFromPEPassportReponse>>
    {
        private readonly ICertificationsAPIRepository _repository;

        public GetRegistrationTypesFromPEPassportRequestHandler(ICertificationsAPIRepository repository)
        {
            _repository = repository;
        }
        public async Task<ActionResult<GetRegistrationTypesFromPEPassportReponse>> Handle(GetRegistrationTypesFromPEPassportRequest request, CancellationToken cancellationToken)
        {
            return await _repository.GetRegistrationTypesSelectListFromPEPassport(request.PEPassortID, request.ProcessID, request.ExamDate);
        }
    }
}