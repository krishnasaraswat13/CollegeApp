using System.ComponentModel.DataAnnotations; // Imports validation attributes.

namespace CollegeApp.Dtos // Namespace for DTO classes.
{
    // DTO used when a user logs in.
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty; // Username entered during login.

        [Required]
        public string Password { get; set; } = string.Empty; // Password entered during login.
    }
}
