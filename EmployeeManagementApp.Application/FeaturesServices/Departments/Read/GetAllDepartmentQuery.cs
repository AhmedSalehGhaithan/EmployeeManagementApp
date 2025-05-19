using EmployeeManagementApp.Application.Abstractions.CQRS;
using EmployeeManagementApp.Application.Dtos;
using EmployeeManagementApp.Domain.Core.Entities;
using EmployeeManagementApp.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Application.FeaturesServices.Departments.Read;

public record GetAllDepartmentsQuery : IQuery<ICollection<DepartmentDto>>;

public class GetAllDepartmentsQueryHandler(IBaseRepository<Department> _repo)
    : IQueryHandler<GetAllDepartmentsQuery, ICollection<DepartmentDto>>
{
    public async Task<ICollection<DepartmentDto>> Handle(
        GetAllDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        var departments = await _repo.GetAllAsync(q =>
        q.Include(d => d.Employees), cancellationToken);

        return departments.Select(d => new DepartmentDto(
            d.Id,
            d.DepartmentNumber,
            d.DepartmentName,
            d.DepartmentDescription,
            d.Employees?.Select(e => e.Id).ToList() ?? new List<Guid>()))
            .ToList();
    }
}