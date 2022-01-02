using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace global_exception_handling_types.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        public ErrorController()
        {
        }
        
        [Route("/error")]
        public IActionResult Error()
        {
            return this.GetExceptionResult();
        }

        private IActionResult GetExceptionResult()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            this.LogException(exception);
            var result = new MyResponse() { Message = "ErrorController : An error occurred while doing something.", Status = 0, ErrorCode = "UnknownError" };
            return new JsonResult(result);
        }

        private void LogException(Exception ex)
        {
            Debug.WriteLine($"CONTROLLER EXCEPTION : {ex.Message}");
        }
    }
}
