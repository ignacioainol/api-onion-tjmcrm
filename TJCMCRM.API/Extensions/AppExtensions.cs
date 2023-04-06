using TJCMCRM.API.Middlewares;

namespace TJCMCRM.API.Extensions
{
    public static class AppExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            //app.UseMiddleware<ApiKeyMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
