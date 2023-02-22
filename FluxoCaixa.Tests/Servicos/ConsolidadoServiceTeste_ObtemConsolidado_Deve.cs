using Moq;
using FluentAssertions;
using FluxoCaixa.Tests.Fixture;

namespace FluxoCaixa.Tests.Servicos;

public class ConsolidadoServiceTeste_ObtemConsolidadoDeve : LancamentosFixture
{

    [Fact]
    public async Task Dado_ExistiremLancamentosParaDoisDiasDoMes_Quando_ConsolidadoForChamdo_Entao_RetornarConsolidadoDeDoisDias()
    {
        //Adjust
        SetupData();
        // Act
        var result = await ConsolidadoService!.ObtemConsolidado(2, 2023)!;

        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Once());
        result?.Count.Should().Be(2);
        var primeiroRegistro = result?.First();
        primeiroRegistro!.Saldo.Should().Be(primeiroRegistro.Credito-primeiroRegistro.Debito);
    }

    [Fact]
    public async Task Dado_NaoExistiremLancamentosNoMes_Quando_ConsolidadoForChamado_Entao_RetornarListaVazia()
    {
        //Adjust
        SetupData();

        // Act
        var result = await ConsolidadoService!.ObtemConsolidado(3, 2023)!;

        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Once());
        result?.Count.Should().Be(0);
    }
}
