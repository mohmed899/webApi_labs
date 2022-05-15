using System.Collections.Generic;
using webApi_labs.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webApi_labs.repo
{
    public class DepartmentRepo : IDepartmentRepo
    {
        CompanyEntity db;
        public DepartmentRepo(CompanyEntity db)
        {
            this.db = db;
        }
        public List<Department> getAll()
        {
            return db.Departments.Include(d=>d.Employees).ToList();
        }

        public Department getById(int id)
        {
            return db.Departments.Include(d => d.Employees).FirstOrDefault(d => d.Id == id);
        }
    }
}
