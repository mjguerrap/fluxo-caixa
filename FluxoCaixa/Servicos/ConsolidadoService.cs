using FluxoCaixa.DBContext;
using FluxoCaixa.Dominio;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Servicos;

public class ConsolidadoService : IConsolidadoService
{
    private readonly FluxoCaixaContext fluxoCaixaDb;

    public ConsolidadoService(FluxoCaixaContext fluxoCaixaDb) => this.fluxoCaixaDb = fluxoCaixaDb;
    public async Task<ICollection<Consolidado>> ObtemConsolidado(int mes, int ano)
    {
        var lancamentos = await fluxoCaixaDb.Lancamentos.Where( x => x.Data.Month == mes && x.Data.Year == ano).ToListAsync();
        var retorno = lancamentos.GroupBy(l => l.Data)
        .Select(x => new Consolidado{
            Data = x.First().Data,
            Credito = x.Sum(x => x.TipoLancamento == TipoLancamento.Credito ? x.Valor : 0),
            Debito = x.Sum(x => x.TipoLancamento == TipoLancamento.Debito ? x.Valor : 0)
            }).ToList();
        return retorno;
    }
}