namespace Webapi_EFCore.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
