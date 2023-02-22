using Asp.Versioning;

namespace FluxoCaixa.Extentions;

public static class AddVersioningConfiguration
{
    public static IApiVersioningBuilder AddCustomApiVersioning(this IServiceCollection services) =>
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("Api-Version"), new QueryStringApiVersionReader("Query-String-Version"));
        });

}
