using AutoMapper;
using EmployeeManagementApp.Application.Dtos;
using EmployeeManagementApp.Domain.Core.Entities;

namespace EmployeeManagementApp.Application.Common.Mapping;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.departmentId));
    }
}