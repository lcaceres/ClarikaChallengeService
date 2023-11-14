using System.Net.Mime;
using System.Net;
using Newtonsoft.Json;
using ClarikaChallengeService.WebApi.DTOs;
using ClarikaChallengeService.Infraestructure.Exceptions;

namespace ClarikaChallengeService.WebApi.Infraestructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public void Invoke(HttpContext context)
        {
            try
            {
                _next(context);
            }
            catch (Exception ex)
            {
                HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, error) = GetErrorResponse(exception);
            var result = JsonConvert.SerializeObject(error);

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }

        private static (HttpStatusCode, ErrorResponse) GetErrorResponse(Exception exception)
        {
            string exceptionMessage;
            string userMessage;
            var type = exception.GetType();
            HttpStatusCode statusCode;

            switch (exception)
            {
                case BusinessRuleValidationException:
                    exceptionMessage = exception.Message;
                    userMessage = exception.Message;
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case ApplicationArgumentException:
                    exceptionMessage = exception.Message;
                    userMessage = exception.Message;
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case InvalidApplicationOperationException:
                    exceptionMessage = exception.Message;
                    userMessage = exception.Message;
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                default:
                    exceptionMessage = exception.Message;
                    userMessage = exception.Message;
                    statusCode = HttpStatusCode.InternalServerError;
                    type = typeof(Exception);
                    break;
            }

            return (statusCode, new ErrorResponse
            {
                ExceptionMessage = exceptionMessage,
                Message = userMessage,
                ExceptionType = type,
            });
        }
    }
}
