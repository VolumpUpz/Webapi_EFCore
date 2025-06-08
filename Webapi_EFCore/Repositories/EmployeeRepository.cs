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
            return await _context.Employees
                .Include(e => e.EmployeeDetails)
                .Include(e => e.EmployeeProjects)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            var t = await _context.Employees
                .Include(e => e.EmployeeDetails)
                .Include(e => e.EmployeeProjects)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
            return t;

        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            //ทำการ tracking ให้ ef ถ้าไมได้ get ข้อมูลจาก db
           //_context.Employees.Attach(employee);
           //_context.Entry(employee).State = EntityState.Modified;
             

            //ef  track ให้แล้วเพราะ get employee จาก db
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
