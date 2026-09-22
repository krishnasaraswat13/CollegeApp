using CollegeApp.Models; // Imports the Student model.
using CollegeApp.Repositories.Interfaces; // Imports repository interface.
using CollegeApp.Services.Interfaces; // Imports service interface.

namespace CollegeApp.Services // Namespace for service implementations.
{
    // This class contains business logic for Student.
    public class StudentService : IStudentService
    {
        // Private readonly field to store student repository.
        private readonly IStudentRepository _studentRepository;

        // Constructor injection for student repository.
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository; // Saves injected repository.
        }

        // Returns all students.
        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _studentRepository.GetAllAsync(); // Calls repository method.
        }

        // Returns one student by ID.
        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id); // Calls repository method.
        }

        // Adds a new student.
        public async Task AddAsync(Student student)
        {
            await _studentRepository.AddAsync(student); // Calls repository add method.
        }

        // Updates an existing student.
        public async Task UpdateAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student); // Calls repository update method.
        }

        // Deletes a student by ID.
        public async Task DeleteAsync(int id)
        {
            await _studentRepository.DeleteAsync(id); // Calls repository delete method.
        }
    }
}
