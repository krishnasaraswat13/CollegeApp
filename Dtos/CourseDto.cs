namespace CollegeApp.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; } // Course ID
        public string Name { get; set; } = string.Empty; // Course name
        public int DurationInYears { get; set; } // Course duration
        public int DepartmentId { get; set; } // Related department ID
        public string DepartmentName { get; set; } = string.Empty; // Related department name
    }
}
