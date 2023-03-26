using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.PEPassports
{
    public class GetPEPassportsIndexRequest : IRequest<IActionResult>
    {
        public string SortOrder { get; }
        public string CurrentFilter { get; }
        public string SearchString { get; set; }
        public int? PageNumber { get; set; }
        public UserManager<AppUser> UserManager { get; }
        public Controller Controller { get; }

        public GetPEPassportsIndexRequest(string sortOrder, string currentFilter, string searchString, int? pageNumber, UserManager<AppUser> userMangager, Controller controller)
        {
            SortOrder = sortOrder;
            CurrentFilter = currentFilter;
            SearchString = searchString;
            PageNumber = pageNumber;
            UserManager = userMangager;
            Controller = controller;
        }
    }
}
