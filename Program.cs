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

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            // ---- TEMPORARY SEED DATA ----
            // Creates one test student so the Profile page has something
            // real to display before Auth/login exists. Remove this block
            // once registration is built and real accounts are created.
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();

                if (!db.Users.Any())
                {
                    var testUser = new User
                    {
                        FullName = "Test Student",
                        Email = "test.student@studenthelphub.co.za",
                        Role = UserRole.Student
                    };
                    db.Users.Add(testUser);
                    db.SaveChanges();

                    db.Students.Add(new Student
                    {
                        UserId = testUser.UserId,
                        StudentNumber = "2026001234",
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
