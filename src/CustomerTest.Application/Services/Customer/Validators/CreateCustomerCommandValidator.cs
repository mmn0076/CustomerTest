using FluentValidation;
using CustomerTest.Application.Services.Customer.Commands;
using CustomerTest.Application.Services.Order.Common;
using PhoneNumbers;

namespace CustomerTest.Application.Services.Customer.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("FirstName is required.");

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("LastName is required.");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email is not Valid.");

            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .Must(PhoneNumberUtil.IsViablePhoneNumber);

            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty();

        }

    }
}
