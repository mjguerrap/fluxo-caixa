using FluxoCaixa.Servicos;

namespace FluxoCaixa.Extentions;

public static class AddDIConfiguration
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services) {
        services.AddScoped<IConsolidadoService, ConsolidadoService>();
        services.AddScoped<ILancamentoService, LancamentoService>();
        return services;
    }

}