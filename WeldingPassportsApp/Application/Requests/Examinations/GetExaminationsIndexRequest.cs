using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.Examinations
{
    public class GetExaminationsIndexRequest : IRequest<IActionResult>
    {
        public string SortOrder { get; }
        public string CurrentFilter { get; }
        public string SearchString { get; set; }
        public int? PageNumber { get; set; }
        public string ExaminationsControllerName { get; }
        public string ExaminationDetailsActionName { get; }
        public Controller Controller { get; }

        public GetExaminationsIndexRequest(string sortOrder, string currentFilter, string searchString, int? pageNumber, string examinationsControllerName, string examinationDetailsActionName, Controller controller)
        {
            SortOrder = sortOrder;
            CurrentFilter = currentFilter;
            SearchString = searchString;
            PageNumber = pageNumber;
            ExaminationsControllerName = examinationsControllerName;
            ExaminationDetailsActionName = examinationDetailsActionName;
            Controller = controller;
        }
    }
}
