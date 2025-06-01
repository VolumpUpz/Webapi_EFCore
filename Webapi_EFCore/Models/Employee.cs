using System.ComponentModel.DataAnnotations;

namespace Webapi_EFCore.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Salary { get; set; }

        public EmployeeDetails EmployeeDetails { get; set; }

        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

        public virtual ICollection<Project> Projects { get; set; } // virtual => Lazy Loading

        public Employee()
        {
            Projects = new List<Project>();
        }
    }
}
