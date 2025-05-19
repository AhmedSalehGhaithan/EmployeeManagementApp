namespace EmployeeManagementApp.Application.Dtos;

public record DepartmentDto(
    Guid Id,
    int DepartmentNumber,
    string? Name,
    string? Description,
    List<Guid> CustomersIds);
