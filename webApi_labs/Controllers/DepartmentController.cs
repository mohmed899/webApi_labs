using webApi_labs.DTO;
using Microsoft.AspNetCore.Mvc;
using webApi_labs.repo;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace webApi_labs.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        IDepartmentRepo repo;
        public DepartmentController(IDepartmentRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult getAll()
        {

            List<DepartmentWithEmpListDTO> deptList = new List<DepartmentWithEmpListDTO>();
            foreach (var departmet in repo.getAll())
            {
                DepartmentWithEmpListDTO deptDTO = new DepartmentWithEmpListDTO()
                {
                    ID = departmet.Id,
                    manager = departmet.Manager,
                    name = departmet.Name,
                };
                foreach (var emp in departmet.Employees)
                {
                    deptDTO.employeesName.Add(emp.Name);
                }
                deptList.Add(deptDTO);

            }

            return Ok(deptList);
        }

        [HttpGet("{id:int}")]
        public IActionResult getById([FromRoute]int id)
        {
            var department = repo.getById(id);
            if(department != null)
            {
                DepartmentWithEmpListDTO deptWithEmpName = new DepartmentWithEmpListDTO()
                {
                    ID = department.Id,
                    name = department.Name,
                    manager = department.Manager,
                };

                foreach(var emp in department.Employees)
                {
                    deptWithEmpName.employeesName.Add(emp.Name);
                }
                return Ok(deptWithEmpName);

            }
            return BadRequest("wrong id");
        }

    }
}

