using Application.Requests.Users;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(IMediator mediator, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var query = new GetUserIndexRequest(_userManager, this);
            return await _mediator.Send(query);
        }
    }
}
