using CollegeApp.Models; // Imports the Department model.

namespace CollegeApp.Services.Interfaces // Namespace for service interfaces.
{
    // This interface defines all business operations related to Department.
    public interface IDepartmentService
    {
        // Returns all departments.
        Task<IEnumerable<Department>> GetAllAsync();

        // Returns one department by ID, or null if not found.
        Task<Department?> GetByIdAsync(int id);

        // Adds a new department.
        Task AddAsync(Department department);

        // Updates an existing department.
        Task UpdateAsync(Department department);

        // Deletes a department by ID.
        Task DeleteAsync(int id);
    }
}
