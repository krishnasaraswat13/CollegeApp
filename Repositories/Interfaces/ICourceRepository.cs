using CollegeApp.Models; // Imports the Course model.

namespace CollegeApp.Repositories.Interfaces // Namespace for repository interfaces.
{
    // This interface defines all database operations related to Course.
    public interface ICourseRepository
    {
        // Returns all course records from database.
        Task<IEnumerable<Course>> GetAllAsync();

        // Returns one course by ID, or null if not found.
        Task<Course?> GetByIdAsync(int id);

        // Adds a new course to database.
        Task AddAsync(Course course);

        // Updates an existing course in database.
        Task UpdateAsync(Course course);

        // Deletes a course by ID.
        Task DeleteAsync(int id);
    }
}
