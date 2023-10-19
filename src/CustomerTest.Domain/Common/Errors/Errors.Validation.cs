using ErrorOr;

namespace CustomerTest.Domain.Common.Errors;

public static partial class Errors
{
    public static class Validation
    {
        public static Error FirstNameRequired => Error.NotFound(code: "FirstName", description: "FirstName is Required.");
        public static Error LastNameRequired => Error.NotFound(code: "LastName", description: "LastName is Required.");
        public static Error EmailIsNotValid => Error.NotFound(code: "Email", description: "Email is not Valid.");
        public static Error PriceRequired => Error.NotFound(code: "Price", description: "Price is required.");
    }
}