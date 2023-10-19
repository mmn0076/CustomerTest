using FluentValidation;
using CustomerTest.Application.Services.Order.Commands;
using CustomerTest.Domain.Common.Errors;

namespace CustomerTest.Application.Services.Order.Validators
{
    public class EditOrderCommandValidator : AbstractValidator<EditOrderCommand>
    {
        public EditOrderCommandValidator()
        {
            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty()
                .WithMessage(Errors.Validation.PriceRequired.Description);
        }
    }
}