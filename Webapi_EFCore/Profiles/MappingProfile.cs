using AutoMapper;
using Webapi_EFCore.DTOs;
using Webapi_EFCore.Models;

namespace Webapi_EFCore.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
        }

    }
}
