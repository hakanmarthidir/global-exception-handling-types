using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace global_exception_handling_types.Filters
{
    //WAY 1 : With IExceptionFilter
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //do something 
            var exception = context.Exception;
            //log to exception...
            this.LogException(exception);
            //create meaningful message to client
            var result = new MyResponse() { Message = "ExceptionFilter : An error occurred while doing something.", Status = 0, ErrorCode = "UnknownError" };
            context.Result = new JsonResult(result);
        }

        private void LogException(Exception ex)
        {
            Debug.WriteLine($"FILTER EXCEPTION : {ex.Message}");
        }
    }
}
