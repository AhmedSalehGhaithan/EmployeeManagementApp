using EmployeeManagementApp.Application.Abstractions.CQRS;
using EmployeeManagementApp.Application.Dtos;
using EmployeeManagementApp.Domain.Core.Entities;
using EmployeeManagementApp.Domain.Core.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Application.FeaturesServices.Departments.Read;

public record GetDepartmentByIdQuery(Guid Id) : IQuery<DepartmentDto>;

public class GetDepartmentByIdQueryValidator : AbstractValidator<GetDepartmentByIdQuery>
{
    public GetDepartmentByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Department Id is required!!");
    }
}

public class GetDepartmentByIdQueryHandler(IBaseRepository<Department> _repo)
    : IQueryHandler<GetDepartmentByIdQuery, DepartmentDto>
{
    public async Task<DepartmentDto> Handle(
        GetDepartmentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var department = await _repo.GetByIdAsync(
            request.Id,
            q => q.Include(d => d.Employees),
            cancellationToken);


        var employeeIds = department.Employees?.Select(e => e.Id).ToList() ?? new List<Guid>();

        return new DepartmentDto(
            department.Id,
            department.DepartmentNumber,
            department.DepartmentName,
            department.DepartmentDescription,
            employeeIds);
    }
}

