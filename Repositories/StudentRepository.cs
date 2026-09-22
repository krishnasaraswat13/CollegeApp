using CollegeApp.Data;
using CollegeApp.Models;
using CollegeApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace CollegeApp.Repositories // Namespace for repository implementations.
{
    // This class handles all database operations related to Student.
    public class StudentRepository : IStudentRepository
    {
        // Private field to store database context.
        private readonly ApplicationDbContext _context;

        // Constructor injection for database context.
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context; // Saves the injected context.
        }

        // Returns all students.
        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students // Reads from Students table.
                .Include(s => s.Department) // Loads related Department data.
                .Include(s => s.Course) // Loads related Course data.
                .OrderBy(s => s.FirstName) // Sorts by first name.
                .ThenBy(s => s.LastName) // Then sorts by last name.
                .ToListAsync(); // Executes query asynchronously.
        }

        // Returns one student by ID.
        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students // Reads from Students table.
                .Include(s => s.Department) // Includes Department relation.
                .Include(s => s.Course) // Includes Course relation.
                .FirstOrDefaultAsync(s => s.Id == id); // Returns matching student or null.
        }

        // Adds a new student.
        public async Task AddAsync(Student student)
        {
            _context.Students.Add(student); // Marks student for insertion.
            await _context.SaveChangesAsync(); // Saves insert operation.
        }

        // Updates an existing student.
        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student); // Marks student as modified.
            await _context.SaveChangesAsync(); // Saves update operation.
        }

        // Deletes a student by ID.
        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id); // Finds student by primary key.

            if (student != null) // Checks if student exists.
            {
                _context.Students.Remove(student); // Marks student for deletion.
                await _context.SaveChangesAsync(); // Saves delete operation.
            }
        }
    }
}
