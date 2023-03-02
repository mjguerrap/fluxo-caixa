namespace FluxoCaixa.AppSetup;

public static class AddCorsConfiguration
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services, out string myAllowSpecificOrigins)
    {
        var allowSpecifiOrigins = "_myAllowSpecificOrigins";
        myAllowSpecificOrigins = allowSpecifiOrigins;
        return services.AddCors(options =>
        {
            options.AddPolicy(name: allowSpecifiOrigins,
            builder =>
            {
                builder.WithOrigins("*");
            });
        });

    }
}