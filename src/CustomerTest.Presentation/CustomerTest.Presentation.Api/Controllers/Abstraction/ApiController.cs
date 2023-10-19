using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CustomerTest.Presentation.Api.Controllers.Abstraction;

[ApiController]
public class ApiController : ControllerBase
{

    [Route("Error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        HttpContext.Items["errors"] = errors;

        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }


}