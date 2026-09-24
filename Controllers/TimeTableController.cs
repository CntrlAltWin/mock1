using Microsoft.AspNetCore.Mvc;

namespace mock1.Controllers
{
    public class TimetableController : Controller
    {
        // Opens the timetable page
        public IActionResult Index()
        {
            return View();
        }
    }
}