using EmployeeManagementApp.Api.Controllers.Base;
using EmployeeManagementApp.Application.Dtos;
using EmployeeManagementApp.Application.FeaturesServices.Departments.Read;
using EmployeeManagementApp.Application.FeaturesServices.Departments.Write;
using MediatR;

namespace EmployeeManagementApp.Api.Controllers;

public class DepartmentController : CrudController<
    DepartmentDto,
    CreateDepartmentCommand,
    UpdateDepartmentCommand,
    GetDepartmentByIdQuery,
    GetAllDepartmentsQuery,
    DeleteDepartmentCommand,
    GetPaginatedDepartmentsQuery>
{
    public DepartmentController(IMediator mediator) : base(mediator) { }
}
