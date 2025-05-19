//// GenericGetAllQueryHandler.cs
//using EmployeeManagementApp.Application.Abstractions.CQRS;
//using EmployeeManagementApp.Application.Dtos;
//using EmployeeManagementApp.Domain.Core.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace EmployeeManagementApp.Application.Common.Handlers;

//public abstract class GenericGetAllQueryHandler<TEntity, TDto>(
//    IBaseRepository<TEntity> repo,
//    Func<TEntity, TDto> mapper)
//    : IQueryHandler<GetAllQuery<TEntity, TDto>, ICollection<TDto>>
//    where TEntity : class
//    where TDto : class
//{
//    public async Task<ICollection<TDto>> Handle(
//        GetAllQuery<TEntity, TDto> request,
//        CancellationToken cancellationToken)
//    {
//        var entities = await repo.GetAllAsync(
//            q => IncludeRelations(q),
//            cancellationToken);

//        return entities.Select(mapper).ToList();
//    }

//    protected virtual IQueryable<TEntity> IncludeRelations(IQueryable<TEntity> query)
//    {
//        return query;
//    }
//}
//// GetAllQuery.cs
//public record GetAllQuery<TEntity> : IQuery<ICollection<TDto>>;

//// GenericDto.cs
//public record GenericEntityDto(
//    Guid Id,
//    string Name,
//    string Description,
//    int Number,
//    List<Guid> RelatedIds);

//public record GenericEmployeeDto(
//    Guid Id,
//    string FirstName,
//    string LastName,
//    string PhoneNumber,
//    int Age,
//    Guid DepartmentId);
