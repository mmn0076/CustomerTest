using FluentValidation;
using CustomerTest.Application.Services.Customer.Commands;
using CustomerTest.Application.Services.Order.Common;
using CustomerTest.Domain.Common.Errors;
using PhoneNumbers;

namespace CustomerTest.Application.Services.Customer.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {

            RuleFor(x => x.FirstName)
                .NotNull().WithMessage(Errors.Validation.FirstNameRequired.Description)
                .NotEmpty().WithMessage(Errors.Validation.FirstNameRequired.Description);

            RuleFor(x => x.LastName)
                .NotNull().WithMessage(Errors.Validation.LastNameRequired.Description)
                .NotEmpty().WithMessage(Errors.Validation.LastNameRequired.Description);

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage(Errors.Validation.EmailIsNotValid.Description);

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
