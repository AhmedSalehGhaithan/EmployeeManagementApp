using EmployeeManagementApp.Application.Abstractions.CQRS;
using EmployeeManagementApp.Application.Dtos;
using EmployeeManagementApp.Domain.Core.Entities;
using EmployeeManagementApp.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagementApp.Application.FeaturesServices.Departments.Read;

public record GetPaginatedDepartmentsQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? SearchTerm = null)
    : IQuery<PaginatedResult<DepartmentDto>>;

public class GetPaginatedDepartmentsQueryHandler(IBaseRepository<Department> _repo)
    : IQueryHandler<GetPaginatedDepartmentsQuery, PaginatedResult<DepartmentDto>>
{
    public async Task<PaginatedResult<DepartmentDto>> Handle(
        GetPaginatedDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        // Create predicate for search if search term is provided
        Expression<Func<Department, bool>>? predicate = null;
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            predicate = d => d.DepartmentName.Contains(request.SearchTerm) ||
                            d.DepartmentDescription.Contains(request.SearchTerm);
        }

        // Get paginated result from repository
        var paginatedResult = await _repo.GetPaginatedAsync(
            request.PageNumber,
            request.PageSize,
            predicate,
            orderBy: q => q.OrderBy(d => d.DepartmentName),
            inculde: q => q.Include(d => d.Employees),
            cancellationToken);

        // Map to DTOs
        var items = paginatedResult.Items.Select(d => new DepartmentDto(
            d.Id,
            d.DepartmentNumber,
            d.DepartmentName,
            d.DepartmentDescription,
            d.Employees?.Select(e => e.Id).ToList() ?? new List<Guid>()))
            .ToList();

        // Return new paginated result with DTOs
        return new PaginatedResult<DepartmentDto>(
            items,
            paginatedResult.TotalCount,
            paginatedResult.PageNumber,
            paginatedResult.PageSize);
    }
}

