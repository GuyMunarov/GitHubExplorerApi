using System.Net;
using System.Text.Json;

namespace GitHubExplorerApi.Helpers
{
    public class ErrorHandlingMiddlewear
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddlewear(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsync("");
            }
        }
    }
}