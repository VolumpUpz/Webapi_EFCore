using Microsoft.EntityFrameworkCore;
using Webapi_EFCore.Data;
using Webapi_EFCore.Models;
using Webapi_EFCore.Repositories.Interfaces;

namespace Webapi_EFCore.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            var t = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            return t;

        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
    }
}
