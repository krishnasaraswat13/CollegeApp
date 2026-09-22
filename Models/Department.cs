using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class Department
    {
        public int Id { get; set; } // Primary key

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; // Department name
    }
}
