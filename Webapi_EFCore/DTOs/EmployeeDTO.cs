namespace Webapi_EFCore.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Salary { get; set; }
        public int ManagerId { get; set; }
    }
}
