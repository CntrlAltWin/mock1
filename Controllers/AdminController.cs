using Microsoft.AspNetCore.Mvc;

namespace mock1.Controllers
{// Handles administrator functions such as managing users, announcements, and system content.

    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
