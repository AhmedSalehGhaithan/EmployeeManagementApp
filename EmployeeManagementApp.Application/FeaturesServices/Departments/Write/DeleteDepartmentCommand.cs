using EmployeeManagementApp.Application.Abstractions.CQRS;
using EmployeeManagementApp.Domain.Core.Entities;
using EmployeeManagementApp.Domain.Core.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Application.FeaturesServices.Departments.Write;

public record DeleteDepartmentCommand(Guid Id) : ICommand<Guid>;

public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
{
    public DeleteDepartmentCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("none");
    }
}

public class DeleteDepartmentCommandHandler(IBaseRepository<Department> _repo) 
    : ICommandHandler<DeleteDepartmentCommand, Guid>
{
    public async Task<Guid> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _repo.GetByIdAsync(
             request.Id,
             q => q.Include(d => d.Employees),
             cancellationToken);

        if (department == null)
            return Guid.Empty;

        await _repo.DeleteAsync(department);
        return request.Id;
    }
}