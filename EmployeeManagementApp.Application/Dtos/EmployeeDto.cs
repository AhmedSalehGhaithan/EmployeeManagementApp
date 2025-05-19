namespace EmployeeManagementApp.Application.Dtos;

public record EmployeeDto(
Guid EmployeeId,
string FirstName,
string LastName,
string PhoneNumber,
int Age,
Guid DepartmentId);
