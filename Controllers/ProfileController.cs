using Microsoft.AspNetCore.Mvc;
using mock1.Data;
using System.Linq;
using System.Text.RegularExpressions;

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
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var student = context.Students.FirstOrDefault(s => s.UserId == userId);
            var user = context.Users.FirstOrDefault(u => u.UserId == userId);

            if (student == null || user == null)
                return RedirectToAction("Login", "Auth");

            ViewBag.User = user;
            return View(student);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var student = context.Students.FirstOrDefault(s => s.UserId == userId);
            var user = context.Users.FirstOrDefault(u => u.UserId == userId);

            if (student == null || user == null)
                return RedirectToAction("Login", "Auth");

            ViewBag.User = user;
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(string fullName, string course, string yearLevel, string phone)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var student = context.Students.FirstOrDefault(s => s.UserId == userId);
            var user = context.Users.FirstOrDefault(u => u.UserId == userId);

            if (student == null || user == null)
                return RedirectToAction("Login", "Auth");

            if (string.IsNullOrWhiteSpace(fullName) || !Regex.IsMatch(fullName, @"^[A-Za-z\s]+$"))
            {
                ViewBag.Error = "Full name can only contain letters and spaces.";
                ViewBag.User = user;
                return View(student);
            }

            if (string.IsNullOrWhiteSpace(course) || !Regex.IsMatch(course, @"^[A-Za-z\s]+$"))
            {
                ViewBag.Error = "Course can only contain letters and spaces.";
                ViewBag.User = user;
                return View(student);
            }

            if (!string.IsNullOrWhiteSpace(phone) && !Regex.IsMatch(phone, @"^0\d{9}$"))
            {
                ViewBag.Error = "Phone number must be 10 digits starting with 0.";
                ViewBag.User = user;
                return View(student);
            }

            user.FullName = fullName;
            student.Course = course;
            student.YearLevel = yearLevel;
            student.Phone = string.IsNullOrWhiteSpace(phone) ? null : phone;

            context.SaveChanges();
            HttpContext.Session.SetString("FullName", user.FullName);

            TempData["Success"] = "Profile updated successfully.";
            return RedirectToAction("Index");
        }
    }
}
