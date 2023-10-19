using FluentValidation;
using CustomerTest.Application.Services.Order.Commands;
using CustomerTest.Domain.Common.Errors;

namespace CustomerTest.Application.Services.Order.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Price)
                .NotNull().WithMessage(Errors.Validation.PriceRequired.Description)
                .NotEmpty().WithMessage(Errors.Validation.PriceRequired.Description);
        }

    }
}
