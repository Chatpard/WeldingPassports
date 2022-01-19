using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Admin
{
    public class PostAdminEditRequestHandler : IRequestHandler<PostAdminAppSettingsEditRequest, IActionResult>
    {
        private readonly IAppSettingsSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostAdminEditRequestHandler(IAppSettingsSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(PostAdminAppSettingsEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                AppSettings appSettings = _mapper.Map<AppSettings>(request.AppSettingsChanges);
                _repository.UpdateAppSettings(appSettings);
                await _repository.SaveAsync(cancellationToken);
                
                return request.Controller.RedirectToAction(request.NameOfDetailsAction);
            }

            return request.Controller.View();
        }
    }
}
