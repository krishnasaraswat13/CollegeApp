using CollegeApp.Models; // Imports the Student model.

namespace CollegeApp.Services.Interfaces // Namespace for service interfaces.
{
    // This interface defines all business operations related to Student.
    public interface IStudentService
    {
        // Returns all students.
        Task<IEnumerable<Student>> GetAllAsync();

        // Returns one student by ID, or null if not found.
        Task<Student?> GetByIdAsync(int id);

        // Adds a new student.
        Task AddAsync(Student student);

        // Updates an existing student.
        Task UpdateAsync(Student student);

        // Deletes a student by ID.
        Task DeleteAsync(int id);
    }
}
