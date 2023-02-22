
using Moq;
using FluxoCaixa.DB;
using FluxoCaixa.Servicos;
using FluxoCaixa.Dominio;
using Moq.EntityFrameworkCore;

namespace FluxoCaixa.Tests.Fixture;

public class LancamentosFixture
{
    public Mock<FluxoCaixaContext>? FluxoCaixaContextMock {get; private set;}
    public ConsolidadoService? ConsolidadoService {get;  private set;}
    public LancamentoService? LancamentoService {get; private set; }
    public List<Lancamento>? Lancamentos {get; private set; }
    public void SetupData()
    {
        Lancamentos = GerarLancamentos();
        FluxoCaixaContextMock = new Mock<FluxoCaixaContext>();
        FluxoCaixaContextMock.Setup(x => x.Lancamentos).ReturnsDbSet(Lancamentos);
        FluxoCaixaContextMock!.SetupSequence(x => x.Set<Lancamento>()).ReturnsDbSet(Lancamentos);
        ConsolidadoService = new ConsolidadoService(FluxoCaixaContextMock.Object);
        LancamentoService = new LancamentoService(FluxoCaixaContextMock.Object);
    }

    public List<Lancamento> GerarLancamentos() => new List<Lancamento> {
        new Lancamento(1, "Abastecimento estoque", TipoLancamento.Debito, new DateTime(2023,2,10), 100.10m),
        new Lancamento(2, "Troca letreiro", TipoLancamento.Debito, new DateTime(2023,2,10), 200.20m),
        new Lancamento(3, "Venda Bolsa Feminina", TipoLancamento.Credito, new DateTime(2023,2,10), 50.10m),
        new Lancamento(4, "Compra de prateleira", TipoLancamento.Debito, new DateTime(2023,2,12), 100.10m),
        new Lancamento(5, "Pagamento manobrista", TipoLancamento.Debito, new DateTime(2023,2,12), 200.20m),
        new Lancamento(6, "Receita qualquer", TipoLancamento.Credito, new DateTime(2023,2,12), 500.10m),
        new Lancamento(7, "Compra de roupas", TipoLancamento.Credito, new DateTime(2023,2,10), 10.10m)
       };
}