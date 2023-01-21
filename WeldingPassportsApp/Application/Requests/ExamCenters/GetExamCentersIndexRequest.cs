using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.ExamCenters
{
    public class GetExamCentersIndexRequest: IRequest<ActionResult>
    {
        public GetExamCentersIndexRequest(string sortOrder, string currentFilter, string searchString, int? pageNumber, Controller controller)
        {
            SortOrder=sortOrder;
            CurrentFilter=currentFilter;
            SearchString=searchString;
            PageNumber=pageNumber;
            Controller=controller;
        }

        public string SortOrder { get; }
        public string CurrentFilter { get; }
        public string SearchString { get; set; }
        public int? PageNumber { get; set; }
        public Controller Controller { get; }
    }
}
