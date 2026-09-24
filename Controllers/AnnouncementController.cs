using Microsoft.AspNetCore.Mvc;

namespace mock1.Controllers
{
    public class AnnouncementController : Controller
    {
        // Opens the announcements page
        public IActionResult Index()
        {
            return View();
        }
    }
}