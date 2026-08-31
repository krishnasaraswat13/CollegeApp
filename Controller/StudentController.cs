using CollegeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CollegeApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]                            //this     In an API Controller, validation usually works like this:
    [Produces("application/json") ]              //Client sends data to API
    //API maps request data to a model/DTO
    //Validation rules are checked
    //If validation fails, API returns an error response, usually 400 Bad Request
    //If validation passes, controller continues to business logic
    public class StudentController : ControllerBase

    {

        private readonly ILogger<StudentController> _logger;
        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }


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
            _logger.LogInformation("GetStudents method started");

            var students = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Address = s.Address,
                Email = s.Email,
                Age = s.Age,
                AdmissionDate = s.AdmissionDate

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
            if (id <= 0) //similarly we can make these conditions for name constraint
            { 
                _logger.LogWarning("bad Request");    //Built in loggers(Inbuilt logger) this logger does not require any additional setup //Console
                
                return BadRequest();
            }
            //404 Not Found
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
            {
                _logger.LogError("Student not found with given id");
                return NotFound($"The student with id {id} not found");
            }

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address,
                Age = student.Age,
                AdmissionDate = student.AdmissionDate

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
                Address = student.Address,
                Age= student.Age,
                AdmissionDate= student.AdmissionDate

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

        public ActionResult CreateStudent([FromBody] StudentDTO model) {
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
                //[DateCheck] in studentdto 
            }
            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student
            {
                Id = newId,
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email,
                Age=model.Age,
                AdmissionDate=model.AdmissionDate
            };
            CollegeRepository.Students.Add(student);
            model.Id = student.Id;

            //status-201-created
            //https://localhost:7185/api/Student/newId 

            return CreatedAtRoute("GetStudentById",new {id=model.Id},model); //there are particular routes for particular student so when a new student is created we are also defining the routee by this method 
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]   ///status code for created  i mean for no response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> UpdateStudent([FromBody] StudentDTO model)
        {
            if(model==null|| model.Id <= 0)
                return BadRequest();

       var existingStudent = CollegeRepository.Students.Where(s => s.Id == model.Id).FirstOrDefault();
            if (existingStudent == null)
            {
                return NotFound();

            }
                    existingStudent.StudentName= model.StudentName;
                    existingStudent.Address= model.Address;
                    existingStudent.Email=model.Email;
                    existingStudent.Age=model.Age;


                   // return Ok(existingStudent);//
                    return NoContent();
                
            }


        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]   ///status code for created 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult UpdateStudentPartial(int id,[FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {

            if (patchDocument == null || id <= 0)
                return BadRequest();

            var existingStudent = CollegeRepository.Students.Where(s => s.Id == id).FirstOrDefault();
            if (existingStudent == null)
            {
                return NotFound();

            }
            var studentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Address = existingStudent.Address,
                Email = existingStudent.Email,
                Age = existingStudent.Age
            };
            patchDocument.ApplyTo(studentDTO);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Address = studentDTO.Address;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Age = studentDTO.Age;


            // return Ok(existingStudent);//
            return NoContent();
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
