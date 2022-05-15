using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi_labs.Models;
using webApi_labs.repo;

namespace webApi_labs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo employeeRepo;

        public EmployeeController(IEmployeeRepo employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }
        //[HttpGet]
        //public IActionResult getAll()
        //{
        //    var Employees = employeeRepo.GetAll();
        //    return Ok(Employees);
        //}

        [HttpGet("{id:int}" ,Name ="getById")]
        public IActionResult getByID(int id)
        {
            var Employee = employeeRepo.GetByID(id);
            return Ok(Employee);
        }

        [HttpPost]
        public IActionResult create( [FromBody] Employee employee)
        {
            if (ModelState.IsValid == true)
            {

                var id = employeeRepo.insert(employee);
                if (id != -1)
                {
                    string url = Url.Link("getById", new { id = id });
                    return Created(url, employee);

                }
                else
                   return BadRequest("not well formated");

            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id:int}")]
        public IActionResult update([FromRoute] int id,[FromBody] Employee Newemp)
        {
            if (ModelState.IsValid == true)
            {
                int rowEffected = employeeRepo.Update(id, Newemp);
                if (rowEffected > 0)
                    return Ok(employeeRepo.GetByID(id));
                else
                    return StatusCode(204, employeeRepo.GetByID(id));
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public IActionResult delete(int id)
        {
            var rowEffected = employeeRepo.delete(id);
            if(rowEffected > 0)
                return StatusCode(204, "Record Remove Success");
            else
                return BadRequest("Id Not Found");
        }



        //query string

        [HttpGet]
        public IActionResult getQueryString(string name)
        {
            return Ok(name);
        }
    }
}
