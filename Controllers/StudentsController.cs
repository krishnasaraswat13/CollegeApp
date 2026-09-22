using CollegeApp.Dtos;
using CollegeApp.Models;
using CollegeApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



namespace CollegeApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllAsync();

            var result = students.Select(s => new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Gender = s.Gender,
                Address = s.Address,
                DateOfBirth = s.DateOfBirth,
                AdmissionDate = s.AdmissionDate,
                DepartmentId = s.DepartmentId,
                DepartmentName = s.Department != null ? s.Department.Name : string.Empty,
                CourseId = s.CourseId,
                CourseName = s.Course != null ? s.Course.Name : string.Empty
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var s = await _studentService.GetByIdAsync(id);

            if (s == null)
            {
                return NotFound(new { message = "Student not found." });
            }

            var result = new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Gender = s.Gender,
                Address = s.Address,
                DateOfBirth = s.DateOfBirth,
                AdmissionDate = s.AdmissionDate,
                DepartmentId = s.DepartmentId,
                DepartmentName = s.Department != null ? s.Department.Name : string.Empty,
                CourseId = s.CourseId,
                CourseName = s.Course != null ? s.Course.Name : string.Empty
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _studentService.AddAsync(student);

            return Ok(new { message = "Student created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student student)
        {
            if (id != student.Id)
            {
                return BadRequest(new { message = "Route ID and body ID do not match." });
            }

            var existingStudent = await _studentService.GetByIdAsync(id);

            if (existingStudent == null)
            {
                return NotFound(new { message = "Student not found." });
            }

            await _studentService.UpdateAsync(student);

            return Ok(new { message = "Student updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingStudent = await _studentService.GetByIdAsync(id);

            if (existingStudent == null)
            {
                return NotFound(new { message = "Student not found." });
            }

            await _studentService.DeleteAsync(id);

            return Ok(new { message = "Student deleted successfully." });
        }
    }
}
