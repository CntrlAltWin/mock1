using System.ComponentModel.DataAnnotations;

namespace mock1.Models
{
    public enum UserRole
    {
        Student,
        Admin
    }

    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Full name can only contain letters and spaces.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        public string? Password { get; set; }
        public UserRole Role { get; set; } = UserRole.Student;
    }
}
