using CollegeApp.Models; // Imports the Course model.
using CollegeApp.Repositories.Interfaces; // Imports repository interface.
using CollegeApp.Services.Interfaces; // Imports service interface.

namespace CollegeApp.Services // Namespace for service implementations.
{
    // This class contains business logic for Course.
    public class CourseService : ICourseService
    {
        // Private readonly field to store course repository.
        private readonly ICourseRepository _courseRepository;

        // Constructor injection for course repository.
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository; // Saves injected repository.
        }

        // Returns all courses.
        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _courseRepository.GetAllAsync(); // Calls repository method.
        }

        // Returns one course by ID.
        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _courseRepository.GetByIdAsync(id); // Calls repository method.
        }

        // Adds a new course.
        public async Task AddAsync(Course course)
        {
            await _courseRepository.AddAsync(course); // Calls repository add method.
        }

        // Updates an existing course.
        public async Task UpdateAsync(Course course)
        {
            await _courseRepository.UpdateAsync(course); // Calls repository update method.
        }

        // Deletes a course by ID.
        public async Task DeleteAsync(int id)
        {
            await _courseRepository.DeleteAsync(id); // Calls repository delete method.
        }
    }
}
