using global_exception_handling_types;
using global_exception_handling_types.Filters;
using global_exception_handling_types.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => {
    //options.Filters.Add(typeof(ExceptionFilter)); -> WAY 1 : ExceptionFilter if you choose this way, open it
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

//WAY 2 : Custom Middleware 
//app.UseMiddleware<ExceptionHandlerMiddleware>();

//WAY 3 : existing error handler using with ErrorControllers
//app.UseExceptionHandler("/error");

//WAY 4 : existing error handler using without ErrorController
app.UseExceptionHandler(a => a.Run(async context =>
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature.Error;
    Debug.WriteLine($"CONTROLLER EXCEPTION : {exception.Message}");
    var result = new MyResponse() { Message = "ErrorHandlerWithoutController: An error occurred while doing something.", Status = 0, ErrorCode = "UnknownError" };
    await context.Response.WriteAsJsonAsync(result);
}));

app.UseAuthorization();

app.MapControllers();

app.Run();
