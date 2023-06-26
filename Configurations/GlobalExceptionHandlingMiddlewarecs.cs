using System.Net;
using System.Text.Json;
using UserSubscriptionWebApi.Exceptions;

using KeyNotFoundException = UserSubscriptionWebApi.Exceptions.KeyNotFoundException;

using UnauthorizedAccessException = UserSubscriptionWebApi.Exceptions.UnauthorizedAccessException;

namespace UserSubscriptionWebApi.Configurations
{
    public class GlobalExceptionHandlingMiddlewarecs
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddlewarecs> _logger;

        public GlobalExceptionHandlingMiddlewarecs(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddlewarecs> logger)
        {
            _next = next;
            _logger = logger;
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

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode status;
            var stacTrace = string.Empty;
            var message = "";
            var exceptionType = ex.GetType();

            if (exceptionType == typeof(NotFoundException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                stacTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(System.NotImplementedException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotImplemented;
                stacTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(BadRequestException))
            {
                message = ex.Message;
                status = HttpStatusCode.BadRequest;
                stacTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                stacTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = ex.Message;
                status = HttpStatusCode.Unauthorized;
                stacTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(ObjectAlreadyExistsException))
            {
                message = ex.Message;
                status = HttpStatusCode.Conflict;
                stacTrace = ex.StackTrace;
            }
            else
            {
                message = ex.Message;
                status = HttpStatusCode.InternalServerError;
                stacTrace = ex.StackTrace;

            }
            var exceptionResult = JsonSerializer.Serialize(new { error = message, stacTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            _logger.LogError(message);


            return context.Response.WriteAsync(exceptionResult);

        }
    }
}
