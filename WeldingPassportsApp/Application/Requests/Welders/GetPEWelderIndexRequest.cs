using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Welders
{
    public class GetPEWelderIndexRequest : IRequest<IActionResult>
    {
        public string SortOrder { get; }
        public string CurrentFilter { get; }
        public string SearchString { get; set; }
        public int? PageNumber { get; set; }
        public UserManager<AppUser> UserManager { get; }
        public Controller Controller { get; }

        public GetPEWelderIndexRequest(string sortOrder, string currentFilter, string searchString, int? pageNumber, UserManager<AppUser> userManager, Controller controller)
        {
            SortOrder = sortOrder;
            CurrentFilter = currentFilter;
            SearchString = searchString;
            PageNumber = pageNumber;
            UserManager = userManager;
            Controller = controller;
        }
    }
}
