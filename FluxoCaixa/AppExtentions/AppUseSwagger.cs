namespace FluxoCaixa.AppExtentions;

public static class AppUseSwagger
{
    public static IApplicationBuilder UseSwaggerConfiguration(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fluxo de Caixa API V1");
        });
        return app;
    }
}