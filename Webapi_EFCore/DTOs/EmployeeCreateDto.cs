namespace Webapi_EFCore.DTOs
{
    public class EmployeeCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Salary { get; set; }
        public int ManagerId { get; set; }

        public EmployeeDetailsDto EmployeeDetails { get; set; }
        public List<int> ProjectIds { get; set; } = new();
    }
}
