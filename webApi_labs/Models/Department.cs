using System.Collections.Generic;

namespace webApi_labs.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }

        virtual  public List<Employee> Employees { get; set; }
    }
}
