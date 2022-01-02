using System.Diagnostics;

namespace global_exception_handling_types.Middlewares
{
    //WAY 2 : With CustomMiddleware
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _handler;

        public ExceptionHandlerMiddleware(RequestDelegate handler)
        {
            this._handler = handler;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await this._handler(httpContext);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = 500;            
            var result = new MyResponse() { Message = "Middleware : An error occurred while doing something.", Status = 0, ErrorCode = "UnknownError" };
            await httpContext.Response.WriteAsJsonAsync(result).ConfigureAwait(false);
        }

        private void LogException(Exception ex)
        {
            Debug.WriteLine($"MIDDLEWARE EXCEPTION : {ex.Message}");
        }

    }
}
