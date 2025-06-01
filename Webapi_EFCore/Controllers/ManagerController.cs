using Microsoft.AspNetCore.Mvc;
using Webapi_EFCore.Data;
using Webapi_EFCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Webapi_EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ManagerController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET api/<ManagerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            //Manager manager = new Manager
            //{
            //    FirstName = "benz",
            //    LastName = "Meesilp"
            //};

            //_db.Managers.Add(manager);
            //_db.SaveChanges();

            Employee employee = new Employee
            {
                FirstName = "benz_emp",
                LastName = "benz_last_emp", 
                Salary = 50000,
                ManagerId = 1,
            };

            _db.Employees.Add(employee);
            _db.SaveChanges();
            return "value";
        }

        // POST api/<ManagerController>
        [HttpPost]
        public void Post()
        {

        }


    }
}
