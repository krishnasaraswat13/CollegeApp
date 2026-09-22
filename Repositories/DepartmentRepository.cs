using CollegeApp.Data; // Imports ApplicationDbContext for database access.
using CollegeApp.Models; // Imports Department model.
using CollegeApp.Repositories.Interfaces; // Imports repository interface.
using Microsoft.EntityFrameworkCore; // Imports EF Core async methods like ToListAsync and FirstOrDefaultAsync.

namespace CollegeApp.Repositories // Namespace for repository implementations.
{
    // This class implements all actual database logic for Department.
    public class DepartmentRepository : IDepartmentRepository
    {
        // Private field to hold the database context object.
        private readonly ApplicationDbContext _context;

        // Constructor injection: ApplicationDbContext will be provided automatically by ASP.NET Core.
        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context; // Stores the injected DbContext into the private field.
        }

        // Fetches all departments from the database.
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments // Reads data from Departments table.
                .OrderBy(d => d.Name) // Sorts departments by name.
                .ToListAsync(); // Executes query and returns list asynchronously.
        }

        // Fetches one department by ID.
        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments // Reads data from Departments table.
                .FirstOrDefaultAsync(d => d.Id == id); // Returns the first matching record or null.
        }

        // Adds a new department to database.
        public async Task AddAsync(Department department)
        {
            _context.Departments.Add(department); // Marks department to be inserted.
            await _context.SaveChangesAsync(); // Saves changes to database.
        }

        // Updates an existing department in database.
        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department); // Marks entity as modified.
            await _context.SaveChangesAsync(); // Saves changes to database.
        }

        // Deletes a department by ID.
        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id); // Finds department by primary key.

            if (department != null) // Checks if department exists.
            {
                _context.Departments.Remove(department); // Marks entity for deletion.
                await _context.SaveChangesAsync(); // Saves delete operation.
            }
        }
    }
}
