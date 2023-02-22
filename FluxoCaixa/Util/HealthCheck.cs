using FluxoCaixa.DBContext;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FluxoCaixa.Util;
public class HealthCheck : IHealthCheck
{
    private readonly FluxoCaixaContext _fluxoCaixaDb;
    public HealthCheck(FluxoCaixaContext fluxoCaixaDB)
    {
        _fluxoCaixaDb = fluxoCaixaDB;
    }

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = _fluxoCaixaDb.Database.CanConnect();

      
        if (isHealthy)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("A healthy result."));
        }

        return Task.FromResult(
            new HealthCheckResult(
                context.Registration.FailureStatus, "An unhealthy result."));
    }
}