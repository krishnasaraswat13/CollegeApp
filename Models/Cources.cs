using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApp.Models
{
    public class Course
    {
        public int Id { get; set; } // Primary key

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; // Course name

        [Required]
        public int DurationInYears { get; set; } // Duration

        public int DepartmentId { get; set; } // FK only

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; } // Optional single navigation
    }
}
