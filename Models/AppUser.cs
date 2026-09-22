using System.ComponentModel.DataAnnotations; // Imports validation attributes.

namespace CollegeApp.Models // Namespace for models.
{
    // This class represents users who can register and login into the system.
    public class AppUser
    {
        public int Id { get; set; } // Primary key.

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty; // Stores user full name.

        [Required]
        [StringLength(100)]
        public string UserName { get; set; } = string.Empty; // Stores login username.

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; // Stores email.

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // Stores hashed password.

        [Required]
        public string Role { get; set; } = "User"; // Stores user role like Admin or User.
    }
}
