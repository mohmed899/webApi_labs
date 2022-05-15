using System.Collections.Generic;
using webApi_labs.Models;

namespace webApi_labs.repo
{
    public interface IDepartmentRepo
    {
        List<Department> getAll();
        Department getById(int id);
    }
}