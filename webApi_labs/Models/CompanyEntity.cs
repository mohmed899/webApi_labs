using Microsoft.EntityFrameworkCore;

namespace webApi_labs.Models
{
    public class CompanyEntity: DbContext
    {
        public CompanyEntity()
        {
        }
        public CompanyEntity(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
