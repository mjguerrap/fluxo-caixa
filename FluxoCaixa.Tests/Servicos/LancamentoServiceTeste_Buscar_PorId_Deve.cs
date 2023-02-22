using Moq;
using FluentAssertions;
using FluxoCaixa.Tests.Fixture;
using FluxoCaixa.Dominio;
using FluxoCaixa.Servicos;
using FluxoCaixa.AppUtil;

namespace FluxoCaixa.Tests.Servicos;

public class LancamentoServiceTeste_Buscar_PorId_Deve : LancamentosFixture
{
    [Fact]
    public async Task Dado_BuscarLancamento_Quando_LancamentoEncontrado_Entao_LancamentoEhRetornado()
    {
        //Adjust
        SetupData();
        var lancamentoEncontrado = new Lancamento(1, "Despesa", TipoLancamento.Debito, new DateTime(2023, 2, 15), 50.00m);
        FluxoCaixaContextMock!.Setup(c => c.Lancamentos.FindAsync(It.IsAny<int>())).ReturnsAsync(lancamentoEncontrado);

        // Act
        var result = await LancamentoService!.Buscar(1)!;
        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Exactly(1));
        result.Should().BeOfType<Lancamento>();
    }

    [Fact]
    public async Task Dado_BuscarLancamento_Quando_LancamentoNaoEncontrado_Entao_RetornaNull()
    {
        //Adjust
        //Adjust
        SetupData();
        Lancamento lancamentoEncontrado = null!;
        FluxoCaixaContextMock!.Setup(c => c.Lancamentos.FindAsync(It.IsAny<int>())).ReturnsAsync(lancamentoEncontrado);

        // Act
        var result = await LancamentoService!.Buscar(1)!;
        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Exactly(1));
        result.Should().BeNull();
    }
}