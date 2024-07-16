using System.Net;
using System.Text.Json;
using Crosscutting.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Middleware;

public static class ExceptionMiddleware
{
    public static void TratarExcecao(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appBuilder =>
        {
            appBuilder.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var exception = contextFeature.Error;
                    var message = exception.Message;

                    context.Response.StatusCode = exception switch
                    {
                        ClienteNaoEncontradoException => (int)HttpStatusCode.NotFound,
                        ContaInvalidaException => (int)HttpStatusCode.BadRequest,
                        ContaNaoEncontradaException => (int)HttpStatusCode.NotFound,
                        ClienteInvalidoException => (int)HttpStatusCode.BadRequest,
                        _ => context.Response.StatusCode
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(new { message }));
                }
            });
        });
    }
}