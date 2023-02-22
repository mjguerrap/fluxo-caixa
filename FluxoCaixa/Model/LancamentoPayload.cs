using FluxoCaixa.Dominio;

namespace FluxoCaixa.Model;

public class LancamentoPayload
{
    public string Descricao { get;  set; } = string.Empty;
    public TipoLancamento TipoLancamento { get;  set; }
    public DateTime Data { get;  set; } = DateTime.Now.Date;
    public decimal Valor { get;  set; }
}