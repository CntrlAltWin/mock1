using Microsoft.AspNetCore.Mvc;

namespace mock1.Controllers
{
    public class CampusMapController : Controller
    {
        // Opens the campus map page
        public IActionResult Index()
        {
            return View();
        }
    }
}