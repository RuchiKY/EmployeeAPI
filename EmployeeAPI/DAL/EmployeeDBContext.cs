using EmployeeAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.DAL
{
    public class EmployeeDBContext : IdentityDbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public EmployeeDBContext()
        { }

        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
        {

        }
    }
}
