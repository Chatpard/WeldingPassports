using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Admin
{
    public class PostAdminEditRequest : IRequest<IActionResult>
    {
        public AppSettingsViewModel AppSettingsChanges { get; }
        public string NameOfDetailsAction { get; }
        public Controller Controller { get; }

        public PostAdminEditRequest(AppSettingsViewModel appSettingsChanges, string nameOfDetailsAction, Controller controller)
        {
            AppSettingsChanges = appSettingsChanges;
            NameOfDetailsAction = nameOfDetailsAction;
            Controller = controller;
        }
    }
}
