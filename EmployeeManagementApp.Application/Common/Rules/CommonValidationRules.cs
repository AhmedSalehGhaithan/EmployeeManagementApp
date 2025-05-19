//// Application/Common/Rules/CommonValidationRules.cs
//using FluentValidation;

//namespace EmployeeManagementApp.Application.Common.Rules;

//public static class CommonValidationRules
//{
//    public static IRuleBuilderOptions<T, Guid> ValidEntityId<T>(
//        this IRuleBuilder<T, Guid> ruleBuilder)
//    {
//        return ruleBuilder
//            .NotEmpty().WithMessage("{PropertyName} is required")
//            .NotEqual(Guid.Empty).WithMessage("Invalid {PropertyName} format");
//    }

//    public static IRuleBuilderOptions<T, string> StandardNameValidation<T>(
//        this IRuleBuilder<T, string> ruleBuilder,
//        int maxLength = 100)
//    {
//        return ruleBuilder
//            .NotEmpty().WithMessage("{PropertyName} is required")
//            .MaximumLength(maxLength)
//            .Matches(@"^[a-zA-Z\u0600-\u06FF\s\-']+$")
//            .WithMessage("{PropertyName} contains invalid characters");
//    }
//}