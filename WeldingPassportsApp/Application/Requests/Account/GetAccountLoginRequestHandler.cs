using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Account
{
    public class GetAccountLoginRequestHandler : IRequestHandler<GetAccountLoginRequest, IActionResult>
    {
        public GetAccountLoginRequestHandler()
        {
            
        }

        public async Task<IActionResult> Handle(GetAccountLoginRequest request, CancellationToken cancellationToken)
        {
            var vm = new LoginViewModel
            {
                ReturnUrl = request.ReturnUrl,
                ExternalLogins = (await request.SignInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return request.Controller.View(vm);
        }
    }
}
