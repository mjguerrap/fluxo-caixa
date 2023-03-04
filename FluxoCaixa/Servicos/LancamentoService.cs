using FluxoCaixa.DBContext;
using FluxoCaixa.Model;
using FluxoCaixa.Dominio;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Servicos;

public class LancamentoService : ILancamentoService
{
    private readonly FluxoCaixaContext fluxoCaixaDb;

    public LancamentoService(FluxoCaixaContext fluxoCaixaDb) => this.fluxoCaixaDb = fluxoCaixaDb;

    public async Task<Lancamento?> Ajustar(int id, LancamentoPayload lancamento)
    {
        var lancamentoToUpdate = await fluxoCaixaDb.Lancamentos.FindAsync(id);
        if (lancamentoToUpdate is null) return null;
        lancamentoToUpdate.Atuazliza(lancamento.Descricao, lancamento.TipoLancamento, lancamento.Data, lancamento.Valor);

        await fluxoCaixaDb.SaveChangesAsync();
        return lancamentoToUpdate;
    }

    public async Task<Lancamento> Novo(LancamentoPayload lancamento)
    {
        var lancamentoToAdd = new Lancamento(0, lancamento.Descricao, lancamento.TipoLancamento, lancamento.Data, lancamento.Valor);
        await fluxoCaixaDb.Lancamentos.AddAsync(lancamentoToAdd);
        await fluxoCaixaDb.SaveChangesAsync();
        return lancamentoToAdd;
    }
    public async Task<Lancamento?> Remover(int id)
    {
        var lancamentoToRemove = await fluxoCaixaDb.Lancamentos.FindAsync(id);
        if (lancamentoToRemove is null) return null;

        fluxoCaixaDb.Lancamentos.Remove(lancamentoToRemove);
        await fluxoCaixaDb.SaveChangesAsync();
        return lancamentoToRemove;
    }

    public async Task<List<Lancamento>> Buscar(int mes, int ano, int pagina)
    {
        int tamanhoPagina = 1000;
        int pular = tamanhoPagina * (pagina == 0 ? 0 : pagina-1);
        return await fluxoCaixaDb.Lancamentos.Where(x => x.Data.Year == ano && x.Data.Month == mes).Skip(pular).Take(tamanhoPagina).ToListAsync();
    } 
    
      
    public async Task<Lancamento?> Buscar(int id) => 
        await fluxoCaixaDb.Lancamentos.FindAsync(id);
    
}