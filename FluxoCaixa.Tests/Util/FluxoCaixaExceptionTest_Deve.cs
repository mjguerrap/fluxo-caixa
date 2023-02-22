using FluentAssertions;
using FluxoCaixa.AppUtil;
using System.Runtime.Serialization;

namespace FluxoCaixa.Tests.Util;

public class FluxoCaixaExceptionTest_Deve
{
    [Fact]
    public void Dado_InstanciandoExcecao_Quando_SemParametros_Entao_ExcecaoSemMensagem()
    {
        //Arrange
        var excecao = new FluxoCaixaException();
        //Act
        Action act = () => throw excecao;
        //Assert
        act.Should().Throw<FluxoCaixaException>();
    }

    [Fact]
    public void Dado_InstanciandoExcecao_Quando_PassadaMensagem_Entao_ExcecaoSemMensagem()
    {
        //Arrange
        var mensagemPassada = "mensagem passada";
        var excecao = new FluxoCaixaException(mensagemPassada);
        //Act
        Action act = () => throw excecao;
        //Assert
        act.Should().Throw<FluxoCaixaException>().WithMessage(mensagemPassada);
    }

    [Fact]
    public void Dado_InstanciandoExcecao_Quando_PassadaMensagemEInnerException_Entao_ExcecaoSemMensagem()
    {
        //Arrange
        var mensagemPassada = "mensagem passada";
        var mensagemException = "ErroDesconhecido";
        var mensagemEsperada = $"{mensagemException}: {mensagemPassada}";
        var mensagemInnerException = "inner exception fake";
        Exception innerException = new Exception(mensagemInnerException);
        //Act
        Action act = () =>
        {
            try
            {
                throw new Exception(mensagemException, innerException);
            }
            catch (Exception ex)
            {
                throw new FluxoCaixaException($"{ex.Message}: {mensagemPassada}", ex.InnerException);
            }
        };
        //Assert
        act.Should().Throw<FluxoCaixaException>().WithMessage(mensagemEsperada);
        act.Should().Throw<FluxoCaixaException>().WithInnerException<Exception>(mensagemInnerException, innerException);
    }
}