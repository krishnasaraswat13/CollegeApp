using CollegeApp.Models; // Imports the Course model.

namespace CollegeApp.Services.Interfaces // Namespace for service interfaces.
{
    // This interface defines all business operations related to Course.
    public interface ICourseService
    {
        // Returns all courses.
        Task<IEnumerable<Course>> GetAllAsync();

        // Returns one course by ID, or null if not found.
        Task<Course?> GetByIdAsync(int id);

        // Adds a new course.
        Task AddAsync(Course course);

        // Updates an existing course.
        Task UpdateAsync(Course course);

        // Deletes a course by ID.
        Task DeleteAsync(int id);
    }
}
