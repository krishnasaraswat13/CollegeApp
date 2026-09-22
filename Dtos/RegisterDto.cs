using System.ComponentModel.DataAnnotations; // Imports validation attributes.

namespace CollegeApp.Dtos // Namespace for DTO classes.
{
    // DTO used when a new user registers.
    public class RegisterDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty; // User full name.

        [Required]
        public string UserName { get; set; } = string.Empty; // User login name.

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; // User email.

        [Required]
        public string Password { get; set; } = string.Empty; // Plain password from user input.

        public string Role { get; set; } = "User"; // Optional role, default is User.
    }
}
