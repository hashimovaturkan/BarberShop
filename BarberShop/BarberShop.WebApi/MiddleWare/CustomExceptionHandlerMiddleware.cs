using FluentValidation;
using BarberShop.Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BarberShop.WebApi.MiddleWare
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
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
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
                _logger.LogError(exception, exception.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.UnprocessableEntity;

                    if (validationException.Errors.Any())
                        result = JsonSerializer.Serialize(validationException.Errors);
                    break;

                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;

                case DefaultValidationException:
                    code = HttpStatusCode.UnprocessableEntity;
                    break;

                case UnauthorizedException:
                    code = HttpStatusCode.Unauthorized;
                    break;

                case BadRequestException:
                    code = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;

            if (string.IsNullOrEmpty(result))
            {
                result = JsonSerializer.Serialize(new {error = exception.Message});
            }

            await context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}