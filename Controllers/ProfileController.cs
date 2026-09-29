using Microsoft.AspNetCore.Mvc;
using mock1.Data;
using System.Linq;

namespace mock1.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDbContext context;

        public ProfileController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            // TEMPORARY: no login system exists yet, so this shows the
            // first student in the database. Once Auth is built, replace
            // this with a lookup by the logged-in user's session/UserId,
            // e.g.:
            //   var userId = HttpContext.Session.GetInt32("UserId");
            //   var student = context.Students.FirstOrDefault(s => s.UserId == userId);
            var student = context.Students.FirstOrDefault();

            if (student == null)
                return NotFound("No student records exist yet.");

            var user = context.Users.FirstOrDefault(u => u.UserId == student.UserId);

            if (user == null)
                return NotFound("No matching user account found for this student.");

            ViewBag.User = user;
            return View(student);
        }
    }
}
