using System.ComponentModel.DataAnnotations.Schema;

namespace webApi_labs.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Phone { get; set; }

        //forigenKeys
        [ForeignKey("Department")]
        public int DeptId { get; set; }

        //Navigator
        virtual public Department Department{ get; set; } 
    }
}
