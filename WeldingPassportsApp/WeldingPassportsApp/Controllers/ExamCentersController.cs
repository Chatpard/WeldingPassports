using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WeldingPassportsApp.Controllers
{
    [Authorize]
    public class ExamCentersController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
