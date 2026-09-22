using CollegeApp.Data; // Imports the database context.
using CollegeApp.Models; // Imports the Course model.
using CollegeApp.Repositories.Interfaces; // Imports the repository interface.
using Microsoft.EntityFrameworkCore; // Imports EF Core methods like ToListAsync and FirstOrDefaultAsync.

namespace CollegeApp.Repositories // Defines namespace for repository classes.
{
    public class CourseRepository : ICourseRepository // Implements the course repository interface.
    {
        private readonly ApplicationDbContext _context; // Stores database context in a private field.

        public CourseRepository(ApplicationDbContext context) // Constructor gets DbContext through dependency injection.
        {
            _context = context; // Assigns injected context to private field.
        }

        public async Task<IEnumerable<Course>> GetAllAsync() // Returns all courses.
        {
            return await _context.Courses // Reads data from Courses table.
                .Include(c => c.Department) // Includes only Department data with Course.
                .AsNoTracking() // Improves read performance and avoids unnecessary tracking.
                .OrderBy(c => c.Name) // Sorts courses by name.
                .ToListAsync(); // Executes query asynchronously and returns list.
        }

        public async Task<Course?> GetByIdAsync(int id) // Returns one course by ID.
        {
            return await _context.Courses // Reads from Courses table.
                .Include(c => c.Department) // Includes related Department.
                .AsNoTracking() // Makes result read-only for better performance.
                .FirstOrDefaultAsync(c => c.Id == id); // Returns matching course or null.
        }

        public async Task AddAsync(Course course) // Adds a new course.
        {
            _context.Courses.Add(course); // Marks course entity for insertion.
            await _context.SaveChangesAsync(); // Saves changes to database.
        }

        public async Task UpdateAsync(Course course) // Updates an existing course.
        {
            _context.Courses.Update(course); // Marks entity as modified.
            await _context.SaveChangesAsync(); // Saves update to database.
        }

        public async Task DeleteAsync(int id) // Deletes course by ID.
        {
            var course = await _context.Courses.FindAsync(id); // Finds course by primary key.

            if (course != null) // Checks if course exists.
            {
                _context.Courses.Remove(course); // Marks course for deletion.
                await _context.SaveChangesAsync(); // Saves delete operation.
            }
        }
    }
}
