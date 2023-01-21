using Application.Requests.ExamCenters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    [Authorize]
    public class ExamCentersController : Controller
    {
        private readonly IMediator _mediator;

        public ExamCentersController(IMediator mediator)
        {
            _mediator=mediator;
        }

        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            var query = new GetExamCentersIndexRequest(sortOrder, currentFilter, searchString, pageNumber, this);
            return await _mediator.Send(query);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
