using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApp.Models
{
    public class Student
    {
        public int Id { get; set; } // Primary key

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime AdmissionDate { get; set; }

        public int DepartmentId { get; set; } // FK only
        public int CourseId { get; set; } // FK only

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; } // Optional single navigation

        [ForeignKey("CourseId")]
        public Course? Course { get; set; } // Optional single navigation
    }
}
