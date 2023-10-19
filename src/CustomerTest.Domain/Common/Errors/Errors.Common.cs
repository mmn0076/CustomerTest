using ErrorOr;

namespace CustomerTest.Domain.Common.Errors;

public static partial class Errors
{
    public static class Common
    {
        public static Error DatabaseError => Error.NotFound(code: "DatabaseError", description: "Internal Error");
        public static Error InternalServerError => Error.NotFound(code: "InternalServer", description: "Internal Error");

    }
}