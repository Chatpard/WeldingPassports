using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.PEPassports
{
    public class PostPEPassportUpdateRequest: IRequest<IActionResult>
    {
        public PEPassportUpdateViewModel PePassportUpdates { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public PostPEPassportUpdateRequest(PEPassportUpdateViewModel pePassportUpdates, string returnUrl, Controller controller)
        {
            PePassportUpdates = pePassportUpdates;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
