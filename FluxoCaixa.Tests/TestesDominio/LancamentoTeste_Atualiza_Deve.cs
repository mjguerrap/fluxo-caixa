using FluentAssertions;
using FluxoCaixa.AppUtil;
using FluxoCaixa.Dominio;

public class LancamentoTeste_Atualiza_Deve
{
    [Fact]
    public void Dado_AtualizarLancamento_Quando_DadosCorretos_Entao_DadosAlterados()
    {
        //Arrange
        var dataDoBanco = DateTime.Now.AddDays(-1);
        var lancamentoBanco = new Lancamento(1, "Lancamento", TipoLancamento.Credito, dataDoBanco, 100);
        var dataAjutada = DateTime.Now;
        var descricaoAjustada = "Descricao Alterada";
        var valorAjustado = 200.22m;

        //Act
        lancamentoBanco.Atuazliza(descricaoAjustada, TipoLancamento.Debito, dataAjutada, valorAjustado);

        //Assert
        lancamentoBanco.Descricao.Should().Be(descricaoAjustada);
        lancamentoBanco.TipoLancamento.Should().Be(TipoLancamento.Debito);
        lancamentoBanco.Data.Should().Be(dataAjutada.Date);
        lancamentoBanco.Valor.Should().Be(valorAjustado);
    }

    [Theory]
    [InlineData("", 0, 222.22)]
    [InlineData(" ", 1, 3322.23)]
    public void Dado_AtualizarLancamento_Quando_DescricaoIncorreta_Entao_DadosNaoAlterados(string descricao, int tipoLancamento, decimal valor)
    {
        //Arrange
        var dataDoBanco = DateTime.Now.AddDays(-1);
        var descricaoDoBanco = "Descricao do banco";
        var valorDoBanco = 100;
        var lancamentoBanco = new Lancamento(1, descricaoDoBanco, TipoLancamento.Credito, dataDoBanco, valorDoBanco);
        var dataAjutada = DateTime.Now;
        //Act
        Action act = () => lancamentoBanco.Atuazliza(descricao, (TipoLancamento)tipoLancamento, dataAjutada, valor);
        //Assert
        act.Should().Throw<FluxoCaixaException>().WithMessage("O campo descrição deve estar preenchido.");
        lancamentoBanco.Descricao.Should().Be(descricaoDoBanco);
        lancamentoBanco.TipoLancamento.Should().Be(TipoLancamento.Credito);
        lancamentoBanco.Data.Should().Be(dataDoBanco.Date);
        lancamentoBanco.Valor.Should().Be(valorDoBanco);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(-1)]
    public void Dado_AtualizarLancamento_Quando_TipoLancamentoIncorreto_Entao_DadosNaoAlterados(int tipoLancamento)
    {
        //Arrange
        var dataDoBanco = DateTime.Now.AddDays(-1);
        var descricaoDoBanco = "Descricao do banco";
        var valorDoBanco = 100;
        var lancamentoBanco = new Lancamento(1, descricaoDoBanco, TipoLancamento.Credito, dataDoBanco, valorDoBanco);
        var dataAjutada = DateTime.Now;
        //Act
        Action act = () => lancamentoBanco.Atuazliza("ajustando a descricao", (TipoLancamento)tipoLancamento, dataAjutada, 222.22m);
        //Assert
        act.Should().Throw<FluxoCaixaException>().WithMessage("O campo tipo do lançamento deve ser 0 (zero) para Débito ou 1 (um) para Crédito.");
        lancamentoBanco.Descricao.Should().Be(descricaoDoBanco);
        lancamentoBanco.TipoLancamento.Should().Be(TipoLancamento.Credito);
        lancamentoBanco.Data.Should().Be(dataDoBanco.Date);
        lancamentoBanco.Valor.Should().Be(valorDoBanco);
    }

    [Fact]
    public void Dado_AtualizarLancamento_Quando_ValorIncorreto_Entao_DadosNaoAlterados()
    {
        //Arrange
        var dataDoBanco = DateTime.Now.AddDays(-1);
        var descricaoDoBanco = "Descricao do banco";
        var valorDoBanco = 100;
        var lancamentoBanco = new Lancamento(1, descricaoDoBanco, TipoLancamento.Credito, dataDoBanco, valorDoBanco);
        var dataAjutada = DateTime.Now;
        //Act
        Action act = () => lancamentoBanco.Atuazliza("ajustando a descricao", TipoLancamento.Credito, dataAjutada, -222.22m);
        //Assert
        act.Should().Throw<FluxoCaixaException>().WithMessage("O campo valor não pode ser negativo.");
        lancamentoBanco.Descricao.Should().Be(descricaoDoBanco);
        lancamentoBanco.TipoLancamento.Should().Be(TipoLancamento.Credito);
        lancamentoBanco.Data.Should().Be(dataDoBanco.Date);
        lancamentoBanco.Valor.Should().Be(valorDoBanco);
    }
}