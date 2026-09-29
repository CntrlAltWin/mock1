using System.ComponentModel.DataAnnotations;

namespace mock1.Models
{
    // NOTE: Password/Role are here so whoever builds Auth doesn't have to
    // touch this file again -- Profile only reads FullName and Email.
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
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        // Not used yet -- reserved for the Auth feature.
        public string? Password { get; set; }
        public UserRole Role { get; set; } = UserRole.Student;
    }
}
