using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Account
{
    public class GetAccountRegisterRequestHandler : IRequestHandler<GetAccountRegisterRequest, IActionResult>
    {
        private readonly ICompaniesSQLRepository _repository;

        public GetAccountRegisterRequestHandler(ICompaniesSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetAccountRegisterRequest request, CancellationToken cancellationToken)
        {
            var companies = await _repository.GetAllCompaniesAsync();

            UserRegistrationViewModel vm = new UserRegistrationViewModel();

            var info = await request.SignInManager.GetExternalLoginInfoAsync();
            if (info != null)
            {
                var emailClaim = info.Principal.Claims.ToList().Find(c => c.Type == ClaimTypes.Email);
                if (emailClaim != null)
                    vm.Email = emailClaim.Value;
                var firstNameClaim = info.Principal.Claims.ToList().Find(c => c.Type == ClaimTypes.GivenName);
                if (firstNameClaim != null)
                    vm.FirstName = firstNameClaim.Value;
                var lastNameClaim = info.Principal.Claims.ToList().Find(c => c.Type == ClaimTypes.Surname);
                vm.LastName = lastNameClaim.Value;
            }

            for (int i = 0; i < companies.Count(); i++)
            {
                vm.CompanyNameItems.Add(new SelectListItem() { Value = Convert.ToString(i), Text = companies.ElementAt(i).CompanyName });
            }

            return request.Controller.View(vm);
        }
    }
}
