using AutoMapper;
using Webapi_EFCore.DTOs;
using Webapi_EFCore.Models;

namespace Webapi_EFCore.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.ProjectIds,
                       opt => opt.MapFrom(src => src.EmployeeProjects.Select(ep => ep.ProjectId).ToList())); 


            CreateMap<EmployeeDetails, EmployeeDetailsDto>();

            CreateMap<EmployeeCreateDto, Employee>()
                .ForMember(dest => dest.EmployeeProjects, opt => opt.MapFrom(src =>
                src.ProjectIds.Select(pid => new EmployeeProject { ProjectId = pid }).ToList()));

            CreateMap<EmployeeDetailsDto, EmployeeDetails>();
        }

    }
}
