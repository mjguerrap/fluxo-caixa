using Moq;
using FluentAssertions;
using FluxoCaixa.Tests.Fixture;
using FluxoCaixa.Dominio;
using Moq.EntityFrameworkCore;

namespace FluxoCaixa.Tests.Servicos;

public class LancamentoServiceTeste_Buscar_PorMesAno_Deve : LancamentosFixture
{
    [Fact]
    public async Task Dado_BuscarLancamentoPorMesAno_Quando_LancamentosEncontrados_Entao_LancamentosEhRetornado()
    {       
        //Adjust
        SetupData();
        //FluxoCaixaContextMock!.SetupSequence(x => x.Set<Lancamento>()).ReturnsDbSet(Lancamentos);
        // Act
        var result = await LancamentoService!.Buscar(2, 2023, 1)!;
        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Exactly(1));
        result.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Dado_BuscarLancamentoPorMesAno_Quando_LancamentoNaoEncontrado_Entao_RetornaListaVazia()
    {
        //Adjust
        SetupData();
        //FluxoCaixaContextMock!.SetupSequence(x => x.Set<Lancamento>()).ReturnsDbSet(Lancamentos);
        // Act
        var result = await LancamentoService!.Buscar(2, 2022, 1)!;
        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Exactly(1));
        result.Count.Should().Be(0);
    }
}