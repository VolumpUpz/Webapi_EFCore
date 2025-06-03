using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public Manager Manager { get; set; }


        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}
