﻿using Webapi_EFCore.DTOs;
using Webapi_EFCore.Models;

namespace Webapi_EFCore.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);     

        Task<Employee> CreateAsync(Employee employee);

        Task UpdateAsync(Employee employee);

        Task DeleteAsync(Employee employee);

    }
}
