using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Users
{
    public class GetUserIndexRequestHandler : IRequestHandler<GetUserIndexRequest, IActionResult>
    {
        private readonly IUsersSQLRepository _usersSQLRepository;

        public GetUserIndexRequestHandler(IUsersSQLRepository usersSQLRepository)
        {
            _usersSQLRepository=usersSQLRepository;
        }

        public async Task<IActionResult> Handle(GetUserIndexRequest request, CancellationToken cancellationToken)
        {
            var vm = _usersSQLRepository.GetUserIndexPagination();
            return request.Controller.Ok(vm);
        }
    }
}
