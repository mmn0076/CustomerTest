using FluentValidation;
using CustomerTest.Application.Services.Order.Commands;

namespace CustomerTest.Application.Services.Order.Validators
{
    public class EditOrderCommandValidator : AbstractValidator<EditOrderCommand>
    {
        public EditOrderCommandValidator()
        {
            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty()
                .WithMessage("Price is required.");
        }
    }
}