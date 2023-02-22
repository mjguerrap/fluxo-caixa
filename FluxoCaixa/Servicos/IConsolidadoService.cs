using FluxoCaixa.Dominio;
namespace FluxoCaixa.Servicos;

public interface IConsolidadoService
{
    Task<ICollection<Consolidado>> ObtemConsolidado(int mes, int ano);
}
