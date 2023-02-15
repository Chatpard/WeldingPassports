using Application.Interfaces.Repositories.SQL;
using Application.Requests.PEPassports;
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

namespace Application.Requests.Welders
{
    public class PostPEWelderEditRequestHandler : IRequestHandler<PostPEWelderEditRequest, IActionResult>
    {
        private readonly IPEWeldersSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostPEWelderEditRequestHandler(IPEWeldersSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(PostPEWelderEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                PEWelder peWelder = _mapper.Map<PEWelder>(request.PEWelderChanges);
                _repository.PutPEWelderUpdate(peWelder);
                await _repository.SaveChangesAsync(cancellationToken);
                
                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View();
        }
    }
}
