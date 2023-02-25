using FluentAssertions;
using FluxoCaixa.AppUtil;
using FluxoCaixa.Dominio;

public class ConsolidadoTeste_Populado_Deve
{
    [Fact]
    public void Dado_Instanciado_Quando_ValorCreditoMaiorQueDebito_Entao_SaldoPositivo()
    {
        //Arrange
        var consolidado = new Consolidado();

        //Act
        consolidado.Data = DateTime.Now;
        consolidado.Credito = 500.99m;
        consolidado.Debito = 250.89m;

        //Assert
        consolidado.Saldo.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Dado_Instanciado_Quando_ValorCreditoMenorQueDebito_Entao_SaldoNegativo()
    {
        //Arrange
        var consolidado = new Consolidado();

        //Act
        consolidado.Data = DateTime.Now;
        consolidado.Credito = 100.99m;
        consolidado.Debito = 250.89m;

        //Assert
        consolidado.Saldo.Should().BeLessThan(0);
    }
    
    [Fact]
    public void Dado_Instanciado_Quando_ValorCreditoIgualQueDebito_Entao_SaldoZero()
    {
        //Arrange
        var consolidado = new Consolidado();

        //Act
        consolidado.Data = DateTime.Now;
        consolidado.Credito = 500.99m;
        consolidado.Debito = 500.99m;

        //Assert
        consolidado.Saldo.Should().Be(0);
    }
}