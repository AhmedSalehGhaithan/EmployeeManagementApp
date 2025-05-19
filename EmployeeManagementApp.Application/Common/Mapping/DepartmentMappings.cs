using AutoMapper;
using EmployeeManagementApp.Application.Dtos;
using EmployeeManagementApp.Domain.Core.Entities;

namespace EmployeeManagementApp.Application.Common.Mapping;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<Department, DepartmentDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DepartmentName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.DepartmentDescription))
            .ForMember(dest => dest.CustomersIds, opt => opt.MapFrom(src => 
                src.Employees != null ? src.Employees.Select(e => e.Id).ToList() : new List<Guid>()));
    }
}