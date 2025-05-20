//// Application/Common/Handlers/BaseCommandHandler.cs
//using MediatR;
//using AutoMapper;
//using EmployeeManagementApp.Application.Abstractions.CQRS;
//using FluentValidation;
//using FluentValidation.Results;

//namespace EmployeeManagementApp.Application.Common.Handlers;

//public abstract class BaseCommandHandler<TCommand, TResult>
//    : IRequestHandler<TCommand, TResult>
//    where TCommand : ICommand<TResult>
//{
//    protected readonly IMapper _mapper;
//    private readonly IValidator<TCommand>? _validator;

//    protected BaseCommandHandler(
//        IMapper mapper,
//        IValidator<TCommand>? validator = null)
//    {
//        _mapper = mapper;
//        _validator = validator;
//    }

//    public async Task<TResult> Handle(
//        TCommand command,
//        CancellationToken cancellationToken)
//    {
//        if (_validator != null)
//        {
//            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
//            if (!validationResult.IsValid)
//            {
//                throw new ValidationException(validationResult.Errors);
//            }
//        }

//        return await HandleCommand(command, cancellationToken);
//    }

//    protected abstract Task<TResult> HandleCommand(
//        TCommand command,
//        CancellationToken cancellationToken);
//}