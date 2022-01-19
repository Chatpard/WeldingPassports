using Application.Requests.Addresses;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    public class AddressesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public AddressesController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Create(string returnUrl)
        {
            try
            {
                var query = new GetAddressCreateRequest(returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in GetAddressCreate");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddressCreateViewModel addressCreateVM, string returnUrl)
        {
            try
            {
                var query = new PostAddressCreateRequest(addressCreateVM, returnUrl, this);
                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                await Task.CompletedTask;
                return Utilities.ErrorView(_env, this, e, "Error in PostAddressCreate");
            }
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
