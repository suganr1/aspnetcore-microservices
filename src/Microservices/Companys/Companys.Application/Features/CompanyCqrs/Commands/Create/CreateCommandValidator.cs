using FluentValidation;

namespace Companys.Application.Features.CompanyCqrs.Commands.Create
{
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator()
        {
            //TODO. Add Validations
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("{Code} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{Code} must not exceed 50 characters.");

            RuleFor(p => p.TurnOver)
                .NotEmpty().WithMessage("{TurnOver} is required.")
                .GreaterThan(0).WithMessage("{TurnOver} should be greater than zero.");
        }
    }
}
