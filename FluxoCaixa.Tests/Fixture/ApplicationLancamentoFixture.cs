using Microsoft.AspNetCore.Mvc.Testing;
using FluxoCaixa.Dominio;
using Microsoft.Extensions.Hosting;
using FluxoCaixa.Servicos;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Tests.Fixture;

public class ApplicationLancamentoFixture : WebApplicationFactory<Lancamento>
{
   protected override IHost CreateHost(IHostBuilder builder)
   {
       builder.ConfigureServices(services =>
       {
           services.AddScoped<ILancamentoService, LancamentoServiceFixture>();
       });

       return base.CreateHost(builder);
   }
}
