using HRLeaveManagement.Api.Models;
using HRLeaveManagement.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace HRLeaveManagement.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails problem = new();

            switch (ex)
            {
                case Application.Exceptions.BadRequestException badRequestException:

                    statusCode = HttpStatusCode.BadRequest;

                    problem = new CustomProblemDetails
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(Application.Exceptions.BadRequestException),
                        Errors = badRequestException.ValidationErrors
                    };
                    break;

                case EntityNotFoundException notFound:

                    statusCode = HttpStatusCode.NotFound;

                    problem = new CustomProblemDetails
                    {
                        Title = notFound.Message,
                        Status = (int)statusCode,
                        Type = nameof(EntityNotFoundException),
                        Detail = notFound.InnerException?.Message,
                    };
                    break;

                default:

                    problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.StackTrace,
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;

            var logMessage = JsonConvert.SerializeObject(problem);
            _logger.LogError(logMessage);

            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}