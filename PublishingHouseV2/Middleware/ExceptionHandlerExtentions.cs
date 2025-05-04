namespace Library.Api.Middleware
{
    public static class ExceptionHandlerExtentions
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandler>();
        }
    }
}
