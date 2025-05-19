using EmployeeManagementApp.Application.Abstractions.CQRS;
using EmployeeManagementApp.Application.Dtos;
using EmployeeManagementApp.Domain.Core.Entities;
using EmployeeManagementApp.Domain.Core.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Application.FeaturesServices.Employees.Read;

public record GetEmployeeByIdQuery(Guid Id) : IQuery<EmployeeDto>;

public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Employee ID is required")
            .NotEqual(Guid.Empty).WithMessage("Employee ID cannot be empty");
    }
}

public class GetEmployeeByIdQueryHandler(IBaseRepository<Employee> _repo)
    : IQueryHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(
        GetEmployeeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var employee = await _repo.GetByIdAsync(request.Id,
            p => p.Include(d => d.Department),cancellationToken);

        if (employee == null)
        {
            throw new KeyNotFoundException($"Employee with ID {request.Id} not found");
        }

        return new EmployeeDto(
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.PhoneNumber,
            employee.Age,
            employee.departmentId ?? Guid.Empty); // Handle null departmentId
    }
}
