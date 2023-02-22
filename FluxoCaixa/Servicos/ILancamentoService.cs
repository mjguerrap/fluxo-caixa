using FluxoCaixa.Model;
using FluxoCaixa.Dominio;

namespace FluxoCaixa.Servicos;

public interface ILancamentoService
{
    Task<Lancamento> Novo(LancamentoPayload lancamento);
    Task<Lancamento?> Ajustar(int id, LancamentoPayload lancamento);
    Task<Lancamento?> Remover(int id);
    Task<List<Lancamento>> Buscar(int mes, int ano, int pagina);
   
    Task<Lancamento?> Buscar(int id);
}
