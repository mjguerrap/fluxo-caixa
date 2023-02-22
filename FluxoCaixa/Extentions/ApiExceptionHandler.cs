using FluxoCaixa.Util;
using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace FluxoCaixa.Extentions;

public static class ApiExceptionHandler
{
    public static IApplicationBuilder AddCustomExceptionHandler(this WebApplication app) =>
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            
            context.Response.ContentType = Text.Plain;

            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is FluxoCaixaException)
            {
                await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.Message);
            }
            else
            {
                await context.Response.WriteAsync("Algo de errado aconteceu, favor tentar novamente mais tarde, se o problema persistir, entre em contato com o suporte.");
            }          
        });
    });
}