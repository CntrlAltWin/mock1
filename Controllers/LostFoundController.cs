using Microsoft.AspNetCore.Mvc;

namespace mock1.Controllers
{
    public class LostFoundController : Controller
    {
        // Opens the Lost & Found page
        public IActionResult Index()
        {
            return View();
        }
    }
}