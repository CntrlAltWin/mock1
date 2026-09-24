using Microsoft.AspNetCore.Mvc;

namespace mock1.Controllers
// Handles login, registration, logout, and other authentication-related actions.
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
