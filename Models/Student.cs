using System.ComponentModel.DataAnnotations;

namespace mock1.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        // Links this Student record to a User account.
        // No navigation property on purpose -- keeps this simple until
        // Auth assigns a real UserId at registration/login time.
        public int UserId { get; set; }

        [Required(ErrorMessage = "Student number is required.")]
        [StringLength(20)]
        public string StudentNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course is required.")]
        [StringLength(150)]
        public string Course { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year level is required.")]
        [StringLength(20)]
        public string YearLevel { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Enter a valid phone number.")]
        public string? Phone { get; set; }
    }
}
