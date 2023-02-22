using FluentAssertions;
using FluxoCaixa.DBContext;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Tests.DBContext;
public class FluxoCaixaContextTeste_Deve {
    [Fact]
    public void Dado_Instanciar_Quando_PassarDbContextOptionCorretamente_Entao_UmaInstanciaEhMontada (){

        DbContextOptions<FluxoCaixaContext> options;
        var builder = new DbContextOptionsBuilder<FluxoCaixaContext>();
        builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        options = builder.Options;
        var dataBaseContext = new FluxoCaixaContext(options);

        dataBaseContext.ContextId.Should().BeOfType<DbContextId>();
        
    }

    
}