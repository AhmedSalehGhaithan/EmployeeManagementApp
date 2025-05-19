using EmployeeManagementApp.Application.Abstractions.CQRS;
using EmployeeManagementApp.Application.Dtos;
using EmployeeManagementApp.Domain.Core.Entities;
using EmployeeManagementApp.Domain.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagementApp.Application.FeaturesServices.Employees.Read;

public record GetPaginatedEmployeesQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? SearchTerm = null)
    : IQuery<PaginatedResult<EmployeeDto>>;

public class GetPaginatedEmployeesQueryHandler(IBaseRepository<Employee> _repo)
    : IQueryHandler<GetPaginatedEmployeesQuery, PaginatedResult<EmployeeDto>>
{
    public async Task<PaginatedResult<EmployeeDto>> Handle(
        GetPaginatedEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        // Create predicate for search if search term is provided
        Expression<Func<Employee, bool>>? predicate = null;
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            predicate = e => e.FirstName.Contains(request.SearchTerm) ||
                             e.LastName.Contains(request.SearchTerm) ||
                             e.PhoneNumber.Contains(request.SearchTerm);
        }

        // Get paginated result from repository
        var paginatedResult = await _repo.GetPaginatedAsync(
            request.PageNumber,
            request.PageSize,
            predicate,
            orderBy: q => q.OrderBy(e => e.LastName).ThenBy(e => e.FirstName),
            inculde: q => q.Include(e => e.Department),
            cancellationToken);

        // Map to DTOs
        var items = paginatedResult.Items.Select(e => new EmployeeDto(
            e.Id,
            e.FirstName,
            e.LastName,
            e.PhoneNumber,
            e.Age,
            e.departmentId ?? Guid.Empty))
            .ToList();

        // Return new paginated result with DTOs
        return new PaginatedResult<EmployeeDto>(
            items,
            paginatedResult.TotalCount,
            paginatedResult.PageNumber,
            paginatedResult.PageSize);
    }
}