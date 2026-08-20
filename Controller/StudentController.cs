using CollegeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CollegeApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]                            //this     In an API Controller, validation usually works like this:

                                                        //Client sends data to API
                                                        //API maps request data to a model/DTO
                                                        //Validation rules are checked
                                                        //If validation fails, API returns an error response, usually 400 Bad Request
                                                        //If validation passes, controller continues to business logic
    public class StudentController : ControllerBase

    {
        [HttpGet]
        [Route("All",Name= "GetAllStudents")]     //method 1
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()                  //Action result means we are returning the results status code   ActionResult<>
        {
            //var students = new List<StudentDTO>();                                         //dto works with the business logic layer so instead of returning the student model we are returning the dto 
            //foreach(var item in CollegeRepository.Students)
            //{
            //    StudentDTO obj = new StudentDTO()
            //    {
            //        Id = item.Id,
            //        StudentName = item.StudentName,
            //        Address = item.Address,
            //        Email = item.Email
            //    };
            //    students.Add(obj);

            //}


            var students = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Address = s.Address,
                Email = s.Email
            });

            //Ok-200-Success
            return Ok(students);
        }
        [HttpGet]
        [Route("{id:min(1):max(100)}", Name = "GetStudentById")]   //or {id:int}  
        [ProducesResponseType(StatusCodes.Status200OK)]   ///if remembering status code is difficult then there are predefined status codes
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(200,Type = typeof(Student))]   //this is how we document responses     //when we are defining the return type as ActionResult<Student> now we do not require to tell type on there i.e.<Student> we are defining the type here
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]

        //these three responses are undocumented so we use ProduceResponseType so that user also able to see those

        public ActionResult<StudentDTO> GetStudentById(int id)          //if we use typeOf() then no require to define type here
        {
            //BadRequest-400- ClientError
            if (id <= 0)                                                                //similarly we can make these conditions for name constraint
                return BadRequest();

            //404 Not Found
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound($"The student with id {id} not found");
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address

            };

            //Ok
            return Ok(studentDTO);
        }


        [HttpGet("{name:alpha}",Name="GetStudentByName")]    //method 2    //here name type written is alpha bcz if we write string but there is no string type supported in routes so we use alphabetical type

        //[HttpGet]
        //[Route("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]   ///if remembering status code is difficult then there are predefined status codes
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            //BadRequest-400- ClientError
            if (string.IsNullOrEmpty(name))                                                             
                return BadRequest();

            //404 Not Found
            var student = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
            if (student == null)
                return NotFound($"The student with name {name} not found");
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address

            };
            //Ok
            return Ok(studentDTO);
        }

        [HttpPost]
        [Route("Create",Name ="CreateStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]   ///status code for created 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model) {
            //if (!ModelState.IsValid)    //this is used to validate when we are not using [ApiController]
            //    return BadRequest(ModelState);

            if (model == null)
            {
                return BadRequest();

            }
            if (model.AdmissionDate < DateTime.Now)
            {
                ////1.directly adding error message to modelstate
                //ModelState.AddModelError("Admission date error", "Admission date must be greater than or equal to todays date");
                // return BadRequest(ModelState);

                //2. using custom attribute



            }
            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student
            {
                Id = newId,
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email
            };
            CollegeRepository.Students.Add(student);
            model.Id = student.Id;

            //status-201-created
            //https://localhost:7185/api/Student/newId 

            return CreatedAtRoute("GetStudentById",new {id=model.Id},model); //there are particular routes for particular student so when a new student is created we are also defining the routee by this method 
        }

        [HttpDelete]
        [Route("{id:int}",Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]   ///if remembering status code is difficult then there are predefined status codes
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeletetStudentById(int id)
        {
            //BadRequest-400- ClientError
            if (id <= 0)                                                                 
                return BadRequest();

            //404 Not Found
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound($"The student with id {id} not found");

            //Ok
            CollegeRepository.Students.Remove(student);
            return Ok(true);

        }
    }
}
