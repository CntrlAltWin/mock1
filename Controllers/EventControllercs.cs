using Microsoft.AspNetCore.Mvc;

namespace mock1.Controllers
{
    public class EventController : Controller
    {
        // Opens the events page
        public IActionResult Index()
        {
            return View();
        }
    }
}