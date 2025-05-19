using EmployeeManagementApp.Application.Abstractions.CQRS;
using EmployeeManagementApp.Domain.Core.Entities;
using EmployeeManagementApp.Domain.Core.Interfaces;

namespace EmployeeManagementApp.Application.FeaturesServices.Employees.Write;

public record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string PhoneNumber,
    int Age,
    Guid? DepartmentId) 
    : ICommand<Employee>;

public record CreateEmployeeCommandResponse(
    string FullName,
    string PhoneNumber,
    int age,
    Guid? DepartmentId);
public class CreateEmployeeCommandHandler(IBaseRepository<Employee> _repo) 
    : ICommandHandler<CreateEmployeeCommand, Employee>
{
    public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Age = request.Age,
            departmentId = request.DepartmentId
        };

        return await _repo.AddAsync(employee,cancellationToken);
    }
}

