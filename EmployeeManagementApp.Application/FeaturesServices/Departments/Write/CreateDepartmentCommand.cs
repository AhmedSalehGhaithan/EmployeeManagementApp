using EmployeeManagementApp.Application.Abstractions.CQRS;
using EmployeeManagementApp.Domain.Core.Entities;
using EmployeeManagementApp.Domain.Core.Interfaces;
using FluentValidation;

namespace EmployeeManagementApp.Application.FeaturesServices.Departments.Write;

public record CreateDepartmentCommand(
    string departmentName,
    string departmentDescription,
    int departmentNumber) : ICommand<CreateDepartmentCommandResponse>;

public record CreateDepartmentCommandResponse(Guid Id, string Name, int Number);

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.departmentName).NotEmpty().WithMessage("Department name is required");
        RuleFor(x => x.departmentNumber).NotEmpty().WithMessage("Department Number is required");
    }
}
public class CreateDepartmentCommandHandler(IBaseRepository<Department> _repo) : ICommandHandler<CreateDepartmentCommand, CreateDepartmentCommandResponse>
{
    public async Task<CreateDepartmentCommandResponse> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department
        {
            Id = Guid.NewGuid(),
            DepartmentName = request.departmentName,
            DepartmentDescription = request.departmentDescription,
            DepartmentNumber = request.departmentNumber
        };
        await _repo.AddAsync(department);

        return new CreateDepartmentCommandResponse(department.Id, department.DepartmentName, department.DepartmentNumber);
    }
}