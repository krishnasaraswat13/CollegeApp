namespace CollegeApp.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; } // Student ID
        public string FirstName { get; set; } = string.Empty; // First name
        public string LastName { get; set; } = string.Empty; // Last name
        public string Email { get; set; } = string.Empty; // Email
        public string PhoneNumber { get; set; } = string.Empty; // Phone
        public string Gender { get; set; } = string.Empty; // Gender
        public string Address { get; set; } = string.Empty; // Address
        public DateTime DateOfBirth { get; set; } // DOB
        public DateTime AdmissionDate { get; set; } // Admission date
        public int DepartmentId { get; set; } // Department ID
        public string DepartmentName { get; set; } = string.Empty; // Department name
        public int CourseId { get; set; } // Course ID
        public string CourseName { get; set; } = string.Empty; // Course name
    }
}
