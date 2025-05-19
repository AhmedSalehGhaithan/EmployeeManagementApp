//// Application/Common/Handlers/BaseQueryHandler.cs
//using MediatR;
//using AutoMapper;
//using EmployeeManagementApp.Application.Abstractions.CQRS;
//using FluentValidation;
//using FluentValidation.Results;

//namespace EmployeeManagementApp.Application.Common.Handlers;

//public abstract class BaseQueryHandler<TQuery, TResult>
//    : IRequestHandler<TQuery, TResult>
//    where TQuery : IQuery<TResult>
//{
//    protected readonly IMapper _mapper;
//    private readonly IValidator<TQuery>? _validator;

//    protected BaseQueryHandler(
//        IMapper mapper,
//        IValidator<TQuery>? validator = null)
//    {
//        _mapper = mapper;
//        _validator = validator;
//    }

//    public async Task<TResult> Handle(
//        TQuery query,
//        CancellationToken cancellationToken)
//    {
//        if (_validator != null)
//        {
//            ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
//            if (!validationResult.IsValid)
//            {
//                throw new ValidationException(validationResult.Errors);
//            }
//        }

//        return await HandleQuery(query, cancellationToken);
//    }

//    protected abstract Task<TResult> HandleQuery(
//        TQuery query,
//        CancellationToken cancellationToken);
//}