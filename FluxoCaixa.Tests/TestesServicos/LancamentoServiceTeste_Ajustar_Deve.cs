using Moq;
using FluxoCaixa.Tests.Fixture;
using FluxoCaixa.Dominio;
using FluentAssertions;
using FluxoCaixa.AppUtil;

namespace FluxoCaixa.Tests.TestesServicos;

public class LancamentoServiceTeste_Ajustar_Deve : LancamentosFixture
{

    [Fact]
    public async Task Dado_AlteracaoSolicitada_Quando_DadosDoPayloadOkELancamentoEncontrado_Entao_SalvarEhChamado()
    {
        //Adjust
        SetupData();
        var lancamentoEncontrado = new Lancamento(1, "Despesa", TipoLancamento.Debito, new DateTime(2023, 2, 15), 50.00m);
        FluxoCaixaContextMock!.Setup(c => c.Lancamentos.FindAsync(It.IsAny<int>())).ReturnsAsync(lancamentoEncontrado);
        FluxoCaixaContextMock!.Setup(c => c.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(() => 1));

        // Act
        var result = await LancamentoService!.Ajustar(1, new Model.LancamentoPayload { Descricao = "Lancamento novo", TipoLancamento = TipoLancamento.Credito, Data = DateTime.Now, Valor = 100.12m })!;

        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Once());
        FluxoCaixaContextMock.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Exactly(1));
    }

    [Fact]
    public async Task Dado_AlteracaoSolicitado_Quando_LancamentoNaoEncontrado_Entao_LancamentoNaoSalvo()
    {
        //Adjust
        SetupData();
        Lancamento lancamentoEncontrado = null!;
        FluxoCaixaContextMock!.Setup(c => c.Lancamentos.FindAsync(It.IsAny<int>())).ReturnsAsync(lancamentoEncontrado);
        FluxoCaixaContextMock!.Setup(c => c.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(() => 1));
        // Act
        var result = await LancamentoService!.Ajustar(1, new Model.LancamentoPayload { Descricao = "Lancamento novo", TipoLancamento = TipoLancamento.Credito, Data = DateTime.Now, Valor = 100.12m })!;
        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Once());
        FluxoCaixaContextMock.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Never);
        result.Should().BeNull();
    }

    [Theory]
    [InlineData("", 0, 100, "O campo descrição deve estar preenchido.")]
    [InlineData("Lancamento", 5, 100,"O campo tipo do lançamento deve ser 0 (zero) para Débito ou 1 (um) para Crédito." )]
    [InlineData("Lancamento", 0, -100, "O campo valor não pode ser negativo.")]
    public void Dado_AlteracaoSolicitado_Quando_ComDadosDoPayloadErrados_Entao_ExcecaoComErroEhDisparada(string descricao, int tipoLancamento, decimal valor, string mensagemErro)
    {
        //Adjust
        SetupData();
        var lancamentoEncontrado = new Lancamento(1, "Despesa", TipoLancamento.Debito, new DateTime(2023, 2, 15), 50.00m);
        FluxoCaixaContextMock!.Setup(c => c.Lancamentos.FindAsync(It.IsAny<int>())).ReturnsAsync(lancamentoEncontrado);
        FluxoCaixaContextMock!.Setup(c => c.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(() => 1));
        // Act
        Func<Task> act  = async () => await LancamentoService!.Ajustar(1, new Model.LancamentoPayload { Descricao = descricao, TipoLancamento = (TipoLancamento)tipoLancamento, Data = DateTime.Now, Valor = valor });
        // Assert        
        act.Should().ThrowAsync<FluxoCaixaException>().WithMessage(mensagemErro);
    }

}
