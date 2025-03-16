using System.Net;
using System.Text.Json;

namespace Backend.Middleware
{
    public class ExceptionHandlingMiddleware : ErrorResponse
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            _logger.LogError(exception, "Erro inesperado: {Message}", exception.Message);

            var response = new ErrorResponse
            {
                Message = exception.Message ?? "Ocorreu um erro inesperado.",
                Details = _env.IsDevelopment() ? exception.InnerException?.Message : null,
                StackTrace = _env.IsDevelopment() ? exception.StackTrace : null,
                StatusCode = statusCode,
                ErrorCode = Guid.NewGuid().ToString()
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var jsonResponse = JsonSerializer.Serialize(response, 
                options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}