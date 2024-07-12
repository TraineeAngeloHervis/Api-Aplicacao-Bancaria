using System.Net;
using System.Text.Json;
using Crosscutting.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Middleware
{
    public static class ExceptionMiddleware
    {
        public static void TratarExcecao(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var exception = contextFeature.Error;
                        var response = context.Response;
                        var message = exception.Message;
                        var statusCode = (int)HttpStatusCode.InternalServerError;

                        switch (exception)
                        {
                            case ValidationException validationException:
                                statusCode = (int)HttpStatusCode.BadRequest;
                                var validationErrors = validationException.Errors.Select(e => e.ErrorMessage).ToList();
                                message = string.Join(" ", validationErrors);
                                break;
                            case ClienteNaoEncontradoException _:
                                statusCode = (int)HttpStatusCode.NotFound;
                                break;
                            case ClienteInvalidoException _:
                                statusCode = (int)HttpStatusCode.BadRequest;
                                break;
                            case ContaInvalidaException _:
                                statusCode = (int)HttpStatusCode.BadRequest;
                                break;
                            case ContaNaoEncontradaException _:
                                statusCode = (int)HttpStatusCode.NotFound;
                                break;
                        }

                        response.StatusCode = statusCode;
                        var result = JsonSerializer.Serialize(new { message });
                        await response.WriteAsync(result);
                    }
                });
            });
        }
    }
}
