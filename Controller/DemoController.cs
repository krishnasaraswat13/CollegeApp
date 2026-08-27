using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class DemoController : ControllerBase
    {
        //1. Stronglt coupled/tightly coupled
        //private readonly IMyLogger _myLogger;
        //public DemoController()
        //{
        //    _myLogger = new LogToFile();  //here you can see if we want to shift from log to file to any other service we need to change it here and similarly there are multiple controllers so then we  need t o change it in every controller so it is tightly coupled

        //}
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    _myLogger.Log("Index method started");
        //    return Ok();
        }

        //2. Loosely coupled
         private readonly IMyLogger _myLogger;
        public DemoController(IMyLogger myLogger)     //here we can take that in parameter     //we are writing logic in program.cs
        {
            _myLogger = myLogger;
        }
        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.Log("Index method started");
            return Ok();

        }
}
