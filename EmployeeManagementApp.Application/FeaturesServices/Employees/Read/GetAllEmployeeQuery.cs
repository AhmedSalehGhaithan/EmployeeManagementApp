using EmployeeManagementApp.Application.Abstractions.CQRS;
using EmployeeManagementApp.Application.Dtos;
using EmployeeManagementApp.Domain.Core.Entities;
using EmployeeManagementApp.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Application.FeaturesServices.Employees.Read;

public record GetAllEmployeesQuery : IQuery<ICollection<EmployeeDto>>;

public class GetAllEmployeesQueryHandler(IBaseRepository<Employee> _repo)
    : IQueryHandler<GetAllEmployeesQuery, ICollection<EmployeeDto>>
{
    public async Task<ICollection<EmployeeDto>> Handle(
        GetAllEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var employees = await _repo.GetAllAsync( q => 
        q.Include(e => e.Department),cancellationToken);

        return employees.Select(e => new EmployeeDto(
            e.Id,
            e.FirstName,
            e.LastName,
            e.PhoneNumber,
            e.Age,
            e.departmentId ?? Guid.Empty))  // Handle null departmentId
            .ToList();
    }
}
