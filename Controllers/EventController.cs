using Microsoft.AspNetCore.Mvc;

namespace mock1.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "Auth");

            return View();
        }
    }
}
