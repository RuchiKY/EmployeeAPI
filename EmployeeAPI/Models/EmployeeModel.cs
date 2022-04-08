using EmployeeAPI.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Models
{
    public class EmployeeModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Department { get; set; }


        public Employee BindModel()
        {
            Employee emp = new();
            emp.FirstName = FirstName;
            emp.LastName = LastName;
            emp.Department = Department;
            emp.Role = Role;
            emp.CreatedAt = DateTime.Now;
            return emp;
        }
    }
}
