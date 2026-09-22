using CollegeApp.Dtos;
using CollegeApp.Models;
using CollegeApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllAsync();

            var result = departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound(new { message = "Department not found." });
            }

            var result = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _departmentService.AddAsync(department);

            return Ok(new { message = "Department created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Department department)
        {
            if (id != department.Id)
            {
                return BadRequest(new { message = "Route ID and body ID do not match." });
            }

            var existingDepartment = await _departmentService.GetByIdAsync(id);

            if (existingDepartment == null)
            {
                return NotFound(new { message = "Department not found." });
            }

            await _departmentService.UpdateAsync(department);

            return Ok(new { message = "Department updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingDepartment = await _departmentService.GetByIdAsync(id);

            if (existingDepartment == null)
            {
                return NotFound(new { message = "Department not found." });
            }

            await _departmentService.DeleteAsync(id);

            return Ok(new { message = "Department deleted successfully." });
        }
    }
}
