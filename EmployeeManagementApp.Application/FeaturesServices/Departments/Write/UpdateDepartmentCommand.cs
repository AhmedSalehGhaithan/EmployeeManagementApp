using EmployeeManagementApp.Application.Abstractions.CQRS;
using EmployeeManagementApp.Domain.Core.Entities;
using EmployeeManagementApp.Domain.Core.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Application.FeaturesServices.Departments.Write;

public record UpdateDepartmentCommand(
 Guid DepartmentId,
 string departmentName,
 string departmentDescription,
 int departmentNumber) : ICommand<Guid>;

public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(x => x.DepartmentId).NotEmpty().WithMessage("Department Id is required!!");
    }
}

public class UpdateDepartmentCommandHandler(IBaseRepository<Department> _repo) : ICommandHandler<UpdateDepartmentCommand, Guid>
{
    public async Task<Guid> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _repo.GetByIdAsync(
           request.DepartmentId,
           q => q.Include(d => d.Employees),  // Include employees here
           cancellationToken);

        if (department == null) 
            return Guid.Empty;

        department.DepartmentName = request.departmentName;
        department.DepartmentDescription = request.departmentDescription;
        department.DepartmentNumber = request.departmentNumber;

        await _repo.UpdateAsync(department);
        return department.Id;
    }
}
