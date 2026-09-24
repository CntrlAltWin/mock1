using Microsoft.AspNetCore.Mvc;

namespace mock1.Controllers
{
    public class AssistanceController : Controller
    {
        // Opens the assistance page
        public IActionResult Index()
        {
            return View();
        }
    }
}