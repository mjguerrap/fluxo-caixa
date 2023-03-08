using FluxoCaixa.AppUtil;
using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
namespace FluxoCaixa.AppSetup;

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
                app.Logger.LogWarning(exceptionHandlerPathFeature?.Error, exceptionHandlerPathFeature?.Error.Message);
            }
            else
            {
                await context.Response.WriteAsync("Algo de errado aconteceu, favor tentar novamente mais tarde, se o problema persistir, entre em contato com o suporte.");
                app.Logger.LogError(exceptionHandlerPathFeature?.Error,$"Erro inexperado");
            }          
        });
    });
}