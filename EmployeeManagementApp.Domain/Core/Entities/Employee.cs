namespace EmployeeManagementApp.Domain.Core.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty ;
    public string PhoneNumber { get; set; } = string.Empty;
    public int Age { get; set; }
    public Guid? departmentId { get; set; }
    public virtual Department? Department { get; set; }
}
