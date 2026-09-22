using CollegeApp.Models; // Imports the Student model.

namespace CollegeApp.Repositories.Interfaces // Namespace for repository interfaces.
{
    // This interface defines all database operations related to Student.
    public interface IStudentRepository
    {
        // Returns all student records from database.
        Task<IEnumerable<Student>> GetAllAsync();

        // Returns one student by ID, or null if not found.
        Task<Student?> GetByIdAsync(int id);

        // Adds a new student to database.
        Task AddAsync(Student student);

        // Updates an existing student in database.
        Task UpdateAsync(Student student);

        // Deletes a student by ID.
        Task DeleteAsync(int id);
    }
}
