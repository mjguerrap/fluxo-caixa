using Moq;
using FluentAssertions;
using FluxoCaixa.Tests.Fixture;
using FluxoCaixa.Dominio;
using FluxoCaixa.Servicos;
using FluxoCaixa.AppUtil;

namespace FluxoCaixa.Tests.TestesServicos;

public class LancamentoServiceTeste_Novo_Deve : LancamentosFixture
{

    [Fact]
    public async Task Dado_InserirLancamentoCredito_Quando_NovoForchamado_Entao_LancamentoEhRegistrado()
    {
        //Adjust
        SetupData();
        FluxoCaixaContextMock!.Setup(c => c.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(() => 1));
        // Act
        var result = await LancamentoService!.Novo(new Model.LancamentoPayload { Descricao = "Lancamento novo", TipoLancamento = TipoLancamento.Credito, Data = DateTime.Now, Valor = 100.12m })!;

        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Once());
        FluxoCaixaContextMock.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Exactly(1));
    }

    [Fact]
    public async Task Dado_InserirLancamentoDebito_Quando_NovoForchamado_Entao_LancamentoEhRegistrado()
    {
        //Adjust
        SetupData();
        FluxoCaixaContextMock!.Setup(c => c.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(() => 1));
        var lancamentoServiceMock = new Mock<LancamentoService>();
        // Act
        var result = await LancamentoService!.Novo(new Model.LancamentoPayload { Descricao = "Lancamento novo", TipoLancamento = TipoLancamento.Debito, Data = DateTime.Now, Valor = 100.12m })!;

        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Once());
        FluxoCaixaContextMock.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Exactly(1));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Dado_InserirLancamentoDebito_Quando_DescricaoErrada_Entao_MensagemDeErroRetornaPorExcecao(string descricao)
    {
        //Adjust
        SetupData();
        FluxoCaixaContextMock!.Setup(c => c.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(() => 1));
        
        var lancamentoMock = new Mock<LancamentoService>();
        // Act
        Func<Task> act  = async () => await LancamentoService!.Novo(new Model.LancamentoPayload { Descricao = descricao, TipoLancamento = TipoLancamento.Debito, Data = DateTime.Now, Valor = 100.12m })!;
        
        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Never);
        FluxoCaixaContextMock.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Never);
        act.Should().ThrowAsync<FluxoCaixaException>()
        .WithMessage("O campo descrição deve estar preenchido.");
    }

    [Fact]
    public void Dado_InserirLancamentoDebito_Quando_ValorNegativo_Entao_MensagemDeErroRetornaPorExcecao()
    {
        //Adjust
        SetupData();
        FluxoCaixaContextMock!.Setup(c => c.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(() => 1));
        
        var lancamentoMock = new Mock<LancamentoService>();
        // Act
        Func<Task> act  = async () => await LancamentoService!.Novo(new Model.LancamentoPayload { Descricao = "descricao", TipoLancamento = TipoLancamento.Debito, Data = DateTime.Now, Valor = -100.12m })!;
        
        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Never);
        FluxoCaixaContextMock.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Never);
        act.Should().ThrowAsync<FluxoCaixaException>()
        .WithMessage("O campo valor não pode ser negativo.");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(10)]
    [InlineData(3)]
    public void Dado_InserirLancamentoDebito_Quando_TipoLancamentoInexistente_Entao_MensagemDeErroRetornaPorExcecao(int tipoLancamento)
    {
        //Adjust
        SetupData();
        FluxoCaixaContextMock!.Setup(c => c.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(() => 1));
        
        var lancamentoMock = new Mock<LancamentoService>();
        // Act
        Func<Task> act  = async () => await LancamentoService!.Novo(new Model.LancamentoPayload { Descricao = "descricao", TipoLancamento = (TipoLancamento)tipoLancamento, Data = DateTime.Now, Valor = 100.12m })!;
        
        // Assert
        FluxoCaixaContextMock!.Verify(mock => mock.Lancamentos, Times.Never);
        FluxoCaixaContextMock.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Never);
        act.Should().ThrowAsync<FluxoCaixaException>()
        .WithMessage("O campo tipo do lançamento deve ser 0 (zero) para Débito ou 1 (um) para Crédito.");
    }
}
