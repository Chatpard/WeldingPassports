using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.Users
{
    public class GetUserIndexRequest: IRequest<IActionResult>
    {
        public GetUserIndexRequest(UserManager<AppUser> userManager, Controller controller)
        {
            UserManager=userManager;
            Controller=controller;
        }

        public UserManager<AppUser> UserManager { get; }
        public Controller Controller { get; }
    }
}
