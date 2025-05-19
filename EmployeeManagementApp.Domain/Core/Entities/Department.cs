namespace EmployeeManagementApp.Domain.Core.Entities;

public class Department
{
    public Guid Id { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string DepartmentDescription { get; set; } = string.Empty;
    public int DepartmentNumber { get; set; }
    public virtual ICollection<Employee>?  Employees { get; set; }
}
