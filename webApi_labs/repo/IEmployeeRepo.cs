using System.Collections.Generic;
using webApi_labs.Models;

namespace webApi_labs.repo
{
    public interface IEmployeeRepo
    {
        int delete(int id);
        List<Employee> GetAll();
        Employee GetByID(int id);
        int insert(Employee emp);
        int Update(int id, Employee newEmp);
    }
}