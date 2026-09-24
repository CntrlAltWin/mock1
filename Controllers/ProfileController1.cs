using Microsoft.AspNetCore.Mvc;

namespace mock1.Controllers
{
    public class ProfileController : Controller
    {
        // Opens the profile page
        public IActionResult Index()
        {
            return View();
        }
    }
}