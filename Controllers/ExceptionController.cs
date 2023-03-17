using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UnitessTestApp.Api.Core.Exceptions;

namespace UnitessTestApp.Api.Controllers
{
    /// <summary>
    /// Exception handler
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ExceptionController : ControllerBase
    {
        [Route("exception")]
        public UnitessExceptionResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            var code = 500;

            if (exception is UnitessException httpException)
            {
                code = (int) httpException.Status;
            }

            Response.StatusCode = code;

            return new UnitessExceptionResponse(exception);
        }
    }
}
