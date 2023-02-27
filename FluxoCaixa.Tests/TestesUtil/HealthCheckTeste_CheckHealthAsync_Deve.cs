using FluentAssertions;
using FluxoCaixa.AppUtil;
using FluxoCaixa.DBContext;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;

namespace FluxoCaixa.Tests.TestesUtil;
public class HealthCheck_teste_CheckHealthAsync_Deve
{
    [Fact]
    public async Task Dado_SolicitacaoVerificacaoStatus_QuandoNOk_Entao_RetornaUnhealth()
    {
        var fluxoCaixaContextMock = new Mock<FluxoCaixaContext>();
        var teste = fluxoCaixaContextMock.Object.Database;
        var healthCheck = new HealthCheck(fluxoCaixaContextMock.Object);
        
        var result = await healthCheck.CheckHealthAsync(new HealthCheckContext());
        
        result.Status.Should().Be(HealthStatus.Unhealthy);
    }

    [Fact]
    public async Task Dado_SolicitacaoVerificacaoStatus_QuandoOk_Entao_RenornaHealth()
    {
        DbContextOptions<FluxoCaixaContext> options;
        var builder = new DbContextOptionsBuilder<FluxoCaixaContext>();
        builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        options = builder.Options;
        var dataBaseContext = new FluxoCaixaContext(options);
   
        var healthCheck = new HealthCheck(dataBaseContext);
        
        var result = await healthCheck.CheckHealthAsync(new HealthCheckContext());
        
        result.Status.Should().Be(HealthStatus.Healthy);
    }
}