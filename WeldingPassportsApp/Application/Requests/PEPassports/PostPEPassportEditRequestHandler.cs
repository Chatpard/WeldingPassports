using Application.Interfaces.Repositories.SQL;
using Application.Requests.PEPassports;
using Application.Requests.Welders;
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
    public class PostPEWelderEditRequestHandler : IRequestHandler<PostPEPassportEditRequest, IActionResult>
    {
        private readonly IPEPassportsSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostPEWelderEditRequestHandler(IPEPassportsSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(PostPEPassportEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                PEPassport pePassport = _mapper.Map<PEPassport>(request.PEPassportChanges);
                _repository.PostPEPassportEditasync(pePassport);
                await _repository.SaveAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View();
        }
    }
}
