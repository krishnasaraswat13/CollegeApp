using CollegeApp.Dtos;
using CollegeApp.Models;
using CollegeApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();

            var result = courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Name = c.Name,
                DurationInYears = c.DurationInYears,
                DepartmentId = c.DepartmentId,
                DepartmentName = c.Department != null ? c.Department.Name : string.Empty
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound(new { message = "Course not found." });
            }

            var result = new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                DurationInYears = course.DurationInYears,
                DepartmentId = course.DepartmentId,
                DepartmentName = course.Department != null ? course.Department.Name : string.Empty
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _courseService.AddAsync(course);

            return Ok(new { message = "Course created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Course course)
        {
            if (id != course.Id)
            {
                return BadRequest(new { message = "Route ID and body ID do not match." });
            }

            var existingCourse = await _courseService.GetByIdAsync(id);

            if (existingCourse == null)
            {
                return NotFound(new { message = "Course not found." });
            }

            await _courseService.UpdateAsync(course);

            return Ok(new { message = "Course updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingCourse = await _courseService.GetByIdAsync(id);

            if (existingCourse == null)
            {
                return NotFound(new { message = "Course not found." });
            }

            await _courseService.DeleteAsync(id);

            return Ok(new { message = "Course deleted successfully." });
        }
    }
}
