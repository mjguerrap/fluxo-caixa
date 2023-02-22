using FluentAssertions;

namespace FluxoCaixa.Tests.TestesApp;
public class ProgramTeste_Deve : Program {

    [Fact]
    public void Dado_TentarInstanciarProgram_Quando_UtilizadoConstrutorSemParametros_Entao_InstanciaDeveSerCriada(){
         var instancia = new ProgramFixture();

        instancia.Should().NotBeNull();
    }

}