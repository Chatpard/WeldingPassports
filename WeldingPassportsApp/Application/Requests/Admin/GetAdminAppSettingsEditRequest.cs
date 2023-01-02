using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Admin
{
    public class GetAdminAppSettingsEditRequest : IRequest<IActionResult>
    {
        public int ID { get; }
        public Controller Controller { get; }

        public GetAdminAppSettingsEditRequest(int id, Controller controller)
        {
            ID = id;
            Controller = controller;
        }
    }
}
