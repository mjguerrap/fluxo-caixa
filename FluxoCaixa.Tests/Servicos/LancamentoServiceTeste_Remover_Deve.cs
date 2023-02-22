using Moq;
using FluentAssertions;
using FluxoCaixa.Tests.Fixture;
using FluxoCaixa.Dominio;
using FluxoCaixa.Servicos;
using FluxoCaixa.Util;

namespace FluxoCaixa.Tests.Servicos;

public class LancamentoServiceTeste_Remover_Deve : LancamentosFixture
{
    [Fact]
    public async Task Dado_RemoverLancamentoCredito_Quando_LancamentoEncontrado_Entao_LancamentoEhRemovido()
    {
        //Adjust
        SetupData();
        var lancamentoEncontrado = new Lancamento(1, "Despesa", TipoLancamento.Debito, new DateTime(2023, 2, 15), 50.00m);
        FluxoCaixaContextMock!.Setup(c => c.Lancamentos.FindAsync(It.IsAny<int>())).ReturnsAsync(lancamentoEncontrado);
        FluxoCaixaContextMock!.Setup(c => c.Lancamentos.Remove(It.IsAny<Lancamento>()));
        FluxoCaixaContextMock!.Setup(c => c.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(() => 1));
        // Act
        var result = await LancamentoService!.Remover(1)!;
        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Exactly(2));
        FluxoCaixaContextMock.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once);
        FluxoCaixaContextMock!.Verify(c => c.Lancamentos.Remove(It.IsAny<Lancamento>()), Times.Once);
    }

    [Fact]
    public async Task Dado_RemoverLancamentoCredito_Quando_LancamentoNaoEncontrado_Entao_LancamentoEhRemovido()
    {
        //Adjust
        SetupData();
        Lancamento lancamentoEncontrado = null!;
        FluxoCaixaContextMock!.Setup(c => c.Lancamentos.FindAsync(It.IsAny<int>())).ReturnsAsync(lancamentoEncontrado);
        FluxoCaixaContextMock!.Setup(c => c.Lancamentos.Remove(It.IsAny<Lancamento>()));
        FluxoCaixaContextMock!.Setup(c => c.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(() => 1));
        // Act
        var result = await LancamentoService!.Remover(1)!;
        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Once());
        FluxoCaixaContextMock.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Never);
        FluxoCaixaContextMock!.Verify(c => c.Lancamentos.Remove(It.IsAny<Lancamento>()), Times.Never);
    }
}