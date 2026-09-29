using Microsoft.EntityFrameworkCore;
using mock1.Models;

namespace mock1.Data
{
    // Only Users and Students are wired up here -- add other DbSets
    // (Announcement, Timetable, LostItem, etc.) as those features
    // get their own database work done by other team members.
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
