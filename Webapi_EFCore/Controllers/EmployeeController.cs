using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Webapi_EFCore.DTOs;
using Webapi_EFCore.Models;
using Webapi_EFCore.Repositories.Interfaces;
using Webapi_EFCore.Services.Interfaces;

namespace Webapi_EFCore.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employee = await _employeeService.GetAllAsync();
            return Ok(employee);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = _employeeRepository.GetByIdAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Salary = dto.Salary,
                ManagerId = dto.ManagerId,
                EmployeeDetails = new EmployeeDetails
                {
                    Address = dto.EmployeeDetails.Address,
                    PhoneNumber = dto.EmployeeDetails.PhoneNumber,
                    Email = dto.EmployeeDetails.Email
                },
                EmployeeProjects = dto.ProjectIds.Select(pid => new EmployeeProject
                {
                    ProjectId = pid
                }).ToList()
            };

            var created = await _employeeRepository.CreateAsync(employee);
            return CreatedAtAction(nameof(GetById), new { id = created.EmployeeId }, created);
        }
    }
}
