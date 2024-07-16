using System.Net;
using System.Text.Json;
using Crosscutting.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Middleware;

public static class ExceptionMiddleware
{
    public static void TratarExcecao(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var statusCode = HttpStatusCode.InternalServerError;
                var message = "Ocorreu um erro interno no servidor.";

                switch (exception)
                {
                    case ValidationException ex:
                        statusCode = HttpStatusCode.BadRequest;
                        message = JsonSerializer.Serialize(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
                        break;
                    case ClienteNaoEncontradoException _:
                        statusCode = HttpStatusCode.NotFound;
                        message = "Cliente não encontrado.";
                        break;
                }

                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(message);
            });
        });
    }
}