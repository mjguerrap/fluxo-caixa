using FluxoCaixa.DBContext;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FluxoCaixa.AppUtil;
public class HealthCheck : IHealthCheck
{
    private readonly FluxoCaixaContext _fluxoCaixaDb;
    public HealthCheck(FluxoCaixaContext fluxoCaixaDB)
    {
        _fluxoCaixaDb = fluxoCaixaDB;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        bool isHealthy = _fluxoCaixaDb.Database?.CanConnect() ?? false;

      
        if (isHealthy)
        {
            return await Task.FromResult(
                HealthCheckResult.Healthy("Healty"));
        }
       

        return await Task.FromResult(
            HealthCheckResult.Unhealthy("Unhealth"));
            
    }
}