using Application.Interfaces.Repositories.SQL;
using Application.Requests.PEPassports;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Welders
{
    public class PostPEWelderCreateRequestHandler : IRequestHandler<PostPEWelderCreateRequest, IActionResult>
    {
        private readonly IPEWeldersSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostPEWelderCreateRequestHandler(IPEWeldersSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(PostPEWelderCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                PEWelder peWelder = _mapper.Map<PEWelder>(request.PEWelderVM);

                await _repository.PostPEWelderCreateAsync(peWelder);
                await _repository.SaveAsync(cancellationToken);
                PEWelderEditViewModel newPEWelder = _mapper.Map<PEWelderEditViewModel>(peWelder);
                
                return request.Controller.RedirectToAction(request.NameOfDetailsAction, new { id = newPEWelder.EncryptedID, returnUrl = request.ReturnUrl });
            }

            return request.Controller.View();
        }
    }
}
