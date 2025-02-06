namespace WebApi.Middleware;

public static class CustomEXceptionHandlerMiddlerwareExtensions
{
    public static IApplicationBuilder UseCustomEXceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomEXceptionHandlerMiddlerware>();
    }
}