using FluentValidation;
using CustomerTest.Application.Services.Order.Commands;

namespace CustomerTest.Application.Services.Order.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {

            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty()
                .WithMessage("Price is required.");
            
        }

    }
}
