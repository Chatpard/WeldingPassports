using Application.ViewModels;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Account
{
    public class PostAccountRegisterRequest : IRequest<IActionResult>
    {
        public UserRegistrationViewModel ViewModel { get; }
        public string NameOfConfirmEmailAction { get; }
        public Controller Controller { get; }

        public PostAccountRegisterRequest(UserRegistrationViewModel viewModel, string nameOfConfirmEmailAction, Controller controller)
        {
            ViewModel = viewModel;
            NameOfConfirmEmailAction = nameOfConfirmEmailAction;
            Controller = controller;
        }
    }
}
