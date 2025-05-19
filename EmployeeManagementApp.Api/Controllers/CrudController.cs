using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApp.Api.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class CrudController
    <TEntityDto,
    TCreateCommand,
    TUpdateCommand,
    TGetByIdQuery,
    TGetAllQuery, 
    TDeleteCommand,
    TPaginatedQuery>   : ControllerBase
    where TEntityDto : class
{
    protected readonly IMediator _mediator;

    protected CrudController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public virtual async Task<ActionResult<TEntityDto>> Create([FromBody] TCreateCommand command)
    {
        var result = await _mediator.Send(command!);
        return CreatedAtAction(nameof(GetById), new { id = ((dynamic)result!).Id }, result);
    }

    [HttpPut("{id:guid}")]
    public virtual async Task<IActionResult> Update(Guid id, [FromBody] TUpdateCommand command)
    {
        if (id != ((dynamic)command!).DepartmentId)
            return BadRequest("ID mismatch");

        var result = await _mediator.Send(command!);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        var command = (TDeleteCommand)Activator.CreateInstance(typeof(TDeleteCommand), id)!;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public virtual async Task<ActionResult<TEntityDto>> GetById(Guid id)
    {
        var query = (TGetByIdQuery)Activator.CreateInstance(typeof(TGetByIdQuery), id)!;
        var result = await _mediator.Send(query);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TEntityDto>>> GetAll()
    {
        var result = await _mediator.Send(Activator.CreateInstance<TGetAllQuery>());
        return Ok(result);
    }

    [HttpGet("paginated")]
    public virtual async Task<ActionResult<object>> GetPaginated(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null)
    {
        var query = (TPaginatedQuery)Activator.CreateInstance(typeof(TPaginatedQuery), pageNumber, pageSize, searchTerm!)!;
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
