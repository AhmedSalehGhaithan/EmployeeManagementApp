using EmployeeManagementApp.Api.Controllers.Base;
using EmployeeManagementApp.Application.Dtos;
using EmployeeManagementApp.Application.FeaturesServices.Employees.Read;
using EmployeeManagementApp.Application.FeaturesServices.Employees.Write;
using MediatR;

namespace EmployeeManagementApp.Api.Controllers;

public class EmployeeController : CrudController<
    EmployeeDto,
    CreateEmployeeCommand,
    UpdateEmployeeCommand,
    GetEmployeeByIdQuery,
    GetAllEmployeesQuery,
    DeleteEmployeeCommand,
    GetPaginatedEmployeesQuery>
{
    public EmployeeController(IMediator mediator) : base(mediator) { }
}
