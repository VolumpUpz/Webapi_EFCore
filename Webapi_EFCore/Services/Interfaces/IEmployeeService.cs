using Webapi_EFCore.DTOs;
using Webapi_EFCore.Models;

namespace Webapi_EFCore.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllAsync();
    }
}
