using ErrorOr;

namespace CustomerTest.Domain.Common.Errors;

public static partial class Errors
{
    public static class Order
    {
        public static Error NotFound => Error.NotFound(code: "NotFound", description: "Order Not Found");
    }
}