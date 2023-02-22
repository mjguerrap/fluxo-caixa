using System.Reflection;
using Microsoft.OpenApi.Models;

namespace FluxoCaixa.AppExtentions;

public static class AddSwaggerGenConfiguration
{
    public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services) =>
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Fluxo de Caixa",
                Description = "Sistema para controlar o fluxo de caixa e obter o seu consolidado",
                Version = "v1"
            });
            options.EnableAnnotations();
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

}