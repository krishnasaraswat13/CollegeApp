using CollegeApp.Models; // Imports the Department model.
using CollegeApp.Repositories.Interfaces; // Imports the repository interface.
using CollegeApp.Services.Interfaces; // Imports the service interface.

namespace CollegeApp.Services // Namespace for service implementations.
{
    // This class contains business logic for Department.
    public class DepartmentService : IDepartmentService
    {
        // Private readonly field to store the repository dependency.
        private readonly IDepartmentRepository _departmentRepository;

        // Constructor injection: repository is provided automatically by dependency injection.
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository; // Saves repository into private field.
        }

        // Returns all departments by calling repository.
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _departmentRepository.GetAllAsync(); // Delegates work to repository.
        }

        // Returns one department by ID.
        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _departmentRepository.GetByIdAsync(id); // Calls repository method.
        }

        // Adds a new department.
        public async Task AddAsync(Department department)
        {
            await _departmentRepository.AddAsync(department); // Calls repository add method.
        }

        // Updates an existing department.
        public async Task UpdateAsync(Department department)
        {
            await _departmentRepository.UpdateAsync(department); // Calls repository update method.
        }

        // Deletes a department by ID.
        public async Task DeleteAsync(int id)
        {
            await _departmentRepository.DeleteAsync(id); // Calls repository delete method.
        }
    }
}
