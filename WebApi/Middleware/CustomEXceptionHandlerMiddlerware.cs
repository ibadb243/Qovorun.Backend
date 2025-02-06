using System.Net;
using System.Text.Json;

namespace WebApi.Middleware;

public class CustomEXceptionHandlerMiddlerware
{
    private readonly RequestDelegate _next;

    public CustomEXceptionHandlerMiddlerware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty) result = JsonSerializer.Serialize(exception);
        
        return context.Response.WriteAsync(result);
    }
}