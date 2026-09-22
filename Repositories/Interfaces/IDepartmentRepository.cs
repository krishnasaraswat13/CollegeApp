using CollegeApp.Models; // Imports the Department model.

namespace CollegeApp.Repositories.Interfaces // Namespace for repository interfaces.
{
    // This interface defines all database operations related to Department.
    public interface IDepartmentRepository
    {
        // Returns all department records from database.
        Task<IEnumerable<Department>> GetAllAsync();

        // Returns one department by its ID, or null if not found.
        Task<Department?> GetByIdAsync(int id);

        // Adds a new department to database.
        Task AddAsync(Department department);

        // Updates an existing department in database.
        Task UpdateAsync(Department department);

        // Deletes a department by ID.
        Task DeleteAsync(int id);
    }
}
