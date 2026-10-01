using Microsoft.EntityFrameworkCore;
using mock1.Data;
using mock1.Models;

namespace mock1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();

            app.UseSession();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Register}/{id?}")
                .WithStaticAssets();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();

                if (!db.Users.Any())
                {
                    var testUser = new User
                    {
                        FullName = "Test Student",
                        Email = "223022568@stud.cut.ac.za",
                        Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                        Role = UserRole.Student
                    };
                    db.Users.Add(testUser);
                    db.SaveChanges();

                    db.Students.Add(new Student
                    {
                        UserId = testUser.UserId,
                        StudentNumber = "223022568",
                        Course = "Diploma in Information Technology",
                        YearLevel = "2",
                        Phone = "0821234567"
                    });
                    db.SaveChanges();
                }
            }

            app.Run();
        }
    }
}
