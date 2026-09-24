using Microsoft.AspNetCore.Mvc;

namespace mock1.Controllers
{
    public class StudentController : Controller
    {
        // Opens the student dashboard
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}