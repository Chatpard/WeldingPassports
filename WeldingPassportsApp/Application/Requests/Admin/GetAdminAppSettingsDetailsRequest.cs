using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Admin
{
    public class GetAdminAppSettingsDetailsRequest : IRequest<IActionResult>
    {
        public Controller Controller { get; }

        public GetAdminAppSettingsDetailsRequest(Controller controller)
        {
            Controller = controller;
        }
    }
}
