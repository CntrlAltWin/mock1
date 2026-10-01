using System.ComponentModel.DataAnnotations;

namespace mock1.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Student number is required.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Student number must be exactly 9 digits.")]
        public string StudentNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course is required.")]
        [StringLength(150)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Course can only contain letters and spaces.")]
        public string Course { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year level is required.")]
        [StringLength(20)]
        public string YearLevel { get; set; } = string.Empty;

        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must be 10 digits starting with 0.")]
        public string? Phone { get; set; }
    }
}
