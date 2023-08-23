using FluentValidation;
using CustomerTest.Application.Services.Customer.Commands;
using PhoneNumbers;

namespace CustomerTest.Application.Services.Customer.Validators
{
    public class EditCustomerCommandValidator : AbstractValidator<EditCustomerCommand>
    {
        public EditCustomerCommandValidator()
        {

            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Id is Not Valid.");

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
                .Must(x => PhoneNumberUtil.IsViablePhoneNumber(x.ToString()));

            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty();

        }

    }
}
