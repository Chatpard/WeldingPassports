using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Admin
{
    public class GetAdminAppSettingsDetailsRequestHandler : IRequestHandler<GetAdminAppSettingsDetailsRequest, IActionResult>
    {
        private readonly IAppSettingsSQLRepository _repository;
        private readonly IMapper _mapper;

        public GetAdminAppSettingsDetailsRequestHandler(IAppSettingsSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetAdminAppSettingsDetailsRequest request, CancellationToken cancellationToken)
        {
            AppSettings appSettings = await _repository.GetAppsetingsAsync();

            if (appSettings == null) 
                throw new InvalidOperationException("App settings not found!");

            var vm = _mapper.Map<AppSettingsViewModel>(appSettings);

            return request.Controller.View(vm);
        }
    }
}
