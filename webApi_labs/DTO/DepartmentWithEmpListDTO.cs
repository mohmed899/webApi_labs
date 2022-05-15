using webApi_labs.Models;
using System.Collections.Generic;
namespace webApi_labs.DTO
{
    public class DepartmentWithEmpListDTO
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string manager { get; set; }
        public List<string> employeesName { get; set; }=new List<string>();

    }
}
