using Microsoft.AspNetCore.Mvc;

namespace web_panel_api.Middlewares
{
    public class GlobalExceptionHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                var temp = new ProblemDetails { Detail = ex.Message, Status = 500, Title = "Exception" };
                await context.Response.WriteAsJsonAsync(temp);
                context.Response.ContentType = "application/json";
            }
        }
    }
}
