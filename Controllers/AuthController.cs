using Microsoft.AspNetCore.Mvc;
using mock1.Data;
using mock1.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace mock1.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext context;

        public AuthController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null || user.Password == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());
            HttpContext.Session.SetString("FullName", user.FullName);

            if (user.Role == UserRole.Admin)
                return RedirectToAction("Index", "Admin");

            return RedirectToAction("Dashboard", "Student");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullName, string studentNumber, string email,
            string password, string confirmPassword, string course, string yearLevel, string phone)
        {
            if (string.IsNullOrWhiteSpace(fullName) || !Regex.IsMatch(fullName, @"^[A-Za-z\s]+$"))
            {
                ViewBag.Error = "Full name can only contain letters and spaces.";
                return View();
            }

            if (string.IsNullOrWhiteSpace(studentNumber) || !Regex.IsMatch(studentNumber, @"^\d{9}$"))
            {
                ViewBag.Error = "Student number must be exactly 9 digits.";
                return View();
            }

            if (string.IsNullOrWhiteSpace(email) || !email.EndsWith("@stud.cut.ac.za", System.StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.Error = "Please register with your CUT student email (e.g. 223022568@stud.cut.ac.za).";
                return View();
            }

            string emailPrefix = email.Split('@')[0];
            if (!emailPrefix.Equals(studentNumber, System.StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.Error = "Your email must start with your student number.";
                return View();
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                ViewBag.Error = "Password must be at least 8 characters.";
                return View();
            }

            if (password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }

            if (string.IsNullOrWhiteSpace(course) || !Regex.IsMatch(course, @"^[A-Za-z\s]+$"))
            {
                ViewBag.Error = "Course can only contain letters and spaces.";
                return View();
            }

            if (string.IsNullOrWhiteSpace(yearLevel))
            {
                ViewBag.Error = "Year level is required.";
                return View();
            }

            if (!string.IsNullOrWhiteSpace(phone) && !Regex.IsMatch(phone, @"^0\d{9}$"))
            {
                ViewBag.Error = "Phone number must be 10 digits starting with 0.";
                return View();
            }

            if (context.Users.Any(u => u.Email == email))
            {
                ViewBag.Error = "An account with that email already exists.";
                return View();
            }

            var user = new User
            {
                FullName = fullName,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Role = UserRole.Student
            };
            context.Users.Add(user);
            context.SaveChanges();

            context.Students.Add(new Student
            {
                UserId = user.UserId,
                StudentNumber = studentNumber,
                Course = course,
                YearLevel = yearLevel,
                Phone = string.IsNullOrWhiteSpace(phone) ? null : phone
            });
            context.SaveChanges();

            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());
            HttpContext.Session.SetString("FullName", user.FullName);

            return RedirectToAction("Dashboard", "Student");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
