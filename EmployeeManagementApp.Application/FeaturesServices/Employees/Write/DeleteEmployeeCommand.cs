using EmployeeManagementApp.Application.Abstractions.CQRS;
using EmployeeManagementApp.Domain.Core.Entities;
using EmployeeManagementApp.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Application.FeaturesServices.Employees.Write;

public record DeleteEmployeeCommand(Guid employeeId) : ICommand<Guid>;

public class DeleteEmployeeCommandHandler(IBaseRepository<Employee> _repo) : ICommandHandler<DeleteEmployeeCommand, Guid>
{
    public async Task<Guid> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _repo.GetByIdAsync(request.employeeId,
            p => p.Include(d => d.Department), cancellationToken);
        if (employee == null) 
            return Guid.Empty;

        await _repo.DeleteAsync(employee, cancellationToken);
        return request.employeeId;
    }
}