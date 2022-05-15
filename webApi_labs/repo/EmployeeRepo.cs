using System.Collections.Generic;
using System.Linq;
using webApi_labs.Models;

namespace webApi_labs.repo
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private CompanyEntity db;

        public EmployeeRepo(CompanyEntity company)
        {
            this.db = company;
        }
        public List<Employee> GetAll()
        {
            return db.Employees.ToList();
        }

        public Employee GetByID(int id)
        {
            return db.Employees.SingleOrDefault(emp => emp.Id == id);
        }

        public int insert(Employee emp)
        {
            db.Employees.Add(emp);
            try { 
                db.SaveChanges();
                return emp.Id;
            }
            catch {
                return -1;
            }
        }


        public int Update(int id, Employee newEmp)
        {
           
            try
            {
                var oldEmp = db.Employees.SingleOrDefault(emp => emp.Id == id);
                if (oldEmp != null)
                {
                    oldEmp.Name = newEmp.Name;
                    oldEmp.Salary = newEmp.Salary;
                    oldEmp.Phone = newEmp.Phone;
                }
                return  db.SaveChanges();
            }
            catch (System.Exception)
            {

                return -1;
            }

        }


        public int delete(int id)
        {
            
            try
            {
                var oldEmp = db.Employees.SingleOrDefault(emp => emp.Id == id);
                if (oldEmp != null)
                {
                    db.Employees.Remove(oldEmp);
                    return db.SaveChanges();
                }
                else
                    return -1;

            }
            catch (System.Exception)
            {

                return -1;

            }
        }


    }
}
