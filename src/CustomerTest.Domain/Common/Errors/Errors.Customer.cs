using ErrorOr;

namespace CustomerTest.Domain.Common.Errors;

public static partial class Errors
{
    public static class Customer
    {
        public static Error NotFound => Error.NotFound(code: "NotFound", description: "Customer Not Found");
        public static Error DuplicateUser => Error.Conflict(code: "DuplicateUser", description: "Duplicate User Is Found");
    }
}