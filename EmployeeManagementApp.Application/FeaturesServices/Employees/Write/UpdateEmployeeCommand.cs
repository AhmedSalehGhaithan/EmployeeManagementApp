using EmployeeManagementApp.Application.Abstractions.CQRS;
using EmployeeManagementApp.Domain.Core.Entities;
using EmployeeManagementApp.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Application.FeaturesServices.Employees.Write;

public record UpdateEmployeeCommand(
    Guid employeeId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    int Age,
    Guid? DepartmentId) : ICommand<Guid>;


public class UpdateEmployeeCommandHandler(IBaseRepository<Employee> _repo) : ICommandHandler<UpdateEmployeeCommand, Guid>
{
    public async Task<Guid> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _repo.GetByIdAsync(request.employeeId,
            p => p.Include(d => d.Department), cancellationToken);
        if (employee == null)
            return Guid.Empty;

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.PhoneNumber = request.PhoneNumber;
        employee.Age = request.Age;
        employee.departmentId = request.DepartmentId;

        await _repo.UpdateAsync(employee);
        return employee.Id;
    }
}