using FluxoCaixa.Servicos;
using FluxoCaixa.Dominio;
using FluxoCaixa.Model;

namespace FluxoCaixa.Tests.Fixture;

public class LancamentoServiceFixture : ILancamentoService
{
    public async Task<Lancamento?> Ajustar(int id, LancamentoPayload lancamento) =>
        await Task.Run(() => id == 1 ? new Lancamento(1, lancamento.Descricao, lancamento.TipoLancamento, lancamento.Data, lancamento.Valor): null);

    public async Task<List<Lancamento>> Buscar(int mes, int ano, int pagina) =>
        await Task.Run(() => mes ==2 && ano == 2022 ? new List<Lancamento>{ new Lancamento(1, "teste", TipoLancamento.Credito, DateTime.Now, 100.00m)} : null!);

    public async Task<Lancamento?> Buscar(int id) =>
        await Task.Run(() => id == 1 ? new Lancamento(1, "teste", TipoLancamento.Credito, DateTime.Now, 100.00m) : null);

    public async Task<Lancamento> Novo(LancamentoPayload lancamento) =>
        await Task.Run(() => new Lancamento(1, lancamento.Descricao, lancamento.TipoLancamento, lancamento.Data, lancamento.Valor));

    public async Task<Lancamento?> Remover(int id)   {
        if (id == 10) throw new Exception();
        return await Task.Run(() => id == 1 ? new Lancamento(1, "teste", TipoLancamento.Credito, DateTime.Now, 100.00m) : null);
    }
}