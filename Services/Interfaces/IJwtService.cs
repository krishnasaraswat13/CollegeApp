using CollegeApp.Models;

namespace CollegeApp.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(AppUser user);
    }
}
