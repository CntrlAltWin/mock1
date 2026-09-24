using Microsoft.AspNetCore.Mvc;

namespace mock1.Controllers
{
    public class HomeController : Controller
    {
        // Opens the student dashboard when the app starts
        public IActionResult Index()
        {
            return RedirectToAction("Dashboard", "Student");
        }
    }
}