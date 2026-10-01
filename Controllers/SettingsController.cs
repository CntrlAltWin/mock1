using Microsoft.AspNetCore.Mvc;
using mock1.Data;
using System.Linq;

namespace mock1.Controllers
{
    public class SettingsController : Controller
    {
        private readonly AppDbContext context;

        public SettingsController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var user = context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            return View(user);
        }

        [HttpPost]
        public IActionResult ChangePassword(string currentPassword, string newPassword, string confirmNewPassword)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var user = context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            if (user.Password == null || !BCrypt.Net.BCrypt.Verify(currentPassword, user.Password))
            {
                ViewBag.Error = "Current password is incorrect.";
                return View("Index", user);
            }

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 8)
            {
                ViewBag.Error = "New password must be at least 8 characters.";
                return View("Index", user);
            }

            if (newPassword != confirmNewPassword)
            {
                ViewBag.Error = "New passwords do not match.";
                return View("Index", user);
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            context.SaveChanges();

            TempData["Success"] = "Password updated successfully.";
            return RedirectToAction("Index");
        }
    }
}
