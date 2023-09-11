using FluentValidation;
using FluentValidation.Validators;

namespace CustomerTest.Application.Services.Customer.Validators;

public class PhoneNumberValidation<T, TProperty> : PropertyValidator<T, TProperty>
{

    public override string Name => "PhoneNumberValidator";

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
        var phone = phoneNumberUtil.Parse(value?.ToString(), "ZZ");
        var result = phoneNumberUtil.IsValidNumber(phone);
        return result;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "'{PropertyName}' is not valid.";
}


public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, TElement> PhoneNumberValidator<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new PhoneNumberValidation<T, TElement>());
    }
    
}