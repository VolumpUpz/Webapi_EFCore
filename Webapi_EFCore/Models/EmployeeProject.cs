using System.Text.Json.Serialization;

namespace Webapi_EFCore.Models
{
    public class EmployeeProject
    {
        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }

        [JsonIgnore]
        public Employee Employee { get; set; }

        [JsonIgnore]
        public Project Project { get; set; }
    }
}
