using System.Text;
using FluxoCaixa.AppUtil;

namespace FluxoCaixa.Dominio;

public record Lancamento
{
    public int Id { get; private set; }
    public string Descricao { get; private set; } = string.Empty;
    public TipoLancamento TipoLancamento { get; private set; } 
    public DateTime Data { get; private set; } = DateTime.Now.Date;
    public decimal Valor { get; private set; } 
    public Lancamento(int id, string descricao, TipoLancamento tipoLancamento, DateTime data, decimal valor)
    {
        Id = id;
        Atuazliza(descricao, tipoLancamento, data, valor);
    }

    public void Atuazliza(string descricao, TipoLancamento tipoLancamento, DateTime data, decimal valor)
    {
        var guardClauseResult = new StringBuilder();
        if (valor <0) guardClauseResult.AppendLine("O campo valor não pode ser negativo.");
        if (tipoLancamento != TipoLancamento.Debito && tipoLancamento != TipoLancamento.Credito) guardClauseResult.AppendLine("O campo tipo do lançamento deve ser 0 (zero) para Débito ou 1 (um) para Crédito.");
        if (String.IsNullOrWhiteSpace(descricao)) guardClauseResult.AppendLine("O campo descrição deve estar preenchido.");
        if (guardClauseResult.Length > 0) throw new FluxoCaixaException(guardClauseResult.ToString());

        Valor = valor;
        Descricao = descricao;
        TipoLancamento = tipoLancamento; 
        Data = data.Date;  
    }
}
