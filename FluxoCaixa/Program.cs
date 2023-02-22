using FluxoCaixa.Extentions;
using FluxoCaixa.Util;
namespace FluxoCaixa;

public class Program
{
    protected Program()
    {
        
    }
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder;

        Configuration(args, out builder, out string myAllowSpecificOrigins);
        AppStart(builder, myAllowSpecificOrigins);
    }
    static void Configuration(string[] args, out WebApplicationBuilder builder, out string myAllowSpecificOrigins)
    {
        builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCustomApiVersioning();
        builder.Services.AddHealthChecks().AddCheck<HealthCheck>("Database connection");
        builder.AddCustomDbContext();
        builder.Services.AddDependencyInjection();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCustomSwaggerGen();

        builder.Services.AddCustomCors(out myAllowSpecificOrigins);
    }

    static void AppStart(WebApplicationBuilder builder, string myAllowSpecificOrigins)
    {
        var app = builder.Build();
        app.AddCustomExceptionHandler();
        app.UseSwaggerConfiguration();

        app.UseCors(myAllowSpecificOrigins);
        app.MapHealthChecks("/HealthCheck");
        app.APIMapping();
        app.Run();
    }
}


