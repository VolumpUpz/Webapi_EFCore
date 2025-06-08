using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeService employeeService, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
            _mapper = mapper;
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
            var employee = await _employeeService.GetByIdAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        //// READ by id
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> GetEmployee(int id)
        //{
        //    var employee = await _context.Employees
        //        .Include(e => e.EmployeeDetails)
        //        .Include(e => e.EmployeeProjects)
        //            .ThenInclude(ep => ep.Project)
        //        .FirstOrDefaultAsync(e => e.EmployeeId == id);

        //    if (employee == null)
        //        return NotFound();

        //    return Ok(employee);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateDto dto)
        {
            if (!ModelState.IsValid) //attribute validation
            {
                return BadRequest(ModelState);
            }

            var employee = _mapper.Map<Employee>(dto);

            //var employee = new Employee
            //{
            //    FirstName = dto.FirstName,
            //    LastName = dto.LastName,
            //    Salary = dto.Salary,
            //    ManagerId = dto.ManagerId,
            //    EmployeeDetails = new EmployeeDetails
            //    {
            //        Address = dto.EmployeeDetails.Address,
            //        PhoneNumber = dto.EmployeeDetails.PhoneNumber,
            //        Email = dto.EmployeeDetails.Email
            //    },
            //    EmployeeProjects = dto.ProjectIds.Select(pid => new EmployeeProject
            //    {
            //        ProjectId = pid
            //    }).ToList()
            //};

            var created = await _employeeRepository.CreateAsync(employee);
            var map_created = _mapper.Map<EmployeeDTO>(created);
            return CreatedAtAction(nameof(GetById), new { id = created.EmployeeId }, map_created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ต้องเป็น Entity!
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
                return NotFound();

            _mapper.Map(dto, existingEmployee);

            //// จัดการ EmployeeProjects ใหม่
            //existingEmployee.EmployeeProjects = dto.ProjectIds
            //    .Select(pid => new EmployeeProject { ProjectId = pid, EmployeeId = id })
            //    .ToList();

            await _employeeRepository.UpdateAsync(existingEmployee);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] EmployeePatchDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            // Patch field ทีละอัน
            if (dto.FirstName != null) employee.FirstName = dto.FirstName;
            if (dto.LastName != null) employee.LastName = dto.LastName;
            if (dto.Salary.HasValue) employee.Salary = dto.Salary.Value;
            if (dto.ManagerId.HasValue) employee.ManagerId = dto.ManagerId.Value;

            if (dto.EmployeeDetails != null)
            {
                employee.EmployeeDetails ??= new EmployeeDetails();
                employee.EmployeeDetails.Address = dto.EmployeeDetails.Address;
                employee.EmployeeDetails.PhoneNumber = dto.EmployeeDetails.PhoneNumber;
                employee.EmployeeDetails.Email = dto.EmployeeDetails.Email;
            }

            if (dto.ProjectIds != null)
            {
                employee.EmployeeProjects = dto.ProjectIds
                    .Select(pid => new EmployeeProject { ProjectId = pid, EmployeeId = id })
                    .ToList();
            }

            await _employeeRepository.UpdateAsync(employee);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if(employee != null)
            {
                await _employeeRepository.DeleteAsync(employee);
                return NoContent();
            }
            return NotFound(new{
                message = "Delete unsuccessful"
            });
        }

    }
}
